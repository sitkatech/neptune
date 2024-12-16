using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Common;

public static class SimplifiedBMPsCsvParserHelper
{
    public static List<QuickBMP> CSVUpload(NeptuneDbContext dbContext, Stream fileStream,
        out List<string> errorList)
    {
        var streamReader = new StreamReader(fileStream);
        var parser = new TextFieldParser(streamReader);
        return ParseWqmpRowsFromCsv(dbContext, parser, out errorList);
    }
    public static List<QuickBMP> ParseWqmpRowsFromCsv(NeptuneDbContext dbContext, TextFieldParser parser, out List<string> errorList)
    {
        parser.SetDelimiters(",");
        errorList = new List<string>();
        var quickBMPsToUpload = new List<QuickBMP>();
        var fieldsDict = new Dictionary<string, int>();

        var requiredFields = new List<string> { "WQMP", "BMP Name", "BMP Type", "Count of BMPs", "Dry Weather Flow Override?" };

        try
        {
            var header = parser.ReadFields();
            fieldsDict = ValidateHeader(header, requiredFields, out errorList);
            if (errorList.Any())
            {
                return null;
            }
        }
        catch
        {
            errorList.Add("Unable to read file and/or parse header");
        }

        // if the fields don't match throw an exception
        var rowCount = 1;
        var treatmentBMPTypes = TreatmentBMPTypes.List(dbContext);
        var quickBMPNamesInCsv = new List<string>();
        while (!parser.EndOfData)
        {
            var currentRow = parser.ReadFields();

            var currentQuickBMP = ParseRequiredAndOptionalFieldsAndCreateSimplifiedBMPs(dbContext, currentRow, fieldsDict, rowCount, out var currentErrorList, treatmentBMPTypes, quickBMPNamesInCsv);
            if (currentQuickBMP != null)
            {
                quickBMPsToUpload.Add(currentQuickBMP);
            }

            errorList.AddRange(currentErrorList);
            rowCount++;
        }

        return quickBMPsToUpload;
    }

    private static Dictionary<string, int> ValidateHeader(string[] row, List<string> requiredFields, out List<string> errorList)
    {
        errorList = new List<string>();
        var fieldsDict = new Dictionary<string, int>();

        for (var fieldIndex = 0; fieldIndex < row.Length; fieldIndex++)
        {
            var temp = row[fieldIndex].Trim();
            if (!string.IsNullOrWhiteSpace(temp))
            {
                fieldsDict.Add(temp, fieldIndex);
            }
        }

        var headers = fieldsDict.Keys.ToList();
        var requiredFieldDifference = requiredFields.Except(headers).ToList();

        if (requiredFieldDifference.Any())
        {
            errorList.Add("One or more required headers have not been provided. Required Fields are: " +
                          string.Join(", ", requiredFieldDifference));
        }

        return fieldsDict;
    }

    private static QuickBMP ParseRequiredAndOptionalFieldsAndCreateSimplifiedBMPs(NeptuneDbContext dbContext, string[] row,
            Dictionary<string, int> fieldsDict, int rowNumber, out List<string> errorList, List<TreatmentBMPType> treatmentBMPTypes, List<string> quickBMPNamesInCsv)
    {
        errorList = new List<string>();

        var wqmpName = SetStringValue(row, fieldsDict, rowNumber, errorList, "WQMP", WaterQualityManagementPlan.FieldLengths.WaterQualityManagementPlanName, true);

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

        var bmpName = SetStringValue(row, fieldsDict, rowNumber, errorList, "BMP Name",
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

        var bmpType = FindLookupValue(row, fieldsDict, "BMP Type", rowNumber, errorList,
            treatmentBMPTypes, x => x.TreatmentBMPTypeName,
            x => x.TreatmentBMPTypeID, true, true);
        if (bmpType.HasValue)
        {
            quickBMP.TreatmentBMPTypeID = bmpType.Value;
        }

        var countOfBMPs = GetIntFieldValue(row, fieldsDict, rowNumber, errorList, "Count of BMPs", true);
        if (countOfBMPs.HasValue)
        {
            quickBMP.NumberOfIndividualBMPs = countOfBMPs.Value;
        }

        var percentageOfSiteTreated = GetDecimalFieldValue(row, fieldsDict, rowNumber, errorList, "% of Site Treated", false);
        if (percentageOfSiteTreated.HasValue)
        {
            quickBMP.PercentOfSiteTreated = percentageOfSiteTreated;
        }

        var wetWeatherPercentageCapture = GetDecimalFieldValue(row, fieldsDict, rowNumber, errorList, "Wet Weather % Capture", false);
        if (wetWeatherPercentageCapture.HasValue)
        {
            quickBMP.PercentCaptured = wetWeatherPercentageCapture;
        }

        var wetWeatherPercentageRetained = GetDecimalFieldValue(row, fieldsDict, rowNumber, errorList, "Wet Weather % Retained", false);
        if (wetWeatherPercentageRetained.HasValue)
        {
            quickBMP.PercentRetained = wetWeatherPercentageRetained;
        }

        var dryWeatherFlowOverride = FindLookupValue(row, fieldsDict, "Dry Weather Flow Override?", rowNumber, errorList,
            DryWeatherFlowOverride.All, x => x.DryWeatherFlowOverrideDisplayName,
            x => x.DryWeatherFlowOverrideID, true, true);
        if (dryWeatherFlowOverride.HasValue)
        {
            quickBMP.DryWeatherFlowOverrideID = dryWeatherFlowOverride.Value;
        }

        var notes = SetStringValue(row, fieldsDict, rowNumber, errorList, "Notes", QuickBMP.FieldLengths.QuickBMPNote, false);
        if (notes != null)
        {
            quickBMP.QuickBMPNote = notes;
        }

        return quickBMP;
    }

    private static string SetStringValue(string[] row, Dictionary<string, int> fieldsDict, int rowNumber, List<string> errorList, string fieldName,
            int fieldLength, bool requireNotEmpty)
    {
        if (fieldsDict.TryGetValue(fieldName, out var value))
        {
            var fieldValue = row[value];
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
        }
        return null;
    }

    private static int? FindLookupValue<T>(string[] row, Dictionary<string, int> fieldsDict, string fieldName, int rowNumber, List<string> errorList,
        List<T> lookupValues, Func<T, string> funcDisplayName, Func<T, int> funcID, bool showAvailableValuesInErrorMessage, bool requireNotEmpty)
    {
        if (fieldsDict.TryGetValue(fieldName.Trim(), out var value))
        {
            var fieldValue = row[value];
            if (!string.IsNullOrWhiteSpace(fieldValue))
            {
                if (!lookupValues.Select(funcDisplayName.Invoke).Contains(fieldValue))
                {
                    var errorMessage = $"No {fieldName} with the name '{fieldValue}' exists in our records, row: {rowNumber}.";
                    if (showAvailableValuesInErrorMessage)
                    {
                        errorMessage += $" Acceptable Values Are: {string.Join(", ", lookupValues.Select(funcDisplayName.Invoke))}";
                    }
                    errorList.Add(errorMessage);
                }
                else
                {
                    var entity = lookupValues.Single(x => funcDisplayName.Invoke(x) == fieldValue);
                    return funcID.Invoke(entity);
                }
            }
            else
            {
                if (requireNotEmpty)
                {
                    errorList.Add($"{fieldName} is null, empty, or just whitespaces for row: {rowNumber}");
                }
            }
        }

        return null;
    }

    private static int? GetIntFieldValue(string[] row, Dictionary<string, int> fieldsDict, int rowNumber, List<string> errorList, string fieldName, bool requireNotEmpty)
    {
        if (fieldsDict.TryGetValue(fieldName, out var value))
        {
            var fieldValue = row[value];
            if (!string.IsNullOrWhiteSpace(fieldValue))
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
        }

        if (requireNotEmpty)
        {
            errorList.Add($"{fieldName} is null, empty, or just whitespaces for row: {rowNumber}");
        }

        return null;
    }

    private static decimal? GetDecimalFieldValue(string[] row, Dictionary<string, int> fieldsDict, int rowNumber, List<string> errorList, string fieldName, bool requireNotEmpty)
    {
        if (fieldsDict.TryGetValue(fieldName, out var value))
        {
            var fieldValue = row[value];
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
        }

        return null;
    }

}