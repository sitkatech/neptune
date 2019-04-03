using LtInfo.Common.DesignByContract;
using Microsoft.VisualBasic.FileIO;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Neptune.Web.Models;
using TinyCsvParser.Mapping;

namespace Neptune.Web.Common
{

    public static class TreatmentBMPCsvParserHelper
    {
        public static string ParseBmpRowsFromCsv(string fileStream)
        {
            StringReader stringReader = new StringReader(fileStream);
            TextFieldParser parser = new TextFieldParser(stringReader);
            parser.SetDelimiters(",");

            var requiredFields = new List<string> { "Jurisdiction Name", "Name", "Location", "BMP Type", "Sizing Basics", "Trash Capture Status" };

            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes;
            var optionalFields = treatmentBMPTypes.Select(x => x.TreatmentBMPTypeName).ToList();

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
                        throw new PreconditionException("One or more required headers have not been provided, Required Fields are: " + string.Join(", ", requiredFields));
                    }
                    if (optionalFieldDifference.Any())
                    {
                        throw new PreconditionException("Some of the fields provided are not acceptable optional fields or are misspelled required fields. Optional Fields are: " + string.Join(", ", optionalFields));
                    }
                }
                else
                {
                    var treatmentBMPName = fields[0];
                    if (treatmentBMPs.Select(x => x.TreatmentBMPName).Contains(treatmentBMPName))
                    {
                        throw new PreconditionException(string.Format("A BMP by the name, {0}, already exists, row: " + count,
                            treatmentBMPName));
                    }

                    if (string.IsNullOrWhiteSpace(treatmentBMPName))
                    {
                        throw new PreconditionException(
                            "BMP Name is null, empty, or just whitespaces for row: " + count);
                    }

                    var treatmentBMPJurisdictionName = fields[1];
                    int treatmentBMPJurisdictionID;
                    if (!jurisdictionNames.Contains(treatmentBMPJurisdictionName))
                    {
                        throw new PreconditionException("No Jurisdicition with that name exists within our records, row: " + count);
                    }
                    else
                    {
                        treatmentBMPJurisdictionID = jurisdictions
                            .First((x => x.OrganizationName == treatmentBMPJurisdictionName)).OrganizationID;
                    }

                    if (string.IsNullOrWhiteSpace(treatmentBMPJurisdictionName))
                    {
                        throw new PreconditionException(
                            "BMP Jurisdiction Name is null, empty, or just whitespaces for row: " + count);
                    }


                    var treatmentBMPLocation = fields[2];
                    if (string.IsNullOrWhiteSpace(treatmentBMPLocation))
                    {
                        throw new PreconditionException(
                            "Treatment BMP Location is null, empty, or just whitespaces for row: " + count);
                    }

                    var treatmentBMPType = fields[3];
                    int treatmentBMPTypeID;
                    if (!optionalFields.Contains(treatmentBMPType))
                    {
                        throw new PreconditionException("No BMP Type with that name exists within our records, row: " + count);
                    }
                    else
                    {
                        treatmentBMPTypeID = treatmentBMPTypes.First(x => x.TreatmentBMPTypeName == treatmentBMPType)
                            .TreatmentBMPTypeID;
                    }

                    if (string.IsNullOrWhiteSpace(treatmentBMPType))
                    {
                        throw new PreconditionException(
                            "Treatment BMP Type is null, empty, or just whitespaces for row: " + count);
                    }


                    var treatmentSizingBasics = fields[4];
                    int treatmentSizingBasicsID;
                    if (!SizingBasisType.All.Select(x => x.SizingBasisTypeDisplayName).Contains(treatmentSizingBasics))
                    {
                        throw new PreconditionException("No Sizing Basic with that name exists in our records, row: " + count);
                    }
                    else
                    {
                        treatmentSizingBasicsID = SizingBasisType.All.First(x => x.SizingBasisTypeDisplayName == treatmentSizingBasics).SizingBasisTypeID;
                    }
                    if (string.IsNullOrWhiteSpace(treatmentBMPType))
                    {
                        throw new PreconditionException(
                            "Sizing Basics is null, empty, or just whitespaces for row: " + count);
                    }


                    var treatmentBMPTrashCaptureStatus = fields[5];
                    int treatmentBMPTrashCaptureStatusID;
                    if (!TrashCaptureStatusType.All.Select(x => x.TrashCaptureStatusTypeDisplayName)
                        .Contains(treatmentBMPTrashCaptureStatus))
                    {
                        throw new PreconditionException("No Trash Capture Status with that name exists in our records, row: " + count);
                    }
                    else
                    {
                        treatmentBMPTrashCaptureStatusID = TrashCaptureStatusType.All
                            .First(x => x.TrashCaptureStatusTypeDisplayName == treatmentBMPTrashCaptureStatus)
                            .TrashCaptureStatusTypeID;
                    }
                    if (string.IsNullOrWhiteSpace(treatmentBMPType))
                    {
                        throw new PreconditionException(
                            "Trash Capture Status is null, empty, or just whitespaces for row: " + count);
                    }
                }
                count++;
            }


            return "hello world!";
            // throw precondition expression
            //return bmpRowsFromCsv;
        }
    }

    [TestFixture]
    public class TreatmentBMPCsvParserHelperTest
    {

        [Test]
        public void TestInvalidColumns()
        {
            var csv = @"Name,Jurisdiction Name,,Location,BMP Type,Sizing Basics,Trash Capture Status,Yo
Frank,Tank,Texas,28,4,4,";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "Fields don't match!");
        }


        [Test]
        public void TestValidColumns()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank,Tank,Texas,28,Not  Provided,4";
            Assert.DoesNotThrow(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "Fields Match!");

        }

        [Test]
        public void TestBMPNameExists()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Test,Frank,Texas,28,Not Provided,4";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP by that name already Exists");
        }

        [Test]
        public void TestBMPNameNull()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
,Frank,Texas,28,Not Provided,4";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Name is null and a name must be provided");
        }

        [Test]
        public void TestJurisdictionNameExistsBad()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank,Sitka Technology Grou,Texas,28,Not Provided,4";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdiction by that name doesn't exist within our records");
        }

        [Test]
        public void TestJurisdictionNameExistsGood()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank,Sitka Technology Group,Texas,28,Not Provided,4";
            Assert.DoesNotThrow(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdiction by that name doesn't exist within our records");
        }

        [Test]
        public void TestJurisdictionNameNull()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank,  ,Texas,28,Not Provided,4";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdictions is null and a name must be provided");
        }

        [Test]
        public void TestBMPLocationNull()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank, Sitka Technology Group,  ,28,Not Provided,4";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Location is null and one must be provided");
        }

        [Test]
        public void TestBMPTypeExistsBad()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank,Sitka Technology Grou,Texas,28,Not Provided,4";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdiction by that name doesn't exist within our records");
        }

        [Test]
        public void TestBMPTypeExistsGood()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank,Sitka Technology Group,Texas,Drywell,Not Provided,4";
            Assert.DoesNotThrow(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdiction by that name doesn't exist within our records");
        }

        [Test]
        public void TestBMPTypeExistsNull()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank, Sitka Technology Group,Texas,,Not Provided,4";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdictions is null and a name must be provided");
        }

        [Test]
        public void TestSizingBasicsExistsBad()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank,Sitka Technology Grou,Texas,Drywell,4,4";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdiction by that name doesn't exist within our records");
        }

        [Test]
        public void TestSizingBasicsExistsGood()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank,Sitka Technology Group,Texas,Drywell,Not Provided,4";
            Assert.DoesNotThrow(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdiction by that name doesn't exist within our records");
        }

        [Test]
        public void TestSizingBasicsExistsNull()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank, Sitka Technology Group,Texas,Drywell,,4";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdictions is null and a name must be provided");
        }

        [Test]
        public void TestTrashCaptureStatusExistsBad()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank,Sitka Technology Grou,Texas,Drywell,Not Provided,4";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdiction by that name doesn't exist within our records");
        }

        [Test]
        public void TestTrashCaptureStatusExistsGood()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank,Sitka Technology Group,Texas,Drywell,Not Provided,Full";
            Assert.DoesNotThrow(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdiction by that name doesn't exist within our records");
        }

        [Test]
        public void TestTrashCaptureStatusExistsNull()
        {
            var csv = @"Name,Jurisdiction Name,Location,BMP Type,Sizing Basics,Trash Capture Status  
Frank, Sitka Technology Group,Texas,Drywell,Not Provided,";
            Assert.Throws<PreconditionException>(() => TreatmentBMPCsvParserHelper.ParseBmpRowsFromCsv(csv), "BMP Jurisdictions is null and a name must be provided");
        }

    }

}