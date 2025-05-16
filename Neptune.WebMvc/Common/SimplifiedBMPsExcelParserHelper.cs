using System.Data;
using ApprovalUtilities.Reflection;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Irony.Parsing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Common;

public static class SimplifiedBMPsExcelParserHelper
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

    public static List<QuickBMP> ParseWQMPRowsFromXLSX(NeptuneDbContext dbContext, List<int> stormwaterJurisdictionsPersonCanView, DataTable dataTableFromExcel,
        out List<string> errors)
    {
        errors = [];
        var requiredFields = new List<string> { "WQMP Name", "BMP Name", "BMP Type" };
        foreach (var field in requiredFields)
        {
            if (!dataTableFromExcel.Columns.Contains(field))
            {
                errors.Add($"Spreadsheet is missing required column: {field}");
            }
        }

        if (errors.Count > 0)
        {
            return null;
        }
        var numColumns = dataTableFromExcel.Columns.Count;
        var numRows = dataTableFromExcel.Rows.Count;
        foreach (DataRow row in dataTableFromExcel.Rows)
        {
            var wqmpName = row["WQMP Name"].ToString();
            var wqmp = dbContext.WaterQualityManagementPlans.SingleOrDefault(x =>
                x.WaterQualityManagementPlanName == wqmpName);
            if (wqmp == null)
            {
                errors.Add($"WQMP with name {wqmpName} does not exist.");
            }

            else if (wqmp != null && !stormwaterJurisdictionsPersonCanView.Contains(wqmp.StormwaterJurisdictionID))
            {
                errors.Add($"WQMP with name {wqmpName} is in a jurisdition you don't have access to.");
            }

            
        }

        var quickBMPs = new List<QuickBMP>();
        for (var i = 0; i < numRows; i++)
        {
            var row = dataTableFromExcel.Rows[i];
            var rowEmpty = true;
            for (var j = 0; j < numColumns; j++)
            {
                rowEmpty = string.IsNullOrWhiteSpace(row[j].ToString());
                if (!rowEmpty)
                {
                    break;
                }
            }

            if (rowEmpty)
            {
                continue;
            }
            var treatmentBMPTypes = TreatmentBMPTypes.List(dbContext);
            var quickBMPNamesInCsv = new List<string>();
            quickBMPs.Add(ParseRequiredAndOptionalFieldsAndCreateSimplifiedBMPs(dbContext, row, i, out var errorsList,
                treatmentBMPTypes, quickBMPNamesInCsv));
            errors.AddRange(errorsList);
            
        }

        if (errors.Count > 0)
        {
            return null;
        }

        return quickBMPs;
    }

    private static QuickBMP ParseRequiredAndOptionalFieldsAndCreateSimplifiedBMPs(NeptuneDbContext dbContext, DataRow row, int rowNumber, out List<string> errorList, List<TreatmentBMPType> treatmentBMPTypes, List<string> quickBMPNamesInCsv)
    {
        errorList = new List<string>();

        var wqmpName = SetStringValue(row, rowNumber, errorList, "WQMP Name", WaterQualityManagementPlan.FieldLengths.WaterQualityManagementPlanName, true);

        if (string.IsNullOrWhiteSpace(wqmpName))
        {
            // no point in going further if there is no wqmp name
            return null;
        }

        var wqmp = dbContext.WaterQualityManagementPlans.SingleOrDefault(x =>
            x.WaterQualityManagementPlanName == wqmpName);

        if (wqmp == null)
        {
            errorList.Add($"The WQMP with name '{wqmpName}' in row {rowNumber} was not found.");
            return null;
        }

        var bmpName = SetStringValue(row, rowNumber, errorList, "BMP Name",
            QuickBMP.FieldLengths.QuickBMPName, true);

        var quickBMP = dbContext.QuickBMPs.SingleOrDefault(x =>
            x.WaterQualityManagementPlanID == wqmp.WaterQualityManagementPlanID && x.QuickBMPName == bmpName) ?? new QuickBMP() {WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID};

        if (!string.IsNullOrWhiteSpace(bmpName))
        {
            if (quickBMPNamesInCsv.Contains(bmpName))
            {
                errorList.Add(
                    $"The Simplified BMP with Name '{bmpName}' was already added in this upload, duplicate name is found at row: {rowNumber}");
            }
            quickBMPNamesInCsv.Add(bmpName);
            quickBMP.QuickBMPName = bmpName;
        }

        var treatmentBMPTypeName = row["BMP Type"].ToString();
        if (!string.IsNullOrWhiteSpace(treatmentBMPTypeName))
        {
            var treatmentBMPType = treatmentBMPTypes.SingleOrDefault(x => x.TreatmentBMPTypeName == treatmentBMPTypeName);
            if (treatmentBMPType == null)
            {
                errorList.Add($"BMP Type {treatmentBMPTypeName} does not exist");
            }
            else
            {
                quickBMP.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            }
        }
        else
        {
            errorList.Add($"BMP Type in row {rowNumber} is empty or null");
        }

        //var countOfBMPs = GetIntFieldValue(row, rowNumber, errorList, "Count of BMPs", true);
        //if (countOfBMPs.HasValue)
        //{
        //    quickBMP.NumberOfIndividualBMPs = countOfBMPs.Value;
        //}

        var percentageOfSiteTreated = GetDecimalFieldValue(row, rowNumber, errorList, "% of Site Treated", false);
        if (percentageOfSiteTreated.HasValue)
        {
            quickBMP.PercentOfSiteTreated = percentageOfSiteTreated;
        }

        var wetWeatherPercentageCapture = GetDecimalFieldValue(row, rowNumber, errorList, "Wet Weather % Capture", false);
        if (wetWeatherPercentageCapture.HasValue)
        {
            quickBMP.PercentCaptured = wetWeatherPercentageCapture;
        }

        var wetWeatherPercentageRetained = GetDecimalFieldValue(row, rowNumber, errorList, "Wet Weather % Retained", false);
        if (wetWeatherPercentageRetained.HasValue)
        {
            quickBMP.PercentRetained = wetWeatherPercentageRetained;
        }

        var dryWeatherFlowOverrideName = row["Dry Weather Flow Override?"].ToString();
        if (!string.IsNullOrWhiteSpace(dryWeatherFlowOverrideName))
        {
            var dryWeatherFlowOverride = DryWeatherFlowOverride.All.SingleOrDefault(x => x.DryWeatherFlowOverrideDisplayName == dryWeatherFlowOverrideName);
            if (dryWeatherFlowOverride == null)
            {
                errorList.Add($"BMP Type {dryWeatherFlowOverrideName} does not exist");
            }
            else
            {
                quickBMP.DryWeatherFlowOverrideID = dryWeatherFlowOverride.DryWeatherFlowOverrideID;
            }
        }

        var notes = SetStringValue(row, rowNumber, errorList, "Notes", QuickBMP.FieldLengths.QuickBMPNote, false);
        if (notes != null)
        {
            quickBMP.QuickBMPNote = notes;
        }

        return quickBMP;
    }

    private static string SetStringValue(DataRow row, int rowNumber, List<string> errorList, string fieldName,
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
    private static int? GetIntFieldValue(DataRow row, int rowNumber, List<string> errorList, string fieldName, bool requireNotEmpty)
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

    private static decimal? GetDecimalFieldValue(DataRow row, int rowNumber, List<string> errorList, string fieldName, bool requireNotEmpty)
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