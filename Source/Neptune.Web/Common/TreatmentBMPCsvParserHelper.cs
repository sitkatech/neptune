using System;
using LtInfo.Common.DesignByContract;
using Microsoft.VisualBasic.FileIO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    public static class TreatmentBMPCsvParserHelper
    {
        public static List<TreatmentBMP> CSVUpload(Stream fileStream, int bmpType, out List<string> errorList)
        {
            errorList = new List<string>();
            var StreamReader = new StreamReader(fileStream);
            var parser = new TextFieldParser(StreamReader);


            return ParseBmpRowsFromCsv(parser, bmpType, out errorList);
        }

        public static List<TreatmentBMP> CSVUpload(string fileStream, int bmpType, out List<string> errorList)
        {
            errorList = new List<string>();
            var stringReader = new StringReader(fileStream);
            var parser = new TextFieldParser(stringReader);

            return ParseBmpRowsFromCsv(parser, bmpType, out errorList);
        }

        public static List<TreatmentBMP> ParseBmpRowsFromCsv(TextFieldParser parser, int bmpType, out List<string> errorList)
        {
            parser.SetDelimiters(",");
            errorList = new List<string>();
            var treatmentBMPsToUpload = new List<TreatmentBMP>();
            var fieldsDict = new Dictionary<string, int>();

            var requiredFields = new List<string> { "Jurisdiction", "BMP Name", "Latitude", "Longitude", "Sizing Basis", "Trash Capture Status", "Owner" };
            var optionalFields = new List<string> {"Year Built or Installed","Asset ID in System of Record", "Required Lifespan of Installation",
                "Allowable End Date of Installation (if applicable)", "Required Field Visits Per Year", "Required Post-Storm Field Visits Per Year","Notes"};
            var customAttributes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeCustomAttributeTypes
                .Where(x => x.TreatmentBMPTypeID == bmpType).Select(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();

            try
            {
                var header = parser.ReadFields();
                fieldsDict = ValidateHeader(header, requiredFields, optionalFields, customAttributes, out errorList);
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
            var rowCount = 0;
            while (!parser.EndOfData)
            {
                var currentRow = parser.ReadFields();
                var currentErrorList = new List<string>();

                var currentBMP = ParseRequiredAndOptionalFieldAndCreateBMP(bmpType, currentRow, fieldsDict, rowCount, out currentErrorList);
                
                //ParseCustomAttributes(bmpType, currentRow, customAttributeDictionary, rowCount, out currentErrorList);

                if (treatmentBMPsToUpload.Select(x => x.TreatmentBMPName).Contains(currentBMP.TreatmentBMPName))
                {
                    errorList.Add(
                        $"The BMP with Name '{currentBMP.TreatmentBMPName}' was already added in this upload, duplicate name is found at row: {rowCount}");
                }

                treatmentBMPsToUpload.Add(currentBMP);
                errorList.AddRange(currentErrorList);

                rowCount++;
            }
            return treatmentBMPsToUpload;
        }

        private static TreatmentBMP ParseRequiredAndOptionalFieldAndCreateBMP(int bmpType, string[] row, Dictionary<string, int> fieldsDict, int count, out List<string> errorList)
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
            else
            {
                treatmentBMP.TreatmentBMPName = treatmentBMPName;
            }

            treatmentBMP.TreatmentBMPTypeID = bmpType;

            var treatmentBMPLatitude = row[fieldsDict["Latitude"]];
            var treatmentBMPLongitude = row[fieldsDict["Longitude"]];
            if (treatmentBMPLatitude.IsNullOrWhiteSpace() || treatmentBMPLongitude.IsNullOrWhiteSpace())
            {
                errorList.Add(
                    $"Treatment BMP Latitude {treatmentBMPLatitude} or Longitude {treatmentBMPLongitude} is null or empty space at row: {count}");
            }
            else
            {
                treatmentBMP.LocationPoint = DbGeometry.FromText(
                    $"Point ({treatmentBMPLatitude} {treatmentBMPLongitude})", MapInitJson.CoordinateSystemId);
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
                    .First((x => x.Organization.OrganizationName == treatmentBMPJurisdictionName)).OrganizationID;
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
                    .First((x => x.OrganizationName == treatmentBMPOwner)).OrganizationID;
            }

            //start of Optional Fields
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

            var requiredLifespanOfInstallation = row[fieldsDict["Required Lifespan of Installation"]];
            if (!requiredLifespanOfInstallation.IsNullOrWhiteSpace())
            {
                if (!TreatmentBMPLifespanType.All.Select(x => x.TreatmentBMPLifespanTypeDisplayName)
                    .Contains(requiredLifespanOfInstallation))
                {
                    errorList.Add(
                        $"No Required Lifespan Of Installation by the name {requiredLifespanOfInstallation} exists within our records at row: {count}");
                }
                else
                {
                    treatmentBMP.TreatmentBMPLifespanTypeID = TreatmentBMPLifespanType.All
                        .First(x => x.TreatmentBMPLifespanTypeDisplayName == requiredLifespanOfInstallation)
                        .TreatmentBMPLifespanTypeID;
                }
            }


            var allowableEndDateOfInstallation = row[fieldsDict["Allowable End Date of Installation (if applicable)"]];
            if (allowableEndDateOfInstallation.IsNullOrWhiteSpace() && requiredLifespanOfInstallation ==
                TreatmentBMPLifespanType.FixedEndDate.TreatmentBMPLifespanTypeDisplayName)
            {
                errorList.Add(
                    $"An end date must be provided if the 'Required Lifespan of Installation' field is set to fixed end date for row: {count}");
            }
            else if (!allowableEndDateOfInstallation.IsNullOrWhiteSpace() &&
                     (requiredLifespanOfInstallation ==
                      TreatmentBMPLifespanType.Unspecified.TreatmentBMPLifespanTypeDisplayName ||
                      requiredLifespanOfInstallation ==
                      TreatmentBMPLifespanType.Perpetuity.TreatmentBMPLifespanTypeDisplayName))
            {
                errorList.Add(
                    $"An end date was provided when 'Required Lifespan of Installation' field was set to {requiredLifespanOfInstallation} for row: {count}");
            }
            else if (!requiredLifespanOfInstallation.IsNullOrWhiteSpace() &&
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

            var requiredPostStormFieldVisitsPerYear = row[fieldsDict["Required Post-Storm Field Visits Per Year"]];
            if (!requiredPostStormFieldVisitsPerYear.IsNullOrWhiteSpace())
            {
                if (!int.TryParse(requiredPostStormFieldVisitsPerYear, out var requiredPostStormFieldVisitsPerYearInt))
                {
                    errorList.Add(
                        $"Required post-storm field visits per year field cannot be converted to Int at row: {count}");
                }
                else
                {
                    treatmentBMP.RequiredPostStormFieldVisitsPerYear =
                        requiredPostStormFieldVisitsPerYearInt;
                }
            }


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

            //End of Optional Fields

            var treatmentBMPTrashCaptureStatus = row[fieldsDict["Trash Capture Status"]];
            if (treatmentBMPTrashCaptureStatus.IsNullOrWhiteSpace())
            {
                errorList.Add(
                    "Trash Capture Status is null, empty, or just whitespaces for row: " + count);
            }

            if (!TrashCaptureStatusType.All.Select(x => x.TrashCaptureStatusTypeDisplayName)
                .Contains(treatmentBMPTrashCaptureStatus))
            {
                errorList.Add("No Trash Capture Status with that name exists in our records, row: " + count);
            }
            else
            {
                treatmentBMP.TrashCaptureStatusTypeID = TrashCaptureStatusType.All
                    .First(x => x.TrashCaptureStatusTypeDisplayName == treatmentBMPTrashCaptureStatus)
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
                    $"No Sizing Basis with the name '{treatmentSizingBasics}' exists in our records, row: {count}");
            }
            else
            {
                treatmentBMP.SizingBasisTypeID = SizingBasisType.All
                    .First(x => x.SizingBasisTypeDisplayName == treatmentSizingBasics).SizingBasisTypeID;
            }
            
       
            return treatmentBMP;
        }

        private static Dictionary<string, int> BuildCustomAttributeDictionary(int bmpType, string[] optinalFieldDifference)
        {
            var customAttributesDict = new Dictionary<string, int>();

            IEnumerable<string> customAttributeDifference = optionalFieldDifference.Except();

            return customAttributesDict;
        }

        private static Dictionary<string, int> ValidateHeader(string[] row, List<string> requiredFields,
            List<string> optionalFields, List<string> customAttributes, out List<string> errorList)
        {
            errorList = new List<string>();
            var fieldsDict = new Dictionary<string, int>();


            for (int fieldIndex = 0; fieldIndex < row.Length; fieldIndex++)
            {
                fieldsDict.Add(row[fieldIndex].Trim(), fieldIndex);
            }

            List<string> headers = fieldsDict.Keys.ToList();
            IEnumerable<string> requiredFieldDifference = requiredFields.Except(headers);
            IEnumerable<string> optionalFieldDifference = headers.Except(requiredFields).Except(optionalFields);
            IEnumerable<string> customAttributesDifference = optionalFieldDifference.ToList().Except(customAttributes);

            if (requiredFieldDifference.Any())
            {
                errorList.Add("One or more required headers have not been provided. Required Fields are: " +
                              string.Join(", ", requiredFields));
            }

            if (optionalFieldDifference.Any())
            {
                errorList.Add(
                    "Some of the fields provided do not match the list of accepted optional fields. Optional Fields are: " +
                    string.Join(", ", optionalFields));
            }else if (customAttributesDifference.Any())
            {
                errorList.Add("");
            }

            //customAttributeDictionary = BuildCustomAttributeDictionary(bmpType, header);


            return fieldsDict;
        }
    }

    [TestFixture]
    public class TreatmentBMPCsvParserHelperTest
    {

        [Test]
        public void TestInvalidColumns()
        {
            var csv = @"BMP Name,Jurisdiction,,Latitude,Longitude,BMP Type,Trash Capture Status,Sizing Basis,Yo";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("One or more required headers have not been provided, Required Fields are: "));
            errorList.Any(x => x.Contains("Some of the fields provided are not acceptable optional fields or are misspelled required fields. Optional Fields are: "));
        }

        [Test]
        public void TestValidColumns()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("One or more required headers have not been provided, Required Fields are: "));
            errorList.Any(x => !x.Contains("Some of the fields provided are not acceptable optional fields or are misspelled required fields. Optional Fields are: "));
        }

        [Test]
        public void TestBMPNameNull()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("BMP Name is null, empty, or just whitespaces for row: "));
        }

        [Test]
        public void TestBMPNameExists()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Test,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("A BMP by the name,"));

        }

        [Test]
        public void TestBMPLatitudeNull()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("Treatment BMP Latitude "));
        }

        [Test]
        public void TestBMPLongitudeNull()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("Treatment BMP Latitude"));
        }

        [Test]
        public void TestBMPLocationGood()
        {
            var csv =
                @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Treatment BMP Latitude"));
            Assert.That(treatmentBmpUploadSimples[0].LocationPoint.ToString(), Is.EqualTo($"SRID={MapInitJson.CoordinateSystemId};POINT (30 10)"));
        }

        [Test]
        public void TestJurisdictionNameNull()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("BMP Jurisdiction is null, empty, or just whitespaces for row: "));
        }

        [Test]
        public void TestJurisdictionNameExistsBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Grou,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("No Jurisdicition with that name exists within our records, row: "));
        }

        [Test]
        public void TestJurisdictionNameExistsGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,City of Brea,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("No Jurisdicition with that name exists within our records, row: "));
            errorList.Any(x => !x.Contains("BMP Jurisdiction is null, empty, or just whitespaces for row: "));
            Assert.That(treatmentBmpUploadSimples[0].StormwaterJurisdictionID, Is.EqualTo(5));
        }

        [Test]
        public void TestOrganizationOwnerNameNull()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("BMP Organization Owner Name is null, empty, or just whitespaces for row:"));
        }

        [Test]
        public void TestOrganizationOwnerNameExistsBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Grou,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("No BMP Organization Owner with that name exists within our records, row: "));
        }

        [Test]
        public void TestOrganizationOwnerExistsGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,City of Brea,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("BMP Organization Owner Name is null, empty, or just whitespaces for row:"));
            errorList.Any(x => !x.Contains("No BMP Organization Owner with that name exists within our records, row: "));
            Assert.That(treatmentBmpUploadSimples[0].StormwaterJurisdictionID, Is.EqualTo(5));
        }

        //Begin Optional Field Tests

        [Test]
        public void TestYearBuiltIntParseBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,AB34,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("Year Built or Installed Field cannot be converted to Int at row"));
        }
        [Test]
        public void TestYearBuiltIntParseGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Year Built or Installed Field cannot be converted to Int at row"));
            Assert.That(treatmentBmpUploadSimples[0].YearBuilt, Is.EqualTo(2008));
        }

        [Test]
        public void TestYearBuiltIntAcceptsNullGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Year Built or Installed Field cannot be converted to Int at row"));
            Assert.That(treatmentBmpUploadSimples[0].YearBuilt, Is.EqualTo(null));
        }

        [Test]
        public void TestAssetIdInSystemOfRecordsBad()
        {
            string sysID = "";
            for (int i = 0; i < TreatmentBMP.FieldLengths.SystemOfRecordID + 2; i++)
            {
                sysID += "a";
            }

            var csv = $@"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,{sysID},Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("Asset ID In System of Record is too long at row,"));
        }

        [Test]
        public void TestAssetIdInSystemOfRecordsGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Asset ID In System of Record is too long at row, "));
            Assert.That(treatmentBmpUploadSimples[0].SystemOfRecordID, Is.EqualTo("ABCD"));
        }

        [Test]
        public void TestAssetIdInSystemOfRecordsNullGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Asset ID In System of Record is too long at row, "));
            Assert.That(treatmentBmpUploadSimples[0].SystemOfRecordID, Is.EqualTo(null));
        }

        [Test]
        public void TestRequiredLifespanOfInstallationBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpet,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("No Required Lifespan Of Installation by the name"));
        }

        [Test]
        public void TestRequiredLifespanOfInstallationGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("No Required Lifespan Of Installation by the name"));
            Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanTypeID, Is.EqualTo(2));
        }

        [Test]
        public void TestRequiredLifespanOfInstallationNullGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("No Required Lifespan Of Installation by the name"));
            Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanTypeID, Is.EqualTo(null));
        }

        [Test]
        public void TestAllowableEndDateOfInstallationBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("Allowable End Date of Installation can not be converted to Date Time format at row"));
        }

        [Test]
        public void TestAllowableEndDateOfInstallationTypeIsFixedEndDateButDateNullBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Fixed End Date,,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("An end date must be provided if the 'Required Lifespan of Installation' field is set to fixed end date"));
        }

        [Test]
        public void TestAllowableEndDateOfInstallationTypeIsFixedEndDateButDateNotNullGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Fixed End Date,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("An end date must be provided if the 'Required Lifespan of Installation' field is set to fixed end date"));
            Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(DateTime.Parse("11/12/2022")));
        }

        [Test]
        public void TestAllowableEndDateOfInstallationTypeIsNotFixedEndDateButDateNotNullBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to Perpetuity/Life of Project"));
        }

        [Test]
        public void TestAllowableEndDateOfInstallationTypeIsNotFixedEndDateButDateNullGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to Perpetuity/Life of Project"));
            Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(null));
        }

        [Test]
        public void TestAllowableEndDateOfInstallationTypeIsNullButDateNotNullBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to null"));
        }

        [Test]
        public void TestAllowableEndDateOfInstallationTypeIsNullButDateNullGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,,,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to null"));
            Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(null));
        }

        [Test]
        public void TestAllowableEndDateOfInstallationGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Fixed End Date,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Allowable End Date of Installation can not be converted to Date Time format at row"));
            Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(DateTime.Parse("11/12/2022")));
        }

        [Test]
        public void TestAllowableEndDateOfInstallationNullGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Allowable End Date of Installation can not be converted to Date Time format at row"));
            Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(null));
        }

        [Test]
        public void TestRequiredFieldVisitsPerYearBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,Ab5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("Required Field Vists per Year Field can not be converted to Int at row"));
        }

        [Test]
        public void TestRequiredFieldVisitsPerYearGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Required Field Vists per Year Field can not be converted to Int at row"));
            Assert.That(treatmentBmpUploadSimples[0].RequiredFieldVisitsPerYear, Is.EqualTo(5));
        }

        [Test]
        public void TestRequiredFieldVisitsPerYearNullGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Required Field Vists per Year Field can not be converted to Int at row"));
            Assert.That(treatmentBmpUploadSimples[0].RequiredFieldVisitsPerYear, Is.EqualTo(null));
        }

        [Test]
        public void TestRequiredPostStormFieldVisitsPerYearBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,AB6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("Required post-storm field vists per year field cannot be converted to Int at row"));
        }

        [Test]
        public void TestRequiredPostStormFieldVisitsPerYearGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Required post-storm field vists per year field cannot be converted to Int at row"));
            Assert.That(treatmentBmpUploadSimples[0].RequiredPostStormFieldVisitsPerYear, Is.EqualTo(6));
        }

        [Test]
        public void TestRequiredPostStormFieldVisitsPerYearNullGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Required post-storm field vists per year field cannot be converted to Int at row"));
            Assert.That(treatmentBmpUploadSimples[0].RequiredPostStormFieldVisitsPerYear, Is.EqualTo(null));
        }

        [Test]
        public void TestNotesBad()
        {
            string note = "";
            for (int i = 0; i < TreatmentBMP.FieldLengths.Notes + 2; i++)
            {
                note += "a";
            }

            var csv = $@"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,AB6,{note},Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("Note length is too long at row"));
        }

        [Test]
        public void TestNotesGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Note length is too long at row"));
            Assert.That(treatmentBmpUploadSimples[0].Notes, Is.EqualTo("Happy"));
        }

        [Test]
        public void TestNotesNullGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Note length is too long at row"));
            Assert.That(treatmentBmpUploadSimples[0].Notes, Is.EqualTo(null));
        }

        //End Optional Field Tests
        [Test]
        public void TestTrashCaptureStatusExistsNull()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,null,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("No Required Lifespan Of Installation by the name"));
            Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanTypeID, Is.EqualTo(null));
        }

        [Test]
        public void TestTrashCaptureStatusExistsBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,4,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("No Trash Capture Status with that name exists in our records, row: "));
        }

        [Test]
        public void TestTrashCaptureStatusExistsGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("Trash Capture Status is null, empty, or just whitespaces for row: "));
            errorList.Any(x => !x.Contains("No Trash Capture Status with that name exists in our records, row: "));
            Assert.That(treatmentBmpUploadSimples[0].TrashCaptureStatusTypeID, Is.EqualTo(1));
        }


        [Test]
        public void TestSizingBasicsExistsNull()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("Sizing Basis is null, empty, or just whitespaces for row: "));
        }

        [Test]
        public void TestSizingBasicsExistsBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,4";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("No Sizing Basic with the name "));
        }

        [Test]
        public void TestSizingBasicsExistsGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => !x.Contains("No Sizing Basic with the name "));
            errorList.Any(x => !x.Contains("Sizing Basis is null, empty, or just whitespaces for row: "));
            Assert.That(treatmentBmpUploadSimples[0].SizingBasisTypeID, Is.EqualTo(4));
        }


        [Test]
        public void UploadListPopulatesGood()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided
John,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("A BMP by the name,"));
        }

        [Test]
        public void UploadListPopulatesBad()
        {
            var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,,Full,Not Provided";
            List<string> errorList;
            var bmpType = 17;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList);
            errorList.Any(x => x.Contains("Sizing Basis is null, empty, or just whitespaces for row: "));
            errorList.Any(x => x.Contains("A BMP by the name,"));
        }
    }

}