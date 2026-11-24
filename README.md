# Toothly · Sistema de Gestión Odontológica

Sistema de escritorio para una clínica dental, desarrollado en **C# con Windows Forms (.NET 10)** y respaldado por la base de datos relacional **ToothlyDB** en **SQL Server 2019**.

Proyecto integrador del módulo **TDS1.6 · Diseño de Bases de Datos Relacionales** · ITCA-FEPADE, Regional Santa Ana.

## Qué hace el sistema

- Pacientes, ficha del paciente e historia clínica
- Citas y agenda por odontólogo
- Tratamientos, recetas y despacho de medicamentos
- Estados de cuenta (cargos y abonos)
- Inventario con alertas de stock, proveedores y órdenes de compra
- Usuarios con roles y privilegios, bitácora y reportes

## Estructura del repositorio

```
Toothly/
├── Sistema_Toothly.slnx              Solución de Visual Studio
├── Sistema_Toothly/                  Código del sistema (Forms, Datos, Clases, Utilidades, Recursos)
└── BaseDatos/
    ├── 01_Crear_Base_y_Tablas.sql    Base, 20 tablas, claves y restricciones
    ├── 02_Insertar_Datos_Prueba.sql  Datos de prueba ficticios
    ├── 03_Consultas_y_Reportes.sql   Consultas, UPDATE/DELETE, agregados, subconsultas, funciones y JOIN
    └── 04_Trigger_y_Procedimientos.sql  Trigger de auditoría, procedimientos almacenados y casos de prueba
```

## Requisitos

- Windows 10 u 11
- SQL Server 2019 (Developer o Express) y SQL Server Management Studio
- Visual Studio con el SDK de .NET 10 y la carga de trabajo «Desarrollo de escritorio de .NET»

## Cómo crear la base de datos

En SQL Server Management Studio, conectado a la instancia local, ejecutar los scripts **en este orden** y cada uno completo (F5):

1. `01_Crear_Base_y_Tablas.sql` — crea ToothlyDB y sus tablas. Si la base ya existe, primero genera un respaldo automático.
2. `02_Insertar_Datos_Prueba.sql` — carga los datos de prueba.
3. `03_Consultas_y_Reportes.sql` — consultas del proyecto y funciones `fn_EdadPaciente` y `fn_SaldoPendiente`.
4. `04_Trigger_y_Procedimientos.sql` — trigger `tr_Cita_AuditarCambios`, procedimientos `sp_HistorialCitasPaciente` y `sp_RegistrarAbono`, y casos de prueba.

Con los scripts 01 y 02 el sistema ya puede usarse; el 03 y el 04 agregan las funciones, el trigger y los procedimientos.

## Cómo ejecutar el sistema

1. Abrir `Sistema_Toothly.slnx` en Visual Studio.
2. Verificar la conexión en `Sistema_Toothly/Datos/ConexionDB.cs`. Por defecto usa `Server=localhost` con autenticación de Windows. Si SQL Server está instalado como instancia con nombre (por ejemplo SQLEXPRESS), cambiar a `Server=.\SQLEXPRESS`.
3. Ejecutar con **F5**. Visual Studio restaura automáticamente el paquete `Microsoft.Data.SqlClient`.

Los usuarios de prueba son `admin`, `recepcion`, `odontologo` y `asistente`. Las contraseñas se guardan encriptadas y se entregan por separado al docente.

## Integrantes

- Bryan Alexis Hernández Morán
- Jimena Carolina Hernández Ramírez
- José Rodrigo Zavala Lemus

> Todos los datos de pacientes son ficticios y se crearon solo para pruebas.
