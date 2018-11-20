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
    public class CertificadoPresupuestoReport
    {
        // GET: PDF Certificado de Certificación
        #region Declaration
        Document _document;
        MemoryStream _memoryStream = new MemoryStream();

        public static iTextSharp.text.Image logo = null;
        public static iTextSharp.text.Image lineas = null;
        public static iTextSharp.text.Image piepag = null;
        private Usuario Usu = new Usuario();
        private CertificacionPresupuestaria CertificadoP = new CertificacionPresupuestaria();
        Usuario user;
        private ObrasPublicas ObjetoObraPublica1 = new ObrasPublicas();
        ObrasPublicas objetoObraPublica2;
        #endregion

        public byte[] PrepareReport(CertificacionPresupuestaria certificadoP)
        {
            //_resolucionI = resolucionI;

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
            #endregion

            _document.Open();

            //--------------------------------------------
            //this.ReportEncabezado();
            this.ReportContenido(certificadoP);
            //this.ReportPiePag();

            //--------------------------------------------
            //Cerrar el doumento
            _document.Close();
            return _memoryStream.ToArray();
        }

        private void ReportEncabezado()
        {
            //Para colocar el logo 
            logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Assets/IMG/LogoCubijies.jpg"));
            logo.ScalePercent(55f, 35f);
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

        private void ReportContenido(CertificacionPresupuestaria certificadoP)
        {
            #region
            Font FontNegrita = new Font(FontFamily.TIMES_ROMAN, 12, Font.BOLD);//negrita
            Font FontContent = new Font(FontFamily.TIMES_ROMAN, 12);//contenido
            objetoObraPublica2 = ObjetoObraPublica1.ObtenerObraPublica(certificadoP.IdObraPublica);
            String[] FormatsMes = { "Enero", "Febrero", "Marzo", "Ablril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            #endregion

            Paragraph p = new Paragraph("CERTIFICACION PRESUPUESTARIA", FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 2.2f);
            _document.Add(p);

            p = new Paragraph("N° " + certificadoP.NumeroCertificadoP, FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 2.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph("La suscrita Secretaria-Tesorera del Gobierno Autónomo Desentralizado Parroquial Rural "
            + "de Cubijies, certifica que existe disponibilidad presupuestaria para.", FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            this.ReportSaltoLinea();

            p = new Paragraph();
            Chunk texto1 = new Chunk("PARTIDA PRESUPUESTARIA: ", FontNegrita);
            Chunk texto2 = new Chunk(certificadoP.PartidaPresupuestaria.ToString(), FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.8f);
            _document.Add(p);

            p = new Paragraph();
            texto1 = new Chunk("DESCRIPCIÓN: ", FontNegrita);
            texto2 = new Chunk(certificadoP.DescripcionCertificadoP, FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.8f);
            _document.Add(p);

            p = new Paragraph();
            texto1 = new Chunk("VALOR REFERENCIAL: ", FontNegrita);
            texto2 = new Chunk(certificadoP.ValorReferencial.ToString(), FontContent);
            Chunk texto3 = new Chunk(" (Sin IVA)", FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Add(texto3);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.8f);
            _document.Add(p);

            this.ReportSaltoLinea();
            this.ReportSaltoLinea();
            this.ReportSaltoLinea();
            p = new Paragraph("DESTINO: ", FontNegrita);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            p = new Paragraph();
            texto1 = new Chunk("Este valor está destiado para la contratación de “", FontContent);
            texto2 = new Chunk(objetoObraPublica2.ObjetoProcesoObra, FontNegrita);
            texto3 = new Chunk("”.", FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Add(texto3);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            this.ReportSaltoLinea();
            this.ReportSaltoLinea();

            p = new Paragraph("Cubijíes, " + certificadoP.FechaCertificadoP.Day +" de "+ FormatsMes[certificadoP.FechaCertificadoP.Month - 1] + " del "+certificadoP.FechaCertificadoP.Year, FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            user = Usu.ObtenerUsuario("Secretaria Administrativa");
            p = new Paragraph("Ing. " + user.NombreUsuario + " " + user.ApellidoUsuario, FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 8f);
            _document.Add(p);

            p = new Paragraph("SECRETARIA-TESORERA GADPRC", FontNegrita);
            p.Alignment = Element.ALIGN_JUSTIFIED;
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

        private void ReportSaltoLinea()
        {
            Paragraph p = new Paragraph("                                                                                            ", new Font(FontFamily.TIMES_ROMAN, 12, Font.BOLD));
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);
        }
        // Fin de class
    }
}