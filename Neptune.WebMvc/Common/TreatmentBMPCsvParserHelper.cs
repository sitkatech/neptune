﻿using Microsoft.VisualBasic.FileIO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.Models;
using NetTopologySuite.Geometries;
using Neptune.Models.DataTransferObjects;

namespace Neptune.WebMvc.Common
{
    public static class TreatmentBMPCsvParserHelper
    {
        public static List<TreatmentBMP> CSVUpload(NeptuneDbContext dbContext, Stream fileStream, TreatmentBMPType treatmentBMPType,
            out List<string> errorList, out List<CustomAttribute> customAttributes,
            out List<CustomAttributeValue> customAttributeValues,
            out List<TreatmentBMPModelingAttribute> modelingAttributes)
        {
            var streamReader = new StreamReader(fileStream);
            var parser = new TextFieldParser(streamReader);
            return ParseBmpRowsFromCsv(dbContext, parser, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, treatmentBMPType);
        }

        public static List<TreatmentBMP> CSVUpload(NeptuneDbContext dbContext, string fileStream, int treatmentBMPTypeID, out List<string> errorList, out List<CustomAttribute> customAttributes, out List<CustomAttributeValue> customAttributeValues, out List<TreatmentBMPModelingAttribute> modelingAttributes)
        {
            var stringReader = new StringReader(fileStream);
            var parser = new TextFieldParser(stringReader);
            var treatmentBMPType = TreatmentBMPTypes.GetByID(dbContext, treatmentBMPTypeID);
            return ParseBmpRowsFromCsv(dbContext, parser, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, treatmentBMPType);
        }

        public static List<TreatmentBMP> ParseBmpRowsFromCsv(NeptuneDbContext dbContext, TextFieldParser parser,
            out List<string> errorList, out List<CustomAttribute> customAttributes,
            out List<CustomAttributeValue> customAttributeValues,
            out List<TreatmentBMPModelingAttribute> modelingAttributes, TreatmentBMPType treatmentBMPType)
        {
            parser.SetDelimiters(",");
            errorList = new List<string>();
            customAttributes = new List<CustomAttribute>();
            customAttributeValues = new List<CustomAttributeValue>();
            modelingAttributes = new List<TreatmentBMPModelingAttribute>();
            var treatmentBMPsToUpload = new List<TreatmentBMP>();
            var fieldsDict = new Dictionary<string, int>();

            var requiredFields = new List<string> { "Jurisdiction", "BMP Name", "Latitude", "Longitude", "Sizing Basis", "Trash Capture Status", "Owner" };
            var optionalFields = new List<string> {"Year Built or Installed","Asset ID in System of Record", "Required Lifespan of Installation",
                "Allowable End Date of Installation (if applicable)", "Required Field Visits Per Year", "Required Post-Storm Field Visits Per Year","Notes"};
            var availableModelingAttributes = GetAvailableModelingAttributes(treatmentBMPType);
            var customAttributeTypes = treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Select(x => x.CustomAttributeType).ToList();

            try
            {
                var header = parser.ReadFields();
                var customAttributeNames = customAttributeTypes.Select(x => x.CustomAttributeTypeName).ToList();
                fieldsDict = ValidateHeader(header, requiredFields, optionalFields, availableModelingAttributes, customAttributeNames, out errorList, treatmentBMPType);
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
            var organizations = dbContext.Organizations.AsNoTracking().ToList();
            var stormwaterJurisdictions = StormwaterJurisdictions.List(dbContext).ToList();
            var treatmentBMPNamesInCsv = new List<string>();
            while (!parser.EndOfData)
            {
                var currentRow = parser.ReadFields();

                var currentTreatmentBMP = ParseRequiredAndOptionalFieldAndCreateBMP(dbContext, currentRow, fieldsDict, rowCount, out var currentErrorList, treatmentBMPType, organizations, stormwaterJurisdictions, treatmentBMPNamesInCsv);
                if (currentTreatmentBMP != null)
                {
                    treatmentBMPsToUpload.Add(currentTreatmentBMP);
                    errorList.AddRange(currentErrorList);

                    if (availableModelingAttributes.Count > 0)
                    {
                        modelingAttributes.Add(ParseModelingAttributes(currentTreatmentBMP, treatmentBMPType, currentRow, fieldsDict,
                            availableModelingAttributes, rowCount, out currentErrorList));
                        errorList.AddRange(currentErrorList);
                    }

                    customAttributes.AddRange(ParseCustomAttributes(dbContext, currentTreatmentBMP, treatmentBMPType, currentRow, fieldsDict,
                        customAttributeTypes, rowCount, out currentErrorList,
                        out var currentCustomAttributeValues));
                    customAttributeValues.AddRange(currentCustomAttributeValues);
                }

                errorList.AddRange(currentErrorList);
                rowCount++;
            }

            return treatmentBMPsToUpload;
        }

        private static TreatmentBMP ParseRequiredAndOptionalFieldAndCreateBMP(NeptuneDbContext dbContext,
            string[] row,
            Dictionary<string, int> fieldsDict, int rowNumber, out List<string> errorList,
            TreatmentBMPType treatmentBMPType, List<Organization> organizations,
            List<StormwaterJurisdiction> stormwaterJurisdictions, List<string> treatmentBMPNamesInCsv)
        {
            errorList = new List<string>();

            var treatmentBMPName = SetStringValue(row, fieldsDict, rowNumber, errorList, "BMP Name", TreatmentBMP.FieldLengths.TreatmentBMPName, true);
            var stormwaterJurisdictionID = FindLookupValue(row, fieldsDict, "Jurisdiction", rowNumber, errorList, stormwaterJurisdictions, x => x.Organization.OrganizationName, x => x.StormwaterJurisdictionID, false, true);

            if (!stormwaterJurisdictionID.HasValue || string.IsNullOrWhiteSpace(treatmentBMPName))
            {
                // no point in going further if we don't have a name and jurisdiction
                return null;
            }

            if (!string.IsNullOrWhiteSpace(treatmentBMPName))
            {
                if (treatmentBMPNamesInCsv.Contains(treatmentBMPName))
                {
                    errorList.Add(
                        $"The BMP with Name '{treatmentBMPName}' was already added in this upload, duplicate name is found at row: {rowNumber}");
                }
                treatmentBMPNamesInCsv.Add(treatmentBMPName);
            }

            var treatmentBMP = dbContext.TreatmentBMPs.Include(x => x.TreatmentBMPType).SingleOrDefault(x =>
                x.TreatmentBMPName == treatmentBMPName &&
                x.StormwaterJurisdictionID == stormwaterJurisdictionID.Value);
            if (treatmentBMP != null)
            {
                // one last check; make sure the treatment bmp type of the existing treatment bmp matches the passed type
                if (treatmentBMPType.TreatmentBMPTypeID != treatmentBMP.TreatmentBMPTypeID)
                {
                    errorList.Add(
                        $"BMP with name '{treatmentBMPName}' has a Type '{treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName}', which does not match the uploaded Type '{treatmentBMPType.TreatmentBMPTypeName}' for row: {rowNumber}");
                }
            }
            else
            {
                treatmentBMP = new TreatmentBMP()
                {
                    TreatmentBMPName = treatmentBMPName,
                    TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID,
                    StormwaterJurisdictionID = stormwaterJurisdictionID.Value, 
                    InventoryIsVerified = false
                };
            }

            var isNew = !ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMP.TreatmentBMPID);
            var treatmentBMPLatitude = row[fieldsDict["Latitude"]];
            var treatmentBMPLongitude = row[fieldsDict["Longitude"]];
            var locationPoint4326 = ParseLocation(treatmentBMPLatitude, treatmentBMPLongitude, rowNumber, errorList,
                isNew);
            if (locationPoint4326 != null)
            {
                treatmentBMP.LocationPoint4326 = locationPoint4326;
                var locationPoint = locationPoint4326.ProjectTo2771();
                treatmentBMP.LocationPoint = locationPoint;

                treatmentBMP.SetTreatmentBMPPointInPolygonDataByLocationPoint(locationPoint, dbContext);
            }

            var ownerOrganizationID = FindLookupValue(row, fieldsDict, "Owner", rowNumber, errorList, organizations,
                x => x.OrganizationName, x => x.OrganizationID, false, isNew);
            if (ownerOrganizationID.HasValue)
            {
                treatmentBMP.OwnerOrganizationID = ownerOrganizationID.Value;
            }

            //start of Optional Fields
            var yearBuilt =
                GetOptionalIntFieldValue(row, fieldsDict, rowNumber, errorList, "Year Built or Installed");
            if (yearBuilt.HasValue)
            {
                treatmentBMP.YearBuilt = yearBuilt;
            }

            var assetIDInSystemOfRecord = SetStringValue(row, fieldsDict, rowNumber, errorList,
                "Asset ID in System of Record", TreatmentBMP.FieldLengths.SystemOfRecordID, false);
            if (!string.IsNullOrWhiteSpace(assetIDInSystemOfRecord))
            {
                treatmentBMP.SystemOfRecordID = assetIDInSystemOfRecord;
            }

            var notes = SetStringValue(row, fieldsDict, rowNumber, errorList, "Notes",
                TreatmentBMP.FieldLengths.Notes, false);
            if (!string.IsNullOrWhiteSpace(notes))
            {
                treatmentBMP.Notes = notes;
            }

            var fieldNameRequiredLifespanOfInstallation = "Required Lifespan of Installation";
            if (fieldsDict.ContainsKey(fieldNameRequiredLifespanOfInstallation))
            {
                var treatmentBMPLifespanTypeID = FindLookupValue(row, fieldsDict,
                    fieldNameRequiredLifespanOfInstallation, rowNumber, errorList, TreatmentBMPLifespanType.All,
                    x => x.TreatmentBMPLifespanTypeDisplayName, x => x.TreatmentBMPLifespanTypeID, true, false);
                if (treatmentBMPLifespanTypeID.HasValue)
                {
                    treatmentBMP.TreatmentBMPLifespanTypeID = treatmentBMPLifespanTypeID;
                }

                var fieldNameAllowableEndDateOfInstallationIfApplicable =
                    "Allowable End Date of Installation (if applicable)";
                if (fieldsDict.ContainsKey(fieldNameAllowableEndDateOfInstallationIfApplicable))
                {
                    var requiredLifespanOfInstallation = row[fieldsDict[fieldNameRequiredLifespanOfInstallation]];
                    var allowableEndDateOfInstallation =
                        row[fieldsDict[fieldNameAllowableEndDateOfInstallationIfApplicable]];
                    var isAllowableEndDateOfInstallationEmpty =
                        string.IsNullOrWhiteSpace(allowableEndDateOfInstallation);
                    if (isAllowableEndDateOfInstallationEmpty && treatmentBMPLifespanTypeID ==
                        TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeID)
                    {
                        errorList.Add(
                            $"An end date must be provided if the '{fieldNameRequiredLifespanOfInstallation}' field is set to fixed end date for row: {rowNumber}");
                    }

                    if (!isAllowableEndDateOfInstallationEmpty && treatmentBMPLifespanTypeID !=
                        TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeID)
                    {
                        errorList.Add(
                            $"An end date was provided when '{fieldNameRequiredLifespanOfInstallation}' field was set to {requiredLifespanOfInstallation} for row: {rowNumber}");
                    }

                    if (!treatmentBMPLifespanTypeID.HasValue && !isAllowableEndDateOfInstallationEmpty)
                    {
                        errorList.Add(
                            $"An end date was provided when '{fieldNameRequiredLifespanOfInstallation}' field was set to null for row: {rowNumber}");
                    }

                    if (!isAllowableEndDateOfInstallationEmpty)
                    {
                        if (!DateTime.TryParse(allowableEndDateOfInstallation,
                            out var allowableEndDateOfInstallationDateTime))
                        {
                            errorList.Add(
                                $"{fieldNameAllowableEndDateOfInstallationIfApplicable} can not be converted to Date Time format at row: {rowNumber}");
                        }
                        else
                        {
                            treatmentBMP.TreatmentBMPLifespanEndDate = allowableEndDateOfInstallationDateTime;
                        }
                    }
                }
            }

            var requiredFieldVisitsPerYear = GetOptionalIntFieldValue(row, fieldsDict, rowNumber, errorList,
                "Required Field Visits Per Year");
            if (requiredFieldVisitsPerYear.HasValue)
            {
                treatmentBMP.RequiredFieldVisitsPerYear = requiredFieldVisitsPerYear;
            }

            var requiredPostStormFieldVisitsPerYear = GetOptionalIntFieldValue(row, fieldsDict, rowNumber,
                errorList, "Required Post-Storm Field Visits Per Year");
            if (requiredPostStormFieldVisitsPerYear.HasValue)
            {
                treatmentBMP.RequiredPostStormFieldVisitsPerYear = requiredPostStormFieldVisitsPerYear;
            }

            //End of Optional Fields
            var trashCaptureStatusTypeID = FindLookupValue(row, fieldsDict, "Trash Capture Status", rowNumber,
                errorList, TrashCaptureStatusType.All, x => x.TrashCaptureStatusTypeDisplayName,
                x => x.TrashCaptureStatusTypeID, true, isNew);
            if (trashCaptureStatusTypeID.HasValue)
            {
                treatmentBMP.TrashCaptureStatusTypeID = trashCaptureStatusTypeID.Value;
            }

            var treatmentBMPSizingBasisTypeID = FindLookupValue(row, fieldsDict, "Sizing Basis", rowNumber,
                errorList, SizingBasisType.All, x => x.SizingBasisTypeDisplayName, x => x.SizingBasisTypeID, true,
                isNew);
            if (treatmentBMPSizingBasisTypeID.HasValue)
            {
                treatmentBMP.SizingBasisTypeID = treatmentBMPSizingBasisTypeID.Value;
            }

            return treatmentBMP;
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

        private static Geometry ParseLocation(string treatmentBMPLatitude, string treatmentBMPLongitude, int rowNumber, List<string> errorList, bool isNew)
        {
            var locationErrorList = new List<string>();
            if (string.IsNullOrWhiteSpace(treatmentBMPLatitude) || string.IsNullOrWhiteSpace(treatmentBMPLongitude))
            {
                if (!isNew)
                {
                    return null;
                }
                locationErrorList.Add($"Treatment BMP Latitude {treatmentBMPLatitude} or Longitude {treatmentBMPLongitude} is null or empty space at row: {rowNumber}");
            }
            if (!decimal.TryParse(treatmentBMPLatitude, out var treatmentBMPLatitudeDecimal))
            {
                locationErrorList.Add(
                    $"Treatment BMP Latitude can not be converted to Decimal format at row: {rowNumber}");
            }
            if (!(treatmentBMPLatitudeDecimal <= 90 && treatmentBMPLatitudeDecimal >= -90))
            {
                locationErrorList.Add(
                    $"Treatment BMP Latitude {treatmentBMPLatitudeDecimal} is less than -90 or greater than 90 at row: {rowNumber}");
            }
            if (!decimal.TryParse(treatmentBMPLongitude, out var treatmentBMPLongitudeDecimal))
            {
                locationErrorList.Add(
                    $"Treatment BMP Longitude can not be converted to Decimal format at row: {rowNumber}");
            }
            if (!(treatmentBMPLongitudeDecimal <= 180 && treatmentBMPLongitudeDecimal >= -180))
            {
                locationErrorList.Add(
                    $"Treatment BMP Longitude {treatmentBMPLongitudeDecimal} is less than -180 or greater than 180 at row: {rowNumber}");
            }

            if (locationErrorList.Any())
            {
                errorList.AddRange(locationErrorList);
                return null;
            }

            return GeometryHelper.CreateLocationPoint4326FromLatLong(double.Parse(treatmentBMPLatitude), double.Parse(treatmentBMPLongitude));
        }

        private static List<CustomAttribute> ParseCustomAttributes(NeptuneDbContext dbContext, TreatmentBMP treatmentBMP, TreatmentBMPType treatmentBMPType,
            string[] currentRow, Dictionary<string, int> fieldsDict,
            List<CustomAttributeType> customAttributeTypes, int rowNumber, out List<string> currentErrorList,
            out List<CustomAttributeValue> customAttributeValues)
        {
            currentErrorList = new List<string>();
            customAttributeValues = new List<CustomAttributeValue>();
            var customAttributes = new List<CustomAttribute>();
            var isNew = !ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMP.TreatmentBMPID);
            foreach (var customAttributeType in customAttributeTypes)
            {
                var treatmentBMPTypeCustomAttributeType = customAttributeType.TreatmentBMPTypeCustomAttributeTypes.Single(x => x.TreatmentBMPTypeID == treatmentBMPType.TreatmentBMPTypeID);
                var customAttribute =
                    treatmentBMP.CustomAttributes.SingleOrDefault(x =>
                        x.TreatmentBMPTypeCustomAttributeTypeID == treatmentBMPTypeCustomAttributeType
                            .TreatmentBMPTypeCustomAttributeTypeID) ?? new CustomAttribute()
                    {
                        TreatmentBMP = treatmentBMP,
                        TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeCustomAttributeTypeID,
                        TreatmentBMPTypeID = treatmentBMPTypeCustomAttributeType.TreatmentBMPTypeID,
                        CustomAttributeTypeID = treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID
                    };
                if (fieldsDict.ContainsKey(customAttributeType.CustomAttributeTypeName))
                {
                    var value = currentRow[fieldsDict[customAttributeType.CustomAttributeTypeName]];

                    var customAttributeDataTypeEnum = customAttributeType.CustomAttributeDataType.ToEnum;

                    var customAttributeTypeAcceptableValues =
                        customAttributeType.CustomAttributeTypeOptionsSchema != null
                            ? System.Text.Json.JsonSerializer.Deserialize<List<string>>(
                                customAttributeType.CustomAttributeTypeOptionsSchema)
                            : null;

                    if (string.IsNullOrEmpty(value))
                    {
                        //Don't do anything with an empty value if we're updating, but add it if we're new
                        if (isNew)
                        {
                            customAttributeValues.Add(new CustomAttributeValue
                                { CustomAttribute = customAttribute, AttributeValue = value });
                        }
                    }
                    else if (!ValidateCustomAttributeValueEntry(
                        value, 
                        customAttributeDataTypeEnum,
                        customAttributeTypeAcceptableValues))
                    {
                        currentErrorList.Add(GetErrorForCustomAttributeType(
                            value, 
                            customAttributeDataTypeEnum, 
                            customAttributeType.CustomAttributeTypeName,
                            customAttributeType.CustomAttributeDataType.CustomAttributeDataTypeDisplayName, 
                            customAttributeTypeAcceptableValues, 
                            rowNumber));
                    }
                    else
                    {
                        dbContext.CustomAttributeValues.RemoveRange(customAttribute
                            .CustomAttributeValues);
                        customAttribute.CustomAttributeValues.Clear();

                        if (customAttributeType.CustomAttributeDataType == CustomAttributeDataType.MultiSelect)
                        {
                            var attributeValues = value.Split(new[] {','}).Select(x => x.Trim()).Select(x =>
                                new CustomAttributeValue { CustomAttribute = customAttribute, AttributeValue = x });
                            customAttributeValues.AddRange(attributeValues);
                        }
                        else
                        {
                            customAttributeValues.Add(new CustomAttributeValue
                            {
                                CustomAttribute = customAttribute, AttributeValue = value
                            });
                        }
                    }
                }
                customAttributes.Add(customAttribute);
            }
            return customAttributes;
        }

        private static string GetErrorForCustomAttributeType(string value,
            CustomAttributeDataTypeEnum customAttributeDataTypeEnum,
            string customAttributeTypeName, string customAttributeDataTypeDisplayName, List<string> customAttributeTypeAcceptableValues, int rowNumber)
        {
            switch (customAttributeDataTypeEnum)
            {
                case CustomAttributeDataTypeEnum.Integer:
                case CustomAttributeDataTypeEnum.Decimal:
                case CustomAttributeDataTypeEnum.DateTime:
                    return
                        $"{customAttributeTypeName} field can not be converted to {customAttributeDataTypeDisplayName} at row: {rowNumber}";
                case CustomAttributeDataTypeEnum.MultiSelect:
                case CustomAttributeDataTypeEnum.PickFromList:
                    return
                        $"{value} is not a valid {customAttributeTypeName} entry at row: {rowNumber}. Acceptable values are: {string.Join(", ", customAttributeTypeAcceptableValues)}";
                default:
                    return
                        $"{customAttributeTypeName} entry at row: {rowNumber} experienced an unknown error. Please double check the sheet, and contact support with further questions.";
            }
        }

        private static bool ValidateCustomAttributeValueEntry(string value, CustomAttributeDataTypeEnum customAttributeDataType, List<string> customAttributeTypeAcceptableValues)
        {
            switch (customAttributeDataType)
            {
                case CustomAttributeDataTypeEnum.Integer:
                    return int.TryParse(value, out _);
                case CustomAttributeDataTypeEnum.Decimal:
                    return decimal.TryParse(value, out _);
                case CustomAttributeDataTypeEnum.DateTime:
                    return DateTime.TryParse(value, out _);
                case CustomAttributeDataTypeEnum.PickFromList:
                case CustomAttributeDataTypeEnum.MultiSelect:
                    var splitValues = value.Split(',').Select(x => x.Trim());

                    return splitValues.All(customAttributeTypeAcceptableValues.Contains);
                case CustomAttributeDataTypeEnum.String:
                    return true;
                default:
                    return false;
            }
        }

        private static TreatmentBMPModelingAttribute ParseModelingAttributes(TreatmentBMP treatmentBMP,
            TreatmentBMPType treatmentBMPType, string[] currentRow, Dictionary<string, int> fieldsDict,
            List<string> availableModelingAttributesForType, int rowCount, out List<string> currentErrorList)
        {
            currentErrorList = new List<string>();
            var treatmentBMPModelingAttribute = treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP ??
                                                new TreatmentBMPModelingAttribute()
                                                {
                                                    TreatmentBMP = treatmentBMP,
                                                    RoutingConfigurationID = RoutingConfiguration.Online.RoutingConfigurationID
                                                };
            foreach (var attribute in availableModelingAttributesForType)
            {
                if (fieldsDict.ContainsKey(attribute) && !string.IsNullOrWhiteSpace(currentRow[fieldsDict[attribute]]))
                {
                    var modelingProperty = GetAppropriateModelingAttributeColumnName(attribute);
                    var value = currentRow[fieldsDict[attribute]];

                    var propertyToChange = treatmentBMPModelingAttribute.GetType().GetProperty(modelingProperty);
                    var propType = propertyToChange?.Name == "UnderlyingHydrologicSoilGroupID"
                        ?
                        typeof(UnderlyingHydrologicSoilGroup)
                        :
                        propertyToChange?.Name == "TimeOfConcentrationID"
                            ? typeof(TimeOfConcentration)
                            :
                            propertyToChange?.Name == "RoutingConfigurationID"
                                ? typeof(RoutingConfiguration)
                                :
                                propertyToChange?.Name == "MonthsOfOperationID"
                                    ? typeof(MonthsOfOperation)
                                    :
                                    propertyToChange?.PropertyType;

                    if (propType != null && (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        propType = propType.GetGenericArguments()[0];
                    }

                    if (propType == typeof(int))
                    {
                        if (int.TryParse(value, out var valueInt))
                        {
                            propertyToChange.SetValue(treatmentBMPModelingAttribute,
                                valueInt);
                        }
                        else
                        {
                            currentErrorList.Add(
                                $"{attribute} field can not be converted to Integer at row: {rowCount}");
                        }
                    }
                    else if (propType == typeof(double))
                    {
                        if (double.TryParse(value, out var valueDouble))
                        {
                            propertyToChange.SetValue(treatmentBMPModelingAttribute,
                                valueDouble);
                        }
                        else
                        {
                            currentErrorList.Add(
                                $"{attribute} field can not be converted to Double at row: {rowCount}");
                        }

                    }
                    else if (propType == typeof(UnderlyingHydrologicSoilGroup))
                    {
                        var treatmentBMPModelingTypeEnum = treatmentBMPType.TreatmentBMPModelingType.ToEnum;
                        var underlyingHydrologicSoilGroups = UnderlyingHydrologicSoilGroup.All;
                        var underlyingHydrologicSoilGroup =
                            underlyingHydrologicSoilGroups.SingleOrDefault(x =>
                                x.UnderlyingHydrologicSoilGroupDisplayName == value);
                        if (underlyingHydrologicSoilGroup == null ||
                            (treatmentBMPModelingTypeEnum == TreatmentBMPModelingTypeEnum
                                 .BioinfiltrationBioretentionWithRaisedUnderdrain &&
                             underlyingHydrologicSoilGroup.ToEnum == UnderlyingHydrologicSoilGroupEnum.Liner))
                        {
                            currentErrorList.Add(
                                $"{value} is not a valid {attribute} entry for Treatment BMPs of {treatmentBMPType.TreatmentBMPTypeName} type at row: {rowCount}.Acceptable values are :{string.Join(", ", underlyingHydrologicSoilGroups.Where(x => treatmentBMPModelingTypeEnum != TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain || x.UnderlyingHydrologicSoilGroupDisplayName != "Liner").Select(x => x.UnderlyingHydrologicSoilGroupDisplayName))}");
                        }
                        else
                        {
                            propertyToChange.SetValue(treatmentBMPModelingAttribute,
                                underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupID);
                        }
                    }
                    else if (propType == typeof(TimeOfConcentration))
                    {
                        SetModelingAttributeLookupValue(rowCount, currentErrorList, value, propertyToChange,
                            treatmentBMPModelingAttribute, attribute, TimeOfConcentration.All,
                            x => x.TimeOfConcentrationDisplayName);
                    }
                    else if (propType == typeof(RoutingConfiguration))
                    {
                        SetModelingAttributeLookupValue(rowCount, currentErrorList, value, propertyToChange,
                            treatmentBMPModelingAttribute, attribute, RoutingConfiguration.All,
                            x => x.RoutingConfigurationDisplayName);
                    }
                    else if (propType == typeof(MonthsOfOperation))
                    {
                        SetModelingAttributeLookupValue(rowCount, currentErrorList, value, propertyToChange,
                            treatmentBMPModelingAttribute, attribute, MonthsOfOperation.All,
                            x => x.MonthsOfOperationDisplayName);
                    }
                    else
                    {
                        currentErrorList.Add($"{attribute} is not a valid modeling parameter entry at row: {rowCount}");
                    }
                }
            }

            if (treatmentBMPModelingAttribute.RoutingConfigurationID == RoutingConfiguration.Offline.RoutingConfigurationID &&
                treatmentBMPModelingAttribute.DiversionRate == null)
            {
                currentErrorList.Add($"The modeling attribute 'Diversion Rate' is required when the Routing Configuration is set to 'Offline'. Please adjust entry at row: {rowCount}");
            }
            return treatmentBMPModelingAttribute;
        }

        private static void SetModelingAttributeLookupValue<T>(int rowCount, List<string> currentErrorList, string value, PropertyInfo propertyToChange, TreatmentBMPModelingAttribute treatmentBMPModelingAttribute, string attribute, List<T> lookupValues, Func<T, string> funcDisplayName) where T : IHavePrimaryKey
        {
            var lookupValue = lookupValues.SingleOrDefault(x => funcDisplayName.Invoke(x) == value);
            if (lookupValue != null)
            {
                propertyToChange.SetValue(treatmentBMPModelingAttribute, lookupValue.PrimaryKey);
            }
            else
            {
                currentErrorList.Add($"{value} is not a valid {attribute} entry at row: {rowCount}. Acceptable values are:{string.Join(", ", lookupValues.Select(funcDisplayName.Invoke))}");
            }
        }


        private static Dictionary<string, int> ValidateHeader(string[] row, List<string> requiredFields, List<string> optionalFields, List<string> availableModelingAttributes, List<string> customAttributes, out List<string> errorList, TreatmentBMPType treatmentBMPType)
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
            var optionalFieldDifference = headers.Except(requiredFields).Except(optionalFields);
            var modelingAttributesDifference = optionalFieldDifference.Except(availableModelingAttributes).ToList();
            var customAttributesDifference = modelingAttributesDifference.Except(customAttributes).ToList();

            if (requiredFieldDifference.Any())
            {
                errorList.Add("One or more required headers have not been provided. Required Fields are: " +
                              string.Join(", ", requiredFieldDifference));
            }

            if (customAttributesDifference.Any())
            {
                errorList.Add($"The provided fields '{string.Join(", ", customAttributesDifference)}' did not match a property, modeling attribute, or custom attribute of the BMP type '{treatmentBMPType.TreatmentBMPModelingType.TreatmentBMPModelingTypeDisplayName}'");
            }

            return fieldsDict;
        }

        public static string GetAppropriateModelingAttributeColumnName(string fieldDefinition)
        {
            string returnVal;
            if (fieldDefinition == FieldDefinitionType.UnderlyingHydrologicSoilGroupID.FieldDefinitionTypeDisplayName)
            {
                returnVal = "UnderlyingHydrologicSoilGroupID";
            }
            else if (fieldDefinition == FieldDefinitionType.DrawdownTimeForWQDetentionVolume.FieldDefinitionTypeDisplayName)
            {
                returnVal = "DrawdownTimeForWQDetentionVolume";
            }
            else if (fieldDefinition == FieldDefinitionType.PermanentPoolOrWetlandVolume.FieldDefinitionTypeDisplayName)
            {
                returnVal = "PermanentPoolOrWetlandVolume";
            }
            else if (fieldDefinition == FieldDefinitionType.TimeOfConcentrationID.FieldDefinitionTypeDisplayName)
            {
                returnVal = "TimeOfConcentrationID";
            }
            else if (fieldDefinition == FieldDefinitionType.RoutingConfiguration.FieldDefinitionTypeDisplayName)
            {
                returnVal = "RoutingConfigurationID";
            }
            else if (fieldDefinition == FieldDefinitionType.MonthsOfOperationID.FieldDefinitionTypeDisplayName)
            {
                returnVal = "MonthsOfOperationID";
            }
            else
            {
                returnVal = FieldDefinitionType.All.Single(x => x.FieldDefinitionTypeDisplayName == fieldDefinition)
                    .FieldDefinitionTypeName;
            }

            return returnVal;
        }

        public static List<string> GetAvailableModelingAttributes(TreatmentBMPType treatmentBMPType)
        {
            var returnList = new List<string>();
            var modelingType = treatmentBMPType.TreatmentBMPModelingType;
            if (modelingType != null)
            {
                returnList.Add(FieldDefinitionType.TimeOfConcentrationID.FieldDefinitionTypeDisplayName);

                switch (modelingType.ToEnum)
                {
                    case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                        returnList.AddRange(new List<string>()
                        {
                            FieldDefinitionType.RoutingConfiguration.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DiversionRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.TotalEffectiveBMPVolume.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.StorageVolumeBelowLowestOutletElevation.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.MediaBedFootprint.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DesignMediaFiltrationRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.UnderlyingHydrologicSoilGroupID.FieldDefinitionTypeDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
                    case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                    case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                    case TreatmentBMPModelingTypeEnum.PermeablePavement:
                    case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                        returnList.AddRange(new List<string>()
                        {
                            FieldDefinitionType.RoutingConfiguration.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DiversionRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.TotalEffectiveBMPVolume.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.InfiltrationSurfaceArea.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.UnderlyingInfiltrationRate.FieldDefinitionTypeDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
                    case TreatmentBMPModelingTypeEnum.SandFilters:
                        returnList.AddRange(new List<string>()
                        {
                            FieldDefinitionType.RoutingConfiguration.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DiversionRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.TotalEffectiveBMPVolume.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DesignMediaFiltrationRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.MediaBedFootprint.FieldDefinitionTypeDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                        returnList.AddRange(new List<string>()
                        {
                            FieldDefinitionType.RoutingConfiguration.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DiversionRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.TotalEffectiveBMPVolume.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.WinterHarvestedWaterDemand.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.SummerHarvestedWaterDemand.FieldDefinitionTypeDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.ConstructedWetland:
                    case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                        returnList.AddRange(new List<string>()
                        {
                            FieldDefinitionType.RoutingConfiguration.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DiversionRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.PermanentPoolOrWetlandVolume.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.WaterQualityDetentionVolume.FieldDefinitionTypeDisplayName,
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                    case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                    case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinitionType.RoutingConfiguration.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DiversionRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.TotalEffectiveBMPVolume.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.StorageVolumeBelowLowestOutletElevation.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.EffectiveFootprint.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DrawdownTimeForWQDetentionVolume.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.UnderlyingHydrologicSoilGroupID.FieldDefinitionTypeDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinitionType.DesignDryWeatherTreatmentCapacity.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.AverageTreatmentFlowrate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.MonthsOfOperationID.FieldDefinitionTypeDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.Drywell:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinitionType.RoutingConfiguration.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DiversionRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.TotalEffectiveDrywellBMPVolume.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.InfiltrationDischargeRate.FieldDefinitionTypeDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
                    case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
                    case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinitionType.TreatmentRate.FieldDefinitionTypeDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinitionType.DesignLowFlowDiversionCapacity.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.AverageDivertedFlowrate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.MonthsOfOperationID.FieldDefinitionTypeDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                    case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinitionType.RoutingConfiguration.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.DiversionRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.TreatmentRate.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.WettedFootprint.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.EffectiveRetentionDepth.FieldDefinitionTypeDisplayName,
                            FieldDefinitionType.UnderlyingHydrologicSoilGroupID.FieldDefinitionTypeDisplayName
                        });
                        break;
                }
            }
            return returnList;
        }
    }

}