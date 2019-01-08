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

namespace Neptune.Web.Views.ModeledCatchment
{
    public class UpdateModeledCatchmentGeometryViewModel : FormViewModel, IValidatableObject
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

                var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
                    Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                    NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);

                List<string> featureClassNames = null;
                try
                {
                    featureClassNames = OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(new FileInfo(NeptuneWebConfiguration.OgrInfoExecutable),
                        gdbFile,
                        Ogr2OgrCommandLineRunner.DefaultTimeOut);
                }
                catch (Exception e)
                {
                    errors.Add(new ValidationResult("There was a problem uploading your file geodatabase. Verify it meets the requirements and is not corrupt."));
                    SitkaLogger.Instance.LogDetailedErrorMessage(e);
                }

                if (featureClassNames != null)
                {
                    var featureClasses = featureClassNames.ToDictionary(x => x,
                        x =>
                        {
                            try
                            {
                                var geoJson = ogr2OgrCommandLineRunner.ImportFileGdbToGeoJson(gdbFile, x, false);
                                return JsonTools.DeserializeObject<FeatureCollection>(geoJson);
                            }
                            catch (Exception e)
                            {
                                errors.Add(new ValidationResult($"There was a problem processing the Feature Class \"{x}\"."));
                                SitkaLogger.Instance.LogDetailedErrorMessage(e);
                                return null;
                            }
                        }).Where(x => x.Value != null && ModeledCatchmentGeometryStaging.IsUsableFeatureCollectionGeoJson(x.Value));

                    if (!featureClasses.Any())
                    {
                        errors.Add(new ValidationResult("There are no usable Feature Classes in the uploaded file. Feature Classes must contain Polygon and/or Multi-Polygon features."));
                    }
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

                HttpRequestStorage.DatabaseEntities.ModeledCatchmentGeometryStagings.DeleteModeledCatchmentGeometryStaging(currentPerson.ModeledCatchmentGeometryStagings);
                currentPerson.ModeledCatchmentGeometryStagings.Clear();
                ModeledCatchmentGeometryStaging.CreateModeledCatchmentGeometryStagingListFromGdb(gdbFile, currentPerson);
            }
        }
    }
}