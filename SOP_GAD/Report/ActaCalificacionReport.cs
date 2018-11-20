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
    public class ActaCalificacionReport
    {
        // GET: ActaCalificacionReport
        #region Declaration
        Document _document;
        Font _fontStyle;
        MemoryStream _memoryStream = new MemoryStream();
        public static iTextSharp.text.Image logo = null;
        public static iTextSharp.text.Image lineas = null;
        public static iTextSharp.text.Image piepag = null;
        private Usuario Usu = new Usuario();
        private CertificacionPresupuestaria CertificadoP = new CertificacionPresupuestaria();
        Usuario Presidente;
        Usuario user;
        CertificacionPresupuestaria DocumentoCP;
        private ObrasPublicas ObjetoObraPublica1 = new ObrasPublicas();
        ObrasPublicas objetoObraPublica2;
        private Articulos ArticuloDoc1 = new Articulos();
        #endregion
        Articulos ArticuloDoc2; public byte[] PrepareReport(ActaCalificacionOfertas actaCalifi)
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
            this.ReportContenido(actaCalifi);
            //this.ReportPiePag();
            //--------------------------------------------
            //Cerrar el doumento
            _document.Close();
            return _memoryStream.ToArray();

        }


        private void ReportContenido(ActaCalificacionOfertas actaCalifi)
        {


            #region
            Font FontNegrita = new Font(FontFamily.TIMES_ROMAN, 12, Font.BOLD);//negrita
            Font FontContent = new Font(FontFamily.TIMES_ROMAN, 12);//contenido
            Font FontNegrita2 = new Font(FontFamily.TIMES_ROMAN, 11, Font.BOLD);//negrita
            Font FontContent2 = new Font(FontFamily.TIMES_ROMAN, 11);//contenido
            Presidente = Usu.ObtenerUsuario("Presidente GAD");
            objetoObraPublica2 = ObjetoObraPublica1.ObtenerObraPublica(actaCalifi.IdObraPublica);
            String[] FormatsMes = { "Enero", "Febrero", "Marzo", "Ablril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            String[] FormatsMes2 = { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };
            int numero = 1;
            Ofertantes ofertante1 = new Ofertantes();
            List<Ofertantes> ofertante2;
            Ofertas ofer1 = new Ofertas();
            List<Ofertas> ofer2;
            #endregion

            Paragraph p = new Paragraph("ACTA DE CALIFICACIÓN DE OFERTAS", FontNegrita);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph();
            Chunk texto1 = new Chunk("CODIGO DEL PROCESO: ", FontContent);
            Chunk texto2 = new Chunk(objetoObraPublica2.CodigoProcesoObra, FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();

            p = new Paragraph();
            texto2 = new Chunk(objetoObraPublica2.ObjetoProcesoObra, FontNegrita);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_CENTER;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);
            
            //TrimEnd(' ')--------------------------
            string aaa = objetoObraPublica2.CodigoProcesoObra;
            string bbb = aaa.Remove(aaa.Length - 2);
            this.ReportSaltoLinea();

            p = new Paragraph();
            texto1 = new Chunk("En la oficina del Gobierno Autónomo Descentralizado Parroquial Rural de Cubijies, siendo las " +
                +actaCalifi.HoraActaCalificacion + " del " + actaCalifi.FechaActaCalificacion.Day + " de " + FormatsMes[actaCalifi.FechaActaCalificacion.Month - 1] + " del " + actaCalifi.FechaActaCalificacion.Year +
                ", los responsables del proceso procedemos a realizar la presente acta de calificación de ofertas presentadas en el proceso de Menor Cuantia Obras " +
                bbb, FontContent);
            texto2 = new Chunk(", para la " + objetoObraPublica2.ObjetoProcesoObra + " con el siguiente detalle:", FontContent);
            p.Add(texto1);
            p.Add(texto2);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            ofertante2 = ofertante1.Listar(actaCalifi.IdActaCalificacion);
            int califica = 0;
            string letracalifico = "";
            PdfPCell cellfinal;
            //foreach (Ofertantes obj2 in ofertante2)
            //ofertante2.Count()
            for (int i=0;i< ofertante2.Count(); i++)
            {
                if (ofertante2[i].CodigoOfertantes < 10)
                {
                    p = new Paragraph();
                    texto2 = new Chunk("Ofertante N.-0" + ofertante2[i].CodigoOfertantes+": "+ ofertante2[i].NombreOfertante, FontNegrita);
                    p.Add(texto2);
                    p.Alignment = Element.ALIGN_JUSTIFIED;
                    p.SetLeading(0f, 1.2f);
                    _document.Add(p);
                }
                else
                {
                    p = new Paragraph();
                    texto2 = new Chunk("Ofertante N.-" + ofertante2[i].CodigoOfertantes + ": " + ofertante2[i].NombreOfertante, FontNegrita);
                    p.Add(texto2);
                    p.Alignment = Element.ALIGN_JUSTIFIED;
                    p.SetLeading(0f, 1.2f);
                    _document.Add(p);
                }
                this.ReportSaltoLinea();
                PdfPTable Tabla = new PdfPTable(4);
                //---------------------------------------------
                Tabla.WidthPercentage = 100;
                float[] widhs = new float[] { 0.8f, 8f, 3f, 12f };
                Tabla.SetWidths(widhs);
                PdfPCell cell = new PdfPCell(new Paragraph("N.", FontNegrita2));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla.AddCell(cell);
                cell = new PdfPCell(new Paragraph("PARAMETRO", FontNegrita2));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla.AddCell(cell);
                cell = new PdfPCell(new Paragraph("Cumple", FontNegrita2));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla.AddCell(cell);
                cell = new PdfPCell(new Paragraph("Fundamento", FontNegrita2));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                Tabla.AddCell(cell);

                //+++++++++++++++
                ofer2 = ofer1.Listar(ofertante2[i].IdOfertantes, actaCalifi.IdActaCalificacion);
                //+++++++++++++++

                foreach (Ofertas Obj in ofer2)
                {
                    califica = califica + Obj.Cumple;

                    cell = new PdfPCell(new Paragraph(numero.ToString(), FontContent2));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    Tabla.AddCell(cell);
                    cell = new PdfPCell(new Paragraph(Obj.Parametro, FontContent2));
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    Tabla.AddCell(cell);
                    if (Obj.Cumple == 0)
                    {
                        cell = new PdfPCell(new Paragraph("No Cumple", FontContent2));
                    }
                    else
                    {
                        cell = new PdfPCell(new Paragraph("Si Cumple", FontContent2));
                    }
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    Tabla.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(Obj.Fundamento, FontContent2));
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    Tabla.AddCell(cell);
                    numero++;
                    if (califica==8) {
                        letracalifico = ofertante2[i].NombreOfertante;
                    }
                }
                cell = new PdfPCell(new Paragraph(" ", FontContent2));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 4;
                cell.BorderColor = new BaseColor(255,255,255);
                Tabla.AddCell(cell);

                if (califica == 8)
                {
                    cellfinal = new PdfPCell(new Paragraph("SI CALIFICA ", FontNegrita2));
                }
                else {
                    cellfinal = new PdfPCell(new Paragraph("NO CALIFICA ", FontNegrita2));
                }
                cellfinal.HorizontalAlignment = Element.ALIGN_CENTER;
                cellfinal.Colspan = 4;
                cellfinal.BackgroundColor = new BaseColor(213, 204, 202);
                cellfinal.BorderColor = new BaseColor(213, 204, 202);
                Tabla.AddCell(cellfinal);

                _document.Add(Tabla);
                this.ReportSaltoLinea();
                numero = 1;
                califica = 0;
            }
            //-----------------------------------------------------------
            p = new Paragraph("En base a estos resultados el oferente: "+ letracalifico + ", CUMPLE con los requerimientos técnicos y legales establecidos en el pliego, por lo que puede pasar a la siquiente etapa del proceso.", FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 1.2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            p = new Paragraph("Para constancia de lo actuado firman:", FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 2f);
            _document.Add(p);

            this.ReportSaltoLinea();
            user = Usu.ObtenerUsuario("Secretaria Administrativa");
            p = new Paragraph("Sr. " + Presidente.NombreUsuario + " " + Presidente.ApellidoUsuario + "                            Ing. " + user.NombreUsuario + " " + user.ApellidoUsuario, FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 6f);
            _document.Add(p);
            p = new Paragraph("PRESIDENTE GADPRC                                SECRETARIA-TESORERA GADPRC", FontNegrita);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            _document.Add(p);

            this.ReportSaltoLinea();
            p = new Paragraph("Ing. " + letracalifico, FontContent);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            p.SetLeading(0f, 6f);
            _document.Add(p);
            p = new Paragraph("TÉCNICO A FIN A LA CONTRATACIÓN", FontNegrita);
            p.Alignment = Element.ALIGN_JUSTIFIED;
            _document.Add(p);

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

        //Fin de clase
    }
}