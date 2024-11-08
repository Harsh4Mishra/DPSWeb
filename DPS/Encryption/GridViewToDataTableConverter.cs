using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DPS.Encryption
{
    public class GridViewToDataTableConverter
    {
        public DataTable ConvertGridViewToDataTable(GridView gridView, List<string> selectedColumns)
        {
            DataTable table = new DataTable();

            // Add columns to the DataTable based on selectedColumns
            foreach (var columnName in selectedColumns)
            {
                table.Columns.Add(columnName);
            }

            // Iterate through each GridView row
            foreach (GridViewRow row in gridView.Rows)
            {
                // Create a new DataRow for each GridView row
                DataRow dataRow = table.NewRow();

                // Iterate through each column (cell) in the selectedColumns list
                for (int i = 0; i < selectedColumns.Count; i++)
                {
                    string columnName = selectedColumns[i];

                    // Get the corresponding control inside the cell and extract its value
                    object value = GetCellValue(row, columnName);

                    // Get the column type in the DataTable
                    DataColumn column = table.Columns[columnName];

                    // Handle DBNull.Value or empty string based on column type
                    if (value == null || (value is string && string.IsNullOrEmpty((string)value)))
                    {
                        // Handle DBNull for string or null values
                        if (column.DataType == typeof(string))
                        {
                            dataRow[columnName] = DBNull.Value;
                        }
                        else if (column.DataType == typeof(bool) || column.DataType == typeof(int) || column.DataType == typeof(double) || column.DataType == typeof(DateTime))
                        {
                            // If the column expects a specific type, we use the default value for that type
                            dataRow[columnName] = Activator.CreateInstance(column.DataType);  // Default value (e.g., 0 for int, false for bool, etc.)
                        }
                    }
                    else
                    {
                        // Otherwise, assign the value to the row
                        dataRow[columnName] = value;
                    }
                }

                // Add the row to the DataTable
                table.Rows.Add(dataRow);
            }

            return table;
        }

        // Helper method to extract value from GridView cell
        private object GetCellValue(GridViewRow row, string columnName)
        {
            foreach (TableCell cell in row.Cells)
            {
                // Check for the specific controls in the cell based on the column name
                foreach (Control control in cell.Controls)
                {
                    if (control is Label)
                    {
                        Label label = (Label)control;
                        if (label.ID == "lbl" + columnName)  // Matching the ID from the GridView
                        {
                            return label.Text;
                        }
                    }
                    else if (control is CheckBox)
                    {
                        CheckBox checkBox = (CheckBox)control;
                        if (checkBox.ID == "chk" + columnName)
                        {
                            return checkBox.Checked ? "True" : "False";
                        }
                    }
                    else if (control is Image)
                    {
                        Image image = (Image)control;
                        if (image.ID == "img" + columnName)
                        {
                            return image.ImageUrl;
                        }
                    }
                }
            }

            return null;
        }
    }
}