using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DPS.Encryption
{
    public static class ExcelWorker
    {
        public static MemoryStream ConvertDataTableToExcelInMemory(DataTable dt, List<string> columnNames, string generatedByName, string reportTitle)
        {
            // Enable ExcelPackage license context (EPPlus requirement)
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Create a new memory stream
            var memoryStream = new MemoryStream();

            using (var package = new ExcelPackage())
            {
                // Add a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                int columnCount = columnNames.Count;
                int dataStartRow = 4;

                // Add the report title, centered and spanning the width of the data columns
                worksheet.Cells["A1"].Value = reportTitle;
                worksheet.Cells[1, 1, 1, columnCount].Merge = true;  // Merge cells from A1 across the number of columns
                worksheet.Cells["A1"].Style.Font.Size = 14;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Set custom column headers starting at row 3 with a gray background
                for (int i = 0; i < columnCount; i++)
                {
                    worksheet.Cells[dataStartRow, i + 1].Value = columnNames[i];
                    worksheet.Cells[dataStartRow, i + 1].Style.Font.Bold = true;
                    worksheet.Cells[dataStartRow, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[dataStartRow, i + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); // Add borders

                    // Apply a gray background color to the header
                    worksheet.Cells[dataStartRow, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[dataStartRow, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                // Populate the worksheet with data from the DataTable starting at row 5
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cells[i + dataStartRow + 1, j + 1].Value = dt.Rows[i][j]; // Data starts at row 5
                        worksheet.Cells[i + dataStartRow + 1, j + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); // Add borders to each cell
                    }
                }

                // Calculate the last row based on the DataTable row count
                int lastDataRow = dataStartRow + dt.Rows.Count;

                // Insert export date and generated-by information at the last row + 2
                string exportDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                worksheet.Cells[lastDataRow + 2, 1].Value = $"Exported on: {exportDate} by {generatedByName}";
                worksheet.Cells[lastDataRow + 2, 1, lastDataRow + 2, columnCount].Merge = true;  // Merge cells across the column count
                worksheet.Cells[lastDataRow + 2, 1].Style.Font.Bold = true;
                worksheet.Cells[lastDataRow + 2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Auto-fit columns for better readability
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Save the package into the memory stream
                package.SaveAs(memoryStream);
            }

            // Reset the position of the stream to the beginning
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}