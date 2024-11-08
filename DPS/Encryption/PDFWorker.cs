using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DPS.Encryption
{
    public static class PDFWorker
    {
        public static MemoryStream ConvertDataTableToPdfInMemory(DataTable dt, List<string> columnNames, List<string> selectedColumns, string generatedByName, string reportTitle)
        {
            var memoryStream = new MemoryStream();
            var document = new Document(PageSize.A4, 20f, 20f, 20f, 40f);
            var pdfWriter = PdfWriter.GetInstance(document, memoryStream);
            pdfWriter.CloseStream = false;

            // Set the page events
            var pageEvents = new PdfPageEvents(generatedByName);
            pdfWriter.PageEvent = pageEvents;

            document.Open();

            // Add report title
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
            var titleParagraph = new Paragraph(reportTitle, titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20f
            };
            document.Add(titleParagraph);

            // Add metadata (exported by and date)
            string exportDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var metadataFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.ITALIC);
            var metadataParagraph = new Paragraph($"Exported on: {exportDate} by {generatedByName}", metadataFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20f
            };
            document.Add(metadataParagraph);

            // Create a table with the number of columns equal to columnNames.Count
            PdfPTable pdfTable = new PdfPTable(columnNames.Count)
            {
                WidthPercentage = 100
            };

            // Add header cells with gray background
            var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            foreach (var columnName in columnNames)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(columnName, headerFont))
                {
                    BackgroundColor = new BaseColor(211, 211, 211), // Light gray background
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5
                };
                pdfTable.AddCell(headerCell);
            }

            // Add data rows
            var cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            foreach (DataRow row in dt.Rows)
            {
                foreach (var columnName in selectedColumns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(row[columnName]?.ToString() ?? string.Empty, cellFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    pdfTable.AddCell(cell);
                }
            }

            document.Add(pdfTable);
            document.Close();

            // Reset the position of the stream to the beginning
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}