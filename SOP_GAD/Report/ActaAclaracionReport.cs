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
    public class ActaAclaracionReport
    {
        #region Declaration
        Document _document;
        Font _fontStyle;
        MemoryStream _memoryStream = new MemoryStream();
        public static iTextSharp.text.Image logo = null;
        public static iTextSharp.text.Image lineas = null;
        public static iTextSharp.text.Image piepag = null;

        private Usuario Usu = new Usuario();
        Usuario Presidente;
        Usuario user;
        Usuario user2;
        CertificacionPresupuestaria DocumentoCP;
        private ObrasPublicas ObjetoObraPublica1 = new ObrasPublicas();
        ObrasPublicas objetoObraPublica2;
        private ResolucionInicio resolucionI1 = new ResolucionInicio();
        ResolucionInicio resolucionI2;
        private PreguntaRespuesta preguntarespuesta1 = new PreguntaRespuesta();
        List<PreguntaRespuesta> preguntarespuesta2;
        #endregion

        Articulos ArticuloDoc2; public byte[] PrepareReport(InformePreguntaR informeAclaracion)
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
            this.ReportContenido(informeAclaracion);
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

        private void ReportContenido(InformePreguntaR informeAcla) {

            #region
            Font FontNegrita = new Font(FontFamily.TIMES_ROMAN, 12, Font.BOLD);//negrita
            Font FontContent = new Font(FontFamily.TIMES_ROMAN, 12);//contenido
            Presidente = Usu.ObtenerUsuario("Presidente GAD");
            objetoObraPublica2 = ObjetoObraPublica1.ObtenerObraPublica(informeAcla.IdObraPublica);
            resolucionI2 = resolucionI1.ObtenerResolucionInicial2(informeAcla.IdObraPublica);
            String[] FormatsMes = { "Enero", "Febrero", "Marzo", "Ablril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            String[] FormatsMes2 = { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO","JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            int numero = 1;
            #endregion

            Paragraph p = new Paragraph("INFORME DE PREGUNTAS, RESPUESTAS Y/O ACLARACIONES", FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 1.5f);
            _document.Add(p);

            this.ReportSaltoLinea();
        
            p = new Paragraph();
            Chunk texto1 = new Chunk("CODIGO DEL PROCESO:       ", FontNegrita);
            Chunk texto2 = new Chunk(objetoObraPublica2.CodigoProcesoObra, FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            p = new Paragraph();
            texto1 = new Chunk("OBJETO DE CONTRATO:   ", FontNegrita);
            texto2 = new Chunk(objetoObraPublica2.ObjetoProcesoObra, FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);
            
            p = new Paragraph();
            texto1 = new Chunk("RESOLUCION DE INICIO:     ", FontNegrita);
            texto2 = new Chunk(resolucionI2.NumeroResolucionI, FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);
            
            p = new Paragraph();
            texto1 = new Chunk("LUGAR Y FECHA:                    ", FontNegrita);
            texto2 = new Chunk(informeAcla.LugarInformePR+", "+informeAcla.FechaInformePR.Day+ " DE " + FormatsMes2[informeAcla.FechaInformePR.Month - 1] + " DEL " + informeAcla.FechaInformePR.Year, FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);
            
            p = new Paragraph();
            texto1 = new Chunk("HORA:                                         ", FontNegrita);
            texto2 = new Chunk(informeAcla.Hora.ToString(), FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            //TrimEnd(' ')--------------------------
            string aaa = objetoObraPublica2.CodigoProcesoObra;
            string bbb = aaa.Remove(aaa.Length - 2);
            this.ReportSaltoLinea();

            p = new Paragraph();
            texto1 = new Chunk("En la oficina del Gobierno Autónomo Descentralizado Parroquial Rural de Cubijies, siendo las "+
                +informeAcla.Hora+" del "+informeAcla.FechaInformePR.Day+" de " + FormatsMes[informeAcla.FechaInformePR.Month - 1] + " del " +informeAcla.FechaInformePR.Year+
                ", los responsables del proceso procedemos a contestar las pregungas formuladas en el proceso de Menor Cuantia Obras "+
                bbb, FontContent);
            texto2 = new Chunk(", " + objetoObraPublica2.ObjetoProcesoObra, FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            preguntarespuesta2 = preguntarespuesta1.Listar(informeAcla.IdInformePR);

            foreach (PreguntaRespuesta Obj in preguntarespuesta2)
            {
                p = new Paragraph("Pregunta "+numero,FontNegrita);
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.SetLeading(0f, 2f);
                _document.Add(p);

                this.ReportSaltoLinea();
                p = new Paragraph();
                texto1 = new Chunk("Pregunta/Aclaración: ", FontNegrita);
                texto2 = new Chunk(Obj.Pregunta, FontContent);
                p.Add(texto1);
                p.Add(texto2);
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.SetLeading(0f, 1.2f);
                _document.Add(p);
                
                p = new Paragraph();
                texto1 = new Chunk("Respuesta/Aclaración: ", FontNegrita);
                texto2 = new Chunk(Obj.Respuesta, FontContent);
                p.Add(texto1);
                p.Add(texto2);
                p.Alignment = Element.ALIGN_JUSTIFIED;
                p.SetLeading(0f, 1.2f);
                _document.Add(p);
                //---------------------------------
                numero++;
            }

            p = new Paragraph("Para constancia de lo actuado firmamos:", FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            user = Usu.ObtenerUsuario("Secretaria Administrativa");
            p = new Paragraph("Sr. "+Presidente.NombreUsuario+" "+Presidente.ApellidoUsuario+ "                            Ing. " + user.NombreUsuario + " " + user.ApellidoUsuario, FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 6f);
            _document.Add(p);
            p = new Paragraph("PRESIDENTE GADPRC                                SECRETARIA-TESORERA GADPRC", FontNegrita);
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

        //Fin de class
    }
}