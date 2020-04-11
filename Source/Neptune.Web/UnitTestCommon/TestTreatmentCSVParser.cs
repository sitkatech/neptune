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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
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
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = 17;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                errorList.Any(x => x.Contains("Sizing Basis is null, empty, or just whitespaces for row: "));
                errorList.Any(x => x.Contains("A BMP by the name,"));
            }

            [Test]
            public void TestBMPModelingAttributeAverageDivertedFlowRateGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Average Diverted Flowrate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.LowFlowDiversions.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Average Diverted Flowrate")), Is.True);
                Assert.That(modelingAttributes[0].AverageDivertedFlowrate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeAverageDivertedFlowRateBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Average Diverted Flowrate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.LowFlowDiversions.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Average Diverted Flowrate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeAverageTreatmentFlowRateGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Average Treatment Flowrate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Average Treatment Flowrate")), Is.True);
                Assert.That(modelingAttributes[0].AverageTreatmentFlowrate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeAverageTreatmentFlowRateBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Average Treatment Flowrate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Average Treatment Flowrate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDesignDryWeatherTreatmentCapacityGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Dry Weather Treatment Capacity,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Design Dry Weather Treatment Capacity")), Is.True);
                Assert.That(modelingAttributes[0].DesignDryWeatherTreatmentCapacity, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDesignDryWeatherTreatmentCapacityBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Dry Weather Treatment Capacity,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Design Dry Weather Treatment Capacity")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDesignLowFlowDiversionCapacityGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Low Flow Diversion Capacity,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.LowFlowDiversions.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Design Low Flow Diversion Capacity")), Is.True);
                Assert.That(modelingAttributes[0].DesignLowFlowDiversionCapacity, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDesignLowFlowDiversionCapacityBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Low Flow Diversion Capacity,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.LowFlowDiversions.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Design Low Flow Diversion Capacity")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDesignMediaFiltrationRateGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Media Filtration Rate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Design Media Filtration Rate")), Is.True);
                Assert.That(modelingAttributes[0].DesignMediaFiltrationRate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDesignMediaFiltrationRateBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Media Filtration Rate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Design Media Filtration Rate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDesignResidenceTimeforPermanentPoolGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Residence Time for Permanent Pool,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Design Residence Time for Permanent Pool")), Is.True);
                Assert.That(modelingAttributes[0].DesignResidenceTimeforPermanentPool, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDesignResidenceTimeforPermanentPoolBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Design Residence Time for Permanent Pool,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Design Residence Time for Permanent Pool")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeDiversionRateGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Diversion Rate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Diversion Rate")), Is.True);
                Assert.That(modelingAttributes[0].DiversionRate, Is.EqualTo(1.0));
            }

             [Test]
            public void TestBMPModelingAttributeDiversionRateBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Diversion Rate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Diversion Rate")), Is.True);
            }
            
            [Test]
            public void TestBMPModelingAttributeDrawdownTimeforWQDetentionVolumeGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Drawdown Time for WQ Detention Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Drawdown Time for WQ Detention Volume")), Is.True);
                Assert.That(modelingAttributes[0].DrawdownTimeforWQDetentionVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDrawdownTimeforWQDetentionVolumeBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Drawdown Time for WQ Detention Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Drawdown Time for WQ Detention Volume")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeEffectiveFootprintGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Effective Footprint,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryExtendedDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Effective Footprint")), Is.True);
                Assert.That(modelingAttributes[0].EffectiveFootprint, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeEffectiveFootprintBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Effective Footprint,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryExtendedDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Effective Footprint")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeEffectiveRetentionDepthGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Effective Retention Depth,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Effective Retention Depth")), Is.True);
                Assert.That(modelingAttributes[0].EffectiveRetentionDepth, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeEffectiveRetentionDepthBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Effective Retention Depth,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Effective Retention Depth")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeInfiltrationDischargeRateGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Infiltration Discharge Rate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.Drywell.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Infiltration Discharge Rate")), Is.True);
                Assert.That(modelingAttributes[0].InfiltrationDischargeRate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeInfiltrationDischargeRateBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Infiltration Discharge Rate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.Drywell.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Infiltration Discharge Rate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeInfiltrationSurfaceAreaGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Infiltration Surface Area,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioretentionWithNoUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Infiltration Surface Area")), Is.True);
                Assert.That(modelingAttributes[0].InfiltrationSurfaceArea, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeInfiltrationSurfaceAreaBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Infiltration Surface Area,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioretentionWithNoUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Infiltration Surface Area")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeMediaBedFootprintGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Media Bed Footprint,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Media Bed Footprint")), Is.True);
                Assert.That(modelingAttributes[0].MediaBedFootprint, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeMediaBedFootprintBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Media Bed Footprint,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Media Bed Footprint")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributePermanentPoolorWetlandVolumeGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Permanent Pool or Wetland Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Permanent Pool or Wetland Volume")), Is.True);
                Assert.That(modelingAttributes[0].PermanentPoolorWetlandVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributePermanentPoolorWetlandVolumeBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Permanent Pool or Wetland Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Permanent Pool or Wetland Volume")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeStorageVolumeBelowLowestOutletElevationGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Storage Volume Below Lowest Outlet Elevation,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Storage Volume Below Lowest Outlet Elevation")), Is.True);
                Assert.That(modelingAttributes[0].StorageVolumeBelowLowestOutletElevation, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeStorageVolumeBelowLowestOutletElevationBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Storage Volume Below Lowest Outlet Elevation,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Storage Volume Below Lowest Outlet Elevation")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeSummerHarvestedWaterDemandGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Summer Harvested Water Demand,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Summer Harvested Water Demand")), Is.True);
                Assert.That(modelingAttributes[0].SummerHarvestedWaterDemand, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeSummerHarvestedWaterDemandBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Summer Harvested Water Demand,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.ConstructedWetland.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Summer Harvested Water Demand")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID5Good()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,5";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(1));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID10Good()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,10";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(2));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID15Good()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,15";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(3));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID20Good()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,20";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(4));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID30Good()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,30";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(5));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID45Good()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,45";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(6));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationID60Good()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,60";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Time of Concentration")), Is.True);
                Assert.That(modelingAttributes[0].TimeOfConcentrationID, Is.EqualTo(7));
            }

            [Test]
            public void TestBMPModelingAttributeTimeOfConcentrationIDBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Time of Concentration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,25";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Time of Concentration")), Is.True);
            }

            /*[Test]
            public void TestBMPModelingAttributeDrawdownTimeForDetentionVolumeGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Drawdown Time For Detention Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Drawdown Time For Detention Volume")), Is.True);
                Assert.That(modelingAttributes[0].DrawdownTimeForDetentionVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeDrawdownTimeForDetentionVolumeBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Drawdown Time For Detention Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Drawdown Time For Detention Volume")), Is.True);


            }*/

            [Test]
            public void TestBMPModelingAttributeTotalEffectiveBMPVolumeGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Total Effective BMP Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Total Effective BMP Volume")), Is.True);
                Assert.That(modelingAttributes[0].TotalEffectiveBMPVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeTotalEffectiveBMPVolumeBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Total Effective BMP Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Total Effective BMP Volume")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeTotalEffectiveDrywellBMPVolumeGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Total Effective Drywell BMP Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.Drywell.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Total Effective Drywell BMP Volume")), Is.True);
                Assert.That(modelingAttributes[0].TotalEffectiveDrywellBMPVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeTotalEffectiveDrywellBMPVolumeBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Total Effective Drywell BMP Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.Drywell.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Total Effective Drywell BMP Volume")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeTreatmentRateGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Treatment Rate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.HydrodynamicSeparator.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Treatment Rate")), Is.True);
                Assert.That(modelingAttributes[0].TreatmentRate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeTreatmentRateBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Treatment Rate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.HydrodynamicSeparator.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Treatment Rate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDAGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,A";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingHydrologicSoilGroupID, Is.EqualTo(1));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDBGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,B";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingHydrologicSoilGroupID, Is.EqualTo(2));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDCGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,C";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingHydrologicSoilGroupID, Is.EqualTo(3));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDDGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,D";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingHydrologicSoilGroupID, Is.EqualTo(4));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDLinerGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Liner";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingHydrologicSoilGroupID, Is.EqualTo(5));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDLinerWithBioinfiltrationBioretentionWithRaisedUnderdrainBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Liner";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.BioinfiltrationBioretentionWithRaisedUnderdrain.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingHydrologicSoilGroupIDBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Hydrologic Soil Group (HSG),  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.FlowDurationControlTank.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Underlying Hydrologic Soil Group (HSG)")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingInfiltrationRateGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Infiltration Rate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.UndergroundInfiltration.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Underlying Infiltration Rate")), Is.True);
                Assert.That(modelingAttributes[0].UnderlyingInfiltrationRate, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeUnderlyingInfiltrationRateBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Underlying Infiltration Rate,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.UndergroundInfiltration.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Underlying Infiltration Rate")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeWaterQualityDetentionVolumeGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Water Quality Detention Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Water Quality Detention Volume")), Is.True);
                Assert.That(modelingAttributes[0].WaterQualityDetentionVolume, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeWaterQualityDetentionVolumeBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Water Quality Detention Volume,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Water Quality Detention Volume")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeWettedFootprintGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Wetted Footprint,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Wetted Footprint")), Is.True);
                Assert.That(modelingAttributes[0].WettedFootprint, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeWettedFootprintBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Wetted Footprint,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.VegetatedSwale.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Wetted Footprint")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeWinterHarvestedWaterDemandGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Winter Harvested Water Demand,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,1";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Winter Harvested Water Demand")), Is.True);
                Assert.That(modelingAttributes[0].WinterHarvestedWaterDemand, Is.EqualTo(1.0));
            }

            [Test]
            public void TestBMPModelingAttributeWinterHarvestedWaterDemandBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Winter Harvested Water Demand,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Winter Harvested Water Demand")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeRoutingConfigurationIDOnlineGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Routing Configuration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Online";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Routing Configuration")), Is.True);
                Assert.That(modelingAttributes[0].RoutingConfigurationID, Is.EqualTo(1));
            }

            [Test]
            public void TestBMPModelingAttributeRoutingConfigurationIDOfflineGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Routing Configuration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Offline";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Routing Configuration")), Is.True);
                Assert.That(modelingAttributes[0].RoutingConfigurationID, Is.EqualTo(2));
            }

            [Test]
            public void TestBMPModelingAttributeRoutingConfigurationIDBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Routing Configuration,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.WetDetentionBasin.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Routing Configuration")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeMonthsOfOperationJanGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Months of Operation,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Jan";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Months of Operation")), Is.True);
                Assert.That(operationMonths[0].OperationMonth, Is.EqualTo(1));
            }

            [Test]
            public void TestBMPModelingAttributeMonthsOfOperationJanSpaceFebGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Months of Operation,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Jan Feb";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Months of Operation")), Is.True);
                Assert.That(operationMonths[0].OperationMonth, Is.EqualTo(1));
                Assert.That(operationMonths[1].OperationMonth, Is.EqualTo(2));
            }

            [Test]
            public void TestBMPModelingAttributeMonthsOfOperationJanFebMarAprMayJunJulAugSepOctNovDecWithSpacesGood()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Months of Operation,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => !x.Contains("Months of Operation")), Is.True);
                Assert.That(operationMonths[0].OperationMonth, Is.EqualTo(1));
                Assert.That(operationMonths[1].OperationMonth, Is.EqualTo(2));
                Assert.That(operationMonths[2].OperationMonth, Is.EqualTo(3));
                Assert.That(operationMonths[3].OperationMonth, Is.EqualTo(4));
                Assert.That(operationMonths[4].OperationMonth, Is.EqualTo(5));
                Assert.That(operationMonths[5].OperationMonth, Is.EqualTo(6));
                Assert.That(operationMonths[6].OperationMonth, Is.EqualTo(7));
                Assert.That(operationMonths[7].OperationMonth, Is.EqualTo(8));
                Assert.That(operationMonths[8].OperationMonth, Is.EqualTo(9));
                Assert.That(operationMonths[9].OperationMonth, Is.EqualTo(10));
                Assert.That(operationMonths[10].OperationMonth, Is.EqualTo(11));
                Assert.That(operationMonths[11].OperationMonth, Is.EqualTo(12));
            }

            [Test]
            public void TestBMPModelingAttributeMonthsOfOperationJanFebNoSpacesBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Months of Operation,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,JanFeb";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Months of Operation")), Is.True);
            }

            [Test]
            public void TestBMPModelingAttributeMonthsOfOperationBad()
            {
                var csv =
                    @"BMP Name,Latitude,Longitude,Jurisdiction, Owner,Year Built or Installed,Asset ID in System of Record, Required Lifespan of Installation,Allowable End Date of Installation (if applicable), Required Field Visits Per Year, Required Post-Storm Field Visits Per Year,Notes,Trash Capture Status,Sizing Basis,Months of Operation,  
Frank,30,10,Sitka Technology Group,Sitka Technology Group,2008,ABCD,Perpetuity/Life of Project,11/12/2022,5,6,Happy,Full,Not Provided,blah";
                List<string> errorList;
                List<CustomAttribute> customAttributes;
                List<CustomAttributeValue> customAttributeValues;
                List<TreatmentBMPModelingAttribute> modelingAttributes;
                List<TreatmentBMPOperationMonth> operationMonths;
                var bmpType = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.Single(x => x.TreatmentBMPModelingTypeID == TreatmentBMPModelingType.DryWeatherTreatmentSystems.TreatmentBMPModelingTypeID).TreatmentBMPTypeID;
                var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.CSVUpload(csv, bmpType, out errorList, out customAttributes, out customAttributeValues, out modelingAttributes, out operationMonths);
                Assert.That(errorList.Any(x => x.Contains("Months of Operation")), Is.True);
            }
        }
    }
}
