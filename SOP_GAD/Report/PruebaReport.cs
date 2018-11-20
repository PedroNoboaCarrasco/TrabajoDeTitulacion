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
    public class PruebaReport
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
        Articulos ArticuloDoc2; public byte[] PrepareReport()
        {
            //_resolucionI = resolucionI;

            #region
            _document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            _document.SetPageSize(PageSize.A4);
            _document.SetMargins(50f, 50f, 120f, 125f);
            _events e = new _events();
            ITextEvents ie = new ITextEvents();
            PdfWriter pw = PdfWriter.GetInstance(_document, _memoryStream);
            //pw.PageEvent = e;
            pw.PageEvent = ie;
            //--------------------------------------------
            _document.Open();
            #endregion
            //--------------------------------------------
            for (int i = 0; i < 100; i++)
            {
                _document.Add(new Phrase("TESTING\n"));
            }
            //--------------------------------------------
            //Cerrar el doumento
            _document.Close();
            return _memoryStream.ToArray();

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