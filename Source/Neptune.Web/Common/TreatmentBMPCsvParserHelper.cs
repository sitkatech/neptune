using LtInfo.Common.DesignByContract;
using Microsoft.VisualBasic.FileIO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Net;
using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    public class TreatmentBMPUploadSimple
    {
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public DbGeometry LocationPoint { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int TrashCaptureStatusID { get; set; }
        public int SizingBasicsID { get; set; }
    }

    public static class TreatmentBMPCsvParserHelper
    {
        public static List<TreatmentBMPUploadSimple> ParseBmpRowsFromCsv(string fileStream, out List<string> errorList)
        {
            errorList = new List<string>();
            List<TreatmentBMPUploadSimple> upload = new List<TreatmentBMPUploadSimple>();
            var TreatmentBMPUploadRow = new TreatmentBMPUploadSimple();

            StringReader stringReader = new StringReader(fileStream);
            TextFieldParser parser = new TextFieldParser(stringReader);
            parser.SetDelimiters(",");

            var requiredFields = new List<string> { "Jurisdiction Name", "Name", "Latitude", "Longitude", "BMP Type", "Sizing Basics", "Trash Capture Status" };
            var optionalFields = new List<string> {"Year built or installed","Asset ID in System of Record", "Required Lifespan of Installation",
                "Allowable End Date of Installation (if applicable)", "Required Field Visits Per Year", "Required Post-Storm Field Visits Per Year","Notes"};

            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes;
            var treatmentBMPTypeNames = treatmentBMPTypes.Select(x => x.TreatmentBMPTypeName).ToList();


            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs;
            var jurisdictions = HttpRequestStorage.DatabaseEntities.Organizations;
            var jurisdictionNames = jurisdictions.Select(x => x.OrganizationName);

            // if the fields don't match throw an exception
            int count = 0;
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                List<string> headers = new List<string>();
                if (count == 0)
                {
                    headers = fields.Select(x => x.Trim()).ToList();
                    IEnumerable<string> requiredFieldDifference = requiredFields.Except(headers);
                    IEnumerable<string> optionalFieldDifference = headers.Except(requiredFields).Except(optionalFields);

                    if (requiredFieldDifference.Any())
                    {
                        errorList.Add("One or more required headers have not been provided, Required Fields are: " + string.Join(", ", requiredFields));
                       
                    }
                    if (optionalFieldDifference.Any())
                    {
                        errorList.Add("Some of the fields provided are not acceptable optional fields or are misspelled required fields. Optional Fields are: " + string.Join(", ", optionalFields));
                    }
                }
                else
                {
                    var treatmentBMPName = fields[0];
                    if (string.IsNullOrWhiteSpace(treatmentBMPName))
                    {
                        errorList.Add(
                            "BMP Name is null, empty, or just whitespaces for row: " + count);
                    }
                    else if (treatmentBMPs.Select(x => x.TreatmentBMPName).Contains(treatmentBMPName))
                    {
                        errorList.Add(string.Format("A BMP by the name, {0}, already exists, row: " + count,
                            treatmentBMPName));
                    }
                    else
                    {
                        TreatmentBMPUploadRow.TreatmentBMPName = treatmentBMPName;
                    }



                    var treatmentBMPType = fields[1];
                    int treatmentBMPTypeID;
                    if (string.IsNullOrWhiteSpace(treatmentBMPType))
                    {
                        errorList.Add(
                            $"Treatment BMP Type is null, empty, or just whitespaces for BMP '{treatmentBMPName}' at row: {count}");
                    }
                    if (!treatmentBMPTypeNames.Contains(treatmentBMPType))
                    {
                        errorList.Add($"No BMP Type with the name '{treatmentBMPType}' exists within our records, row: " + count);
                    }
                    else
                    {
                        treatmentBMPTypeID = treatmentBMPTypes.First(x => x.TreatmentBMPTypeName == treatmentBMPType)
                            .TreatmentBMPTypeID;
                        TreatmentBMPUploadRow.TreatmentBMPTypeID = treatmentBMPTypeID;
                    }


                    var treatmentBMPLatitude = fields[2];
                    var treatmentBMPLongitude = fields[3];
                    if (string.IsNullOrWhiteSpace(treatmentBMPLatitude))
                    {
                        errorList.Add(
                            "Treatment BMP Latitude is null, empty, or just whitespaces for row: " + count);
                    }
                    if (string.IsNullOrWhiteSpace(treatmentBMPLongitude))
                    {
                        errorList.Add(
                            "Treatment BMP Longitude is null, empty, or just whitespaces for row: " + count);
                    }
                    TreatmentBMPUploadRow.LocationPoint = DbGeometry.FromText("Point (" + treatmentBMPLatitude + " " + treatmentBMPLongitude + ")");

                    var treatmentBMPJurisdictionName = fields[4];
                    int treatmentBMPJurisdictionID;
                    if (string.IsNullOrWhiteSpace(treatmentBMPJurisdictionName))
                    {
                        errorList.Add(
                            "BMP Jurisdiction Name is null, empty, or just whitespaces for row: " + count);
                    }
                    if (!jurisdictionNames.Contains(treatmentBMPJurisdictionName))
                    {
                        errorList.Add("No Jurisdicition with that name exists within our records, row: " + count);
                    }
                    else
                    {
                        treatmentBMPJurisdictionID = jurisdictions
                            .First((x => x.OrganizationName == treatmentBMPJurisdictionName)).OrganizationID;
                        TreatmentBMPUploadRow.StormwaterJurisdictionID = treatmentBMPJurisdictionID;
                    }


                    var treatmentBMPTrashCaptureStatus = fields[5];
                    int treatmentBMPTrashCaptureStatusID;
                    if (string.IsNullOrWhiteSpace(treatmentBMPTrashCaptureStatus))
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
                        treatmentBMPTrashCaptureStatusID = TrashCaptureStatusType.All
                            .First(x => x.TrashCaptureStatusTypeDisplayName == treatmentBMPTrashCaptureStatus)
                            .TrashCaptureStatusTypeID;
                        TreatmentBMPUploadRow.TrashCaptureStatusID = treatmentBMPTrashCaptureStatusID;
                    }


                    var treatmentSizingBasics = fields[6];
                    int treatmentSizingBasicsID;
                    if (string.IsNullOrWhiteSpace(treatmentSizingBasics))
                    {
                        errorList.Add(
                            "Sizing Basics is null, empty, or just whitespaces for row: " + count);
                    }
                    if (!SizingBasisType.All.Select(x => x.SizingBasisTypeDisplayName).Contains(treatmentSizingBasics))
                    {
                        errorList.Add($"No Sizing Basic with the name '{treatmentSizingBasics}' exists in our records, row: " + count);
                    }
                    else
                    {
                        treatmentSizingBasicsID = SizingBasisType.All.First(x => x.SizingBasisTypeDisplayName == treatmentSizingBasics).SizingBasisTypeID;
                        TreatmentBMPUploadRow.SizingBasicsID = treatmentSizingBasicsID;
                    }

                }

                if (TreatmentBMPUploadRow.TreatmentBMPName != null)
                {
                    if (upload.Select(x => x.TreatmentBMPName).Contains(TreatmentBMPUploadRow.TreatmentBMPName))
                    {
                        errorList.Add("The BMP Name, " + TreatmentBMPUploadRow.TreatmentBMPName +
                                      " was already added in this upload, duplicate name is found at row: " + count);
                    }
                    upload.Add(TreatmentBMPUploadRow);
                }

                count++;
            }
            return upload;
        }

    }

    [TestFixture]
    public class TreatmentBMPCsvParserHelperTest
    {

        [Test]
        public void TestInvalidColumns()
        {
            var csv = @"Name,Jurisdiction Name,,Latitude,Longitude,BMP Type,Trash Capture Status,Sizing Basics,Yo";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("One or more required headers have not been provided, Required Fields are: "));
            errorList.Any(x => x.Contains("Some of the fields provided are not acceptable optional fields or are misspelled required fields. Optional Fields are: "));
            
        }


        [Test]
        public void TestValidColumns()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("One or more required headers have not been provided, Required Fields are: "));
            errorList.Any(x => x.Contains("Some of the fields provided are not acceptable optional fields or are misspelled required fields. Optional Fields are: "));
        }

        [Test]
        public void TestBMPNameNull()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
,Drywell,30,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("BMP Name is null, empty, or just whitespaces for row: "));
        }

        [Test]
        public void TestBMPNameExists()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Test,Drywell,30,10,Sitka Technology Group,Full,Not Provided,";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("A BMP by the name,"));
            
        }

        [Test]
        public void TestBMPTypeNull()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,,30,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("Treatment BMP Type is null, empty, or just whitespaces for BMP "));
            Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPName, Is.EqualTo("Frank"));
        }

        [Test]
        public void TestBMPTypeExistsBad()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,28,30,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("No BMP Type with that name exists within our records, row: "));
        }

        [Test]
        public void TestBMPTypeExistsGood()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("Treatment BMP Type is null, empty, or just whitespaces for BMP "));
            errorList.Any(x => x.Contains("No BMP Type with the name exists within our records, row: "));
            Assert.That(treatmentBmpUploadSimples[0].TreatmentBMPTypeID, Is.EqualTo(17));

        }



        [Test]
        public void TestBMPLatitudeNull()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("Treatment BMP Latitude is null, empty, or just whitespaces for row: "));
        }

        [Test]
        public void TestBMPLongitudeNull()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("Treatment BMP Longitude is null, empty, or just whitespaces for row: "));
        }

        [Test]
        public void TestBMPLocationGood()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("Treatment BMP Latitude is null, empty, or just whitespaces for row: "));
            errorList.Any(x => x.Contains("Treatment BMP Longitude is null, empty, or just whitespaces for row: "));
            //Assert.That(treatmentBmpUploadSimples[0].LocationPoint.ProviderValue, Is.EqualTo(<POINT + (30 10)>));
        }

        [Test]
        public void TestJurisdictionNameNull()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("BMP Jurisdiction Name is null, empty, or just whitespaces for row: "));
        }

        [Test]
        public void TestJurisdictionNameExistsBad()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Grou,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("No Jurisdicition with that name exists within our records, row: "));
        }

        [Test]
        public void TestJurisdictionNameExistsGood()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("No Jurisdicition with that name exists within our records, row: "));
            errorList.Any(x => x.Contains("BMP Jurisdiction Name is null, empty, or just whitespaces for row: "));
            Assert.That(treatmentBmpUploadSimples[0].StormwaterJurisdictionID, Is.EqualTo(1));
        }

        [Test]
        public void TestTrashCaptureStatusExistsNull()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,Full,";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("Trash Capture Status is null, empty, or just whitespaces for row: "));
        }

        [Test]
        public void TestTrashCaptureStatusExistsBad()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,4,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("No Trash Capture Status with that name exists in our records, row: "));
        }

        [Test]
        public void TestTrashCaptureStatusExistsGood()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("Trash Capture Status is null, empty, or just whitespaces for row: "));
            errorList.Any(x => x.Contains("No Trash Capture Status with that name exists in our records, row: "));
            Assert.That(treatmentBmpUploadSimples[0].TrashCaptureStatusID, Is.EqualTo(1));
        }


        [Test]
        public void TestSizingBasicsExistsNull()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,Full,";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("Sizing Basics is null, empty, or just whitespaces for row: "));
        }

        [Test]
        public void TestSizingBasicsExistsBad()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,Full,4";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("No Sizing Basic with the name "));
        }

        [Test]
        public void TestSizingBasicsExistsGood()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("No Sizing Basic with the name "));
            errorList.Any(x => x.Contains("Sizing Basics is null, empty, or just whitespaces for row: "));
            Assert.That(treatmentBmpUploadSimples[0].SizingBasicsID, Is.EqualTo(4));
        }


        [Test]
        public void UploadListPopulatesGood()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,Full,Not Provided
John,Drywell,30,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("A BMP by the name,"));
        }

        [Test]
        public void UploadListPopulatesBad()
        {
            var csv = @"Name,BMP Type,Latitude,Longitude,Jurisdiction Name,Trash Capture Status,Sizing Basics  
Frank,Drywell,30,10,Sitka Technology Group,Full,Not Provided
Frank,Drywell,30,10,Sitka Technology Group,Full,Not Provided";
            List<string> errorList;
            var treatmentBmpUploadSimples = TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv, out errorList);
            errorList.Any(x => x.Contains("Sizing Basics is null, empty, or just whitespaces for row: "));
            errorList.Any(x => x.Contains("A BMP by the name,"));
        }
    }

}