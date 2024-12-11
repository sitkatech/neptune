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
        var wqmpsToUpload = new List<QuickBMP>();
        var fieldsDict = new Dictionary<string, int>();

        var requiredFields = new List<string> { "WQMP", "BMP Name", "BMP Type", "Count of BMPs", "Dry Weather Flow Override?" };
        var optionalFields = new List<string> { "% of Site Treated", "Wet Weather % Capture", "Wet Weather % Retained", "Notes" };

        try
        {
            var header = parser.ReadFields();
            fieldsDict = ValidateHeader(header, requiredFields, optionalFields, out errorList);
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
        var stormwaterJurisdictions = StormwaterJurisdictions.List(dbContext).ToList();
        var wqmpNamesInCsv = new List<string>();
        var treatmentBMPTypes = TreatmentBMPTypes.List(dbContext);
        while (!parser.EndOfData)
        {
            var currentRow = parser.ReadFields();

            var currentWQMP = ParseRequiredAndOptionalFieldsAndCreateSimplifiedBMPs(dbContext, currentRow, fieldsDict, rowCount, out var currentErrorList, stormwaterJurisdictions, wqmpNamesInCsv, treatmentBMPTypes);
            if (currentWQMP != null)
            {
                wqmpsToUpload.Add(currentWQMP);
                errorList.AddRange(currentErrorList);
            }

            errorList.AddRange(currentErrorList);
            rowCount++;
        }

        return wqmpsToUpload;
    }

    private static Dictionary<string, int> ValidateHeader(string[] row, List<string> requiredFields, List<string> optionalFields, out List<string> errorList)
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
        var optionalFieldDifference = headers.Except(requiredFields).Except(optionalFields).ToList();

        if (requiredFieldDifference.Any())
        {
            errorList.Add("One or more required headers have not been provided. Required Fields are: " +
                          string.Join(", ", requiredFieldDifference));
        }

        //if (optionalFieldDifference.Any())
        //{
        //    errorList.Add($"The provided fields '{string.Join(", ", optionalFieldDifference)}' did not match a property of Water Quality Management Plan");
        //}

        return fieldsDict;
    }

    private static QuickBMP ParseRequiredAndOptionalFieldsAndCreateSimplifiedBMPs(NeptuneDbContext dbContext, string[] row,
            Dictionary<string, int> fieldsDict, int rowNumber, out List<string> errorList,
            List<StormwaterJurisdiction> stormwaterJurisdictions, List<string> wqmpNamesInCsv, List<TreatmentBMPType> treatmentBMPTypes)
    {
        errorList = new List<string>();

        var wqmpName = SetStringValue(row, fieldsDict, rowNumber, errorList, "WQMP", WaterQualityManagementPlan.FieldLengths.WaterQualityManagementPlanName, true);

        if (string.IsNullOrWhiteSpace(wqmpName))
        {
            // no point in going further if we don't have a name and jurisdiction
            return null;
        }

        var wqmp = dbContext.WaterQualityManagementPlans.SingleOrDefault(x =>
            x.WaterQualityManagementPlanName == wqmpName);

        if (wqmp == null)
        {
            errorList.Add($"The WQMP with name '{wqmpName}' in row {rowNumber} was not found.");
            return null;
        }

        var quickBMP = new QuickBMP() {WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID};

        var bmpName = SetStringValue(row, fieldsDict, rowNumber, errorList, "BMP Name",
            QuickBMP.FieldLengths.QuickBMPName, false);
        if (!string.IsNullOrWhiteSpace(bmpName))
        {
            quickBMP.QuickBMPName = bmpName;
        }

        var bmpType = FindLookupValue(row, fieldsDict, "BMP Type", rowNumber, errorList,
            treatmentBMPTypes, x => x.TreatmentBMPTypeName,
            x => x.TreatmentBMPTypeID, true, false);
        if (bmpType.HasValue)
        {
            quickBMP.TreatmentBMPTypeID = bmpType.Value;
        }

        var countOfBMPs = GetOptionalIntFieldValue(row, fieldsDict, rowNumber, errorList, "Count of BMPs");
        if (countOfBMPs.HasValue)
        {
            quickBMP.NumberOfIndividualBMPs = countOfBMPs.Value;
        }

        var percentageOfSiteTreated = GetOptionalDecimalFieldValue(row, fieldsDict, rowNumber, errorList, "% of Site Treated");
        if (percentageOfSiteTreated.HasValue)
        {
            quickBMP.PercentOfSiteTreated = percentageOfSiteTreated;
        }

        var wetWeatherPercentageCapture = GetOptionalDecimalFieldValue(row, fieldsDict, rowNumber, errorList, "Wet Weather % Capture");
        if (wetWeatherPercentageCapture.HasValue)
        {
            quickBMP.PercentCaptured = wetWeatherPercentageCapture;
        }

        var wetWeatherPercentageRetained = GetOptionalDecimalFieldValue(row, fieldsDict, rowNumber, errorList, "Wet Weather % Retained");
        if (wetWeatherPercentageRetained.HasValue)
        {
            quickBMP.PercentRetained = wetWeatherPercentageRetained;
        }

        var dryWeatherFlowOverride = FindLookupValue(row, fieldsDict, "Dry Weather Flow Override?", rowNumber, errorList,
            DryWeatherFlowOverride.All, x => x.DryWeatherFlowOverrideDisplayName,
            x => x.DryWeatherFlowOverrideID, true, false);
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
        if (fieldsDict.ContainsKey(fieldName))
        {
            var fieldValue = row[fieldsDict[fieldName]];
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
        if (fieldsDict.ContainsKey(fieldName))
        {
            var fieldValue = row[fieldsDict[fieldName]];
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

    private static int? GetOptionalIntFieldValue(string[] row, Dictionary<string, int> fieldsDict, int rowNumber, List<string> errorList, string fieldName)
    {
        if (fieldsDict.ContainsKey(fieldName))
        {
            var fieldValue = row[fieldsDict[fieldName]];
            if (!string.IsNullOrWhiteSpace(fieldValue))
            {
                if (!int.TryParse(fieldValue, out var fieldValueAsInt))
                {
                    errorList.Add($"{fieldName} can not be converted to Int at row: {rowNumber}");
                }
                else
                {
                    return fieldValueAsInt;
                }
            }
        }

        return null;
    }

    private static decimal? GetOptionalDecimalFieldValue(string[] row, Dictionary<string, int> fieldsDict, int rowNumber, List<string> errorList, string fieldName)
    {
        if (fieldsDict.ContainsKey(fieldName))
        {
            var fieldValue = row[fieldsDict[fieldName]];
            if (!string.IsNullOrWhiteSpace(fieldValue))
            {
                if (!decimal.TryParse(fieldValue, out var fieldValueAsInt))
                {
                    errorList.Add($"{fieldName} can not be converted to decimal at row: {rowNumber}");
                }
                else
                {
                    return fieldValueAsInt;
                }
            }
        }

        return null;
    }

}