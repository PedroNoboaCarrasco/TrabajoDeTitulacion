using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOP_GAD.Report
{
    public class ITextEvents : PdfPageEventHelper
    {
        public static iTextSharp.text.Image logo = null;
        public static iTextSharp.text.Image lineas = null;
        public static iTextSharp.text.Image piepag = null;

        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);
            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font baseFontNormal2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 14f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            iTextSharp.text.Font baseFontBig2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 14f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            
            Phrase p1Header = new Phrase("GOBIERNO AUTÓNOMO DESENTRALIZADO", baseFontBig2);

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);

            //We will have to create separate cells to include image logo and 2 separate strings
            
            
            PdfPCell pdfCell0 = new PdfPCell(new Phrase(" ", baseFontNormal));

            //Row 1
            PdfPCell pdfCell1 = new PdfPCell();
            PdfPCell pdfCell2 = new PdfPCell(p1Header);
            PdfPCell pdfCell3 = new PdfPCell();
            String text = "DIRECCIÓN: Calle Arahualpa s/n junto al Parque Central";
            String text1 = "E-MAIL: juntapacubijies2010@yahoo.com";
            String text2 = "WEB: gadprcubijies.blogspot.com";
            String text3 = "TELÉfono: (03)2323072";
            //Add paging to header
            {
                //Para colocar el logo 
                logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Assets/IMG/LogoCubijies.jpg"));
                logo.ScalePercent(60f, 35f);
                logo.SetAbsolutePosition(50f, 745f);
                cb.AddImage(logo);
                lineas = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Assets/IMG/Linea.jpg"));
                lineas.ScalePercent(40f);
                lineas.SetAbsolutePosition(50f, 730f);
                cb.AddImage(lineas);
                
                cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) /*+ len*/, document.PageSize.GetTop(45));
            }

            //Add paging to footer
            {
                piepag = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Assets/IMG/PieDePag.png"));
                piepag.ScalePercent(60f);
                piepag.SetAbsolutePosition(0f, 0f);
                cb.AddImage(piepag);
                //---------------
                cb.BeginText();
                cb.SetFontAndSize(bf, 9);
                cb.SetTextMatrix(document.PageSize.GetRight(287), document.PageSize.GetBottom(65));
                cb.ShowText(text);
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(bf, 9);
                cb.SetTextMatrix(document.PageSize.GetRight(226), document.PageSize.GetBottom(55));
                cb.ShowText(text1);
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(bf, 9);
                cb.SetTextMatrix(document.PageSize.GetRight(193), document.PageSize.GetBottom(45));
                cb.ShowText(text2);
                cb.EndText();

                cb.BeginText();
                cb.SetFontAndSize(bf, 9);
                cb.SetTextMatrix(document.PageSize.GetRight(154), document.PageSize.GetBottom(35));
                cb.ShowText(text3);
                cb.EndText();

                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) /*+ len*/, document.PageSize.GetBottom(30));
            }

            //Row 2
            PdfPCell pdfCell4_1 = new PdfPCell(new Phrase("", baseFontNormal));
            PdfPCell pdfCell4_2 = new PdfPCell(new Phrase("PARROQUIAL RURAL DE CUBIJÍES", baseFontBig2));
            PdfPCell pdfCell4_3 = new PdfPCell(new Phrase("No Usado", baseFontNormal));
            //Row 3 
            PdfPCell pdfCell5 = new PdfPCell();
            PdfPCell pdfCell6 = new PdfPCell(new Phrase("RIOBAMBA - CHIMBORAZO - ECUADOR", baseFontNormal2));
            PdfPCell pdfCell7 = new PdfPCell(new Phrase("TIME:" + string.Format("{0:t}", DateTime.Now), baseFontBig));

            //set the alignment of all three cells and set border to 0
            pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell4_1.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell4_2.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell4_3.HorizontalAlignment = Element.ALIGN_CENTER;

            pdfCell5.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell6.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell7.HorizontalAlignment = Element.ALIGN_CENTER;

            pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;
            pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell4_1.VerticalAlignment = Element.ALIGN_TOP;
            pdfCell4_2.VerticalAlignment = Element.ALIGN_TOP;
            pdfCell4_3.VerticalAlignment = Element.ALIGN_TOP;
            pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;

            //-------------------------------------------------------------
            pdfCell0.Colspan = 3;
            pdfCell2.Colspan = 2;
            pdfCell4_2.Colspan = 2;
            pdfCell6.Colspan = 2;

            pdfCell0.Border = 0;
            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4_1.Border = 0;
            pdfCell4_2.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 0;
            pdfCell7.Border = 0;

            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell0);
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell4_1);
            pdfTab.AddCell(pdfCell4_2);
            pdfTab.AddCell(pdfCell5);
            pdfTab.AddCell(pdfCell6);

            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            pdfTab.WidthPercentage = 70;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;    

            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
            //set pdfContent value

            //Move the pointer and draw line to separate header section from rest of page
            cb.MoveTo(40, document.PageSize.Height - 100);
            //cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
            cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, document.PageSize.GetBottom(50));
            //cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
            cb.Stroke();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.EndText();
        }

        //Fin e clase
    }
}