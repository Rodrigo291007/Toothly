USE master;
GO

IF DB_ID(N'ToothlyDB') IS NOT NULL
BEGIN
    BEGIN TRY
        DECLARE @ruta NVARCHAR(400) = CAST(SERVERPROPERTY('InstanceDefaultBackupPath') AS NVARCHAR(400));
        IF RIGHT(@ruta, 1) <> N'\'
            SET @ruta = @ruta + N'\';
        SET @ruta = @ruta + N'ToothlyDB_antes_script01_' + FORMAT(GETDATE(), 'yyyyMMdd_HHmmss') + N'.bak';

        BACKUP DATABASE ToothlyDB
            TO DISK = @ruta
            WITH INIT, NAME = N'ToothlyDB - respaldo antes del script 01';

        PRINT 'Respaldo creado en: ' + @ruta;
    END TRY
    BEGIN CATCH
        PRINT 'NO se pudo crear el respaldo. El script se detiene sin borrar nada.';
        PRINT ERROR_MESSAGE();
        -- NOEXEC ON hace que SQL Server ya no ejecute los lotes siguientes
        SET NOEXEC ON;
        THROW;
    END CATCH
END
ELSE
BEGIN
    PRINT 'ToothlyDB no existía: no se necesita respaldo.';
END
GO

-- 2. CREAR LA BASE DE DATOS SI NO EXISTE
IF DB_ID(N'ToothlyDB') IS NULL
BEGIN
    CREATE DATABASE ToothlyDB;
    PRINT 'Base de datos ToothlyDB creada con exito.';
END
ELSE
BEGIN
    PRINT 'La base de datos ToothlyDB ya existe.';
END
GO

USE ToothlyDB;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- 3. ELIMINAR TABLAS SI YA EXISTEN
IF OBJECT_ID('dbo.AuditoriaCita', 'U') IS NOT NULL DROP TABLE dbo.AuditoriaCita;
IF OBJECT_ID('dbo.OrdenCompraDetalle', 'U') IS NOT NULL DROP TABLE dbo.OrdenCompraDetalle;
IF OBJECT_ID('dbo.OrdenCompra', 'U') IS NOT NULL DROP TABLE dbo.OrdenCompra;
IF OBJECT_ID('dbo.MovimientoInventario', 'U') IS NOT NULL DROP TABLE dbo.MovimientoInventario;
IF OBJECT_ID('dbo.RecetaDetalle', 'U') IS NOT NULL DROP TABLE dbo.RecetaDetalle;
IF OBJECT_ID('dbo.Producto', 'U') IS NOT NULL DROP TABLE dbo.Producto;
IF OBJECT_ID('dbo.CategoriaProducto', 'U') IS NOT NULL DROP TABLE dbo.CategoriaProducto;
IF OBJECT_ID('dbo.Proveedor', 'U') IS NOT NULL DROP TABLE dbo.Proveedor;
IF OBJECT_ID('dbo.MovimientoCuenta', 'U') IS NOT NULL DROP TABLE dbo.MovimientoCuenta;
IF OBJECT_ID('dbo.EstadoCuenta', 'U') IS NOT NULL DROP TABLE dbo.EstadoCuenta;
IF OBJECT_ID('dbo.Receta', 'U') IS NOT NULL DROP TABLE dbo.Receta;
IF OBJECT_ID('dbo.Tratamiento', 'U') IS NOT NULL DROP TABLE dbo.Tratamiento;
IF OBJECT_ID('dbo.HistoriaClinica', 'U') IS NOT NULL DROP TABLE dbo.HistoriaClinica;
IF OBJECT_ID('dbo.Cita', 'U') IS NOT NULL DROP TABLE dbo.Cita;
IF OBJECT_ID('dbo.Paciente', 'U') IS NOT NULL DROP TABLE dbo.Paciente;
IF OBJECT_ID('dbo.Bitacora', 'U') IS NOT NULL DROP TABLE dbo.Bitacora;
IF OBJECT_ID('dbo.RolPrivilegio', 'U') IS NOT NULL DROP TABLE dbo.RolPrivilegio;
IF OBJECT_ID('dbo.Usuario', 'U') IS NOT NULL DROP TABLE dbo.Usuario;
IF OBJECT_ID('dbo.Privilegio', 'U') IS NOT NULL DROP TABLE dbo.Privilegio;
IF OBJECT_ID('dbo.Rol', 'U') IS NOT NULL DROP TABLE dbo.Rol;
GO


-- 4. CREACIÓN DE TABLAS

-- Seguridad: roles, privilegios y usuarios
CREATE TABLE dbo.Rol (
    id_rol      INT         NOT NULL,
    nombre_rol  VARCHAR(30) NOT NULL,
    CONSTRAINT PK_Rol PRIMARY KEY (id_rol),
    CONSTRAINT UQ_Rol_Nombre UNIQUE (nombre_rol)
);

CREATE TABLE dbo.Privilegio (
    id_privilegio INT         NOT NULL,
    nombre        VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Privilegio PRIMARY KEY (id_privilegio),
    CONSTRAINT UQ_Privilegio_Nombre UNIQUE (nombre)
);

CREATE TABLE dbo.Usuario (
    id_usuario      INT IDENTITY(1,1) NOT NULL,
    nombre_usuario  VARCHAR(30)  NOT NULL,
    clave           VARCHAR(255) NOT NULL,          -- encriptada con TripleDES por el sistema
    nombre_completo VARCHAR(100) NOT NULL,
    correo          VARCHAR(100) NULL,
    id_rol          INT          NOT NULL,
    estado          CHAR(1)      NOT NULL CONSTRAINT DF_Usuario_Estado DEFAULT ('A'),
    CONSTRAINT PK_Usuario PRIMARY KEY (id_usuario),
    CONSTRAINT UQ_Usuario_Nombre UNIQUE (nombre_usuario),
    CONSTRAINT CK_Usuario_Estado CHECK (estado IN ('A', 'I')),   -- A = Activo, I = Inactivo
    CONSTRAINT FK_Usuario_Rol FOREIGN KEY (id_rol) REFERENCES dbo.Rol(id_rol)
);

-- Relación muchos a muchos entre Rol y Privilegio
CREATE TABLE dbo.RolPrivilegio (
    id_rol        INT NOT NULL,
    id_privilegio INT NOT NULL,
    CONSTRAINT PK_RolPrivilegio PRIMARY KEY (id_rol, id_privilegio),
    CONSTRAINT FK_RolPrivilegio_Rol FOREIGN KEY (id_rol) REFERENCES dbo.Rol(id_rol),
    CONSTRAINT FK_RolPrivilegio_Privilegio FOREIGN KEY (id_privilegio) REFERENCES dbo.Privilegio(id_privilegio)
);

-- Bitácora de accesos y acciones (la llena el sistema desde C#)
CREATE TABLE dbo.Bitacora (
    id_bitacora INT IDENTITY(1,1) NOT NULL,
    id_usuario  INT          NOT NULL,
    fecha       DATE         NOT NULL CONSTRAINT DF_Bitacora_Fecha DEFAULT (CAST(GETDATE() AS DATE)),
    hora        VARCHAR(10)  NOT NULL,              -- formato HH:mm:ss
    accion      VARCHAR(100) NOT NULL,
    descripcion VARCHAR(200) NULL,
    CONSTRAINT PK_Bitacora PRIMARY KEY (id_bitacora),
    CONSTRAINT FK_Bitacora_Usuario FOREIGN KEY (id_usuario) REFERENCES dbo.Usuario(id_usuario)
);

-- Pacientes y atención clínica
CREATE TABLE dbo.Paciente (
    id_paciente      INT IDENTITY(1,1) NOT NULL,
    nombres          VARCHAR(100) NOT NULL,
    apellidos        VARCHAR(100) NOT NULL,
    dui              VARCHAR(10)  NULL,             -- formato 00000000-0 (el sistema lo exige)
    fecha_nacimiento DATE         NULL,
    sexo             CHAR(1)      NULL,
    telefono         VARCHAR(15)  NULL,
    correo           VARCHAR(100) NULL,
    direccion        VARCHAR(200) NULL,
    alergias         VARCHAR(MAX) NULL,
    antecedentes     VARCHAR(MAX) NULL,
    estado           CHAR(1)      NOT NULL CONSTRAINT DF_Paciente_Estado DEFAULT ('A'),
    fecha_registro   DATETIME     NOT NULL CONSTRAINT DF_Paciente_FechaRegistro DEFAULT (GETDATE()),
    CONSTRAINT PK_Paciente PRIMARY KEY (id_paciente),
    CONSTRAINT CK_Paciente_Sexo CHECK (sexo IN ('M', 'F')),
    CONSTRAINT CK_Paciente_Estado CHECK (estado IN ('A', 'I')),
    CONSTRAINT CK_Paciente_FechaNacimiento CHECK (fecha_nacimiento <= CAST(GETDATE() AS DATE))
);

-- El DUI no se repite, pero se permiten varios pacientes sin DUI
CREATE UNIQUE INDEX UQ_Paciente_Dui ON dbo.Paciente(dui) WHERE dui IS NOT NULL;

CREATE TABLE dbo.Cita (
    id_cita       INT IDENTITY(1,1) NOT NULL,
    id_paciente   INT          NOT NULL,
    id_odontologo INT          NOT NULL,
    fecha         DATE         NOT NULL,
    hora          VARCHAR(10)  NOT NULL,            -- formato HH:mm (lo elige el sistema)
    motivo        VARCHAR(150) NULL,
    estado        VARCHAR(20)  NOT NULL CONSTRAINT DF_Cita_Estado DEFAULT ('Programada'),
    CONSTRAINT PK_Cita PRIMARY KEY (id_cita),
    CONSTRAINT CK_Cita_Estado CHECK (estado IN ('Programada', 'Atendida', 'Cancelada', 'Reprogramada')),
    CONSTRAINT FK_Cita_Paciente FOREIGN KEY (id_paciente) REFERENCES dbo.Paciente(id_paciente),
    CONSTRAINT FK_Cita_Odontologo FOREIGN KEY (id_odontologo) REFERENCES dbo.Usuario(id_usuario)
);

CREATE TABLE dbo.HistoriaClinica (
    id_historia             INT IDENTITY(1,1) NOT NULL,
    id_paciente             INT          NOT NULL,
    antecedentes            VARCHAR(MAX) NULL,
    observaciones_generales VARCHAR(MAX) NULL,
    observaciones_clinicas  VARCHAR(MAX) NULL,
    fecha_apertura          DATETIME     NOT NULL CONSTRAINT DF_Historia_FechaApertura DEFAULT (GETDATE()),
    motivo_consulta         VARCHAR(200) NULL,
    diagnostico             VARCHAR(MAX) NULL,
    id_odontologo           INT          NOT NULL,
    CONSTRAINT PK_HistoriaClinica PRIMARY KEY (id_historia),
    CONSTRAINT UQ_Historia_Paciente UNIQUE (id_paciente),          -- una historia por paciente
    CONSTRAINT FK_Historia_Paciente FOREIGN KEY (id_paciente) REFERENCES dbo.Paciente(id_paciente),
    CONSTRAINT FK_Historia_Odontologo FOREIGN KEY (id_odontologo) REFERENCES dbo.Usuario(id_usuario)
);

CREATE TABLE dbo.Tratamiento (
    id_tratamiento INT IDENTITY(1,1) NOT NULL,
    id_paciente    INT           NOT NULL,
    id_odontologo  INT           NOT NULL,
    descripcion    VARCHAR(200)  NOT NULL,
    fecha_inicio   DATE          NOT NULL,
    fecha_fin      DATE          NULL,
    costo          DECIMAL(10,2) NOT NULL CONSTRAINT DF_Tratamiento_Costo DEFAULT (0.00),
    estado         VARCHAR(20)   NOT NULL CONSTRAINT DF_Tratamiento_Estado DEFAULT ('Pendiente'),
    CONSTRAINT PK_Tratamiento PRIMARY KEY (id_tratamiento),
    CONSTRAINT CK_Tratamiento_Costo CHECK (costo >= 0),
    CONSTRAINT CK_Tratamiento_Estado CHECK (estado IN ('Pendiente', 'En proceso', 'Finalizado', 'Anulado')),
    CONSTRAINT CK_Tratamiento_Fechas CHECK (fecha_fin IS NULL OR fecha_fin >= fecha_inicio),
    CONSTRAINT FK_Tratamiento_Paciente FOREIGN KEY (id_paciente) REFERENCES dbo.Paciente(id_paciente),
    CONSTRAINT FK_Tratamiento_Odontologo FOREIGN KEY (id_odontologo) REFERENCES dbo.Usuario(id_usuario)
);

CREATE TABLE dbo.Receta (
    id_receta      INT IDENTITY(1,1) NOT NULL,
    id_paciente    INT          NOT NULL,
    id_tratamiento INT          NULL,
    fecha          DATE         NOT NULL CONSTRAINT DF_Receta_Fecha DEFAULT (CAST(GETDATE() AS DATE)),
    indicaciones   VARCHAR(MAX) NULL,
    CONSTRAINT PK_Receta PRIMARY KEY (id_receta),
    CONSTRAINT FK_Receta_Paciente FOREIGN KEY (id_paciente) REFERENCES dbo.Paciente(id_paciente),
    CONSTRAINT FK_Receta_Tratamiento FOREIGN KEY (id_tratamiento) REFERENCES dbo.Tratamiento(id_tratamiento)
);

-- Cuentas de pacientes
CREATE TABLE dbo.EstadoCuenta (
    id_estado    INT IDENTITY(1,1) NOT NULL,
    id_paciente  INT           NOT NULL,
    saldo_actual DECIMAL(10,2) NOT NULL CONSTRAINT DF_EstadoCuenta_Saldo DEFAULT (0.00),
    CONSTRAINT PK_EstadoCuenta PRIMARY KEY (id_estado),
    CONSTRAINT UQ_EstadoCuenta_Paciente UNIQUE (id_paciente),      -- una cuenta por paciente
    CONSTRAINT FK_EstadoCuenta_Paciente FOREIGN KEY (id_paciente) REFERENCES dbo.Paciente(id_paciente)
);

CREATE TABLE dbo.MovimientoCuenta (
    id_movimiento INT IDENTITY(1,1) NOT NULL,
    id_estado     INT           NOT NULL,
    fecha         DATETIME      NOT NULL CONSTRAINT DF_MovimientoCuenta_Fecha DEFAULT (GETDATE()),
    tipo          VARCHAR(10)   NOT NULL,
    concepto      VARCHAR(150)  NOT NULL,
    monto         DECIMAL(10,2) NOT NULL,
    CONSTRAINT PK_MovimientoCuenta PRIMARY KEY (id_movimiento),
    CONSTRAINT CK_MovimientoCuenta_Tipo CHECK (tipo IN ('Cargo', 'Abono')),
    CONSTRAINT CK_MovimientoCuenta_Monto CHECK (monto > 0),
    CONSTRAINT FK_MovimientoCuenta_Estado FOREIGN KEY (id_estado) REFERENCES dbo.EstadoCuenta(id_estado)
);

-- Inventario y compras
CREATE TABLE dbo.CategoriaProducto (
    id_categoria INT          NOT NULL,
    nombre       VARCHAR(50)  NOT NULL,
    descripcion  VARCHAR(150) NULL,
    CONSTRAINT PK_CategoriaProducto PRIMARY KEY (id_categoria)
);

CREATE TABLE dbo.Producto (
    id_producto       INT IDENTITY(1,1) NOT NULL,
    codigo            VARCHAR(20)   NOT NULL,
    nombre            VARCHAR(100)  NOT NULL,
    id_categoria      INT           NOT NULL,
    stock_actual      INT           NOT NULL CONSTRAINT DF_Producto_StockActual DEFAULT (0),
    stock_minimo      INT           NOT NULL CONSTRAINT DF_Producto_StockMinimo DEFAULT (0),
    precio_compra     DECIMAL(10,2) NOT NULL CONSTRAINT DF_Producto_PrecioCompra DEFAULT (0.00),
    fecha_vencimiento DATE          NULL,
    unidades_por_caja INT           NOT NULL CONSTRAINT DF_Producto_UnidadesCaja DEFAULT (1),
    precio_venta      DECIMAL(10,2) NOT NULL CONSTRAINT DF_Producto_PrecioVenta DEFAULT (0.00),
    CONSTRAINT PK_Producto PRIMARY KEY (id_producto),
    CONSTRAINT UQ_Producto_Codigo UNIQUE (codigo),
    CONSTRAINT CK_Producto_Stock CHECK (stock_actual >= 0 AND stock_minimo >= 0),
    CONSTRAINT CK_Producto_Precios CHECK (precio_compra >= 0 AND precio_venta >= 0),
    CONSTRAINT CK_Producto_UnidadesCaja CHECK (unidades_por_caja >= 1),
    CONSTRAINT FK_Producto_Categoria FOREIGN KEY (id_categoria) REFERENCES dbo.CategoriaProducto(id_categoria)
);

-- Detalle de receta
CREATE TABLE dbo.RecetaDetalle (
    id_detalle          INT IDENTITY(1,1) NOT NULL,
    id_receta           INT          NOT NULL,
    medicamento         VARCHAR(100) NOT NULL,
    dosis               VARCHAR(50)  NULL,
    frecuencia          VARCHAR(50)  NULL,
    duracion            VARCHAR(50)  NULL,
    id_producto         INT          NULL,
    cantidad            INT          NOT NULL CONSTRAINT DF_RecetaDetalle_Cantidad DEFAULT (0),
    cantidad_despachada INT          NOT NULL CONSTRAINT DF_RecetaDetalle_Despachada DEFAULT (0),
    CONSTRAINT PK_RecetaDetalle PRIMARY KEY (id_detalle),
    CONSTRAINT CK_RecetaDetalle_Cantidades CHECK (cantidad >= 0 AND cantidad_despachada >= 0),
    CONSTRAINT FK_RecetaDetalle_Receta FOREIGN KEY (id_receta) REFERENCES dbo.Receta(id_receta) ON DELETE CASCADE,
    CONSTRAINT FK_RecetaDetalle_Producto FOREIGN KEY (id_producto) REFERENCES dbo.Producto(id_producto)
);

CREATE TABLE dbo.MovimientoInventario (
    id_movimiento INT IDENTITY(1,1) NOT NULL,
    id_producto   INT          NOT NULL,
    tipo          VARCHAR(20)  NOT NULL,
    cantidad      INT          NOT NULL,
    fecha         DATETIME     NOT NULL CONSTRAINT DF_MovimientoInventario_Fecha DEFAULT (GETDATE()),
    descripcion   VARCHAR(150) NULL,
    CONSTRAINT PK_MovimientoInventario PRIMARY KEY (id_movimiento),
    CONSTRAINT CK_MovimientoInventario_Tipo CHECK (tipo IN ('Entrada', 'Salida', 'Ajuste')),
    CONSTRAINT CK_MovimientoInventario_Cantidad CHECK (cantidad >= 0),
    CONSTRAINT FK_MovimientoInventario_Producto FOREIGN KEY (id_producto) REFERENCES dbo.Producto(id_producto)
);

CREATE TABLE dbo.Proveedor (
    id_proveedor INT IDENTITY(1,1) NOT NULL,
    nombre       VARCHAR(100) NOT NULL,
    telefono     VARCHAR(9)   NULL,                 -- formato 0000-0000
    correo       VARCHAR(100) NULL,
    direccion    VARCHAR(150) NULL,
    estado       CHAR(1)      NOT NULL CONSTRAINT DF_Proveedor_Estado DEFAULT ('A'),
    CONSTRAINT PK_Proveedor PRIMARY KEY (id_proveedor),
    CONSTRAINT CK_Proveedor_Estado CHECK (estado IN ('A', 'I'))
);

CREATE TABLE dbo.OrdenCompra (
    id_orden     INT IDENTITY(1,1) NOT NULL,
    id_proveedor INT           NOT NULL,
    fecha        DATETIME      NOT NULL CONSTRAINT DF_OrdenCompra_Fecha DEFAULT (GETDATE()),
    total        DECIMAL(10,2) NOT NULL CONSTRAINT DF_OrdenCompra_Total DEFAULT (0.00),
    estado       VARCHAR(20)   NOT NULL CONSTRAINT DF_OrdenCompra_Estado DEFAULT ('Pendiente'),
    CONSTRAINT PK_OrdenCompra PRIMARY KEY (id_orden),
    CONSTRAINT CK_OrdenCompra_Total CHECK (total >= 0),
    CONSTRAINT CK_OrdenCompra_Estado CHECK (estado IN ('Pendiente', 'Recibida', 'Cancelada')),
    CONSTRAINT FK_OrdenCompra_Proveedor FOREIGN KEY (id_proveedor) REFERENCES dbo.Proveedor(id_proveedor)
);

CREATE TABLE dbo.OrdenCompraDetalle (
    id_detalle      INT IDENTITY(1,1) NOT NULL,
    id_orden        INT           NOT NULL,
    id_producto     INT           NOT NULL,
    cantidad        INT           NOT NULL,         -- cantidad de cajas
    precio_unitario DECIMAL(10,2) NOT NULL,
    subtotal        DECIMAL(10,2) NOT NULL,
    CONSTRAINT PK_OrdenCompraDetalle PRIMARY KEY (id_detalle),
    CONSTRAINT CK_OrdenDetalle_Cantidad CHECK (cantidad > 0),
    CONSTRAINT CK_OrdenDetalle_Montos CHECK (precio_unitario >= 0 AND subtotal >= 0),
    CONSTRAINT FK_OrdenDetalle_Orden FOREIGN KEY (id_orden) REFERENCES dbo.OrdenCompra(id_orden) ON DELETE CASCADE,
    CONSTRAINT FK_OrdenDetalle_Producto FOREIGN KEY (id_producto) REFERENCES dbo.Producto(id_producto)
);

-- Auditoría (uso interno, no se muestra en el sistema)
-- Guarda cada cambio de estado, fecha, hora u odontólogo de una cita.
-- La llena automáticamente el trigger tr_Cita_AuditarCambios (script 04).
-- No lleva FK hacia Cita a propósito: el historial debe conservarse aunque la cita original cambie o se elimine.
CREATE TABLE dbo.AuditoriaCita (
    id_auditoria       INT IDENTITY(1,1) NOT NULL,
    id_cita            INT          NOT NULL,
    id_paciente        INT          NOT NULL,
    estado_anterior    VARCHAR(20)  NULL,
    estado_nuevo       VARCHAR(20)  NULL,
    fecha_anterior     DATE         NULL,
    fecha_nueva        DATE         NULL,
    hora_anterior      VARCHAR(10)  NULL,
    hora_nueva         VARCHAR(10)  NULL,
    odontologo_anterior INT         NULL,
    odontologo_nuevo   INT          NULL,
    tipo_evento        VARCHAR(20)  NOT NULL,
    usuario_sql        SYSNAME      NOT NULL CONSTRAINT DF_AuditoriaCita_Usuario DEFAULT (SUSER_SNAME()),
    fecha_evento       DATETIME2    NOT NULL CONSTRAINT DF_AuditoriaCita_Fecha DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_AuditoriaCita PRIMARY KEY (id_auditoria),
    CONSTRAINT CK_AuditoriaCita_Tipo CHECK (tipo_evento IN ('ESTADO', 'HORARIO', 'ODONTOLOGO'))
);
GO

-- 5. PROCEDIMIENTO DE RESPALDO (uso interno)
-- Lo ejecutan los scripts 03 y 04 antes de modificar datos.
-- Uso: EXEC dbo.sp_RespaldarToothlyDB @etiqueta = 'antes_pruebas';
CREATE OR ALTER PROCEDURE dbo.sp_RespaldarToothlyDB
    @etiqueta VARCHAR(30) = 'manual'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ruta NVARCHAR(400) = CAST(SERVERPROPERTY('InstanceDefaultBackupPath') AS NVARCHAR(400));
    IF RIGHT(@ruta, 1) <> N'\'
        SET @ruta = @ruta + N'\';
    SET @ruta = @ruta + N'ToothlyDB_' + @etiqueta + N'_' + FORMAT(GETDATE(), 'yyyyMMdd_HHmmss') + N'.bak';

    BACKUP DATABASE ToothlyDB
        TO DISK = @ruta
        WITH INIT, NAME = N'ToothlyDB - respaldo automático';

    PRINT 'Respaldo creado en: ' + @ruta;
END;
GO

-- 6. COMPROBAR QUE EL MODELO QUEDÓ CREADO
SELECT t.name AS tabla,
       (SELECT COUNT(*) FROM sys.foreign_keys fk WHERE fk.parent_object_id = t.object_id) AS claves_foraneas,
       (SELECT COUNT(*) FROM sys.check_constraints ck WHERE ck.parent_object_id = t.object_id) AS reglas_check,
       (SELECT COUNT(*) FROM sys.default_constraints df WHERE df.parent_object_id = t.object_id) AS valores_default
FROM sys.tables AS t
ORDER BY t.name;
GO

-- Si el respaldo falló arriba, esto reactiva la ejecución normal de la sesión
SET NOEXEC OFF;
GO
