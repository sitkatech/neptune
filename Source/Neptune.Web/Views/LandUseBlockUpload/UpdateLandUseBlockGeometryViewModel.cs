using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using Hangfire;
using LtInfo.Common;
using LtInfo.Common.GdalOgr;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.ScheduledJobs;

namespace Neptune.Web.Views.LandUseBlockUpload
{
    public class UpdateLandUseBlockGeometryViewModel : FormViewModel, IValidatableObject
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
                    NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds*10);

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
                    if (featureClassNames.Count == 0)
                    {
                        errors.Add(new ValidationResult("The file geodatabase contained no feature class. Please upload a file geodatabase containing exactly one feature class."));
                        return errors;
                    }

                    if (featureClassNames.Count != 1)
                    {
                        errors.Add(new ValidationResult("The file geodatabase contained more than one feature class. Please upload a file geodatabase containing exactly one feature class."));
                        return errors;
                    }

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
                        }).Where(x => x.Value != null && LandUseBlockGeometryStaging.IsUsableFeatureCollectionGeoJson(x.Value));

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

                HttpRequestStorage.DatabaseEntities.LandUseBlockGeometryStagings.DeleteLandUseBlockGeometryStaging(currentPerson.LandUseBlockGeometryStagings);
                currentPerson.LandUseBlockGeometryStagings.Clear();
                var landUseBlockGeometryStagings = LandUseBlockGeometryStaging.CreateLandUseBlockGeometryStagingListFromGdb(gdbFile, currentPerson);

                HttpRequestStorage.DatabaseEntities.LandUseBlockGeometryStagings.AddRange(landUseBlockGeometryStagings);
                HttpRequestStorage.DatabaseEntities.SaveChanges();

                BackgroundJob.Schedule(() =>
                    ScheduledBackgroundJobBootstrapper.RunLandUseBlockUploadBackgroundJob(), TimeSpan.FromSeconds(30));
            }
        }
    }
}