using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

namespace Neptune.Web.Views.DelineationUpload
{
    public class UpdateDelineationGeometryViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("Zipped File Geodatabase to Upload")]
        [SitkaFileExtensions("zip")]
        public HttpPostedFileBase FileResourceData { get; set; }

        [Required]
        [DisplayName("Treatment BMP Name Field")]
        public string TreatmentBMPNameField { get; set; }

        [Required]
        [DisplayName("Stormwater Jurisdiction")]
        public int? StormwaterJurisdictionID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            FileResource.ValidateFileSize(FileResourceData, errors, GeneralUtility.NameOf(() => FileResourceData));

            using (var disposableTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip"))
            {
                var gdbFile = disposableTempFile.FileInfo;
                FileResourceData.SaveAs(gdbFile.FullName);

                try
                {
                    var featureClassNames = OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(
                        new FileInfo(NeptuneWebConfiguration.OgrInfoExecutable),
                        gdbFile,
                        Ogr2OgrCommandLineRunner.DefaultTimeOut);

                    if (featureClassNames?.Count == 0)
                    {
                        errors.Add(new ValidationResult(
                            "The file geodatabase contained no feature class. Please upload a file geodatabase containing exactly one feature class."));
                        return errors;
                    }

                    if (featureClassNames?.Count != 1)
                    {
                        errors.Add(new ValidationResult(
                            "The file geodatabase contained more than one feature class. Please upload a file geodatabase containing exactly one feature class."));
                        return errors;
                    }

                    var featureClassName = featureClassNames[0];
                    if (!OgrInfoCommandLineRunner.ConfirmAttributeExistsOnFeatureClass(
                        new FileInfo(NeptuneWebConfiguration.OgrInfoExecutable),
                        gdbFile,
                        Ogr2OgrCommandLineRunner.DefaultTimeOut, featureClassName, TreatmentBMPNameField))
                    {
                        errors.Add(new ValidationResult($"The feature class in the file geodatabase does not have an attribute named {TreatmentBMPNameField}. Please double-check the attribute name you entered and try again."));
                        return errors;
                    }
                }
                catch (Exception e)
                {
                    SitkaLogger.Instance.LogAbridgedErrorMessage(e);
                    errors.Add(new ValidationResult(
                        "There was a problem uploading your file geodatabase. Verify it meets the requirements and is not corrupt."));
                }
            }

            return errors;
        }

        public bool UpdateModel(Person currentPerson)
        {
            HttpRequestStorage.DatabaseEntities.DelineationStagings.DeleteDelineationStaging(currentPerson.DelineationStagingsWhereYouAreTheUploadedByPerson);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            using (var disposableTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip"))
            {
                var gdbFile = disposableTempFile.FileInfo;
                FileResourceData.SaveAs(gdbFile.FullName);

                var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                    Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                    NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds * 10);

                try
                {
                    var featureClassNames = OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(new FileInfo(NeptuneWebConfiguration.OgrInfoExecutable),
                        gdbFile,
                        Ogr2OgrCommandLineRunner.DefaultTimeOut);
                    if (featureClassNames != null)
                    {
                        var columns = new List<string>
                            {
                                $"{currentPerson.PersonID} as UploadedByPersonID",
                                $"{TreatmentBMPNameField} as TreatmentBMPName",
                                $"{StormwaterJurisdictionID} as StormwaterJurisdictionID"
                            };
                        ogr2OgrCommandLineRunner.ImportFileGdbToMsSql(gdbFile, featureClassNames[0], "DelineationStaging", columns,
                            NeptuneWebConfiguration.DatabaseConnectionString, true, Ogr2OgrCommandLineRunner.GEOMETRY_TYPE_POLYGON, "");

                    }
                }
                catch (Exception e)
                {
                    SitkaLogger.Instance.LogAbridgedErrorMessage(e);
                    return false;
                }
            }

            return true;
        }
    }
}
