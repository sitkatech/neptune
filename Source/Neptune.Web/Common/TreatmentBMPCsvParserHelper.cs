using Microsoft.Ajax.Utilities;
using Microsoft.VisualBasic.FileIO;
using Neptune.Web.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.WebPages;
using LtInfo.Common;

namespace Neptune.Web.Common
{
    public static class TreatmentBMPCsvParserHelper
    {
        public static List<TreatmentBMP> CSVUpload(Stream fileStream, int bmpType, out List<string> errorList, out List<CustomAttribute> customAttributes, out List<CustomAttributeValue> customAttributeValues, out List<TreatmentBMPModelingAttribute> modelingAttributes, out List<TreatmentBMPOperationMonth> treatmentBMPOperationMonths)
        {
            var StreamReader = new StreamReader(fileStream);
            var parser = new TextFieldParser(StreamReader);


            return ParseBmpRowsFromCsv(parser, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out treatmentBMPOperationMonths);
        }

        public static List<TreatmentBMP> CSVUpload(string fileStream, int bmpType, out List<string> errorList, out List<CustomAttribute> customAttributes, out List<CustomAttributeValue> customAttributeValues, out List<TreatmentBMPModelingAttribute> modelingAttributes, out List<TreatmentBMPOperationMonth> treatmentBMPOperationMonths)
        {
            var stringReader = new StringReader(fileStream);
            var parser = new TextFieldParser(stringReader);

            return ParseBmpRowsFromCsv(parser, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out treatmentBMPOperationMonths);
        }

        public static List<TreatmentBMP> ParseBmpRowsFromCsv(TextFieldParser parser, int bmpType, out List<string> errorList, out List<CustomAttribute> customAttributes, out List<CustomAttributeValue> customAttributeValues, out List<TreatmentBMPModelingAttribute> modelingAttributes, out List<TreatmentBMPOperationMonth> treatmentBMPOperationMonths)
        {
            parser.SetDelimiters(",");
            errorList = new List<string>();
            customAttributes = new List<CustomAttribute>();
            customAttributeValues = new List<CustomAttributeValue>();
            treatmentBMPOperationMonths = new List<TreatmentBMPOperationMonth>();
            modelingAttributes = new List<TreatmentBMPModelingAttribute>();
            var treatmentBMPsToUpload = new List<TreatmentBMP>();
            var fieldsDict = new Dictionary<string, int>();

            var requiredFields = new List<string> { "Jurisdiction", "BMP Name", "Latitude", "Longitude", "Sizing Basis", "Trash Capture Status", "Owner" };
            var optionalFields = new List<string> {"Year Built or Installed","Asset ID in System of Record", "Required Lifespan of Installation",
                "Allowable End Date of Installation (if applicable)", "Required Field Visits Per Year", "Required Post-Storm Field Visits Per Year","Notes"};
            var availableModelingAttributes = GetAvailableModelingAttributes(bmpType);
            var customAttributeTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeCustomAttributeTypes
                .Where(x => x.TreatmentBMPTypeID == bmpType).Select(x => x.CustomAttributeType);

            try
            {
                var header = parser.ReadFields();
                var customAttributeNames = customAttributeTypes.Select(x => x.CustomAttributeTypeName).ToList();
                fieldsDict = ValidateHeader(header, requiredFields, optionalFields, availableModelingAttributes, customAttributeNames, bmpType, out errorList);
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
            while (!parser.EndOfData)
            {
                var currentRow = parser.ReadFields();
                var currentErrorList = new List<string>();
                var currentCustomAttributeValues = new List<CustomAttributeValue>();
                var currentTreatmentBMPOperationMonths = new List<TreatmentBMPOperationMonth>();

                var currentBMP = ParseRequiredAndOptionalFieldAndCreateBMP(bmpType, currentRow, fieldsDict, rowCount, out currentErrorList);

                if (treatmentBMPsToUpload.Select(x => x.TreatmentBMPName).Contains(currentBMP.TreatmentBMPName))
                {
                    currentErrorList.Add(
                        $"The BMP with Name '{currentBMP.TreatmentBMPName}' was already added in this upload, duplicate name is found at row: {rowCount}");
                    continue;
                }
                treatmentBMPsToUpload.Add(currentBMP);
                errorList.AddRange(currentErrorList);
                
                if (availableModelingAttributes.Count > 0)
                {
                    modelingAttributes.Add(ParseModelingAttributes(currentBMP, currentRow, fieldsDict,
                        availableModelingAttributes, rowCount, out currentErrorList, out treatmentBMPOperationMonths));
                    errorList.AddRange(currentErrorList);
                    treatmentBMPOperationMonths.AddRange(currentTreatmentBMPOperationMonths);
                }

                customAttributes.AddRange(ParseCustomAttributes(currentBMP, currentRow, fieldsDict, customAttributeTypes.ToList(), rowCount, out currentErrorList, out currentCustomAttributeValues));
                customAttributeValues.AddRange(currentCustomAttributeValues);
                errorList.AddRange(currentErrorList);
                rowCount++;
            }

            return treatmentBMPsToUpload;
        }

        private static TreatmentBMP ParseRequiredAndOptionalFieldAndCreateBMP(int bmpType, string[] row,
            Dictionary<string, int> fieldsDict, int count, out List<string> errorList)
        {
            errorList = new List<string>();
            var treatmentBMP = new TreatmentBMP(string.Empty, default(int), default(int), default(int), default(bool),
                default(int), default(int));
            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs;
            var jurisdictions = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions;
            var jurisdictionNames = jurisdictions.Select(x => x.Organization.OrganizationName);
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations;
            var organizationNames = organizations.Select(x => x.OrganizationName);

            var treatmentBMPName = row[fieldsDict["BMP Name"]];
            if (treatmentBMPName.IsNullOrWhiteSpace())
            {
                errorList.Add(
                    $"BMP Name is null, empty, or just whitespaces for row: {count}");
            }

            if (treatmentBMPs.Select(x => x.TreatmentBMPName).Contains(treatmentBMPName))
            {
                errorList.Add($"A BMP by the name '{treatmentBMPName}' already exists for row: {count}");
            }

            treatmentBMP.TreatmentBMPName = treatmentBMPName;

            treatmentBMP.TreatmentBMPTypeID = bmpType;
            treatmentBMP.TreatmentBMPType =
                HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.GetTreatmentBMPType(bmpType);
          
            var treatmentBMPLatitude = row[fieldsDict["Latitude"]];
            var treatmentBMPLongitude = row[fieldsDict["Longitude"]];
            List<string> locationErrors = ParseLocation(treatmentBMPLatitude, treatmentBMPLongitude, count);
            if (locationErrors.Any())
            {
                errorList.AddRange(locationErrors);
            }
            else
            {
                treatmentBMP.LocationPoint4326 = DbGeometry.FromText(
                    $"Point ({treatmentBMPLongitude} {treatmentBMPLatitude})", CoordinateSystemHelper.WGS_1984_SRID);
                treatmentBMP.LocationPoint =
                    CoordinateSystemHelper.ProjectWebMercatorToCaliforniaStatePlaneVI(treatmentBMP.LocationPoint4326);
            }


            var treatmentBMPJurisdictionName = row[fieldsDict["Jurisdiction"]];
            if (treatmentBMPJurisdictionName.IsNullOrWhiteSpace())
            {
                errorList.Add(
                    $"BMP Jurisdiction is null, empty, or just whitespaces for row: {count}");
            }

            if (!jurisdictionNames.Contains(treatmentBMPJurisdictionName))
            {
                errorList.Add(
                    $"No Jurisdiction with name '{treatmentBMPJurisdictionName}' exists. See row: {count}");
            }
            else
            {
                treatmentBMP.StormwaterJurisdictionID = jurisdictions
                    .Single(x => x.Organization.OrganizationName == treatmentBMPJurisdictionName).StormwaterJurisdictionID;
            }

            var treatmentBMPOwner = row[fieldsDict["Owner"]];
            if (treatmentBMPOwner.IsNullOrWhiteSpace())
            {
                errorList.Add(
                    $"BMP Organization Owner Name is null, empty, or just whitespaces for row: {count}");
            }

            if (!organizationNames.Contains(treatmentBMPOwner))
            {
                errorList.Add(
                    $"No BMP Organization Owner with that name exists within our records, row: {count}");
            }
            else
            {
                treatmentBMP.OwnerOrganizationID = organizations
                    .Single((x => x.OrganizationName == treatmentBMPOwner)).OrganizationID;
            }

            //start of Optional Fields
            if (fieldsDict.ContainsKey("Year Built or Installed"))
            {
                var yearBuiltOrInstalled = row[fieldsDict["Year Built or Installed"]];
                if (!yearBuiltOrInstalled.IsNullOrWhiteSpace())
                {
                    if (!int.TryParse(yearBuiltOrInstalled, out var yearBuiltOrInstalledInt))
                    {
                        errorList.Add($"Year Built or Installed Field cannot be converted to Int at row: {count}");
                    }
                    else
                    {
                        treatmentBMP.YearBuilt = yearBuiltOrInstalledInt;
                    }
                }
                else
                {
                    treatmentBMP.YearBuilt = null;
                }
            }


            if (fieldsDict.ContainsKey("Asset ID in System of Record"))
            {
                var assetIDInSystemOfRecord = row[fieldsDict["Asset ID in System of Record"]];
                if (!assetIDInSystemOfRecord.IsNullOrWhiteSpace())
                {
                    if (assetIDInSystemOfRecord.Length > TreatmentBMP.FieldLengths.SystemOfRecordID)
                    {
                        errorList.Add(
                            $"Asset ID In System of Record was too long. It must be 100 characters or less. Current Length is {assetIDInSystemOfRecord.Length} at row: {count}");
                    }
                    else
                    {
                        treatmentBMP.SystemOfRecordID = assetIDInSystemOfRecord;
                    }
                }
            }

            if (fieldsDict.ContainsKey("Required Lifespan of Installation"))
            {
                var requiredLifespanOfInstallation = row[fieldsDict["Required Lifespan of Installation"]];
                if (!requiredLifespanOfInstallation.IsNullOrWhiteSpace())
                {
                    if (!TreatmentBMPLifespanType.All.Select(x => x.TreatmentBMPLifespanTypeDisplayName)
                        .Contains(requiredLifespanOfInstallation))
                    {
                        errorList.Add(
                            $"No Required Lifespan Of Installation by the name {requiredLifespanOfInstallation} exists within our records at row: {count}. Acceptable Values Are: {string.Join(", ", TreatmentBMPLifespanType.All.Select(x => x.TreatmentBMPLifespanTypeDisplayName))}");
                    }
                    else
                    {
                        treatmentBMP.TreatmentBMPLifespanTypeID = TreatmentBMPLifespanType.All
                            .Single(x => x.TreatmentBMPLifespanTypeDisplayName == requiredLifespanOfInstallation)
                            .TreatmentBMPLifespanTypeID;
                    }
                }

                if (fieldsDict.ContainsKey("Allowable End Date of Installation (if applicable)"))
                {
                    var allowableEndDateOfInstallation = row[fieldsDict["Allowable End Date of Installation (if applicable)"]];
                    if (allowableEndDateOfInstallation.IsNullOrWhiteSpace() && requiredLifespanOfInstallation ==
                        TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeDisplayName)
                    {
                        errorList.Add(
                            $"An end date must be provided if the 'Required Lifespan of Installation' field is set to fixed end date for row: {count}");
                    }
                    if (!allowableEndDateOfInstallation.IsNullOrWhiteSpace() && requiredLifespanOfInstallation != TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeDisplayName)
                    {
                        errorList.Add(
                            $"An end date was provided when 'Required Lifespan of Installation' field was set to {requiredLifespanOfInstallation} for row: {count}" );
                    }
                    if (requiredLifespanOfInstallation.IsNullOrWhiteSpace() &&
                             !allowableEndDateOfInstallation.IsNullOrWhiteSpace())
                    {
                        errorList.Add(
                            $"An end date was provided when 'Required Lifespan of Installation' field was set to null for row: {count}");
                    }

                    if (!allowableEndDateOfInstallation.IsNullOrWhiteSpace())
                    {
                        if (!DateTime.TryParse(allowableEndDateOfInstallation, out var allowableEndDateOfInstallationDateTime))
                        {
                            errorList.Add(
                                $"Allowable End Date of Installation can not be converted to Date Time format at row: {count}");
                        }
                        else
                        {
                            treatmentBMP.TreatmentBMPLifespanEndDate = allowableEndDateOfInstallationDateTime;
                        }
                    }
                }
            }

            if (fieldsDict.ContainsKey("Required Field Visits Per Year"))
            {
                var requiredFieldVisitsPerYear = row[fieldsDict["Required Field Visits Per Year"]];
                if (!requiredFieldVisitsPerYear.IsNullOrWhiteSpace())
                {
                    if (!int.TryParse(requiredFieldVisitsPerYear, out var requiredFieldVisitsPerYearInt))
                    {
                        errorList.Add($"Required Field Visits per Year Field can not be converted to Int at row: {count}");
                    }
                    else
                    {
                        treatmentBMP.RequiredFieldVisitsPerYear = requiredFieldVisitsPerYearInt;
                    }
                }
            }

            if (fieldsDict.ContainsKey("Required Post-Storm Field Visits Per Year"))
            {
                var requiredPostStormFieldVisitsPerYear = row[fieldsDict["Required Post-Storm Field Visits Per Year"]];
                if (!requiredPostStormFieldVisitsPerYear.IsNullOrWhiteSpace())
                {
                    if (!int.TryParse(requiredPostStormFieldVisitsPerYear, out var requiredPostStormFieldVisitsPerYearInt))
                    {
                        errorList.Add(
                            $"Required Post-Storm Field visits per year field cannot be converted to Int at row: {count}");
                    }
                    else
                    {
                        treatmentBMP.RequiredPostStormFieldVisitsPerYear =
                            requiredPostStormFieldVisitsPerYearInt;
                    }
                }
            }


            if (fieldsDict.ContainsKey("Notes"))
            {
                var notes = row[fieldsDict["Notes"]];
                if (!notes.IsNullOrWhiteSpace())
                {
                    if (notes.Length > TreatmentBMP.FieldLengths.Notes)
                    {
                        errorList.Add(
                            $"Note length is too long at and needs to be 1000 characters or less, current length is {notes.Length} at row: {count}");
                    }
                    else
                    {
                        treatmentBMP.Notes = notes;
                    }
                }
            }


            //End of Optional Fields

            var treatmentBMPTrashCaptureStatus = row[fieldsDict["Trash Capture Status"]];
            if (treatmentBMPTrashCaptureStatus.IsNullOrWhiteSpace())
            {
                errorList.Add(
                    $"Trash Capture Status is null, empty, or just whitespaces for row: {count}");
            }

            if (!TrashCaptureStatusType.All.Select(x => x.TrashCaptureStatusTypeDisplayName)
                .Contains(treatmentBMPTrashCaptureStatus))
            {
                errorList.Add($"No Trash Capture Status with that name exists in our records, row: {count}. Acceptable Values Are: {string.Join(", ",TrashCaptureStatusType.All.Select(x => x.TrashCaptureStatusTypeDisplayName))}");
            }
            else
            {
                treatmentBMP.TrashCaptureStatusTypeID = TrashCaptureStatusType.All
                    .Single(x => x.TrashCaptureStatusTypeDisplayName == treatmentBMPTrashCaptureStatus)
                    .TrashCaptureStatusTypeID;
            }


            var treatmentSizingBasics = row[fieldsDict["Sizing Basis"]];
            if (treatmentSizingBasics.IsNullOrWhiteSpace())
            {
                errorList.Add(
                    $"Sizing Basis is null, empty, or just whitespaces for row: {count}");
            }

            if (!SizingBasisType.All.Select(x => x.SizingBasisTypeDisplayName).Contains(treatmentSizingBasics))
            {
                errorList.Add(
                    $"No Sizing Basis with the name '{treatmentSizingBasics}' exists in our records, row: {count}. Acceptable Values Are: {string.Join(", ",SizingBasisType.All.Select(x => x.SizingBasisTypeDisplayName))}");
            }
            else
            {
                treatmentBMP.SizingBasisTypeID = SizingBasisType.All
                    .Single(x => x.SizingBasisTypeDisplayName == treatmentSizingBasics).SizingBasisTypeID;
            }

            return treatmentBMP;
        }

        private static List<string> ParseLocation(string treatmentBMPLatitude, string treatmentBMPLongitude, int count)
        {
            var LocationErrorList = new List<string>();

            if (treatmentBMPLatitude.IsNullOrWhiteSpace() || treatmentBMPLongitude.IsNullOrWhiteSpace())
            {
                LocationErrorList.Add(
                    $"Treatment BMP Latitude {treatmentBMPLatitude} or Longitude {treatmentBMPLongitude} is null or empty space at row: {count}");
            }
            if (!decimal.TryParse(treatmentBMPLatitude, out var treatmentBMPLatitudeDecimal))
            {
                LocationErrorList.Add(
                    $"Treatment BMP Latitude can not be converted to Decimal format at row: {count}");
            }
            if (!(treatmentBMPLatitudeDecimal <= 90 && treatmentBMPLatitudeDecimal >= -90))
            {
                LocationErrorList.Add(
                    $"Treatment BMP Latitude {treatmentBMPLatitudeDecimal} is less than -90 or greater than 90 at row: {count}");
            }
            if (!decimal.TryParse(treatmentBMPLongitude, out var treatmentBMPLongitudeDecimal))
            {
                LocationErrorList.Add(
                    $"Treatment BMP Longitude can not be converted to Decimal format at row: {count}");
            }
            if (!(treatmentBMPLongitudeDecimal <= 180 && treatmentBMPLongitudeDecimal >= -180))
            {
                LocationErrorList.Add(
                    $"Treatment BMP Longitude {treatmentBMPLongitudeDecimal} is less than -180 or greater than 180 at row: {count}");
            }

            return LocationErrorList;
        }

        private static List<CustomAttribute> ParseCustomAttributes(TreatmentBMP treatmentBMP, string[] currentRow, Dictionary<string, int> fieldsDict, List<CustomAttributeType> customAttributeTypes, int rowCount, out List<string> currentErrorList, out List<CustomAttributeValue> customAttributeValues)
        {
            currentErrorList = new List<string>();
            customAttributeValues = new List<CustomAttributeValue>();
            var customAttributes = new List<CustomAttribute>();
            foreach (var customAttributeType in customAttributeTypes)
            {
                TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType = customAttributeType.TreatmentBMPTypeCustomAttributeTypes.Single(x => x.TreatmentBMPTypeID == treatmentBMP.TreatmentBMPTypeID);
                var newCustomAttribute = new CustomAttribute(treatmentBMP, treatmentBMPTypeCustomAttributeType, treatmentBMP.TreatmentBMPType, customAttributeType);
                if (fieldsDict.ContainsKey(customAttributeType.CustomAttributeTypeName))
                {
                    var value = currentRow[fieldsDict[customAttributeType.CustomAttributeTypeName]];

                    var customAttributeDataType = customAttributeType.CustomAttributeDataType.CustomAttributeDataTypeName;

                    if (customAttributeDataType.IsInt() && !int.TryParse(value, out var valueInt))
                    {
                        currentErrorList.Add($"{customAttributeType.CustomAttributeTypeName} field can not be converted to Integer at row: {rowCount}");
                    }
                    else if (customAttributeDataType.IsDecimal() && !decimal.TryParse(value, out var valueDecimal))
                    {
                        currentErrorList.Add($"{customAttributeType.CustomAttributeTypeName} field can not be converted to Decimal at row: {rowCount}");
                    }
                    else if (customAttributeDataType.IsDateTime() && !DateTime.TryParse(value, out var valueDateTime))
                    {
                        currentErrorList.Add(
                            $"{customAttributeType.CustomAttributeTypeName} field can not be converted to Date Time at row: {rowCount}");
                    }
                    else
                    {
                        customAttributeValues.Add(new CustomAttributeValue(newCustomAttribute, value));
                    }
                }
                customAttributes.Add(newCustomAttribute);
            }
            return customAttributes;
        }

        private static TreatmentBMPModelingAttribute ParseModelingAttributes(TreatmentBMP treatmentBMP, string[] currentRow, Dictionary<string, int> fieldsDict, List<string> availableModelingAttributesForType, int rowCount, out List<string> currentErrorList, out List<TreatmentBMPOperationMonth> treatmentBMPOperationMonths)
        {
            currentErrorList = new List<string>();
            var newModelingAttribute = new TreatmentBMPModelingAttribute(treatmentBMP);
            newModelingAttribute.RoutingConfigurationID = RoutingConfiguration.Online.RoutingConfigurationID;
            treatmentBMPOperationMonths = new List<TreatmentBMPOperationMonth>();
            foreach (var attribute in availableModelingAttributesForType)
            {
                if (fieldsDict.ContainsKey(attribute) && !String.IsNullOrWhiteSpace(currentRow[fieldsDict[attribute]]))
                {
                    var modelingProperty = GetAppropriateModelingAttributeColumnName(attribute);
                    var value = currentRow[fieldsDict[attribute]];
                    if (modelingProperty == FieldDefinition.MonthsOfOperation.FieldDefinitionName)
                    {
                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                        var months = value.Split(',');
                        foreach (var month in months)
                        {
                            var titleCasedmonth = textInfo.ToTitleCase(month.Trim().ToLower());
                            if (!monthsToInt.ContainsKey(titleCasedmonth))
                            {
                                currentErrorList.Add($"'{month}' is an invalid entry for {attribute}. Please check the month entries, ensuring that each month is separated by a comma, at row: {rowCount}. \n" +
                                                     $"Acceptable values are: {string.Join(", ", monthsToInt.Select(x => x.Key))}");
                            }
                            else
                            {
                                treatmentBMPOperationMonths.Add(new TreatmentBMPOperationMonth(treatmentBMP, monthsToInt[titleCasedmonth]));
                            }
                        }
                    }
                    else
                    {
                        var propertyToChange = newModelingAttribute.GetType().GetProperty(modelingProperty);
                        var propType = propertyToChange.Name == "UnderlyingHydrologicSoilGroupID" ? typeof(UnderlyingHydrologicSoilGroup) :
                                       propertyToChange.Name == "TimeOfConcentrationID" ? typeof(TimeOfConcentration) :
                                       propertyToChange.Name == "RoutingConfigurationID" ? typeof(RoutingConfiguration) :
                                       propertyToChange.PropertyType;

                        if (propType.IsGenericType &&
                            propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propType = propType.GetGenericArguments()[0];
                        }

                        if (propType == typeof(int))
                        {
                            if (int.TryParse(value, out var valueInt))
                            {
                                propertyToChange.SetValue(newModelingAttribute,
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
                                propertyToChange.SetValue(newModelingAttribute,
                                    valueDouble);
                            }
                            else
                            {
                                currentErrorList.Add($"{attribute} field can not be converted to Double at row: {rowCount}");
                            }
                            
                        }
                        else if (propType == typeof(UnderlyingHydrologicSoilGroup))
                        {
                            var underlyingHydrologicSoilGroup = UnderlyingHydrologicSoilGroup.All
                                .SingleOrDefault(x => x.UnderlyingHydrologicSoilGroupDisplayName == value);
                            if (underlyingHydrologicSoilGroup == null ||
                                (treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType.ToEnum == TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain &&
                                underlyingHydrologicSoilGroup.ToEnum == UnderlyingHydrologicSoilGroupEnum.Liner))
                            {
                                currentErrorList.Add($"{value} is not a valid {attribute} entry for Treatment BMPs of {treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName} type at row: {rowCount}." +
                                                     $"Acceptable values are :{string.Join(", ", UnderlyingHydrologicSoilGroup.All.Where(x => treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType.ToEnum == TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain ? x.UnderlyingHydrologicSoilGroupDisplayName != "Liner" : true).Select(x => x.UnderlyingHydrologicSoilGroupDisplayName))}");
                            }
                            else
                            {
                                propertyToChange.SetValue(newModelingAttribute,
                                    underlyingHydrologicSoilGroup.UnderlyingHydrologicSoilGroupID);
                            }
                        }
                        else if (propType == typeof(TimeOfConcentration))
                        {
                            var timeOfConcentration =
                                TimeOfConcentration.All.SingleOrDefault(x => x.TimeOfConcentrationDisplayName == value);
                            if (timeOfConcentration != null)
                            {
                                propertyToChange.SetValue(newModelingAttribute, timeOfConcentration.TimeOfConcentrationID);
                            }
                            else
                            {
                                currentErrorList.Add($"{value} is not a valid {attribute} entry at row: {rowCount}." +
                                                     $"Acceptable values are:{string.Join(", ", TimeOfConcentration.All.Select(x => x.TimeOfConcentrationDisplayName))}");
                            }
                        }
                        else if (propType == typeof(RoutingConfiguration))
                        {
                            var routingConfiguration =
                                RoutingConfiguration.All.SingleOrDefault(
                                    x => x.RoutingConfigurationDisplayName == value);
                            if (routingConfiguration != null)
                            {
                                propertyToChange.SetValue(newModelingAttribute, routingConfiguration.RoutingConfigurationID);
                            }
                            else
                            {
                                currentErrorList.Add($"{value} is not a valid {attribute} entry at row: {rowCount}." +
                                                     $"Accetpable values are:{string.Join(", ", RoutingConfiguration.All.Select(x => x.RoutingConfigurationDisplayName))}");
                            }
                        }
                        else
                        {
                            currentErrorList.Add($"{attribute} is not a valid modeling parameter entry at row: {rowCount}");
                        }
                    }
                    
                }
            }

            if (newModelingAttribute.RoutingConfigurationID == RoutingConfiguration.Offline.RoutingConfigurationID &&
                newModelingAttribute.DiversionRate == null)
            {
                currentErrorList.Add($"The modeling attribute 'Diversion Rate' is required when the Routing Configuration is set to 'Offline'. Please adjust entry at row: {rowCount}");
            }
            return newModelingAttribute;
        }


        private static Dictionary<string, int> ValidateHeader(string[] row, List<string> requiredFields,
            List<string> optionalFields, List<string> availableModelingAttributes, List<string> customAttributes, int bmpType, out List<string> errorList)
        {
            errorList = new List<string>();
            var fieldsDict = new Dictionary<string, int>();

            string temp;
            for (int fieldIndex = 0; fieldIndex < row.Length; fieldIndex++)
            {
                temp = row[fieldIndex].Trim();
                if (!temp.IsNullOrWhiteSpace())
                {
                    fieldsDict.Add(temp, fieldIndex);
                }
            }

            List<string> headers = fieldsDict.Keys.ToList();
            IEnumerable<string> requiredFieldDifference = requiredFields.Except(headers);
            IEnumerable<string> optionalFieldDifference = headers.Except(requiredFields).Except(optionalFields);
            IEnumerable<string> modelingAttributesDifference =
                optionalFieldDifference.Except(availableModelingAttributes);
            IEnumerable<string> customAttributesDifference = modelingAttributesDifference.ToList().Except(customAttributes);

            if (requiredFieldDifference.Any())
            {
                errorList.Add("One or more required headers have not been provided. Required Fields are: " +
                              string.Join(", ", requiredFieldDifference.ToList()));
            }

            if (customAttributesDifference.Any())
            {
                errorList.Add($"The provided fields '{string.Join(", ", customAttributesDifference.ToList())}' did not match a property, modeling attribute, or custom attribute of the BMP type ‘{HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.GetTreatmentBMPType(bmpType).TreatmentBMPModelingType.TreatmentBMPModelingTypeDisplayName}’");
            }

            return fieldsDict;
        }

        public static string GetAppropriateModelingAttributeColumnName(string fieldDefinition)
        {
            string returnVal;
            if (fieldDefinition == FieldDefinition.UnderlyingHydrologicSoilGroupHSG.FieldDefinitionDisplayName)
            {
                returnVal = "UnderlyingHydrologicSoilGroupID";
            }
            else if (fieldDefinition == FieldDefinition.DesignResidenceTimeForPermanentPool.FieldDefinitionDisplayName)
            {
                returnVal = "DesignResidenceTimeforPermanentPool";
            }
            else if (fieldDefinition == FieldDefinition.DrawdownTimeForWQDetentionVolume.FieldDefinitionDisplayName)
            {
                returnVal = "DrawdownTimeforWQDetentionVolume";
            }
            else if (fieldDefinition == FieldDefinition.PermanentPoolOrWetlandVolume.FieldDefinitionDisplayName)
            {
                returnVal = "PermanentPoolorWetlandVolume";
            }
            else if (fieldDefinition == FieldDefinition.TimeOfConcentration.FieldDefinitionDisplayName)
            {
                returnVal = "TimeOfConcentrationID";
            }
            else if (fieldDefinition == FieldDefinition.RoutingConfiguration.FieldDefinitionDisplayName)
            {
                returnVal = "RoutingConfigurationID";
            }
            else
            {
                returnVal = FieldDefinition.All.Single(x => x.FieldDefinitionDisplayName == fieldDefinition)
                    .FieldDefinitionName;
            }

            return returnVal;
        }

        public static List<string> GetAvailableModelingAttributes(int bmpType)
        {
            List<string> returnList = new List<string>();
            var modelingType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.GetTreatmentBMPType(bmpType).TreatmentBMPModelingType;
            if (modelingType != null)
            {
                returnList.Add(FieldDefinition.TimeOfConcentration.FieldDefinitionDisplayName);

                switch (modelingType.ToEnum)
                {
                    case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                        returnList.AddRange(new List<string>()
                        {
                            FieldDefinition.RoutingConfiguration.FieldDefinitionDisplayName,
                            FieldDefinition.DiversionRate.FieldDefinitionDisplayName,
                            FieldDefinition.TotalEffectiveBMPVolume.FieldDefinitionDisplayName,
                            FieldDefinition.StorageVolumeBelowLowestOutletElevation.FieldDefinitionDisplayName,
                            FieldDefinition.MediaBedFootprint.FieldDefinitionDisplayName,
                            FieldDefinition.DesignMediaFiltrationRate.FieldDefinitionDisplayName,
                            FieldDefinition.UnderlyingHydrologicSoilGroupHSG.FieldDefinitionDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
                    case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                    case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                    case TreatmentBMPModelingTypeEnum.PermeablePavement:
                    case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                        returnList.AddRange(new List<string>()
                        {
                            FieldDefinition.RoutingConfiguration.FieldDefinitionDisplayName,
                            FieldDefinition.DiversionRate.FieldDefinitionDisplayName,
                            FieldDefinition.TotalEffectiveBMPVolume.FieldDefinitionDisplayName,
                            FieldDefinition.InfiltrationSurfaceArea.FieldDefinitionDisplayName,
                            FieldDefinition.UnderlyingInfiltrationRate.FieldDefinitionDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
                    case TreatmentBMPModelingTypeEnum.SandFilters:
                        returnList.AddRange(new List<string>()
                        {
                            FieldDefinition.RoutingConfiguration.FieldDefinitionDisplayName,
                            FieldDefinition.DiversionRate.FieldDefinitionDisplayName,
                            FieldDefinition.TotalEffectiveBMPVolume.FieldDefinitionDisplayName,
                            FieldDefinition.StorageVolumeBelowLowestOutletElevation.FieldDefinitionDisplayName,
                            FieldDefinition.DesignMediaFiltrationRate.FieldDefinitionDisplayName,
                            FieldDefinition.MediaBedFootprint.FieldDefinitionDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                        returnList.AddRange(new List<string>()
                        {
                            FieldDefinition.RoutingConfiguration.FieldDefinitionDisplayName,
                            FieldDefinition.DiversionRate.FieldDefinitionDisplayName,
                            FieldDefinition.TotalEffectiveBMPVolume.FieldDefinitionDisplayName,
                            FieldDefinition.WinterHarvestedWaterDemand.FieldDefinitionDisplayName,
                            FieldDefinition.SummerHarvestedWaterDemand.FieldDefinitionDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.ConstructedWetland:
                    case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                        returnList.AddRange(new List<string>()
                        {
                            FieldDefinition.RoutingConfiguration.FieldDefinitionDisplayName,
                            FieldDefinition.DiversionRate.FieldDefinitionDisplayName,
                            FieldDefinition.PermanentPoolOrWetlandVolume.FieldDefinitionDisplayName,
                            FieldDefinition.DesignResidenceTimeForPermanentPool.FieldDefinitionDisplayName,
                            FieldDefinition.WaterQualityDetentionVolume.FieldDefinitionDisplayName,
                            FieldDefinition.DrawdownTimeForWQDetentionVolume.FieldDefinitionDisplayName,
                            FieldDefinition.WinterHarvestedWaterDemand.FieldDefinitionDisplayName,
                            FieldDefinition.SummerHarvestedWaterDemand.FieldDefinitionDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                    case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                    case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinition.RoutingConfiguration.FieldDefinitionDisplayName,
                            FieldDefinition.DiversionRate.FieldDefinitionDisplayName,
                            FieldDefinition.TotalEffectiveBMPVolume.FieldDefinitionDisplayName,
                            FieldDefinition.StorageVolumeBelowLowestOutletElevation.FieldDefinitionDisplayName,
                            FieldDefinition.EffectiveFootprint.FieldDefinitionDisplayName,
                            FieldDefinition.DrawdownTimeForWQDetentionVolume.FieldDefinitionDisplayName,
                            FieldDefinition.UnderlyingHydrologicSoilGroupHSG.FieldDefinitionDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinition.DesignDryWeatherTreatmentCapacity.FieldDefinitionDisplayName,
                            FieldDefinition.AverageTreatmentFlowrate.FieldDefinitionDisplayName,
                            FieldDefinition.MonthsOfOperation.FieldDefinitionDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.Drywell:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinition.RoutingConfiguration.FieldDefinitionDisplayName,
                            FieldDefinition.DiversionRate.FieldDefinitionDisplayName,
                            FieldDefinition.TotalEffectiveDrywellBMPVolume.FieldDefinitionDisplayName,
                            FieldDefinition.InfiltrationDischargeRate.FieldDefinitionDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
                    case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
                    case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinition.TreatmentRate.FieldDefinitionDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinition.DesignLowFlowDiversionCapacity.FieldDefinitionDisplayName,
                            FieldDefinition.AverageDivertedFlowrate.FieldDefinitionDisplayName,
                            FieldDefinition.MonthsOfOperation.FieldDefinitionDisplayName
                        });
                        break;
                    case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                    case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                        returnList.AddRange(new List<string>
                        {
                            FieldDefinition.RoutingConfiguration.FieldDefinitionDisplayName,
                            FieldDefinition.DiversionRate.FieldDefinitionDisplayName,
                            FieldDefinition.TreatmentRate.FieldDefinitionDisplayName,
                            FieldDefinition.WettedFootprint.FieldDefinitionDisplayName,
                            FieldDefinition.EffectiveRetentionDepth.FieldDefinitionDisplayName,
                            FieldDefinition.UnderlyingHydrologicSoilGroupHSG.FieldDefinitionDisplayName
                        });
                        break;
                }
            }
            return returnList;
        }

        private static readonly Dictionary<string, int> monthsToInt = new Dictionary<string, int>()
        {
            { "Jan", 1 },
            { "Feb", 2 },
            { "Mar", 3 },
            { "Apr", 4 },
            { "May", 5 },
            { "Jun", 6 },
            { "Jul", 7 },
            { "Aug", 8 },
            { "Sep", 9 },
            { "Oct", 10 },
            { "Nov", 11 },
            { "Dec", 12 }
        };
    }

}