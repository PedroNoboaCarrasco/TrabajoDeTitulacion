using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static iTextSharp.text.Font;

namespace SOP_GAD.Report
{
    public class _events : PdfPageEventHelper
    {

        public static iTextSharp.text.Image logo = null;
        public static iTextSharp.text.Image lineas = null;
        public static iTextSharp.text.Image piepag = null;
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable table = new PdfPTable(1);
            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin; //this centers [table]
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPTable table2 = new PdfPTable(2);
            table2.DefaultCell.Border = Rectangle.NO_BORDER;
            float[] values = new float[2];
            values[0] = 45;
            values[1] = 110;
            table2.SetWidths(values);

            //Para colocar el logo 
            logo = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Assets/IMG/LogoCubijies.jpg"));
            logo.ScalePercent(55f, 35f);
            logo.SetAbsolutePosition(50f, 745f);
            PdfPCell cell2 = new PdfPCell(logo);
            cell2.Border = 0;
            table2.AddCell(cell2);

            Paragraph p = new Paragraph();
            Chunk texto1 = new Chunk("GOBIERNO AUTÓNOMO DESCENTRALIZADO \n PARROQUIAL RURAL DE CUBIJÍES \n ", new Font(FontFamily.COURIER, 17, Font.BOLD));
            Chunk texto2 = new Chunk("RIOBAMBA - CHIMBORAZO - ECUADOR \n\n", new Font(FontFamily.TIMES_ROMAN, 12));
            p.Add(texto1);
            p.Add(texto2);
            cell2 = new PdfPCell(p);
            cell2.Border = 0;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            table2.AddCell(cell2);

            //Lineas debajo de logo
            lineas = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Assets/IMG/Linea.jpg"));
            lineas.ScalePercent(40f);
            lineas.SetAbsolutePosition(50f, 730f);

            cell2 = new PdfPCell(lineas);
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Colspan = 2;
            cell2.Border = 0;
            table2.AddCell(cell2);

            p = new Paragraph("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n", new Font(FontFamily.TIMES_ROMAN, 12));
            cell2 = new PdfPCell(p);
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Colspan = 2;
            cell2.Border = 0;
            table2.AddCell(cell2);

            PdfPCell cell = new PdfPCell(table2);
            cell.Border = 0;
            table.AddCell(cell);

            piepag = Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Assets/IMG/PieDePag.png"));
            piepag.ScalePercent(50f);
            piepag.SetAbsolutePosition(0f, 0f);
            cell = new PdfPCell(piepag);
            cell.Border = 0;
            table.AddCell(cell);

            table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 36, writer.DirectContent);

        }

        //Fin class

    }
}