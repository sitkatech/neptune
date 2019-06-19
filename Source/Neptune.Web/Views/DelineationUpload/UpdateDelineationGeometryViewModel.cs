using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.DelineationUpload
{
    public class UpdateDelineationGeometryViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("Zipped File Geodatabase to Upload")]
        [SitkaFileExtensions("zip")]
        public HttpPostedFileBase FileResourceData { get; set; }

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
                }
                catch (Exception e)
                {
                    errors.Add(new ValidationResult(
                        "There was a problem uploading your file geodatabase. Verify it meets the requirements and is not corrupt."));
                    SitkaLogger.Instance.LogDetailedErrorMessage(e);
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

                    // todo: need to have the stormwater jurisdiction and the Treatment BMP Name field here
                    // scratch that, we actually don't. the only path to live for these guys is posting ApproveDGU, where we will know the SWJ ID and the Treatment BMP Name Fieldo

                    if (featureClassNames != null)
                    {
                        var columns = new List<string>
                            {
                                
                                $"{currentPerson.PersonID} as UploadedByPersonID"
                            };
                        ogr2OgrCommandLineRunner.ImportFileGdbToMsSql(gdbFile, featureClassNames[0], "DelineationStaging", columns,
                            NeptuneWebConfiguration.DatabaseConnectionString);

                    }
                }
                catch (Exception e)
                {
                    SitkaLogger.Instance.LogDetailedErrorMessage(e);
                    return false;
                }
            }

            return true;
        }
    }
}
