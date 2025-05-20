using System.Data;
using ClosedXML.Excel;
using Neptune.Common;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Common;

public static class SimplifiedBMPsExcelParserHelper
{
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
            quickBMPs.Add(ParseRequiredAndOptionalFieldsAndCreateSimplifiedBMPs(dbContext, row, i+2, out var errorsList,
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

        var wqmpName = ExcelHelper.SetStringValue(row, rowNumber, errorList, "WQMP Name", WaterQualityManagementPlan.FieldLengths.WaterQualityManagementPlanName, true);

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

        var bmpName = ExcelHelper.SetStringValue(row, rowNumber, errorList, "BMP Name",
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

        var countOfBMPs = ExcelHelper.GetIntFieldValue(row, rowNumber, errorList, "Count of BMPs", true);
        if (countOfBMPs.HasValue)
        {
            quickBMP.NumberOfIndividualBMPs = countOfBMPs.Value;
        }

        var percentageOfSiteTreated = ExcelHelper.GetDecimalFieldValue(row, rowNumber, errorList, "% of Site Treated", false);
        if (percentageOfSiteTreated.HasValue)
        {
            quickBMP.PercentOfSiteTreated = percentageOfSiteTreated;
        }

        var wetWeatherPercentageCapture = ExcelHelper.GetDecimalFieldValue(row, rowNumber, errorList, "Wet Weather % Capture", false);
        if (wetWeatherPercentageCapture.HasValue)
        {
            quickBMP.PercentCaptured = wetWeatherPercentageCapture;
        }

        var wetWeatherPercentageRetained = ExcelHelper.GetDecimalFieldValue(row, rowNumber, errorList, "Wet Weather % Retained", false);
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

        var notes = ExcelHelper.SetStringValue(row, rowNumber, errorList, "Notes", QuickBMP.FieldLengths.QuickBMPNote, false);
        if (notes != null)
        {
            quickBMP.QuickBMPNote = notes;
        }

        return quickBMP;
    }

    

}