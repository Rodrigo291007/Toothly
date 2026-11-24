USE ToothlyDB;
GO

SET DATEFORMAT ymd;
GO

-- Este script modifica datos de prueba (sección 2): primero se respalda la base.
EXEC dbo.sp_RespaldarToothlyDB @etiqueta = 'antes_script03';
GO

-- Comprobación de base de datos en uso
SELECT DB_NAME() AS base_actual;
GO

-- SECCIÓN 1. CONSULTAS BÁSICAS CON FILTROS Y OPERADORES

-- 1.1 Agenda pendiente
SELECT id_cita, id_paciente, id_odontologo, fecha, hora, motivo, estado
FROM dbo.Cita
WHERE estado IN ('Programada', 'Reprogramada')
ORDER BY fecha ASC, hora ASC;
GO

-- 1.2 Pacientes activos que tienen alguna alergia registrada
SELECT id_paciente, nombres, apellidos, alergias
FROM dbo.Paciente
WHERE estado = 'A'
  AND alergias LIKE '%alergia%'
  AND alergias NOT LIKE 'Ningun%'
ORDER BY apellidos;
GO

-- 1.3 Tratamientos de costo medio (entre $40 y $200) que no están anulados.
SELECT id_tratamiento, id_paciente, descripcion, costo, estado
FROM dbo.Tratamiento
WHERE costo BETWEEN 40 AND 200
  AND estado <> 'Anulado'
ORDER BY costo DESC;
GO

-- 1.4 Pacientes sin correo registrado
SELECT id_paciente, nombres, apellidos, telefono
FROM dbo.Paciente
WHERE correo IS NULL
   OR correo = '';
GO

-- 1.5 Los 5 productos con menos existencias
SELECT TOP 5 codigo, nombre, stock_actual, stock_minimo
FROM dbo.Producto
ORDER BY stock_actual ASC;
GO

-- 1.6 Estados de cita que realmente se han usado
SELECT DISTINCT estado
FROM dbo.Cita
ORDER BY estado;
GO

-- SECCIÓN 2. UPDATE Y DELETE CONTROLADOS 

-- 2.1 UPDATE
DECLARE @id_cita INT = (
    SELECT c.id_cita
    FROM dbo.Cita AS c
    INNER JOIN dbo.Paciente AS p ON p.id_paciente = c.id_paciente
    WHERE p.dui = '03456789-0' AND c.fecha = '2026-11-27'
);

-- Antes
SELECT id_cita, id_paciente, fecha, hora, estado FROM dbo.Cita WHERE id_cita = @id_cita;

UPDATE dbo.Cita
SET hora = '15:00',
    estado = 'Reprogramada'
WHERE id_cita = @id_cita;

-- Después
SELECT id_cita, id_paciente, fecha, hora, estado FROM dbo.Cita WHERE id_cita = @id_cita;
GO

-- 2.2 UPDATE
-- Antes
SELECT codigo, nombre, precio_compra, precio_venta FROM dbo.Producto WHERE codigo = 'MED-004';

UPDATE dbo.Producto
SET precio_venta = 6.00
WHERE codigo = 'MED-004';

-- Después
SELECT codigo, nombre, precio_compra, precio_venta FROM dbo.Producto WHERE codigo = 'MED-004';
GO

-- 2.3 DELETE
DECLARE @id_orden INT = (
    SELECT o.id_orden
    FROM dbo.OrdenCompra AS o
    INNER JOIN dbo.Proveedor AS pr ON pr.id_proveedor = o.id_proveedor
    WHERE o.estado = 'Cancelada' AND pr.nombre = 'Distribuidora OdontoPlus'
);

-- Antes:
SELECT id_orden, id_proveedor, fecha, total, estado FROM dbo.OrdenCompra WHERE id_orden = @id_orden;
SELECT id_detalle, id_orden, id_producto, cantidad, subtotal FROM dbo.OrdenCompraDetalle WHERE id_orden = @id_orden;

DELETE FROM dbo.OrdenCompra
WHERE id_orden = @id_orden
  AND estado = 'Cancelada';      -- doble condición: nunca borra una orden activa

-- Después
SELECT id_orden, id_proveedor, fecha, total, estado FROM dbo.OrdenCompra WHERE id_orden = @id_orden;
SELECT id_detalle, id_orden, id_producto, cantidad, subtotal FROM dbo.OrdenCompraDetalle WHERE id_orden = @id_orden;
GO

-- SECCIÓN 3. FUNCIONES DE AGREGADO, GROUP BY Y HAVING

-- 3.1 Resumen general de tratamientos válidos
SELECT COUNT(*)              AS cantidad_tratamientos,
       SUM(costo)            AS total_facturado,
       CAST(AVG(costo) AS DECIMAL(10,2)) AS costo_promedio,
       MIN(costo)            AS costo_minimo,
       MAX(costo)            AS costo_maximo
FROM dbo.Tratamiento
WHERE estado <> 'Anulado';
GO

-- 3.2 Carga de trabajo
SELECT u.nombre_completo AS odontologo,
       c.estado,
       COUNT(*)          AS cantidad_citas
FROM dbo.Cita AS c
INNER JOIN dbo.Usuario AS u ON u.id_usuario = c.id_odontologo
GROUP BY u.nombre_completo, c.estado
ORDER BY u.nombre_completo, c.estado;
GO

-- 3.3 Ingresos por mes
SELECT YEAR(fecha)  AS anio,
       MONTH(fecha) AS mes,
       COUNT(*)     AS cantidad_pagos,
       SUM(monto)   AS total_cobrado
FROM dbo.MovimientoCuenta
WHERE tipo = 'Abono'
  AND concepto NOT LIKE 'Anulación%'      -- una anulación no es dinero recibido
GROUP BY YEAR(fecha), MONTH(fecha)
ORDER BY anio, mes;
GO

-- 3.4 HAVING
SELECT ec.id_paciente,
       SUM(m.monto) AS total_cargado
FROM dbo.MovimientoCuenta AS m
INNER JOIN dbo.EstadoCuenta AS ec ON ec.id_estado = m.id_estado
WHERE m.tipo = 'Cargo'
GROUP BY ec.id_paciente
HAVING SUM(m.monto) > 100
ORDER BY total_cargado DESC;
GO

-- 3.5 HAVING con COUNT
SELECT id_odontologo,
       COUNT(*) AS citas_atendidas
FROM dbo.Cita
WHERE estado = 'Atendida'
GROUP BY id_odontologo
HAVING COUNT(*) >= 3;
GO

-- SECCIÓN 4. SUBCONSULTAS

-- 4.1 Subconsulta de UN valor
SELECT id_tratamiento, descripcion, costo
FROM dbo.Tratamiento
WHERE estado <> 'Anulado'
  AND costo > (SELECT AVG(costo) FROM dbo.Tratamiento WHERE estado <> 'Anulado')
ORDER BY costo DESC;
GO

-- 4.2 Subconsulta de VARIOS valores
SELECT codigo, nombre, stock_actual, stock_minimo
FROM dbo.Producto
WHERE id_producto IN (
        SELECT d.id_producto
        FROM dbo.OrdenCompraDetalle AS d
        INNER JOIN dbo.OrdenCompra AS o ON o.id_orden = d.id_orden
        WHERE o.estado = 'Pendiente'
      )
ORDER BY nombre;
GO

-- 4.3 Subconsulta con NOT IN
SELECT codigo, nombre, stock_actual, stock_minimo
FROM dbo.Producto
WHERE stock_actual < stock_minimo
  AND id_producto NOT IN (
        SELECT d.id_producto
        FROM dbo.OrdenCompraDetalle AS d
        INNER JOIN dbo.OrdenCompra AS o ON o.id_orden = d.id_orden
        WHERE o.estado = 'Pendiente'
      );
GO

-- 4.4 Pacientes cuyo saldo pendiente es mayor que el saldo promedio de la clínica.
SELECT p.id_paciente, p.nombres, p.apellidos, ec.saldo_actual
FROM dbo.Paciente AS p
INNER JOIN dbo.EstadoCuenta AS ec ON ec.id_paciente = p.id_paciente
WHERE ec.saldo_actual > (SELECT AVG(saldo_actual) FROM dbo.EstadoCuenta)
ORDER BY ec.saldo_actual DESC;
GO

-- SECCIÓN 5. FUNCIONES INTEGRADAS Y PERSONALIZADAS

-- 5.1 Funciones integradas de texto, fecha y NULL sobre pacientes
SELECT id_paciente,
       CONCAT(nombres, ' ', apellidos)                 AS nombre_completo,
       UPPER(apellidos)                                AS apellidos_mayuscula,
       LEFT(nombres, 1) + LEFT(apellidos, 1)           AS iniciales,
       ISNULL(NULLIF(correo, ''), 'Sin correo')        AS correo,
       CONVERT(VARCHAR(10), fecha_nacimiento, 103)     AS fecha_nacimiento,
       DATEDIFF(DAY, fecha_registro, GETDATE())        AS dias_como_paciente
FROM dbo.Paciente
ORDER BY apellidos;
GO

-- 5.2 Funciones integradas numéricas y de fecha sobre inventario
SELECT codigo,
       nombre,
       CONVERT(VARCHAR(10), fecha_vencimiento, 103)          AS vence,
       DATEDIFF(DAY, CAST(GETDATE() AS DATE), fecha_vencimiento) AS dias_para_vencer,
       CASE
           WHEN fecha_vencimiento IS NULL THEN 'No vence'
           WHEN fecha_vencimiento < CAST(GETDATE() AS DATE) THEN 'VENCIDO'
           WHEN DATEDIFF(DAY, CAST(GETDATE() AS DATE), fecha_vencimiento) <= 60 THEN 'Vence pronto'
           ELSE 'Vigente'
       END                                                    AS situacion,
       CAST(ROUND(precio_compra / unidades_por_caja, 2) AS DECIMAL(10,2)) AS costo_por_unidad
FROM dbo.Producto
ORDER BY dias_para_vencer;
GO

-- 5.3 FUNCIÓN PERSONALIZADA 1
CREATE OR ALTER FUNCTION dbo.fn_EdadPaciente
(
    @fecha_nacimiento DATE
)
RETURNS INT
AS
BEGIN
    IF @fecha_nacimiento IS NULL
        RETURN NULL;

    DECLARE @hoy DATE = CAST(GETDATE() AS DATE);
    DECLARE @edad INT = DATEDIFF(YEAR, @fecha_nacimiento, @hoy);

    IF DATEADD(YEAR, @edad, @fecha_nacimiento) > @hoy
        SET @edad = @edad - 1;

    RETURN @edad;
END;
GO

-- 5.4 FUNCIÓN PERSONALIZADA 2
CREATE OR ALTER FUNCTION dbo.fn_SaldoPendiente
(
    @id_paciente INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @saldo DECIMAL(10,2);

    SELECT @saldo = SUM(CASE WHEN m.tipo = 'Cargo' THEN m.monto ELSE -m.monto END)
    FROM dbo.MovimientoCuenta AS m
    INNER JOIN dbo.EstadoCuenta AS ec ON ec.id_estado = m.id_estado
    WHERE ec.id_paciente = @id_paciente;

    RETURN ISNULL(@saldo, 0);
END;
GO

-- 5.5 Uso de las funciones personalizadas
SELECT id_paciente,
       CONCAT(nombres, ' ', apellidos)           AS paciente,
       dbo.fn_EdadPaciente(fecha_nacimiento)    AS edad,
       CASE WHEN dbo.fn_EdadPaciente(fecha_nacimiento) < 18 THEN 'Menor de edad' ELSE 'Adulto' END AS grupo,
       dbo.fn_SaldoPendiente(id_paciente)       AS saldo_pendiente
FROM dbo.Paciente
ORDER BY edad;
GO

-- SECCIÓN 6. CONSULTAS CON JOIN

-- 6.1 INNER JOIN
SELECT c.id_cita,
       CONVERT(VARCHAR(10), c.fecha, 103)   AS fecha,
       c.hora,
       CONCAT(p.nombres, ' ', p.apellidos)  AS paciente,
       u.nombre_completo                    AS odontologo,
       c.motivo,
       c.estado
FROM dbo.Cita AS c
INNER JOIN dbo.Paciente AS p ON p.id_paciente = c.id_paciente
INNER JOIN dbo.Usuario  AS u ON u.id_usuario  = c.id_odontologo
WHERE c.fecha BETWEEN '2026-11-01' AND '2026-12-31'
ORDER BY c.fecha, c.hora;
GO

-- 6.2 LEFT JOIN
SELECT p.id_paciente, p.nombres, p.apellidos, p.estado
FROM dbo.Paciente AS p
LEFT JOIN dbo.Cita AS c ON c.id_paciente = p.id_paciente
WHERE c.id_cita IS NULL;
GO

-- 6.3 LEFT JOIN
SELECT p.id_paciente,
       CONCAT(p.nombres, ' ', p.apellidos)          AS paciente,
       ISNULL(h.diagnostico, 'Sin historia clínica') AS diagnostico,
       ISNULL(u.nombre_completo, '-')                AS odontologo_responsable
FROM dbo.Paciente AS p
LEFT JOIN dbo.HistoriaClinica AS h ON h.id_paciente = p.id_paciente
LEFT JOIN dbo.Usuario         AS u ON u.id_usuario  = h.id_odontologo
ORDER BY p.id_paciente;
GO

-- 6.4 INNER JOIN
SELECT o.id_orden,
       CONVERT(VARCHAR(10), o.fecha, 103) AS fecha,
       pr.nombre                          AS proveedor,
       o.estado,
       cat.nombre                         AS categoria,
       prod.nombre                        AS producto,
       d.cantidad                         AS cajas,
       d.precio_unitario,
       d.subtotal
FROM dbo.OrdenCompra AS o
INNER JOIN dbo.Proveedor          AS pr   ON pr.id_proveedor  = o.id_proveedor
INNER JOIN dbo.OrdenCompraDetalle AS d    ON d.id_orden       = o.id_orden
INNER JOIN dbo.Producto           AS prod ON prod.id_producto = d.id_producto
INNER JOIN dbo.CategoriaProducto  AS cat  ON cat.id_categoria = prod.id_categoria
ORDER BY o.id_orden, prod.nombre;
GO

-- 6.5 INNER + LEFT JOIN
SELECT r.id_receta,
       CONVERT(VARCHAR(10), r.fecha, 103)        AS fecha,
       CONCAT(p.nombres, ' ', p.apellidos)       AS paciente,
       d.medicamento,
       d.dosis,
       d.frecuencia,
       ISNULL(prod.codigo, 'Fuera de inventario') AS codigo_producto,
       d.cantidad,
       d.cantidad_despachada,
       d.cantidad - d.cantidad_despachada        AS pendiente_despacho
FROM dbo.Receta AS r
INNER JOIN dbo.Paciente      AS p    ON p.id_paciente   = r.id_paciente
INNER JOIN dbo.RecetaDetalle AS d    ON d.id_receta     = r.id_receta
LEFT  JOIN dbo.Producto      AS prod ON prod.id_producto = d.id_producto
ORDER BY r.id_receta;
GO