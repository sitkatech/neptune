/*-----------------------------------------------------------------------
<copyright file="TestFileResource.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
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
                var csv = @"BMP Name,Jurisdiction,,Latitude,Longitude,BMP Type,Trash Capture Status,Sizing Basis,Yo";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("One or more required headers have not been provided, Required Fields are: "));
                errorList.Any(x => x.Contains("Some of the fields provided are not acceptable optional fields or are misspelled required fields. Optional Fields are: "));
            }

            [Test]
            public void TestValidColumns()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("One or more required headers have not been provided, Required Fields are: "));
                errorList.Any(x => !x.Contains("Some of the fields provided are not acceptable optional fields or are misspelled required fields. Optional Fields are: "));
            }

            [Test]
            public void TestBMPNameNull()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("BMP Name is null, empty, or just whitespaces for row: "));
            }

            [Test]
            public void TestBMPNameExists()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Test,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("A BMP by the name,"));
            }

            [Test]
            public void TestBMPLatitudeNull()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Treatment BMP Latitude "));
            }

            [Test]
            public void TestBMPLatitudeValueNotDecimalParseable()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,AB120,120,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Treatment BMP Latitude can not be converted to Decimal format at row: "));
            }

            [Test]
            public void TestBMPLatitudeValueNotAcceptableDecimalValue()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,95,120,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Treatment BMP Latitude 95"));
            }

            [Test]
            public void TestBMPLongitudeNull()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Treatment BMP Latitude"));
            }

            [Test]
            public void TestBMPLongitudeValueNotDecimalParseable()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,AB120,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Treatment BMP Longitude can not be converted to Decimal format at row: "));
            }

            [Test]
            public void TestBMPLongitudeValueNotAcceptableDecimalValue()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,85,181,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Treatment BMP Longitude 181"));
            }

            [Test]
            public void TestBMPLocationGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("Treatment BMP Latitude"));
                errorList.Any(x => !x.Contains("Treatment BMP Latitude can not be converted to Decimal format at row: "));
                errorList.Any(x => !x.Contains("Treatment BMP Longitude can not be converted to Decimal format at row: "));
                errorList.Any(x => !x.Contains("Treatment BMP Longitude"));
                Assert.That(treatmentBmpUploadSimples[0].LocationPoint.ToString(), Is.EqualTo($"SRID={CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID};POINT (10 30)"));
            }

            [Test]
            public void TestJurisdictionNameNull()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("BMP Jurisdiction is null, empty, or just whitespaces for row: "));
            }

            [Test]
            public void TestJurisdictionNameExistsBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Grou,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("No Jurisdicition with that name exists within our records, row: "));
            }

            [Test]
            public void TestJurisdictionNameExistsGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,City of Dana Point,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("No Jurisdicition with that name exists within our records, row: "));
                errorList.Any(x => !x.Contains("BMP Jurisdiction is null, empty, or just whitespaces for row: "));
                Assert.That(treatmentBmpUploadSimples[0].StormwaterJurisdictionID, Is.EqualTo(2));
            }

            [Test]
            public void TestOrganizationOwnerNameNull()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("BMP Organization Owner Name is null, empty, or just whitespaces for row:"));
            }

            [Test]
            public void TestOrganizationOwnerNameExistsBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Grou,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("No BMP Organization Owner with that name exists within our records, row: "));
            }

            [Test]
            public void TestOrganizationOwnerExistsGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,City of Brea,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("BMP Organization Owner Name is null, empty, or just whitespaces for row:"));
                errorList.Any(x => !x.Contains("No BMP Organization Owner with that name exists within our records, row: "));
                Assert.That(treatmentBmpUploadSimples[0].OwnerOrganizationID, Is.EqualTo(1));
            }

            //Begin Optional Field Tests

            [Test]
            public void TestYearBuiltIntParseBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,AB34,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Year Built or Installed Field cannot be converted to Int at row"));
            }
            [Test]
            public void TestYearBuiltIntParseGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("Year Built or Installed Field cannot be converted to Int at row"));
                Assert.That(treatmentBmpUploadSimples[0].YearBuilt, Is.EqualTo(2008));
            }

            [Test]
            public void TestYearBuiltIntAcceptsNullGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
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
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Asset ID In System of Record is too long at row,"));
            }

            [Test]
            public void TestAssetIdInSystemOfRecordsGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("Asset ID In System of Record is too long at row, "));
                Assert.That(treatmentBmpUploadSimples[0].SystemOfRecordID, Is.EqualTo("ABCD"));
            }

            [Test]
            public void TestAssetIdInSystemOfRecordsNullGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("Asset ID In System of Record is too long at row, "));
                Assert.That(treatmentBmpUploadSimples[0].SystemOfRecordID, Is.EqualTo(null));
            }

            [Test]
            public void TestRequiredLifespanOfInstallationBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpet,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("No Required Lifespan Of Installation by the name"));
            }

            [Test]
            public void TestRequiredLifespanOfInstallationGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("No Required Lifespan Of Installation by the name"));
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanTypeID, Is.EqualTo(2));
            }

            [Test]
            public void TestRequiredLifespanOfInstallationNullGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("No Required Lifespan Of Installation by the name"));
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanTypeID, Is.EqualTo(null));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Allowable End Date of Installation can not be converted to Date Time format at row"));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsFixedEndDateButDateNullBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Fixed End Date,,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("An end date must be provided if the 'Required Lifespan of Installation' field is set to fixed end date"));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsFixedEndDateButDateNotNullGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Fixed End Date,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("An end date must be provided if the 'Required Lifespan of Installation' field is set to fixed end date"));
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(DateTime.Parse("11/12/2022")));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsNotFixedEndDateButDateNotNullBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to Perpetuity/Life of Project"));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsNotFixedEndDateButDateNullGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to Perpetuity/Life of Project"));
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(null));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsNullButDateNotNullBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to null"));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationTypeIsNullButDateNullGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,,,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("An end date was provided when 'Required Lifespan of Installation' field was set to null"));
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(null));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Fixed End Date,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("Allowable End Date of Installation can not be converted to Date Time format at row"));
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(DateTime.Parse("11/12/2022")));
            }

            [Test]
            public void TestAllowableEndDateOfInstallationNullGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("Allowable End Date of Installation can not be converted to Date Time format at row"));
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanEndDate, Is.EqualTo(null));
            }

            [Test]
            public void TestRequiredFieldVisitsPerYearBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,Ab5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Required Field Vists per Year Field can not be converted to Int at row"));
            }

            [Test]
            public void TestRequiredFieldVisitsPerYearGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("Required Field Vists per Year Field can not be converted to Int at row"));
                Assert.That(treatmentBmpUploadSimples[0].RequiredFieldVisitsPerYear, Is.EqualTo(5));
            }

            [Test]
            public void TestRequiredFieldVisitsPerYearNullGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("Required Field Vists per Year Field can not be converted to Int at row"));
                Assert.That(treatmentBmpUploadSimples[0].RequiredFieldVisitsPerYear, Is.EqualTo(null));
            }

            [Test]
            public void TestRequiredPostStormFieldVisitsPerYearBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,AB6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Required post-storm field vists per year field cannot be converted to Int at row"));
            }

            [Test]
            public void TestRequiredPostStormFieldVisitsPerYearGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("Required post-storm field vists per year field cannot be converted to Int at row"));
                Assert.That(treatmentBmpUploadSimples[0].RequiredPostStormFieldVisitsPerYear, Is.EqualTo(6));
            }

            [Test]
            public void TestRequiredPostStormFieldVisitsPerYearNullGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
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
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Note length is too long at row"));
            }

            [Test]
            public void TestNotesGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("Note length is too long at row"));
                Assert.That(treatmentBmpUploadSimples[0].Notes, Is.EqualTo("Happy"));
            }

            [Test]
            public void TestNotesNullGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,,Full,";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
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
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => !x.Contains("No Required Lifespan Of Installation by the name"));
                Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPLifespanTypeID, Is.EqualTo(null));
            }

            [Test]
            public void TestTrashCaptureStatusExistsBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,4,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("No Trash Capture Status with that name exists in our records, row: "));
            }

            [Test]
            public void TestTrashCaptureStatusExistsGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
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
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Sizing Basis is null, empty, or just whitespaces for row: "));
            }

            [Test]
            public void TestSizingBasicsExistsBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,4";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("No Sizing Basic with the name "));
            }

            [Test]
            public void TestSizingBasicsExistsGood()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
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
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("A BMP by the name,"));
            }

            [Test]
            public void UploadListPopulatesBad()
            {
                var csv = @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,,Full,Not Provided";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues);
                errorList.Any(x => x.Contains("Sizing Basis is null, empty, or just whitespaces for row: "));
                errorList.Any(x => x.Contains("A BMP by the name,"));
            }
        }
    }
}
