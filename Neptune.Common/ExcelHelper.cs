using System.Data;
using ClosedXML.Excel;

namespace Neptune.Common;

public class ExcelHelper
{
    public static DataTable GetDataTableFromExcel(Stream inputStream, dynamic worksheet)
    {
        var dataTable = new DataTable();
        using var workBook = new XLWorkbook(inputStream);
        IXLWorksheet workSheet = workBook.Worksheet(worksheet);

        //Loop through the Worksheet rows.
        var firstRow = true;
        foreach (var row in workSheet.Rows())
        {
            //Use the first row to add columns to DataTable.
            if (firstRow)
            {
                foreach (var cell in row.Cells())
                {
                    if (!string.IsNullOrEmpty(cell.Value.ToString()))
                    {
                        dataTable.Columns.Add(cell.Value.ToString());
                    }
                    else
                    {
                        break;
                    }
                }
                firstRow = false;
            }
            else
            {
                var i = 0;
                var toInsert = dataTable.NewRow();
                foreach (var cell in row.Cells(1, dataTable.Columns.Count))
                {
                    toInsert[i] = cell.Value.ToString();
                    i++;
                }
                dataTable.Rows.Add(toInsert);
            }
        }

        return dataTable;
    }
    public static string SetStringValue(DataRow row, int rowNumber, List<string> errorList, string fieldName,
            int fieldLength, bool requireNotEmpty)
    {
        var fieldValue = row[fieldName].ToString();
        if (!string.IsNullOrWhiteSpace(fieldValue))
        {
            if (fieldValue.Length > fieldLength)
            {
                errorList.Add($"{fieldName} is too long at row: {rowNumber}. It must be {fieldLength} characters or less. Current Length is {fieldValue.Length}.");
            }
            else
            {
                return fieldValue;
            }
        }
        else
        {
            if (requireNotEmpty)
            {
                errorList.Add($"{fieldName} is null, empty, or just whitespaces for row: {rowNumber}");
            }
        }

        return null;
    }
    public static int? GetIntFieldValue(DataRow row, int rowNumber, List<string> errorList, string fieldName, bool requireNotEmpty)
    {
        var fieldValue = row[fieldName].ToString();
        if (!string.IsNullOrWhiteSpace(fieldName))
        {
            if (!int.TryParse(fieldValue, out var fieldValueAsInt))
            {
                errorList.Add($"{fieldName} can not be converted to Int at row: {rowNumber}");
            }
            else if (fieldValueAsInt < 0)
            {
                errorList.Add($"{fieldName} cannot be less than 0 at row: {rowNumber}");
            }
            else
            {
                return fieldValueAsInt;
            }
        }

        if (requireNotEmpty)
        {
            errorList.Add($"{fieldName} is null, empty, or just whitespaces for row: {rowNumber}");
        }

        return null;
    }

    public static decimal? GetDecimalFieldValue(DataRow row, int rowNumber, List<string> errorList, string fieldName, bool requireNotEmpty)
    {
        var fieldValue = row[fieldName].ToString();
        if (!string.IsNullOrWhiteSpace(fieldValue))
        {
            if (!decimal.TryParse(fieldValue, out var fieldValueAsDecimal))
            {
                errorList.Add($"{fieldName} can not be converted to decimal at row: {rowNumber}");
            }
            else if (fieldValueAsDecimal < 0.0M)
            {
                errorList.Add($"{fieldName} cannot be less than 0 at row: {rowNumber}");
            }
            else if (fieldValueAsDecimal > 100.0M)
            {
                errorList.Add($"{fieldName} cannot be greater than 100 at row: {rowNumber}");
            }
            else
            {
                return fieldValueAsDecimal;
            }
        }
        if (requireNotEmpty)
        {
            errorList.Add($"{fieldName} is null, empty, or just whitespaces for row: {rowNumber}");
        }

        return null;
    }
}