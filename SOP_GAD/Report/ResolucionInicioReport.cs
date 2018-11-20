using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using SOP_GAD.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static iTextSharp.text.Font;



namespace SOP_GAD.Report
{
    public class ResolucionInicioReport
    {
        #region Declaration
        //int _totalColumn = 4;
        Document _document;
        Font _fontStyle;
        //PdfPTable _pdfTable = new PdfPTable(4);
        //PdfPCell _pdfPCell;
        MemoryStream _memoryStream = new MemoryStream();
        //List<ResolucionInicio> _resolucionI = new List<ResolucionInicio>();
        public static iTextSharp.text.Image logo = null;
        public static iTextSharp.text.Image lineas = null;
        public static iTextSharp.text.Image piepag = null;
        private Usuario Usu = new Usuario();
        private CertificacionPresupuestaria CertificadoP = new CertificacionPresupuestaria(); 
        Usuario Presidente;
        Usuario user;
        CertificacionPresupuestaria DocumentoCP;
        private ObrasPublicas ObjetoObraPublica1= new ObrasPublicas();
        ObrasPublicas objetoObraPublica2;
        private Articulos ArticuloDoc1 = new Articulos();
        Articulos ArticuloDoc2;public byte[] PrepareReport(ResolucionInicio resolucionI)
        {
            #region
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(50f, 50f, 115f, 125f);
            //PdfWriter.GetInstance(_document, _memoryStream);

            //--------------------------------------------

            ITextEvents ie = new ITextEvents();
            PdfWriter pw = PdfWriter.GetInstance(_document, _memoryStream);
            pw.PageEvent = ie;

            //--------------------------------------------

            _document.Open();
            #endregion
            
            //--------------------------------------------
            //this.ReportEncabezado();
            this.ReportContenido(resolucionI);
            //this.ReportPiePag();

            //--------------------------------------------
            //Cerrar el doumento
            _document.Close();
            return _memoryStream.ToArray();
        }
        #endregion

        

        private void ReportContenido(ResolucionInicio resolucionI)
        {
            #region
            Font FontNegrita = new Font(FontFamily.TIMES_ROMAN, 12, Font.BOLD);//negrita
            Font FontContent = new Font(FontFamily.TIMES_ROMAN, 12);//contenido
            Presidente = Usu.ObtenerUsuario("Presidente GAD");
            objetoObraPublica2 = ObjetoObraPublica1.ObtenerObraPublica(resolucionI.IdObraPublica);
            DocumentoCP = CertificadoP.ObtenerCertificacionP2(resolucionI.IdObraPublica);
            String[] FormatsMes = { "Enero", "Febrero", "Marzo", "Ablril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            #endregion

            Paragraph p = new Paragraph("RESOLUCIÓN ADMINISTRATIVA N° " + resolucionI.NumeroResolucionI, FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 1.5f);
            _document.Add(p);
            p = new Paragraph("Sr. " + Presidente.NombreUsuario + " " + Presidente.ApellidoUsuario, FontContent);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 2.5f);
            _document.Add(p);
            p = new Paragraph("PRESIDENTE DEL GOBIERNO AUTONOMO DESCENTRALIZADO PARROQUIAL", FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            _document.Add(p);
            p = new Paragraph("RURAL DE CUBIJIES", FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            _document.Add(p);
            p = new Paragraph("CONSIDERANDO:", FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 2.5f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph("Que, la Asamblea Constituyente expidió la Ley Orgánica del  Sistema Nacional dc Contratación"
            + " Pública, misma que fue publicada en el Suplemento del Registro Oﬁcial N° 395 de 4 de Agosto de 2008.", FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph("Que, el 30 do Abril de 2009, el Economista Rafael Correa Delgado, Presidente Constitucional de la " +
                              " República, expide el Reglamento General de la Ley Orgánica del Sistema Nacional de Contratación Pública.", FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph("Que, la Secretaria - Tesorera del Gobierno Autónomo Descentralizado Parroquial Rural de Cubijíes, " +
                              "mediante certiﬁcación presupuestaria N° " + DocumentoCP.NumeroCertificadoP + " de " + DocumentoCP.FechaCertificadoP.Day + " de " +
                              FormatsMes[DocumentoCP.FechaCertificadoP.Month - 1] + " del " + DocumentoCP.FechaCertificadoP.Year + ", certiﬁca el " +
                              "número de partida presupuestaria y disponibilidad de fondos para la ", FontContent);
            string Tema = objetoObraPublica2.ObjetoProcesoObra;
            Chunk texto1 = new Chunk(Tema, FontNegrita);
            p.Add(texto1);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            ArticuloDoc2 = ArticuloDoc1.ObtenerArticulo(Int32.Parse(resolucionI.Articulo));

            p = new Paragraph("Que, el artículo " + resolucionI.Articulo + " de la Ley Orgánica del Sistema Nacional de Contratación Pública, señala: “" + ArticuloDoc2.DetalleArt, FontContent);
            texto1 = new Chunk("”.", FontNegrita);
            p.Add(texto1);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph("Que, el Ing. " + resolucionI.NombreAnalista + "elaboró el estudio para la “", FontContent);
            texto1 = new Chunk(objetoObraPublica2.Descripcion, FontNegrita);
            Chunk texto2 = new Chunk("” en el mismo que consta el presupuesto actualizado y las especificaciones técnicas.", FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            ArticuloDoc2 = ArticuloDoc1.ObtenerArticulo(51);

            p = new Paragraph("Que, de conformidad al Art. 51.- " + ArticuloDoc2.DetalleArt, FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            p = new Paragraph("Que, el inciso último del Art. 51, de requerirse pliegos, estos serán " +
                              "aprobados por la máxima autoridad o el funcionario competente de la entidad contratante.", FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            ArticuloDoc2 = ArticuloDoc1.ObtenerArticulo(52);

            p = new Paragraph("Que, el Art. 52, " + ArticuloDoc2.DetalleArt, FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph("En uso de lasatribuciones que me confiere ela Constitución de la República, el COOTAD, la Ley " +
                              "Orgánica del Sistema Nacional de Contratación Pública y los Reglamentos pertinentes.", FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph("RESUELVE:", FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph();
            texto1 = new Chunk("Art.1.- ", FontNegrita);
            texto2 = new Chunk("Aprobar los pliegos del proceso de Menor Cuantia Obras ", FontContent);
            Chunk texto3 = new Chunk(resolucionI.NumeroResolucionI, FontNegrita);
            Chunk texto4 = new Chunk(", para la ", FontContent);
            Chunk texto5 = new Chunk(objetoObraPublica2.ObjetoProcesoObra, FontNegrita);
            p.Add(texto1);
            p.Add(texto2);
            p.Add(texto3);
            p.Add(texto4);
            p.Add(texto5);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph();
            texto1 = new Chunk("Art.2.- ", FontNegrita);
            texto2 = new Chunk("Disponer el inicio del proceso de Menor Cuantia Obras ", FontContent);
            texto3 = new Chunk(resolucionI.NumeroResolucionI, FontNegrita);
            texto4 = new Chunk(", para lo cual se publicará en el portal de compras públicas www.compraspublicas.gob.ec. ", FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Add(texto3);
            p.Add(texto4);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            user = Usu.ObtenerUsuario("Secretaria Administrativa");
            p = new Paragraph();
            texto1 = new Chunk("Art.3.- ", FontNegrita);
            texto2 = new Chunk("Nombrar como secretaria del proceso de Menor Cuantia Obras ", FontContent);
            texto3 = new Chunk(resolucionI.NumeroResolucionI, FontNegrita);
            texto4 = new Chunk(", a la Ing. ", FontContent);
            texto5 = new Chunk(user.NombreUsuario, FontContent);
            Chunk texto6 = new Chunk(", quien apoyará al Presidente del Gobierno Autónomo Descentralizádo Parroquial Rural de Cubijíes.", FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Add(texto3);
            p.Add(texto4);
            p.Add(texto5);
            p.Add(texto6);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            // Get the current date.
            DateTime thisDay = DateTime.Today;
            user = Usu.ObtenerUsuario("Secretaria Administrativa");
            p = new Paragraph("Dado en la parroquia de Cubijies, " + thisDay.Day + " de " + FormatsMes[thisDay.Month - 1] + " del " + thisDay.Year, FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            p = new Paragraph("Sr. " + Presidente.NombreUsuario + " " + Presidente.ApellidoUsuario, FontContent);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 8f);
            _document.Add(p);
            p = new Paragraph("PRESIDENTE DEL GOBIERNO AUTONOMO DESCENTRALIZADO PARROQUIAL", FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            _document.Add(p);
            p = new Paragraph("RURAL DE CUBIJIES", FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            _document.Add(p);

            this.ReportSaltoLinea();
            this.ReportSaltoLinea();
            this.ReportSaltoLinea();

            p = new Paragraph("Proveyó y firmó la Resoucion Asministrativa N° " + resolucionI.NumeroResolucionI +
                              " que antecede, el señor " + Presidente.NombreUsuario + " " + Presidente.ApellidoUsuario +
                              ", Presidente del Hobierno Autónomo Descentralizado Parroquial Rural de Cubijies," +
                              " en el lugar y fecha indicados.- ", FontContent);
            texto1 = new Chunk("LO CERTIFICO", FontNegrita);
            p.Add(texto1);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            p = new Paragraph("Ing. " + user.NombreUsuario + " " + user.ApellidoUsuario, FontContent);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 7f);
            _document.Add(p);
            p = new Paragraph("SECRETARIA-TESORERA GADPRC", FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            _document.Add(p);


        }

        private void ReportSaltoLinea()
        {
            Paragraph p = new Paragraph("                                                                                            ", new Font(FontFamily.TIMES_ROMAN, 12, Font.BOLD));
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);
        }
        private void ReportEncabezado()
        {
            //Para colocar el logo 
            logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Assets/IMG/LogoCubijies.jpg"));
            logo.ScalePercent(55f,35f);
            logo.SetAbsolutePosition(50f, 745f);
            _document.Add(logo);
            _document.Add(Chunk.NEWLINE);
            _document.Add(Chunk.NEWLINE);

            //Lineas debajo de logo
            lineas = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Assets/IMG/Linea.jpg"));
            lineas.ScalePercent(40f);
            //chart_imagen.ScaleAbsolute(500f, 500f);
            lineas.SetAbsolutePosition(50f, 730f);
            _document.Add(lineas);
            _document.Add(Chunk.NEWLINE);

            //Letras alado de logo
            //Chunk chunk = new Chunk("Configuración de la fuente", FontFactory.GetFont("dax-black"));
            //chunk.setLineHeight(30f);
            ////chunk.SetUnderline(0.5f, -1.5f);// Para subrayar
            //_document.Add(chunk);


            Paragraph p = new Paragraph("GOBIERNO AUTÓNOMO DESCENTRALIZADO", new Font(FontFamily.COURIER, 17, Font.BOLD));
            //p.setAlignment(Element.ALIGN_CENTER);
            //p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 1.7f);
            p.IndentationLeft = 155;

            _document.Add(p);
            p = new Paragraph("PARROQUIAL RURAL DE CUBIJÍES", new Font(FontFamily.COURIER, 17, Font.BOLD));
            //p.Alignment = Element.ALIGN_RIGHT;
            p.SetLeading(0f, 1.2f);
            p.IndentationLeft = 182;
            _document.Add(p);
            p = new Paragraph("RIOBAMBA - CHIMBORAZO - ECUADOR", new Font(FontFamily.TIMES_ROMAN, 12));
            //p.Alignment = Element.ALIGN_RIGHT;
            p.IndentationLeft = 212;
            _document.Add(p);
        }

        private void ReportPiePag()
        {
            //Para colocar el logo 
            piepag = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Assets/IMG/PieDePag.png"));
            piepag.ScalePercent(60f);
            piepag.SetAbsolutePosition(0f, 0f);
            _document.Add(piepag);
        }
        private void ReportHeader()
        {
            ////Primera parte
            //_fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            //_pdfPCell = new PdfPCell(new Phrase("Gobierno Autónomo Descentralizado", _fontStyle));
            //_pdfPCell.Colspan = _totalColumn;
            //_pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfPCell.Border = 0;
            //_pdfPCell.BackgroundColor = BaseColor.WHITE;
            //_pdfPCell.ExtraParagraphSpace = 0;
            //_pdfTable.AddCell(_pdfPCell);
            //_pdfTable.CompleteRow();

            //// Segunda parte
            //_fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
            //_pdfPCell = new PdfPCell(new Phrase("Lista de Resoluciones de Inicio", _fontStyle));
            //_pdfPCell.Colspan = _totalColumn;
            //_pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfPCell.Border = 0;
            //_pdfPCell.BackgroundColor = BaseColor.WHITE;
            //_pdfPCell.ExtraParagraphSpace = 0;
            //_pdfTable.AddCell(_pdfPCell);
            //_pdfTable.CompleteRow();
        }

        private void ReportBody()
        {
            //#region Table header
            //_fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            //_pdfPCell = new PdfPCell(new Phrase("Código de Resolucion", _fontStyle));
            //_pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //_pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            //_pdfTable.AddCell(_pdfPCell);

            //_pdfPCell = new PdfPCell(new Phrase("Articulo", _fontStyle));
            //_pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //_pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            //_pdfTable.AddCell(_pdfPCell);

            //_pdfPCell = new PdfPCell(new Phrase("Nombre del Analista", _fontStyle));
            //_pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //_pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            //_pdfTable.AddCell(_pdfPCell);

            //_pdfPCell = new PdfPCell(new Phrase("Obra Pública", _fontStyle));
            //_pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //_pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //_pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            //_pdfTable.AddCell(_pdfPCell);
            //_pdfTable.CompleteRow();
            //#endregion

            //#region Table Body
            //_fontStyle = FontFactory.GetFont("Tahoma", 8f, 0);
            //foreach (ResolucionInicio resolucion in _resolucionI)
            //{
            //    _pdfPCell = new PdfPCell(new Phrase(resolucion.NumeroResolucionI, _fontStyle));
            //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //    _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            //    _pdfTable.AddCell(_pdfPCell);

            //    _pdfPCell = new PdfPCell(new Phrase(resolucion.Articulo, _fontStyle));
            //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //    _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            //    _pdfTable.AddCell(_pdfPCell);

            //    _pdfPCell = new PdfPCell(new Phrase(resolucion.NombreAnalista, _fontStyle));
            //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //    _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            //    _pdfTable.AddCell(_pdfPCell);

            //    _pdfPCell = new PdfPCell(new Phrase(resolucion.IdObraPublica.ToString(), _fontStyle));
            //    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //    _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            //    _pdfTable.AddCell(_pdfPCell);
            //    _pdfTable.CompleteRow();
            //}

            //#endregion
        }

        //Fin de clase
    }
}