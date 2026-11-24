using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sistema_Toothly.Clases;

namespace Sistema_Toothly.Utilidades
{
    // Generador de reportes PDF nativo en C# con diseño clínico profesional,
    // logotipo institucional, soporte de múltiples páginas, anchos inteligentes y acentos en español.
    public static class GeneradorPDF
    {
        // Dimensiones estándar de página Carta (Letter) en puntos PDF (72 puntos = 1 pulgada)
        private const float ANCHO_PAGINA = 612f;
        private const float ALTO_PAGINA = 792f;
        private const float MARGEN_IZQ = 36f;
        private const float MARGEN_DER = 576f;
        private const float ANCHO_TABLA = 540f; // MARGEN_DER - MARGEN_IZQ

        private const float ALTO_FILA = 18f;
        private const float ALTO_CABECERA_TABLA = 22f;
        private const float ALTO_LINEA = 10.5f;        // interlineado del texto de las celdas
        private const float TAM_FUENTE_CELDA = 8.5f;
        private const int MAX_LINEAS_CELDA = 12;       // límite de seguridad por celda

        // Estructura para almacenar información formateada de columna
        private class ColumnaInfo
        {
            public DataGridViewColumn Columna { get; set; } = null!;
            public string Titulo { get; set; } = "";
            public float Ancho { get; set; }
            public float PosicionX { get; set; }
            public StringAlignment Alineacion { get; set; } = StringAlignment.Near;
            public int MaxCaracteres { get; set; } = 30;
            public bool EsMoneda { get; set; }
        }

        public static void GenerarDesdeGrid(string tituloReporte, DataGridView dgv)
        {
            try
            {
                if (dgv.Columns.Count == 0)
                {
                    MessageBox.Show("No hay datos en el informe para generar el reporte.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Documento PDF (*.pdf)|*.pdf";
                sfd.Title = "Guardar Reporte en PDF";
                string nombreArchivo = SanitizarNombreArchivo(tituloReporte);
                sfd.FileName = $"Reporte_{nombreArchivo}_{DateTime.Now:yyyyMMdd_HHmm}.pdf";

                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                byte[] pdfBytes = CrearPdfBytes(tituloReporte, dgv);
                File.WriteAllBytes(sfd.FileName, pdfBytes);

                // Abrir automáticamente el visor de PDF predeterminado de Windows
                try
                {
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
                catch { }

                MessageBox.Show(
                    "El reporte en PDF ha sido generado y abierto exitosamente.",
                    "Reporte Generado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static byte[] CrearPdfBytes(string titulo, DataGridView dgv)
        {
            // 1. Obtener columnas visibles
            List<DataGridViewColumn> colsVisibles = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible) colsVisibles.Add(col);
            }
            if (colsVisibles.Count == 0) colsVisibles.AddRange(dgv.Columns.Cast<DataGridViewColumn>());

            // 2. Calcular anchos proporcionales y alineación de columnas
            List<ColumnaInfo> columnas = CalcularColumnas(colsVisibles, dgv);

            // 3. Preparar logotipo institucional en formato JPEG
            byte[] logoJpegBytes;
            int logoImgW = 834;
            int logoImgH = 244;
            try
            {
                using (Bitmap src = Properties.Resources.logo_azul != null ? new Bitmap(Properties.Resources.logo_azul) : new Bitmap(Properties.Resources.logo))
                {
                    logoImgW = src.Width;
                    logoImgH = src.Height;
                    using (Bitmap rgbBmp = new Bitmap(src.Width, src.Height, PixelFormat.Format24bppRgb))
                    {
                        using (Graphics g = Graphics.FromImage(rgbBmp))
                        {
                            g.Clear(Color.White);
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(src, 0, 0, src.Width, src.Height);
                        }
                        using (MemoryStream jms = new MemoryStream())
                        {
                            rgbBmp.Save(jms, ImageFormat.Jpeg);
                            logoJpegBytes = jms.ToArray();
                        }
                    }
                }
            }
            catch
            {
                // Fallback: logotipo mínimo blanco en caso de error
                using (Bitmap dummy = new Bitmap(200, 60, PixelFormat.Format24bppRgb))
                {
                    using (Graphics g = Graphics.FromImage(dummy)) { g.Clear(Color.White); }
                    using (MemoryStream jms = new MemoryStream())
                    {
                        dummy.Save(jms, ImageFormat.Jpeg);
                        logoJpegBytes = jms.ToArray();
                        logoImgW = 200;
                        logoImgH = 60;
                    }
                }
            }

            // 4. Filtrar filas válidas (excluir fila nueva vacía)
            List<DataGridViewRow> filas = new List<DataGridViewRow>();
            foreach (DataGridViewRow r in dgv.Rows)
            {
                if (!r.IsNewRow) filas.Add(r);
            }

            // 5. Preparar el texto de cada celda ajustado (en varias líneas) al ancho de su columna,
            //    en lugar de cortarlo con ".." como antes (se perdía, por ejemplo, la columna de medicamentos)
            Dictionary<DataGridViewRow, List<string>[]> lineasPorFila = new Dictionary<DataGridViewRow, List<string>[]>();
            Dictionary<DataGridViewRow, float> altoPorFila = new Dictionary<DataGridViewRow, float>();
            foreach (var row in filas)
            {
                List<string>[] celdas = new List<string>[columnas.Count];
                int maxLineas = 1;
                for (int i = 0; i < columnas.Count; i++)
                {
                    object val = row.Cells[columnas[i].Columna.Index].Value;
                    string texto = val != null ? val.ToString()! : "";
                    texto = texto.Replace("\r", "");
                    celdas[i] = EnvolverTexto(texto, columnas[i].Ancho - 8f, TAM_FUENTE_CELDA, MAX_LINEAS_CELDA);
                    maxLineas = Math.Max(maxLineas, celdas[i].Count);
                }
                lineasPorFila[row] = celdas;
                altoPorFila[row] = Math.Max(ALTO_FILA, maxLineas * ALTO_LINEA + 7f);
            }

            // 6. Paginación según el alto real de las filas
            const float TOPE_PAGINA_1 = 620f;   // borde superior de la primera fila en la página 1
            const float TOPE_PAGINAS_SIG = 690f; // en las páginas siguientes
            const float LIMITE_INFERIOR = 50f;   // por encima del pie de página

            List<List<DataGridViewRow>> paginasFilas = new List<List<DataGridViewRow>>();
            List<DataGridViewRow> paginaActual = new List<DataGridViewRow>();
            float topeActual = TOPE_PAGINA_1;
            foreach (var row in filas)
            {
                float alto = altoPorFila[row];
                if (paginaActual.Count > 0 && topeActual - alto < LIMITE_INFERIOR)
                {
                    paginasFilas.Add(paginaActual);
                    paginaActual = new List<DataGridViewRow>();
                    topeActual = TOPE_PAGINAS_SIG;
                }
                paginaActual.Add(row);
                topeActual -= alto;
            }
            paginasFilas.Add(paginaActual);

            int totalPaginas = paginasFilas.Count;

            // 7. Generar el stream de contenido de cada página con soporte nativo de caracteres en español (Latin-1)
            List<byte[]> paginasContentBytes = new List<byte[]>();
            Encoding encPdf = Encoding.Latin1;

            for (int pIndex = 0; pIndex < totalPaginas; pIndex++)
            {
                StringBuilder sb = new StringBuilder();
                int numPagina = pIndex + 1;
                bool esPrimeraPagina = (numPagina == 1);
                List<DataGridViewRow> filasEstaPagina = paginasFilas[pIndex];

                float y;

                if (esPrimeraPagina)
                {
                    // Barra decorativa superior azul marino institucional
                    sb.Append("0.03 0.34 0.55 rg\n"); // #08568C
                    sb.Append($"36 756 {ANCHO_TABLA} 4 re f\n");

                    // Dibujo del Logotipo de la clínica
                    float drawLogoW = 120f;
                    float drawLogoH = (float)Math.Round(drawLogoW * ((float)logoImgH / logoImgW));
                    if (drawLogoH > 40f) drawLogoH = 40f;
                    float logoX = 36f;
                    float logoY = 708f;
                    sb.Append(string.Format(CultureInfo.InvariantCulture,
                        "q {0:0.0} 0 0 {1:0.0} {2:0.0} {3:0.0} cm /ImLogo Do Q\n",
                        drawLogoW, drawLogoH, logoX, logoY));

                    // Membrete de contacto a la derecha
                    sb.Append("0.03 0.34 0.55 rg\n");
                    sb.Append("BT\n/F2 11 Tf\n320 735 Td\n(CLÍNICA ODONTOLÓGICA TOOTHLY) Tj\nET\n");
                    sb.Append("0.35 0.4 0.45 rg\n");
                    sb.Append("BT\n/F1 8.5 Tf\n320 722 Td\n(Atención Odontológica Integral y Especializada) Tj\nET\n");
                    sb.Append("0.45 0.5 0.55 rg\n");
                    sb.Append("BT\n/F1 7.5 Tf\n320 710 Td\n(Tel: (503) 2222-0000  •  info@toothly.com  •  San Salvador) Tj\nET\n");

                    // Línea divisoria elegante
                    sb.Append("0.85 0.90 0.95 RG\n1 w\n");
                    sb.Append("36 698 m 576 698 l S\n");

                    // Tarjeta de Título del Reporte y Metadatos
                    float cardY = 648f;
                    float cardH = 42f;
                    sb.Append("0.95 0.98 1.0 rg\n"); // Azul muy suave #F0F7FD
                    sb.Append($"36 {cardY} {ANCHO_TABLA} {cardH} re f\n");
                    sb.Append("0.75 0.86 0.95 RG\n0.8 w\n");
                    sb.Append($"36 {cardY} {ANCHO_TABLA} {cardH} re S\n");

                    // Título del Reporte en negrita
                    sb.Append("0.03 0.34 0.55 rg\n");
                    string titFormateado = EscaparTexto(titulo.ToUpper());
                    sb.Append($"BT\n/F2 12.5 Tf\n48 {cardY + 24} Td\n({titFormateado}) Tj\nET\n");

                    // Subtítulo con fecha, usuario y total
                    string nombreUsuario = !string.IsNullOrEmpty(Sesion.NombreCompleto) ? Sesion.NombreCompleto : "Administrador";
                    string rolUsuario = !string.IsNullOrEmpty(Sesion.NombreRol) ? Sesion.NombreRol : "Sistema";
                    string metaInfo = $"Emisión: {DateTime.Now:dd/MM/yyyy HH:mm}   |   Generado por: {nombreUsuario} ({rolUsuario})   |   Total registros: {filas.Count}";
                    sb.Append("0.4 0.45 0.5 rg\n");
                    sb.Append($"BT\n/F1 8 Tf\n48 {cardY + 9} Td\n({EscaparTexto(metaInfo)}) Tj\nET\n");

                    y = cardY - 26f;
                }
                else
                {
                    // === ENCABEZADO COMPACTO (Páginas 2 en adelante) ===
                    sb.Append("0.03 0.34 0.55 rg\n");
                    sb.Append($"36 756 {ANCHO_TABLA} 3 re f\n");

                    // Mini logotipo
                    float miniW = 80f;
                    float miniH = (float)Math.Round(miniW * ((float)logoImgH / logoImgW));
                    sb.Append(string.Format(CultureInfo.InvariantCulture,
                        "q {0:0.0} 0 0 {1:0.0} 36 726 cm /ImLogo Do Q\n", miniW, miniH));

                    sb.Append("0.03 0.34 0.55 rg\n");
                    sb.Append($"BT\n/F2 10 Tf\n130 735 Td\n(TOOTHLY  •  {EscaparTexto(titulo.ToUpper())}) Tj\nET\n");

                    sb.Append("0.5 0.55 0.6 rg\n");
                    sb.Append($"BT\n/F1 8 Tf\n480 735 Td\n(Página {numPagina} de {totalPaginas}) Tj\nET\n");

                    sb.Append("0.85 0.90 0.95 RG\n0.8 w\n");
                    sb.Append("36 720 m 576 720 l S\n");

                    y = 692f;
                }

                // === DIBUJAR CABECERA DE LA TABLA ===
                sb.Append("0.03 0.34 0.55 rg\n"); // Azul marino oscuro
                sb.Append($"36 {y} {ANCHO_TABLA} {ALTO_CABECERA_TABLA} re f\n");

                sb.Append("1 1 1 rg\n"); // Texto blanco en negrita
                foreach (var c in columnas)
                {
                    float txtX = c.PosicionX + 4f;
                    string tituloCol = EscaparTexto(RecortarAlAncho(c.Titulo, c.Ancho - 6f, 8.5f, true));
                    sb.Append($"BT\n/F2 8.5 Tf\n{txtX.ToString("0.0", CultureInfo.InvariantCulture)} {(y + 6.5f).ToString("0.0", CultureInfo.InvariantCulture)} Td\n({tituloCol}) Tj\nET\n");
                }

                // Borde inferior de la cabecera
                sb.Append("0.7 0.8 0.9 RG\n1 w\n");
                sb.Append($"36 {y} m 576 {y} l S\n");

                // Borde superior de la primera fila (justo debajo de la cabecera)
                float tope = y - 2f;

                // === DIBUJAR FILAS DE DATOS (alto variable según las líneas de texto) ===
                bool esFilaPar = false;

                if (filasEstaPagina.Count == 0 && esPrimeraPagina)
                {
                    sb.Append("0.5 0.55 0.6 rg\n");
                    sb.Append("BT\n/F1 9.5 Tf\n210 560 Td\n(No se encontraron registros para mostrar en este informe) Tj\nET\n");
                }
                else
                {
                    foreach (var row in filasEstaPagina)
                    {
                        float alto = altoPorFila[row];
                        float baseFila = tope - alto;

                        // Fondo alternado para lectura cómoda
                        if (esFilaPar)
                        {
                            sb.Append("0.97 0.985 1.0 rg\n"); // Tinte azul hielo muy suave
                            sb.Append($"36 {F(baseFila)} {ANCHO_TABLA} {F(alto)} re f\n");
                        }
                        esFilaPar = !esFilaPar;

                        // Contenido de cada celda (una o varias líneas)
                        sb.Append("0.15 0.2 0.25 rg\n");
                        List<string>[] celdas = lineasPorFila[row];
                        for (int i = 0; i < columnas.Count; i++)
                        {
                            float celdaX = columnas[i].PosicionX + 4f;
                            float lineaY = tope - 11.5f;
                            foreach (string linea in celdas[i])
                            {
                                sb.Append($"BT\n/F1 {F(TAM_FUENTE_CELDA)} Tf\n{F(celdaX)} {F(lineaY)} Td\n({EscaparTexto(linea)}) Tj\nET\n");
                                lineaY -= ALTO_LINEA;
                            }
                        }

                        // Línea tenue entre filas
                        sb.Append("0.90 0.93 0.96 RG\n0.5 w\n");
                        sb.Append($"36 {F(baseFila)} m 576 {F(baseFila)} l S\n");

                        tope = baseFila;
                    }

                    // Marco perimetral exterior de la tabla
                    sb.Append("0.8 0.86 0.92 RG\n0.8 w\n");
                    sb.Append($"36 {F(tope)} m 36 756 l S\n");
                }

                // === PIE DE PÁGINA PROFESIONAL ===
                sb.Append("0.85 0.88 0.92 RG\n0.8 w\n");
                sb.Append("36 40 m 576 40 l S\n");

                sb.Append("0.5 0.55 0.6 rg\n");
                sb.Append("BT\n/F1 8 Tf\n36 26 Td\n(Toothly  •  Sistema de Gestión Odontológica  •  Documento Oficial) Tj\nET\n");
                sb.Append($"BT\n/F2 8 Tf\n505 26 Td\n(Página {numPagina} de {totalPaginas}) Tj\nET\n");

                paginasContentBytes.Add(encPdf.GetBytes(sb.ToString()));
            }

            // 8. Ensamblar la estructura estándar de archivo PDF 1.4
            using (MemoryStream ms = new MemoryStream())
            {
                List<long> offsets = new List<long>();
                Action<string> writeStr = (s) =>
                {
                    byte[] b = Encoding.ASCII.GetBytes(s);
                    ms.Write(b, 0, b.Length);
                };

                writeStr("%PDF-1.4\n");

                // Identificadores de objetos base
                int objCatalog = 1;
                int objPages = 2;
                int objFontRegular = 3;
                int objFontBold = 4;
                int objImageLogo = 5;

                // Cada página tendrá 2 objetos:
                // Page i: objFirstPage + (i * 2)
                // Content i: objFirstPage + (i * 2) + 1
                int objFirstPage = 6;

                // Obj 1: Catalog
                offsets.Add(ms.Position);
                writeStr($"1 0 obj\n<< /Type /Catalog /Pages {objPages} 0 R >>\nendobj\n");

                // Obj 2: Pages (árbol con todas las páginas hijas)
                offsets.Add(ms.Position);
                StringBuilder kidsList = new StringBuilder();
                for (int i = 0; i < totalPaginas; i++)
                {
                    kidsList.Append($"{objFirstPage + (i * 2)} 0 R ");
                }
                writeStr($"2 0 obj\n<< /Type /Pages /Kids [{kidsList.ToString().Trim()}] /Count {totalPaginas} >>\nendobj\n");

                // Obj 3: Font Regular con codificación Windows-1252 (soporte nativo de tildes y ñ)
                offsets.Add(ms.Position);
                writeStr("3 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>\nendobj\n");

                // Obj 4: Font Bold con codificación Windows-1252
                offsets.Add(ms.Position);
                writeStr("4 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold /Encoding /WinAnsiEncoding >>\nendobj\n");

                // Obj 5: Image XObject (Logotipo institucional embebido como JPEG DCTDecode)
                offsets.Add(ms.Position);
                writeStr($"5 0 obj\n<< /Type /XObject /Subtype /Image /Width {logoImgW} /Height {logoImgH} /ColorSpace /DeviceRGB /BitsPerComponent 8 /Filter /DCTDecode /Length {logoJpegBytes.Length} >>\nstream\n");
                ms.Write(logoJpegBytes, 0, logoJpegBytes.Length);
                writeStr("\nendstream\nendobj\n");

                // Objetos por cada página (Page y Content Stream)
                for (int i = 0; i < totalPaginas; i++)
                {
                    int pageObjId = objFirstPage + (i * 2);
                    int contentObjId = pageObjId + 1;
                    byte[] cBytes = paginasContentBytes[i];

                    // Page Object
                    offsets.Add(ms.Position);
                    writeStr($"{pageObjId} 0 obj\n<< /Type /Page /Parent {objPages} 0 R /MediaBox [0 0 {ANCHO_PAGINA} {ALTO_PAGINA}] /Resources << /Font << /F1 {objFontRegular} 0 R /F2 {objFontBold} 0 R >> /XObject << /ImLogo {objImageLogo} 0 R >> >> /Contents {contentObjId} 0 R >>\nendobj\n");

                    // Content Stream Object
                    offsets.Add(ms.Position);
                    writeStr($"{contentObjId} 0 obj\n<< /Length {cBytes.Length} >>\nstream\n");
                    ms.Write(cBytes, 0, cBytes.Length);
                    writeStr("\nendstream\nendobj\n");
                }

                // Tabla de referencias cruzadas (XRef Table)
                long startXref = ms.Position;
                int totalObjetos = offsets.Count;
                writeStr($"xref\n0 {totalObjetos + 1}\n");
                writeStr("0000000000 65535 f \n");
                foreach (long off in offsets)
                {
                    writeStr($"{off.ToString("D10", CultureInfo.InvariantCulture)} 00000 n \n");
                }

                // Trailer estándar del PDF
                writeStr($"trailer\n<< /Size {totalObjetos + 1} /Root {objCatalog} 0 R >>\n");
                writeStr($"startxref\n{startXref}\n%%EOF\n");

                return ms.ToArray();
            }
        }

        // Calcula anchos proporcionales para cada columna según el tipo de datos y longitud del texto
        private static List<ColumnaInfo> CalcularColumnas(List<DataGridViewColumn> cols, DataGridView dgv)
        {
            List<ColumnaInfo> resultado = new List<ColumnaInfo>();

            // Caso especial: Reporte de Historia Clínica con 2 columnas ("Campo" y "Detalle Clínico")
            if (cols.Count == 2 && cols.Any(c => c.HeaderText.ToLower().Contains("campo")))
            {
                ColumnaInfo c0 = new ColumnaInfo
                {
                    Columna = cols[0],
                    Titulo = cols[0].HeaderText,
                    Ancho = 135f,
                    PosicionX = MARGEN_IZQ,
                    MaxCaracteres = 28
                };
                ColumnaInfo c1 = new ColumnaInfo
                {
                    Columna = cols[1],
                    Titulo = cols[1].HeaderText,
                    Ancho = ANCHO_TABLA - 135f, // 405 puntos disponibles para el detalle clínico
                    PosicionX = MARGEN_IZQ + 135f,
                    MaxCaracteres = 85
                };
                resultado.Add(c0);
                resultado.Add(c1);
                return resultado;
            }

            // Asignar peso base según el tipo de campo
            List<float> pesos = new List<float>();
            foreach (var col in cols)
            {
                string header = col.HeaderText.ToLower();
                float peso = 2.0f; // peso normal

                if (header == "id" || header == "código" || header == "codigo" || header == "no" || header == "num")
                {
                    peso = 0.9f;
                }
                else if (header.Contains("fecha") || header.Contains("inicio") || header.Contains("fin") || header.Contains("vencimiento") || header.Contains("apertura"))
                {
                    peso = 1.6f;
                }
                else if (header.Contains("hora"))
                {
                    peso = 1.1f;
                }
                else if (header == "estado" || header == "sexo")
                {
                    peso = 1.2f;
                }
                else if (header.Contains("dui") || header.Contains("teléfono") || header.Contains("telefono"))
                {
                    peso = 1.6f;
                }
                else if (header.Contains("precio") || header.Contains("costo") || header.Contains("total") || header.Contains("saldo") || header.Contains("stock"))
                {
                    peso = 1.5f;
                }
                else if (header.Contains("medicamentos") || header.Contains("concepto") || header.Contains("producto"))
                {
                    peso = 4.0f;
                }
                else if (header.Contains("correo"))
                {
                    peso = 3.0f;
                }
                else if (header.Contains("paciente") || header.Contains("odontólogo") || header.Contains("odontologo") || header.Contains("proveedor") || header.Contains("usuario") || header.Contains("medicamento"))
                {
                    peso = 2.7f;
                }
                else if (header.Contains("descripción") || header.Contains("descripcion") || header.Contains("motivo") || header.Contains("diagnóstico") || header.Contains("diagnostico") || header.Contains("antecedentes") || header.Contains("observaciones") || header.Contains("indicaciones") || header.Contains("dirección") || header.Contains("direccion"))
                {
                    peso = 3.8f;
                }

                pesos.Add(peso);
            }

            float sumaPesos = pesos.Sum();
            float posXActual = MARGEN_IZQ;

            for (int i = 0; i < cols.Count; i++)
            {
                float anchoCalculado = (pesos[i] / sumaPesos) * ANCHO_TABLA;
                if (i == cols.Count - 1)
                {
                    // La última columna absorbe cualquier residuo de redondeo
                    anchoCalculado = (MARGEN_IZQ + ANCHO_TABLA) - posXActual;
                }

                int maxChars = Math.Max(8, (int)Math.Floor(anchoCalculado / 4.4f));

                ColumnaInfo info = new ColumnaInfo
                {
                    Columna = cols[i],
                    Titulo = cols[i].HeaderText,
                    Ancho = anchoCalculado,
                    PosicionX = posXActual,
                    MaxCaracteres = maxChars
                };

                resultado.Add(info);
                posXActual += anchoCalculado;
            }

            return resultado;
        }

        // Formatea números con punto decimal (requerido por la sintaxis PDF)
        private static string F(float v)
        {
            return v.ToString("0.0", CultureInfo.InvariantCulture);
        }

        // Anchos de Helvetica (unidades por 1000 del tamaño de fuente) para medir el texto
        private static float AnchoCaracter(char ch, bool negrita)
        {
            float w;
            char c = QuitarAcento(ch);
            if (c == ' ') w = 278;
            else if (c >= '0' && c <= '9') w = 556;
            else if ("ijl".IndexOf(c) >= 0) w = 222;
            else if ("ftI!.,:;/|'[]".IndexOf(c) >= 0) w = 278;
            else if ("r()-\"".IndexOf(c) >= 0) w = 333;
            else if (c == 'm' || c == 'M') w = 833;
            else if (c == 'w') w = 722;
            else if (c == 'W') w = 944;
            else if ("ckszvxyJ".IndexOf(c) >= 0) w = 500;
            else if ("CDHNRUw".IndexOf(c) >= 0) w = 722;
            else if ("GOQ".IndexOf(c) >= 0) w = 778;
            else if ("FTZ".IndexOf(c) >= 0) w = 611;
            else if (c == '%') w = 889;
            else if (c >= 'A' && c <= 'Z') w = 667;
            else w = 556;
            return negrita ? w * 1.06f : w;
        }

        private static char QuitarAcento(char c)
        {
            const string con = "áéíóúÁÉÍÓÚñÑüÜ";
            const string sin = "aeiouAEIOUnNuU";
            int i = con.IndexOf(c);
            return i >= 0 ? sin[i] : c;
        }

        private static float AnchoTexto(string s, float tam, bool negrita = false)
        {
            float total = 0;
            foreach (char c in s) total += AnchoCaracter(c, negrita);
            return total * tam / 1000f;
        }

        // Divide el texto en líneas que caben en el ancho indicado (respeta saltos de línea y palabras)
        private static List<string> EnvolverTexto(string texto, float ancho, float tam, int maxLineas)
        {
            List<string> lineas = new List<string>();
            if (string.IsNullOrEmpty(texto))
            {
                lineas.Add("");
                return lineas;
            }

            foreach (string parrafo in texto.Split('\n'))
            {
                string actual = "";
                foreach (string palabraOriginal in parrafo.Split(' '))
                {
                    string palabra = palabraOriginal;
                    string prueba = actual.Length == 0 ? palabra : actual + " " + palabra;
                    if (AnchoTexto(prueba, tam) <= ancho)
                    {
                        actual = prueba;
                        continue;
                    }

                    if (actual.Length > 0)
                    {
                        lineas.Add(actual);
                        actual = "";
                    }

                    // Palabra más larga que la columna: se parte en trozos
                    while (AnchoTexto(palabra, tam) > ancho && palabra.Length > 1)
                    {
                        int n = palabra.Length - 1;
                        while (n > 1 && AnchoTexto(palabra.Substring(0, n), tam) > ancho) n--;
                        lineas.Add(palabra.Substring(0, n));
                        palabra = palabra.Substring(n);
                    }
                    actual = palabra;
                }
                lineas.Add(actual);
            }

            if (lineas.Count > maxLineas)
            {
                lineas = lineas.GetRange(0, maxLineas);
                lineas[maxLineas - 1] = RecortarAlAncho(lineas[maxLineas - 1] + " ...", ancho, tam, false);
            }
            return lineas;
        }

        // Recorta un texto de una sola línea para que no invada la columna vecina
        private static string RecortarAlAncho(string s, float ancho, float tam, bool negrita)
        {
            if (string.IsNullOrEmpty(s) || AnchoTexto(s, tam, negrita) <= ancho) return s ?? "";
            while (s.Length > 1 && AnchoTexto(s + "..", tam, negrita) > ancho) s = s.Substring(0, s.Length - 1);
            return s + "..";
        }

        // Escapa caracteres especiales requeridos por la sintaxis de texto PDF literal: ( ) \
        private static string EscaparTexto(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Replace("\\", "\\\\")
                    .Replace("(", "\\(")
                    .Replace(")", "\\)");
        }

        // Trunca cadenas que excedan el ancho asignado a la columna
        private static string Truncar(string s, int max)
        {
            if (string.IsNullOrEmpty(s)) return "";
            if (s.Length <= max) return s;
            return s.Substring(0, Math.Max(0, max - 2)) + "..";
        }

        // Limpia caracteres no permitidos en nombres de archivo
        private static string SanitizarNombreArchivo(string s)
        {
            if (string.IsNullOrEmpty(s)) return "Reporte";
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                s = s.Replace(c, '_');
            }
            return s.Replace(" ", "_").Trim('_');
        }
    }
}
