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

        public void UpdateModel(Person currentPerson)
        {
            using (var disposableTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip"))
            {
                var gdbFile = disposableTempFile.FileInfo;
                FileResourceData.SaveAs(gdbFile.FullName);

                HttpRequestStorage.DatabaseEntities.DelineationGeometryStagings.DeleteDelineationGeometryStaging(
                    currentPerson.DelineationGeometryStagings);
                currentPerson.DelineationGeometryStagings.Clear();
                DelineationGeometryStaging.CreateDelineationGeometryStagingListFromGdb(gdbFile, currentPerson);
            }
        }
    }
}