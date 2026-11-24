USE ToothlyDB;
GO

SET DATEFORMAT ymd;
GO

-- Este script modifica datos de prueba: primero se respalda la base.
EXEC dbo.sp_RespaldarToothlyDB @etiqueta = 'antes_script04';
GO

-- PARTE I · TRIGGER DE AUDITORÍA

-- Paso 1. Tabla de auditoría
IF OBJECT_ID('dbo.AuditoriaCita', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AuditoriaCita (
        id_auditoria        INT IDENTITY(1,1) NOT NULL,
        id_cita             INT          NOT NULL,
        id_paciente         INT          NOT NULL,
        estado_anterior     VARCHAR(20)  NULL,
        estado_nuevo        VARCHAR(20)  NULL,
        fecha_anterior      DATE         NULL,
        fecha_nueva         DATE         NULL,
        hora_anterior       VARCHAR(10)  NULL,
        hora_nueva          VARCHAR(10)  NULL,
        odontologo_anterior INT          NULL,
        odontologo_nuevo    INT          NULL,
        tipo_evento         VARCHAR(20)  NOT NULL,
        usuario_sql         SYSNAME      NOT NULL CONSTRAINT DF_AuditoriaCita_Usuario DEFAULT (SUSER_SNAME()),
        fecha_evento        DATETIME2    NOT NULL CONSTRAINT DF_AuditoriaCita_Fecha DEFAULT (SYSDATETIME()),
        CONSTRAINT PK_AuditoriaCita PRIMARY KEY (id_auditoria),
        CONSTRAINT CK_AuditoriaCita_Tipo CHECK (tipo_evento IN ('ESTADO', 'HORARIO', 'ODONTOLOGO'))
    );
END;
GO

-- Paso 2. Trigger que registra los cambios relevantes de una cita
CREATE OR ALTER TRIGGER dbo.tr_Cita_AuditarCambios
ON dbo.Cita
AFTER UPDATE
AS
BEGIN
    -- NOCOUNT evita que el sistema reciba conteos de filas adicionales
    SET NOCOUNT ON;

    IF NOT (UPDATE(estado) OR UPDATE(fecha) OR UPDATE(hora) OR UPDATE(id_odontologo))
        RETURN;

    INSERT INTO dbo.AuditoriaCita (
        id_cita, id_paciente,
        estado_anterior, estado_nuevo,
        fecha_anterior, fecha_nueva,
        hora_anterior, hora_nueva,
        odontologo_anterior, odontologo_nuevo,
        tipo_evento, usuario_sql, fecha_evento
    )
    SELECT i.id_cita, i.id_paciente,
           d.estado, i.estado,
           d.fecha, i.fecha,
           d.hora, i.hora,
           d.id_odontologo, i.id_odontologo,
           CASE
               WHEN d.id_odontologo <> i.id_odontologo THEN 'ODONTOLOGO'
               WHEN d.fecha <> i.fecha OR d.hora <> i.hora THEN 'HORARIO'
               ELSE 'ESTADO'
           END,
           SUSER_SNAME(),
           SYSDATETIME()
    FROM inserted AS i
    INNER JOIN deleted AS d ON d.id_cita = i.id_cita
    WHERE d.estado <> i.estado
       OR d.fecha <> i.fecha
       OR d.hora <> i.hora
       OR d.id_odontologo <> i.id_odontologo;
END;
GO

-- Paso 3. Prueba del trigger con una cita que existe
DECLARE @id_cita INT = (
    SELECT c.id_cita
    FROM dbo.Cita AS c
    INNER JOIN dbo.Paciente AS p ON p.id_paciente = c.id_paciente
    WHERE p.dui = '02345678-9' AND c.fecha = '2026-11-25'
);

-- Antes
SELECT id_cita, fecha, hora, estado FROM dbo.Cita WHERE id_cita = @id_cita;

UPDATE dbo.Cita
SET estado = 'Cancelada'
WHERE id_cita = @id_cita;

-- Después
SELECT id_cita, fecha, hora, estado FROM dbo.Cita WHERE id_cita = @id_cita;

-- Fila creada automáticamente por el trigger
SELECT id_auditoria, id_cita, estado_anterior, estado_nuevo,
       fecha_anterior, hora_anterior, hora_nueva, tipo_evento, usuario_sql, fecha_evento
FROM dbo.AuditoriaCita
WHERE id_cita = @id_cita
ORDER BY id_auditoria DESC;
GO

-- PARTE II · PROCEDIMIENTOS ALMACENADOS

-- Procedimiento 1 (CONSULTA con parámetro)
CREATE OR ALTER PROCEDURE dbo.sp_HistorialCitasPaciente
    @id_paciente INT,
    @estado      VARCHAR(20) = NULL       -- NULL = todas las citas
AS
BEGIN
    SET NOCOUNT ON;

    IF @id_paciente IS NULL
       OR NOT EXISTS (SELECT 1 FROM dbo.Paciente WHERE id_paciente = @id_paciente)
    BEGIN
        THROW 50001, 'El paciente indicado no existe.', 1;
    END;

    IF @estado IS NOT NULL
       AND @estado NOT IN ('Programada', 'Atendida', 'Cancelada', 'Reprogramada')
    BEGIN
        THROW 50002, 'Estado no válido. Use Programada, Atendida, Cancelada o Reprogramada.', 1;
    END;

    -- Resultado 1: encabezado del paciente
    SELECT p.id_paciente,
           CONCAT(p.nombres, ' ', p.apellidos)     AS paciente,
           dbo.fn_EdadPaciente(p.fecha_nacimiento) AS edad,
           ISNULL(p.alergias, 'Sin registro')      AS alergias,
           dbo.fn_SaldoPendiente(p.id_paciente)    AS saldo_pendiente
    FROM dbo.Paciente AS p
    WHERE p.id_paciente = @id_paciente;

    -- Resultado 2: citas del paciente
    SELECT c.id_cita,
           CONVERT(VARCHAR(10), c.fecha, 103) AS fecha,
           c.hora,
           u.nombre_completo                  AS odontologo,
           c.motivo,
           c.estado
    FROM dbo.Cita AS c
    INNER JOIN dbo.Usuario AS u ON u.id_usuario = c.id_odontologo
    WHERE c.id_paciente = @id_paciente
      AND (@estado IS NULL OR c.estado = @estado)
    ORDER BY c.fecha DESC, c.hora DESC;
END;
GO

-- Procedimiento 2 (MODIFICACIÓN validada)
CREATE OR ALTER PROCEDURE dbo.sp_RegistrarAbono
    @id_paciente INT,
    @monto       DECIMAL(10,2),
    @concepto    VARCHAR(150) = 'Abono a cuenta'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @id_estado INT;
    DECLARE @saldo     DECIMAL(10,2);
    DECLARE @mensaje   NVARCHAR(200);

    -- 1. Validar parámetros
    IF @monto IS NULL OR @monto <= 0
    BEGIN
        THROW 50010, 'El monto del abono debe ser mayor a $0.00.', 1;
    END;

    IF @concepto IS NULL OR LTRIM(RTRIM(@concepto)) = ''
    BEGIN
        THROW 50011, 'El concepto del abono no puede quedar vacío.', 1;
    END;

    IF NOT EXISTS (SELECT 1 FROM dbo.Paciente WHERE id_paciente = @id_paciente)
    BEGIN
        THROW 50012, 'El paciente indicado no existe.', 1;
    END;

    SELECT @id_estado = id_estado
    FROM dbo.EstadoCuenta
    WHERE id_paciente = @id_paciente;

    IF @id_estado IS NULL
    BEGIN
        THROW 50013, 'El paciente no tiene estado de cuenta.', 1;
    END;

    SET @saldo = dbo.fn_SaldoPendiente(@id_paciente);

    IF @monto > @saldo
    BEGIN
        SET @mensaje = CONCAT('El abono ($', @monto, ') no puede ser mayor al saldo pendiente ($', @saldo, ').');
        THROW 50014, @mensaje, 1;
    END;

    -- 2. Registrar el abono y recalcular el saldo
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.MovimientoCuenta (id_estado, fecha, tipo, concepto, monto)
        VALUES (@id_estado, GETDATE(), 'Abono', @concepto, @monto);

        UPDATE dbo.EstadoCuenta
        SET saldo_actual = dbo.fn_SaldoPendiente(@id_paciente)
        WHERE id_estado = @id_estado;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;

    -- 3. Mostrar el resultado
    SELECT ec.id_paciente,
           @saldo          AS saldo_anterior,
           @monto          AS abono,
           ec.saldo_actual AS saldo_nuevo
    FROM dbo.EstadoCuenta AS ec
    WHERE ec.id_estado = @id_estado;
END;
GO

-- Ejecución de los procedimientos (EXEC válidos)

-- Historial completo de Ana María Rodríguez (DUI 08765432-1)
DECLARE @id_ana INT = (SELECT id_paciente FROM dbo.Paciente WHERE dui = '08765432-1');
EXEC dbo.sp_HistorialCitasPaciente @id_paciente = @id_ana;

-- Solo sus citas programadas
EXEC dbo.sp_HistorialCitasPaciente @id_paciente = @id_ana, @estado = 'Programada';
GO

-- Abono válido de $50.00 a la cuenta de Ana María Rodríguez.
-- SELECT antes y después con la misma condición.
DECLARE @id_ana INT = (SELECT id_paciente FROM dbo.Paciente WHERE dui = '08765432-1');

SELECT id_paciente, saldo_actual FROM dbo.EstadoCuenta WHERE id_paciente = @id_ana;   -- antes

EXEC dbo.sp_RegistrarAbono
     @id_paciente = @id_ana,
     @monto       = 50.00,
     @concepto    = 'Pago en efectivo - segunda cuota';

SELECT id_paciente, saldo_actual FROM dbo.EstadoCuenta WHERE id_paciente = @id_ana;   -- después
GO

-- PARTE III · CASOS DE PRUEBA

-- CP-01 · Abono válido (éxito)
BEGIN TRANSACTION;
    DECLARE @id_carmen INT = (SELECT id_paciente FROM dbo.Paciente WHERE dui = '04567890-1');
    EXEC dbo.sp_RegistrarAbono @id_paciente = @id_carmen, @monto = 20.00, @concepto = 'Prueba CP-01';
ROLLBACK TRANSACTION;
GO

-- CP-02 · Abono con monto negativo (error controlado)
BEGIN TRY
    EXEC dbo.sp_RegistrarAbono @id_paciente = 1, @monto = -15.00;
END TRY
BEGIN CATCH
    SELECT 'CP-02' AS caso, ERROR_NUMBER() AS numero_error, ERROR_MESSAGE() AS mensaje;
END CATCH;
GO

-- CP-03 · Abono mayor que el saldo pendiente (error controlado)
BEGIN TRY
    DECLARE @id_luis INT = (SELECT id_paciente FROM dbo.Paciente WHERE dui = '02345678-9');
    EXEC dbo.sp_RegistrarAbono @id_paciente = @id_luis, @monto = 500.00;
END TRY
BEGIN CATCH
    SELECT 'CP-03' AS caso, ERROR_NUMBER() AS numero_error, ERROR_MESSAGE() AS mensaje;
END CATCH;
GO

-- CP-04 · Consulta de historial de un paciente inexistente (error controlado)
BEGIN TRY
    EXEC dbo.sp_HistorialCitasPaciente @id_paciente = 9999;
END TRY
BEGIN CATCH
    SELECT 'CP-04' AS caso, ERROR_NUMBER() AS numero_error, ERROR_MESSAGE() AS mensaje;
END CATCH;
GO

-- CP-05 · Cita con un estado que no existe (restricción CHECK)
BEGIN TRY
    INSERT INTO dbo.Cita (id_paciente, id_odontologo, fecha, hora, motivo, estado)
    VALUES (1, 3, '2026-12-10', '10:00', 'Prueba CP-05', 'Perdida');
END TRY
BEGIN CATCH
    SELECT 'CP-05' AS caso, ERROR_NUMBER() AS numero_error, ERROR_MESSAGE() AS mensaje;
END CATCH;
GO

-- CP-06 · Paciente con un DUI que ya existe (índice único)
BEGIN TRY
    INSERT INTO dbo.Paciente (nombres, apellidos, dui, sexo)
    VALUES ('Prueba', 'Duplicado', '01234567-8', 'M');
END TRY
BEGIN CATCH
    SELECT 'CP-06' AS caso, ERROR_NUMBER() AS numero_error, ERROR_MESSAGE() AS mensaje;
END CATCH;
GO

-- CP-07 · Cita para un paciente que no existe (clave foránea)
BEGIN TRY
    INSERT INTO dbo.Cita (id_paciente, id_odontologo, fecha, hora, motivo)
    VALUES (9999, 3, '2026-12-10', '10:00', 'Prueba CP-07');
END TRY
BEGIN CATCH
    SELECT 'CP-07' AS caso, ERROR_NUMBER() AS numero_error, ERROR_MESSAGE() AS mensaje;
END CATCH;
GO

-- Comprobación final: historial completo de auditoría de citas
SELECT id_auditoria, id_cita, id_paciente, estado_anterior, estado_nuevo,
       fecha_anterior, fecha_nueva, hora_anterior, hora_nueva,
       tipo_evento, usuario_sql, fecha_evento
FROM dbo.AuditoriaCita
ORDER BY id_auditoria;
GO