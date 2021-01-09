using System;
using System.Linq;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Models;
using NUnit.Framework;

namespace Neptune.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        [TestFixture]
        public class TreatmentBMPCsvParserHelperTest
        {

            [Test]
            public void TestInvalidColumns()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Jurisdiction,,Latitude,Longitude,BMP Type,Trash Capture Status,Sizing Basis,Yo";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("One or more required headers have not been provided. Required Fields are: ")), Is.True, "Expected error about missing required fields in header");
                Assert.That(errorList.Any(x => x.Contains("did not match a property, modeling attribute, or custom attribute of the BMP type '")), Is.True, "Expected error about misspelled headers");
            }

            [Test]
            public void TestValidColumns()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(!errorList.Any(), Is.True, "Should be a valid upload so no error messages expected");
            }

            [Test]
            public void TestBMPNameNull()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("BMP Name is null, empty, or just whitespaces for row: ")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestBMPNameExistsAndMatchingJurisdictionAndType()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Test,30,10,County of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,Not Provided,";
                const int bmpType = 35;
                var treatmentBmps = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(!errorList.Any(), Is.True, "Expected an error message");
                Assert.That(treatmentBmps.Count, Is.EqualTo(1), "Expected only one treatment BMP");
                Assert.That(treatmentBmps[0].TreatmentBMPID, Is.GreaterThan(0), "Treatment BMP ID should be > 0 since it is an existing one");
            }

            [Test]
            public void TestBMPNameExistsAndNonMatchingType()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Test,30,10,County of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,Not Provided,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains(", which does not match the uploaded Type ")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestBMPLatitudeNull()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Treatment BMP Latitude ")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestBMPLatitudeValueNotDecimalParseable()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,AB120,120,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Treatment BMP Latitude can not be converted to Decimal format at row: ")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestBMPLatitudeValueNotAcceptableDecimalValue()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,95,120,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Treatment BMP Latitude 95")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestBMPLongitudeNull()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Treatment BMP Latitude")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestBMPLongitudeValueNotDecimalParseable()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,AB120,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Treatment BMP Longitude can not be converted to Decimal format at row: ")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestBMPLongitudeValueNotAcceptableDecimalValue()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,85,181,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Treatment BMP Longitude 181")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestBMPLocationGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Treatment BMP Latitude")), Is.True, "Expected an error message");
                Assert.That(errorList.Any(x => !x.Contains("Treatment BMP Latitude can not be converted to Decimal format at row: ")), Is.True, "Expected an error message");
                Assert.That(errorList.Any(x => !x.Contains("Treatment BMP Longitude can not be converted to Decimal format at row: ")), Is.True, "Expected an error message");
                Assert.That(errorList.Any(x => !x.Contains("Treatment BMP Longitude")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].LocationPoint4326.ToString(), Is.EqualTo($"SRID={CoordinateSystemHelper.WGS_1984_SRID};POINT (10 30)"), "Should have properly projected to 4326");
            }

            [Test]
            public void TestJurisdictionNameNull()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Jurisdiction is null, empty, or just whitespaces for row: ")), Is.True, "Expected error about blank Jurisdiction");
            }

            [Test]
            public void TestJurisdictionNameExistsBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,Sitka Technology Grou,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("No Jurisdiction with the name '")), Is.True, "Expected error about unknown Jurisdiction");
            }

            [Test]
            public void TestJurisdictionNameExistsGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Dana Point,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("No Jurisdiction with the name '")), Is.True, "Expected an error message");
                Assert.That(errorList.Any(x => !x.Contains("Jurisdiction is null, empty, or just whitespaces for row: ")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].StormwaterJurisdictionID, Is.EqualTo(2));
            }

            [Test]
            public void TestOrganizationOwnerNameNull()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Owner is null, empty, or just whitespaces for row:")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestOrganizationOwnerNameExistsBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Grou,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("No Owner with the name '")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestOrganizationOwnerExistsGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Owner is null, empty, or just whitespaces for row:")), Is.True, "Expected an error message");
                Assert.That(errorList.Any(x => !x.Contains("No Owner with the name '")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].OwnerOrganizationID, Is.EqualTo(1));
            }

            //Begin Optional Field Tests

            [Test]
            public void TestYearBuiltIntParseBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,AB34,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Year Built or Installed can not be converted to Int at row")), Is.True, "Expected an error message");
            }
            [Test]
            public void TestYearBuiltIntParseGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Year Built or Installed can not be converted to Int at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].YearBuilt, Is.EqualTo(2008));
            }

            [Test]
            public void TestYearBuiltIntAcceptsNullGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Year Built or Installed can not be converted to Int at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].YearBuilt, Is.EqualTo(null));
            }

            [Test]
            public void TestAssetIdInSystemOfRecordsBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                var sysID = "";
                for (var i = 0; i < TreatmentBMP.FieldLengths.SystemOfRecordID + 2; i++)
                {
                    sysID += "a";
                }

                var csv = $@"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,{sysID},Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Asset ID in System of Record is too long at row")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestAssetIdInSystemOfRecordsGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Asset ID in System of Record is too long at row")));
                Assert.That(treatmentBmpUploadSimples[0].SystemOfRecordID, Is.EqualTo("ABCD"), "Expected an error message");
            }

            [Test]
            public void TestAssetIdInSystemOfRecordsNullGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Asset ID in System of Record is too long at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].SystemOfRecordID, Is.EqualTo(null));
            }

            [Test]
            public void TestRequiredLifespanOfInstallationBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpet,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("No Required Lifespan of Installation with the name '")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestRequiredLifespanOfInstallationGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("No Required Lifespan of Installation with the name '")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanTypeID, Is.EqualTo(2));
            }

            [Test]
            public void TestRequiredLifespanOfInstallationNullGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("No Required Lifespan Of Installation with the name '")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanTypeID, Is.EqualTo(null));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/,5,6,Happy,Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Allowable End Date of Installation (if applicable) can not be converted to Date Time format at row")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsFixedEndDateButDateNullBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Fixed End Date,,5,6,Happy,Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("An end date must be provided if the 'Required Lifespan of Installation' field is set to fixed end date")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsFixedEndDateButDateNotNullGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Fixed End Date,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("An end date must be provided if the 'Required Lifespan of Installation' field is set to fixed end date")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(DateTime.Parse("11/12/2022")));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsNotFixedEndDateButDateNotNullBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to Perpetuity/Life of Project")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsNotFixedEndDateButDateNullGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to Perpetuity/Life of Project")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(null));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsNullButDateNotNullBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to null")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsNullButDateNullGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,,,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to null")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(null));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Fixed End Date,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Allowable End Date of Installation can not be converted to Date Time format at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(DateTime.Parse("11/12/2022")));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationNullGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Allowable End Date of Installation can not be converted to Date Time format at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(null));
            }

            [Test]
            public void TestRequiredFieldVisitsPerYearBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,Ab5,6,Happy,Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Required Field Visits Per Year can not be converted to Int at row")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestRequiredFieldVisitsPerYearGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Required Field Visits per Year can not be converted to Int at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].RequiredFieldVisitsPerYear, Is.EqualTo(5));
            }

            [Test]
            public void TestRequiredFieldVisitsPerYearNullGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Required Field Visits per Year can not be converted to Int at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].RequiredFieldVisitsPerYear, Is.EqualTo(null));
            }

            [Test]
            public void TestRequiredPostStormFieldVisitsPerYearBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,AB6,Happy,Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Required Post-Storm Field Visits Per Year can not be converted to Int at row")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestRequiredPostStormFieldVisitsPerYearGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Required Post-Storm Field Visits Per Year can not be converted to Int at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].RequiredPostStormFieldVisitsPerYear, Is.EqualTo(6));
            }

            [Test]
            public void TestRequiredPostStormFieldVisitsPerYearNullGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Required Post-Storm Field Visits Per Year can not be converted to Int at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].RequiredPostStormFieldVisitsPerYear, Is.EqualTo(null));
            }

            [Test]
            public void TestNotesBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                var note = "";
                for (var i = 0; i < TreatmentBMP.FieldLengths.Notes + 2; i++)
                {
                    note += "a";
                }

                var csv = $@"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,AB6,{note},Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Notes is too long at row")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestNotesGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Notes is too long at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].Notes, Is.EqualTo("Happy"), "Expected an error message");
            }

            [Test]
            public void TestNotesNullGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Notes length is too long at row")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].Notes, Is.EqualTo(null));
            }

            //End Optional Field Tests
            [Test]
            public void TestTrashCaptureStatusExistsNull()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,null,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("No Required Lifespan Of Installation by the name")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanTypeID, Is.EqualTo(null));
            }

            [Test]
            public void TestTrashCaptureStatusExistsBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,4,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("No Trash Capture Status with the name '")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestTrashCaptureStatusExistsGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Trash Capture Status is null, empty, or just whitespaces for row: ")), Is.True, "Expected an error message");
                Assert.That(errorList.Any(x => !x.Contains("No Trash Capture Status with the name '")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].TrashCaptureStatusTypeID, Is.EqualTo(1));
            }


            [Test]
            public void TestSizingBasisExistsNull()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Sizing Basis is null, empty, or just whitespaces for row: ")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestSizingBasisExistsBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,4";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("No Sizing Basis with the name ")), Is.True, "Expected an error message");
            }

            [Test]
            public void TestSizingBasisExistsGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("No Sizing Basis with the name '")), Is.True, "Expected an error message");
                Assert.That(errorList.Any(x => !x.Contains("Sizing Basis is null, empty, or just whitespaces for row: ")), Is.True, "Expected an error message");
                Assert.That(treatmentBmpUploadSimples[0].SizingBasisTypeID, Is.EqualTo(4));
            }


            [Test]
            public void UploadListPopulatesGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Brea,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,Not Provided
John,30,10,City of Brea,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(!errorList.Any(), Is.True, "Expected no error messages");
            }

            [Test]
            public void UploadListPopulatesBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,,Full,Not Provided";
                const int bmpType = 17;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(), Is.True, "Expected an error message");
            }

            [Test]
            public void TestBMPModelingAttributeAverageDivertedFlowRateGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Average Diverted Flowrate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.LowFlowDiversions.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Average Diverted Flowrate")), Is.True);
                Assert.That(modelingAttributes[0].AverageDivertedFlowrate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeAverageDivertedFlowRateBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Average Diverted Flowrate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.LowFlowDiversions.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Average Diverted Flowrate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeAverageTreatmentFlowRateGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Average Treatment Flowrate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Average Treatment Flowrate")), Is.True);
                Assert.That(modelingAttributes[0].AverageTreatmentFlowrate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeAverageTreatmentFlowRateBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Average Treatment Flowrate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Average Treatment Flowrate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDesignDryWeatherTreatmentCapacityGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Dry Weather Treatment Capacity,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Design Dry Weather Treatment Capacity")), Is.True);
                Assert.That(modelingAttributes[0].DesignDryWeatherTreatmentCapacity, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDesignDryWeatherTreatmentCapacityBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Dry Weather Treatment Capacity,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Design Dry Weather Treatment Capacity")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDesignLowFlowDiversionCapacityGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Low Flow Diversion Capacity,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.LowFlowDiversions.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Design Low Flow Diversion Capacity")), Is.True);
                Assert.That(modelingAttributes[0].DesignLowFlowDiversionCapacity, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDesignLowFlowDiversionCapacityBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Low Flow Diversion Capacity,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.LowFlowDiversions.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Design Low Flow Diversion Capacity")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDesignMediaFiltrationRateGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Media Filtration Rate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Design Media Filtration Rate")), Is.True);
                Assert.That(modelingAttributes[0].DesignMediaFiltrationRate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDesignMediaFiltrationRateBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Media Filtration Rate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Design Media Filtration Rate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDesignResidenceTimeforPermanentPoolGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Residence Time for Permanent Pool,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Design Residence Time for Permanent Pool")), Is.True);
                Assert.That(modelingAttributes[0].DesignResidenceTimeforPermanentPool, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDesignResidenceTimeforPermanentPoolBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Residence Time for Permanent Pool,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Design Residence Time for Permanent Pool")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDiversionRateGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Diversion Rate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Diversion Rate")), Is.True);
                Assert.That(modelingAttributes[0].DiversionRate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDiversionRateBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Diversion Rate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Diversion Rate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDrawdownTimeforWQDetentionVolumeGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Drawdown Time for WQ Detention Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Drawdown Time for WQ Detention Volume")), Is.True);
                Assert.That(modelingAttributes[0].DrawdownTimeforWQDetentionVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDrawdownTimeforWQDetentionVolumeBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Drawdown Time for WQ Detention Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Drawdown Time for WQ Detention Volume")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeEffectiveFootprintGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Effective Footprint,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryExtendedDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Effective Footprint")), Is.True);
                Assert.That(modelingAttributes[0].EffectiveFootprint, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeEffectiveFootprintBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Effective Footprint,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryExtendedDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Effective Footprint")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeEffectiveRetentionDepthGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Effective Retention Depth,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Effective Retention Depth")), Is.True);
                Assert.That(modelingAttributes[0].EffectiveRetentionDepth, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeEffectiveRetentionDepthBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Effective Retention Depth,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Effective Retention Depth")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeInfiltrationDischargeRateGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Infiltration Discharge Rate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.Drywell.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Infiltration Discharge Rate")), Is.True);
                Assert.That(modelingAttributes[0].InfiltrationDischargeRate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeInfiltrationDischargeRateBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Infiltration Discharge Rate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.Drywell.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Infiltration Discharge Rate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeInfiltrationSurfaceAreaGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Infiltration Surface Area,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioretentionWithNoUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Infiltration Surface Area")), Is.True);
                Assert.That(modelingAttributes[0].InfiltrationSurfaceArea, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeInfiltrationSurfaceAreaBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Infiltration Surface Area,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioretentionWithNoUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Infiltration Surface Area")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeMediaBedFootprintGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Media Bed Footprint,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Media Bed Footprint")), Is.True);
                Assert.That(modelingAttributes[0].MediaBedFootprint, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeMediaBedFootprintBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Media Bed Footprint,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Media Bed Footprint")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributePermanentPoolOrWetlandVolumeGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Permanent Pool or Wetland Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Permanent Pool or Wetland Volume")), Is.True);
                Assert.That(modelingAttributes[0].PermanentPoolorWetlandVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributePermanentPoolOrWetlandVolumeBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Permanent Pool or Wetland Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Permanent Pool or Wetland Volume")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeStorageVolumeBelowLowestOutletElevationGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Storage Volume Below Lowest Outlet Elevation,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Storage Volume Below Lowest Outlet Elevation")), Is.True);
                Assert.That(modelingAttributes[0].StorageVolumeBelowLowestOutletElevation, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeStorageVolumeBelowLowestOutletElevationBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Storage Volume Below Lowest Outlet Elevation,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Storage Volume Below Lowest Outlet Elevation")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeSummerHarvestedWaterDemandGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Summer Harvested Water Demand,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Summer Harvested Water Demand")), Is.True);
                Assert.That(modelingAttributes[0].SummerHarvestedWaterDemand, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeSummerHarvestedWaterDemandBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Summer Harvested Water Demand,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Summer Harvested Water Demand")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID5Good()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,5";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(1));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID10Good()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,10";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(2));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID15Good()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,15";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(3));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID20Good()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,20";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(4));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID30Good()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,30";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(5));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID45Good()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,45";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(6));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID60Good()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,60";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(7));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationIDBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,25";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Time of Concentration")), Is.True);
            }

            /*[Test]
            public void TestBMPModelingAttributeDrawdownTimeForDetentionVolumeGood()
            {
AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Drawdown Time For Detention Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _,  out var modelingAttributes, out _);
                Assert.That(errorList.Any(x => !x.Contains("Drawdown Time For Detention Volume")), Is.True);
                Assert.That(modelingAttributes[0].DrawdownTimeForDetentionVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDrawdownTimeForDetentionVolumeBad()
            {
AssertCustom.IgnoreOnBuildServer();
                const string csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Drawdown Time For Detention Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Drawdown Time For Detention Volume")), Is.True);
            }*/

            [Test]
            public void TestBMPModelingAttributeTotalEffectiveBMPVolumeGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Total Effective BMP Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Total Effective BMP Volume")), Is.True);
                Assert.That(modelingAttributes[0].TotalEffectiveBMPVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeTotalEffectiveBMPVolumeBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Total Effective BMP Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Total Effective BMP Volume")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeTotalEffectiveDrywellBMPVolumeGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Total Effective Drywell BMP Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.Drywell.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Total Effective Drywell BMP Volume")), Is.True);
                Assert.That(modelingAttributes[0].TotalEffectiveDrywellBMPVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeTotalEffectiveDrywellBMPVolumeBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Total Effective Drywell BMP Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.Drywell.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Total Effective Drywell BMP Volume")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeTreatmentRateGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Treatment Rate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.HydrodynamicSeparator.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Treatment Rate")), Is.True);
                Assert.That(modelingAttributes[0].TreatmentRate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeTreatmentRateBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Treatment Rate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.HydrodynamicSeparator.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Treatment Rate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDAGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,A";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingHydrologicSoilGroupID, Is.EqualTo(1));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDBGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,B";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingHydrologicSoilGroupID, Is.EqualTo(2));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDCGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,C";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingHydrologicSoilGroupID, Is.EqualTo(3));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDDGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,D";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingHydrologicSoilGroupID, Is.EqualTo(4));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDLinerGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Liner";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingHydrologicSoilGroupID, Is.EqualTo(5));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDLinerWithBioinfiltrationBioretentionWithRaisedUnderdrainBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Liner";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingInfiltrationRateGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Infiltration Rate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.UndergroundInfiltration.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Infiltration Rate")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingInfiltrationRate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingInfiltrationRateBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Infiltration Rate,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.UndergroundInfiltration.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Underlying Infiltration Rate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeWaterQualityDetentionVolumeGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Water Quality Detention Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Water Quality Detention Volume")), Is.True);
                Assert.That(modelingAttributes[0].WaterQualityDetentionVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeWaterQualityDetentionVolumeBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Water Quality Detention Volume,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Water Quality Detention Volume")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeWettedFootprintGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Wetted Footprint,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Wetted Footprint")), Is.True);
                Assert.That(modelingAttributes[0].WettedFootprint, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeWettedFootprintBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Wetted Footprint,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Wetted Footprint")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeWinterHarvestedWaterDemandGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Winter Harvested Water Demand,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Winter Harvested Water Demand")), Is.True);
                Assert.That(modelingAttributes[0].WinterHarvestedWaterDemand, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeWinterHarvestedWaterDemandBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Winter Harvested Water Demand,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Winter Harvested Water Demand")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeRoutingConfigurationIDOnlineGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Routing Configuration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Online";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Routing Configuration")), Is.True);
                Assert.That(modelingAttributes[0].RoutingConfigurationID, Is.EqualTo(1));
            }

            [Test]
            public void TestBMPModelingAttributeRoutingConfigurationIDOfflineGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Routing Configuration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Offline";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out var modelingAttributes);
                Assert.That(errorList.Any(x => !x.Contains("Routing Configuration")), Is.True);
                Assert.That(modelingAttributes[0].RoutingConfigurationID, Is.EqualTo(2));
            }

            [Test]
            public void TestBMPModelingAttributeRoutingConfigurationIDBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Routing Configuration,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Routing Configuration")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeMonthsOfOperationWinterGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Months of Operation,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Winter";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Months of Operation")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeMonthsOfOperationSummerGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Months of Operation,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Summer";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Months of Operation")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeMonthsOfOperationBothGood()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Months of Operation,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Both";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => !x.Contains("Months of Operation")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeMonthsOfOperationBad()
            {
                LtInfo.Common.AssertCustom.IgnoreOnBuildServer();
                const string csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Months of Operation,  
Frank,30,10,City of Orange,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out var errorList, out _, out _, out _);
                Assert.That(errorList.Any(x => x.Contains("Months of Operation")), Is.True);
            }
        }
    }
}
