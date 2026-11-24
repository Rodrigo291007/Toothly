USE ToothlyDB;
GO

SET NOCOUNT ON;
SET DATEFORMAT ymd;   -- las fechas se escriben año-mes-día sin importar el idioma del servidor
GO

-- Protección: si ya hay pacientes, este script no se ejecuta para no duplicar datos.
IF EXISTS (SELECT 1 FROM dbo.Paciente)
BEGIN
    PRINT 'ToothlyDB ya tiene datos. Ejecute primero 01_Crear_Base_y_Tablas.sql.';
    SET NOEXEC ON;
    THROW 50000, 'El script 02 se detuvo para no duplicar datos.', 1;
END;
GO

-- 1. CATÁLOGOS DE SEGURIDAD
-- Roles del sistema
INSERT INTO dbo.Rol (id_rol, nombre_rol) VALUES
(1, 'Administrador'),
(2, 'Recepcionista'),
(3, 'Odontólogo'),
(4, 'Asistente Odontológico');

-- Privilegios
INSERT INTO dbo.Privilegio (id_privilegio, nombre) VALUES
(1, 'Insertar'),
(2, 'Consultar'),
(3, 'Modificar'),
(4, 'Eliminar'),
(5, 'Reportes');

-- Privilegios por rol
INSERT INTO dbo.RolPrivilegio (id_rol, id_privilegio) VALUES
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5),   -- Administrador: todos
(2, 1), (2, 2), (2, 3), (2, 5),           -- Recepcionista
(3, 1), (3, 2), (3, 3), (3, 5),           -- Odontólogo
(4, 1), (4, 2), (4, 3), (4, 5);           -- Asistente Odontológico

-- Usuarios (claves encriptadas con TripleDES por el sistema).
-- Los odontólogos 5 y 6 usan la misma clave encriptada que 'odontologo'
-- y el usuario 7 la misma que 'recepcion'.
INSERT INTO dbo.Usuario (nombre_usuario, clave, nombre_completo, correo, id_rol, estado) VALUES
('admin',      'fTuVBbJuJgQidVmpC6xskA==', 'Administrador del Sistema', 'admin@toothly.com',      1, 'A'), -- 1
('recepcion',  'bF2k2h6f1HwidVmpC6xskA==', 'María López',               'recepcion@toothly.com',  2, 'A'), -- 2
('odontologo', 'EfuDIYZbuAGHXFDc3FQayg==', 'Dr. Carlos Martínez',       'odontologo@toothly.com', 3, 'A'), -- 3
('asistente',  'z7y8dBaZfdsidVmpC6xskA==', 'Ana García',                'asistente@toothly.com',  4, 'A'), -- 4
('shernandez', 'EfuDIYZbuAGHXFDc3FQayg==', 'Dra. Sofía Hernández',      'shernandez@toothly.com', 3, 'A'), -- 5
('lramirez',   'EfuDIYZbuAGHXFDc3FQayg==', 'Dr. Luis Ramírez',          'lramirez@toothly.com',   3, 'A'), -- 6
('palvarado',  'bF2k2h6f1HwidVmpC6xskA==', 'Pedro Alvarado',            'palvarado@toothly.com',  2, 'I'); -- 7 (inactivo)

-- 2. INVENTARIO: categorías, proveedores y productos
INSERT INTO dbo.CategoriaProducto (id_categoria, nombre, descripcion) VALUES
(1, 'Medicamento', 'Medicamentos y fármacos odontológicos'),
(2, 'Material',    'Materiales dentales (resinas, yesos, anestesia, etc.)'),
(3, 'Instrumento', 'Instrumentos y herramientas de odontología'),
(4, 'Higiene oral','Productos de higiene para venta al paciente'),
(5, 'Bioseguridad','Guantes, mascarillas y material desechable');

INSERT INTO dbo.Proveedor (nombre, telefono, correo, direccion, estado) VALUES
('Dental El Salvador S.A.',          '2222-1111', 'ventas@dentalsv.com',        'San Salvador, Col. Médica', 'A'), -- 1
('Distribuidora OdontoPlus',         '2233-4455', 'contacto@odontoplus.com',    'Santa Tecla, La Libertad',  'A'), -- 2
('Suministros Médicos de Occidente', '2440-7788', 'ventas@sumeoccidente.com',   'Santa Ana, Av. Independencia', 'A'), -- 3
('BioDental Centroamérica',          '2260-3030', 'pedidos@biodentalca.com',    'Antiguo Cuscatlán, La Libertad', 'A'), -- 4
('Insumos Odontológicos Martínez',   '2661-9090', 'insumos.martinez@correo.com','San Miguel, Barrio El Centro', 'I'); -- 5 (inactivo)

-- stock_actual en unidades; unidades_por_caja convierte las cajas de una orden en unidades
INSERT INTO dbo.Producto (codigo, nombre, id_categoria, stock_actual, stock_minimo, precio_compra, fecha_vencimiento, unidades_por_caja, precio_venta) VALUES
('MED-001', 'Amoxicilina 500mg (Caja 30)',          1,  90,  30,  8.50, '2026-12-31',  30, 0.50), -- 1
('MED-002', 'Ibuprofeno 400mg (Caja 20)',           1,  54,  20,  4.25, '2027-06-30',  20, 0.35), -- 2
('MAT-001', 'Resina Compuesta A2 4g',               2,  12,   5, 22.00, '2026-08-15',   1, 0.00), -- 3 (vencida)
('INS-001', 'Espejo Bucal N°5 c/mango',             3,  10,   4,  6.00, NULL,           1, 0.00), -- 4
('MED-003', 'Acetaminofén 500mg (Caja 100)',        1,  15,  40,  3.75, '2027-03-31', 100, 0.10), -- 5 (bajo mínimo)
('MED-004', 'Clorhexidina 0.12% enjuague 250ml',    1,   7,  10,  3.90, '2027-01-31',   1, 5.50), -- 6 (bajo mínimo)
('MAT-002', 'Lidocaína 2% con epinefrina (Caja 50)',2, 100,  50, 28.00, '2027-05-31',  50, 0.00), -- 7
('HIG-001', 'Cepillo dental suave adulto',          4,  24,  15,  0.60, NULL,           1, 1.50), -- 8
('BIO-001', 'Guantes de nitrilo talla M (Caja 100)',5, 150, 200,  7.80, NULL,         100, 0.00), -- 9 (bajo mínimo)
('BIO-002', 'Mascarillas desechables (Caja 50)',    5, 400, 100,  4.50, NULL,          50, 0.00); -- 10

-- 3. PACIENTES Y SUS ESTADOS DE CUENTA
INSERT INTO dbo.Paciente (nombres, apellidos, dui, fecha_nacimiento, sexo, telefono, correo, direccion, alergias, antecedentes, estado, fecha_registro) VALUES
('Juan Carlos',      'Pérez Gómez',       '01234567-8', '1990-05-15', 'M', '7788-9900', 'juan.perez@gmail.com',      'San Salvador', 'Alergia a la penicilina', 'Ninguno',                 'A', '2026-09-01T08:15:00'), -- 1
('Ana María',        'Rodríguez Solís',   '08765432-1', '1995-10-20', 'F', '7122-3344', 'ana.rodriguez@gmail.com',   'Santa Tecla',  'Ninguna',                 'Hipertensión controlada', 'A', '2026-09-03T10:20:00'), -- 2
('Luis Fernando',    'Martínez Castro',   '02345678-9', '1985-03-08', 'M', '7456-1122', 'luis.martinez@correo.com',  'Santa Ana',    'Ninguna',                 'Diabetes tipo 2',         'A', '2026-09-10T09:00:00'), -- 3
('Gabriela Beatriz', 'Hernández Ruiz',    '03456789-0', '2001-07-25', 'F', '7012-3456', 'gabriela.hr@correo.com',    'Chalchuapa',   'Alergia al látex',        'Ninguno',                 'A', '2026-09-15T14:30:00'), -- 4
('José Antonio',     'Flores Mejía',      '07890123-4', '2006-11-02', 'M', '7890-1234', NULL,                        'Santa Ana',    'Ninguna',                 'Ninguno',                 'A', '2026-10-01T15:45:00'), -- 5
('Carmen Elena',     'Vásquez de Rivas',  '04567890-1', '1968-01-30', 'F', '7345-6789', 'carmen.vasquez@correo.com', 'Metapán',      'Ninguna',                 'Hipertensión',            'A', '2026-10-05T11:10:00'), -- 6
('Ricardo Alberto',  'Molina Pineda',     '05678901-2', '1978-09-14', 'M', '7567-8901', NULL,                        'Ahuachapán',   'Alergia al ibuprofeno',   'Gastritis',               'A', '2026-10-20T16:00:00'), -- 7
('Sofía Alejandra',  'Campos Linares',    '06789012-3', '1999-12-05', 'F', '7678-9012', 'sofia.campos@correo.com',   'Santa Ana',    'Ninguna',                 'Ninguno',                 'I', '2026-10-22T08:40:00'); -- 8 (inactiva, sin citas)

-- El sistema crea una cuenta por paciente; id_estado coincide con id_paciente
INSERT INTO dbo.EstadoCuenta (id_paciente, saldo_actual) VALUES
(1, 0.00), (2, 0.00), (3, 0.00), (4, 0.00),
(5, 0.00), (6, 0.00), (7, 0.00), (8, 0.00);

-- 4. CITAS (odontólogos: 3 = Dr. Martínez, 5 = Dra. Hernández, 6 = Dr. Ramírez)
-- Horas con el mismo formato del sistema (HH:mm). Pasadas: Atendida/Cancelada.
INSERT INTO dbo.Cita (id_paciente, id_odontologo, fecha, hora, motivo, estado) VALUES
(1, 3, '2026-10-05', '08:00', 'Limpieza dental',                     'Atendida'),     -- 1
(2, 3, '2026-10-07', '09:30', 'Dolor en molar inferior',             'Atendida'),     -- 2
(3, 5, '2026-10-12', '10:00', 'Evaluación general',                  'Atendida'),     -- 3
(4, 6, '2026-10-15', '14:00', 'Valoración de ortodoncia',            'Atendida'),     -- 4
(1, 3, '2026-11-02', '08:30', 'Control posterior a limpieza',        'Cancelada'),    -- 5
(5, 5, '2026-11-09', '15:00', 'Revisión de caries',                  'Atendida'),     -- 6
(6, 6, '2026-11-10', '11:00', 'Prótesis - toma de medidas',          'Atendida'),     -- 7
(2, 3, '2026-11-16', '09:00', 'Endodoncia - sesión 1',               'Atendida'),     -- 8
(7, 5, '2026-11-18', '13:30', 'Extracción de tercer molar',          'Cancelada'),    -- 9
(3, 5, '2026-11-25', '10:30', 'Limpieza y control',                  'Programada'),   -- 10
(2, 3, '2026-11-26', '09:00', 'Endodoncia - sesión 2',               'Programada'),   -- 11
(4, 6, '2026-11-27', '14:30', 'Colocación de brackets',              'Programada'),   -- 12
(6, 6, '2026-12-01', '11:00', 'Prótesis - prueba',                   'Reprogramada'), -- 13
(5, 3, '2026-12-03', '16:00', 'Aplicación de sellantes',             'Programada'),   -- 14
(1, 5, '2026-12-04', '08:00', 'Blanqueamiento dental',               'Programada');   -- 15

-- 5. HISTORIAS CLÍNICAS (una por paciente; el paciente 8 aún no tiene)
INSERT INTO dbo.HistoriaClinica (id_paciente, antecedentes, observaciones_generales, observaciones_clinicas, fecha_apertura, motivo_consulta, diagnostico, id_odontologo) VALUES
(1, 'Ninguno',                 'Paciente colaborador',            'Sarro moderado en sector anterior inferior', '2026-10-05T08:10:00', 'Limpieza dental',          'Gingivitis leve',                         3),
(2, 'Hipertensión controlada', 'Medir presión antes de anestesia','Caries profunda en pieza 36',                '2026-10-07T09:40:00', 'Dolor en molar inferior',  'Pulpitis irreversible pieza 36',          3),
(3, 'Diabetes tipo 2',         'Control de glucosa al día',       'Inflamación gingival generalizada',          '2026-10-12T10:05:00', 'Evaluación general',       'Periodontitis leve',                      5),
(4, 'Ninguno',                 'Alergia al látex: usar nitrilo',  'Apiñamiento dental anterior',                '2026-10-15T14:10:00', 'Valoración de ortodoncia', 'Maloclusión clase I con apiñamiento',     6),
(5, 'Ninguno',                 'Estudiante, primera consulta',             'Caries oclusal en pieza 46',                 '2026-11-09T15:05:00', 'Revisión de caries',       'Caries de esmalte pieza 46',              5),
(6, 'Hipertensión',            'Usa prótesis anterior desgastada','Ausencia de piezas 14, 15 y 24',             '2026-11-10T11:05:00', 'Prótesis',                 'Edentulismo parcial superior',            6),
(7, 'Gastritis',               'Evitar AINES (alergia)',          'Tercer molar inferior semierupcionado',      '2026-11-18T13:00:00', 'Dolor en zona posterior',  'Pericoronaritis pieza 38',                5);

-- 6. TRATAMIENTOS
INSERT INTO dbo.Tratamiento (id_paciente, id_odontologo, descripcion, fecha_inicio, fecha_fin, costo, estado) VALUES
(1, 3, 'Limpieza dental profunda',          '2026-10-05', '2026-10-05',  35.00, 'Finalizado'), -- 1
(2, 3, 'Endodoncia pieza 36',               '2026-11-16', '2026-11-30', 180.00, 'En proceso'), -- 2
(3, 5, 'Profilaxis y detartraje',           '2026-10-12', '2026-10-12',  40.00, 'Finalizado'), -- 3
(4, 6, 'Ortodoncia fija - fase inicial',    '2026-10-15', '2027-04-15', 450.00, 'En proceso'), -- 4
(5, 5, 'Restauración con resina pieza 46',  '2026-11-09', '2026-11-09',  45.00, 'Finalizado'), -- 5
(6, 6, 'Prótesis parcial removible',        '2026-11-10', '2026-12-15', 320.00, 'En proceso'), -- 6
(7, 5, 'Extracción de tercer molar',        '2026-11-18', '2026-11-18',  60.00, 'Anulado'),    -- 7
(1, 5, 'Blanqueamiento dental',             '2026-12-04', '2026-12-04', 120.00, 'Pendiente'),  -- 8
(5, 3, 'Aplicación de sellantes',           '2026-12-03', '2026-12-03',  30.00, 'Pendiente');  -- 9

-- 7. RECETAS Y SU DETALLE
INSERT INTO dbo.Receta (id_paciente, id_tratamiento, fecha, indicaciones) VALUES
(2, 2, '2026-11-16', 'Tomar los medicamentos después de las comidas.'),           -- 1
(3, 3, '2026-10-12', 'Enjuague dos veces al día después del cepillado.'),         -- 2
(5, 5, '2026-11-09', 'Analgésico solo en caso de dolor.'),                        -- 3
(6, 6, '2026-11-10', 'Completar el antibiótico aunque desaparezcan las molestias.'), -- 4
(1, 1, '2026-10-05', 'Cepillado suave tres veces al día y enjuague nocturno.');   -- 5

-- cantidad_despachada > 0 indica que ya se vendió desde el módulo de despacho
INSERT INTO dbo.RecetaDetalle (id_receta, medicamento, dosis, frecuencia, duracion, id_producto, cantidad, cantidad_despachada) VALUES
(1, 'Ibuprofeno 400mg',                 '1 tableta', 'Cada 8 horas',             '3 días',  2,  9, 6),
(1, 'Amoxicilina 500mg',                '1 cápsula', 'Cada 8 horas',             '7 días',  1, 21, 0),
(2, 'Clorhexidina 0.12% enjuague',      '15 ml',     'Cada 12 horas',            '10 días', 6,  1, 1),
(3, 'Acetaminofén 500mg',               '1 tableta', 'Cada 6 horas si hay dolor','2 días',  5,  8, 0),
(4, 'Amoxicilina 500mg',                '1 cápsula', 'Cada 8 horas',             '7 días',  1, 21, 0),
(4, 'Acetaminofén 500mg',               '1 tableta', 'Cada 6 horas si hay dolor','3 días',  5, 10, 0),
(5, 'Enjuague bucal sin alcohol',       '15 ml',     'Cada 12 horas',            '15 días', NULL, 0, 0), -- fuera de inventario
(5, 'Cepillo dental suave',             '1 unidad',  'Uso diario',               'Permanente', 8, 1, 1);

-- 8. MOVIMIENTOS DE CUENTA (cargos por tratamientos/ventas y abonos)
INSERT INTO dbo.MovimientoCuenta (id_estado, fecha, tipo, concepto, monto) VALUES
(1, '2026-10-05T08:50:00', 'Cargo', 'Limpieza dental profunda',                              35.00),
(1, '2026-10-05T08:55:00', 'Abono', 'Pago en efectivo',                                     35.00),
(1, '2026-10-05T09:00:00', 'Cargo', 'Venta: Cepillo dental suave adulto x 1 und (Receta 5)', 1.50),
(1, '2026-11-20T10:00:00', 'Cargo', 'Blanqueamiento dental',                                120.00),
(2, '2026-11-16T09:45:00', 'Cargo', 'Endodoncia pieza 36',                                  180.00),
(2, '2026-11-16T09:50:00', 'Cargo', 'Venta: Ibuprofeno 400mg (Caja 20) x 6 und (Receta 1)',  2.10),
(2, '2026-11-16T09:55:00', 'Abono', 'Pago con tarjeta - primera cuota',                      80.00),
(3, '2026-10-12T10:40:00', 'Cargo', 'Profilaxis y detartraje',                               40.00),
(3, '2026-10-12T10:45:00', 'Cargo', 'Venta: Clorhexidina 0.12% enjuague 250ml x 1 und (Receta 2)', 5.50),
(3, '2026-10-12T10:50:00', 'Abono', 'Pago en efectivo',                                      40.00),
(4, '2026-10-15T14:50:00', 'Cargo', 'Ortodoncia fija - fase inicial',                       450.00),
(4, '2026-10-15T14:55:00', 'Abono', 'Prima de ortodoncia',                                  150.00),
(4, '2026-11-15T15:00:00', 'Abono', 'Cuota mensual de ortodoncia',                           50.00),
(5, '2026-11-09T15:40:00', 'Cargo', 'Restauración con resina pieza 46',                      45.00),
(5, '2026-11-09T15:45:00', 'Abono', 'Pago en efectivo',                                      45.00),
(5, '2026-11-20T16:10:00', 'Cargo', 'Aplicación de sellantes',                               30.00),
(6, '2026-11-10T11:50:00', 'Cargo', 'Prótesis parcial removible',                           320.00),
(6, '2026-11-10T11:55:00', 'Abono', 'Anticipo de prótesis',                                 100.00),
(7, '2026-11-18T13:20:00', 'Cargo', 'Extracción de tercer molar',                            60.00),
(7, '2026-11-18T13:35:00', 'Abono', 'Anulación de tratamiento: Extracción de tercer molar',  60.00);

-- El saldo se calcula igual que en el sistema: SUM(cargos) - SUM(abonos)
UPDATE ec
SET saldo_actual = ISNULL((SELECT SUM(CASE WHEN m.tipo = 'Cargo' THEN m.monto ELSE -m.monto END)
                           FROM dbo.MovimientoCuenta AS m
                           WHERE m.id_estado = ec.id_estado), 0)
FROM dbo.EstadoCuenta AS ec;

-- 9. ÓRDENES DE COMPRA Y SU DETALLE (cantidad = cajas)
INSERT INTO dbo.OrdenCompra (id_proveedor, fecha, total, estado) VALUES
(1, '2026-09-20T10:00:00', 0, 'Recibida'),   -- 1
(2, '2026-10-03T11:30:00', 0, 'Recibida'),   -- 2
(3, '2026-10-28T09:15:00', 0, 'Recibida'),   -- 3
(4, '2026-11-15T14:00:00', 0, 'Pendiente'),  -- 4
(1, '2026-11-19T08:45:00', 0, 'Pendiente'),  -- 5
(2, '2026-11-05T16:20:00', 0, 'Cancelada');  -- 6 (registro de prueba para el DELETE del script 03)

INSERT INTO dbo.OrdenCompraDetalle (id_orden, id_producto, cantidad, precio_unitario, subtotal) VALUES
(1, 1,  3,  8.50, 25.50),
(1, 2,  2,  4.25,  8.50),
(2, 3,  5, 22.00,110.00),
(2, 4,  4,  6.00, 24.00),
(3, 7,  2, 28.00, 56.00),
(3, 10, 8,  4.50, 36.00),
(4, 5,  1,  3.75,  3.75),
(4, 6, 10,  3.90, 39.00),
(5, 8, 20,  0.60, 12.00),
(5, 1,  2,  8.50, 17.00),
(6, 3,  2, 22.00, 44.00);

-- El total de cada orden es la suma de sus subtotales
UPDATE o
SET total = (SELECT SUM(d.subtotal) FROM dbo.OrdenCompraDetalle AS d WHERE d.id_orden = o.id_orden)
FROM dbo.OrdenCompra AS o;

-- 10. MOVIMIENTOS DE INVENTARIO (mismo texto que genera el sistema)
INSERT INTO dbo.MovimientoInventario (id_producto, tipo, cantidad, fecha, descripcion) VALUES
(1,  'Entrada',  90, '2026-09-22T09:00:00', 'Entrada por recepción de Orden de Compra #1 (3 caja(s))'),
(2,  'Entrada',  40, '2026-09-22T09:00:00', 'Entrada por recepción de Orden de Compra #1 (2 caja(s))'),
(3,  'Entrada',   5, '2026-10-05T10:00:00', 'Entrada por recepción de Orden de Compra #2 (5 caja(s))'),
(4,  'Entrada',   4, '2026-10-05T10:00:00', 'Entrada por recepción de Orden de Compra #2 (4 caja(s))'),
(7,  'Entrada', 100, '2026-10-30T08:30:00', 'Entrada por recepción de Orden de Compra #3 (2 caja(s))'),
(10, 'Entrada', 400, '2026-10-30T08:30:00', 'Entrada por recepción de Orden de Compra #3 (8 caja(s))'),
(8,  'Salida',    1, '2026-10-05T09:00:00', 'Venta por receta 5 - Juan Carlos Pérez Gómez'),
(6,  'Salida',    1, '2026-10-12T10:45:00', 'Venta por receta 2 - Luis Fernando Martínez Castro'),
(2,  'Salida',    6, '2026-11-16T09:50:00', 'Venta por receta 1 - Ana María Rodríguez Solís'),
(3,  'Ajuste',   12, '2026-11-01T17:00:00', 'Ajuste por conteo físico de inventario');

-- 11. BITÁCORA (ejemplos de acciones registradas por el sistema)
INSERT INTO dbo.Bitacora (id_usuario, fecha, hora, accion, descripcion) VALUES
(1, '2026-11-15', '13:58:10', 'Registro de orden de compra', 'Se registró la orden de compra para BioDental Centroamérica'),
(2, '2026-11-16', '08:55:31', 'Agendar cita',               'Se agendó cita para paciente ID 2 el 26/11/2026 a las 09:00'),
(3, '2026-11-16', '09:44:02', 'Registro de tratamiento',    'Se registró el tratamiento Endodoncia pieza 36'),
(5, '2026-11-18', '13:34:47', 'Anulación de tratamiento',   'Se anuló el tratamiento ID 7 y se revirtió su cargo de $60.00'),
(2, '2026-11-19', '08:46:15', 'Registro de orden de compra', 'Se registró la orden de compra para Dental El Salvador S.A.'),
(6, '2026-11-20', '10:02:09', 'Reprogramar cita',           'Se reprogramó la cita ID 13 para el 01/12/2026 11:00');
GO

-- 12. COMPROBACIÓN: cantidad de filas por tabla
SELECT 'Rol' AS tabla, COUNT(*) AS filas FROM dbo.Rol
UNION ALL SELECT 'Privilegio', COUNT(*) FROM dbo.Privilegio
UNION ALL SELECT 'RolPrivilegio', COUNT(*) FROM dbo.RolPrivilegio
UNION ALL SELECT 'Usuario', COUNT(*) FROM dbo.Usuario
UNION ALL SELECT 'Bitacora', COUNT(*) FROM dbo.Bitacora
UNION ALL SELECT 'Paciente', COUNT(*) FROM dbo.Paciente
UNION ALL SELECT 'Cita', COUNT(*) FROM dbo.Cita
UNION ALL SELECT 'HistoriaClinica', COUNT(*) FROM dbo.HistoriaClinica
UNION ALL SELECT 'Tratamiento', COUNT(*) FROM dbo.Tratamiento
UNION ALL SELECT 'Receta', COUNT(*) FROM dbo.Receta
UNION ALL SELECT 'RecetaDetalle', COUNT(*) FROM dbo.RecetaDetalle
UNION ALL SELECT 'EstadoCuenta', COUNT(*) FROM dbo.EstadoCuenta
UNION ALL SELECT 'MovimientoCuenta', COUNT(*) FROM dbo.MovimientoCuenta
UNION ALL SELECT 'CategoriaProducto', COUNT(*) FROM dbo.CategoriaProducto
UNION ALL SELECT 'Producto', COUNT(*) FROM dbo.Producto
UNION ALL SELECT 'MovimientoInventario', COUNT(*) FROM dbo.MovimientoInventario
UNION ALL SELECT 'Proveedor', COUNT(*) FROM dbo.Proveedor
UNION ALL SELECT 'OrdenCompra', COUNT(*) FROM dbo.OrdenCompra
UNION ALL SELECT 'OrdenCompraDetalle', COUNT(*) FROM dbo.OrdenCompraDetalle;
GO

SET NOEXEC OFF;
GO
