using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.Models;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.ParcelLayerUpload
{
    public class UploadParcelLayerViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("Zipped File Geodatabase to Upload")]
        [SitkaFileExtensions("zip")]
        public IFormFile FileResourceData { get; set; }

        public int PersonID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            var errors = new List<ValidationResult>();

            var currentPerson = People.GetByID(dbContext, PersonID);

            //todo:
            //dbContext.pParcelStagingDeleteByPersonID(currentPerson.PersonID);
            //FileResource.ValidateFileSize(FileResourceData, errors, GeneralUtility.NameOf(() => FileResourceData));

            //using (var disposableTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".gdb.zip"))
            //{
            //    var gdbFile = disposableTempFile.FileInfo;
            //    FileResourceData.SaveAs(gdbFile.FullName);

            //    var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(NeptuneWebConfiguration.Ogr2OgrExecutable,
            //        Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
            //        NeptuneWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds*10);

            //    List<string> featureClassNames = null;
            //    try
            //    {
            //        featureClassNames = OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(new FileInfo(NeptuneWebConfiguration.OgrInfoExecutable),
            //            gdbFile,
            //            Ogr2OgrCommandLineRunner.DefaultTimeOut);
            //    }
            //    catch (Exception e)
            //    {
            //        errors.Add(new ValidationResult("There was a problem uploading your file geodatabase. Verify it meets the requirements and is not corrupt."));
            //        SitkaLogger.Instance.LogDetailedErrorMessage(e);
            //    }

            //    if (featureClassNames != null)
            //    {
            //        if (featureClassNames.Count == 0)
            //        {
            //            errors.Add(new ValidationResult("The file geodatabase contained no feature class. Please upload a file geodatabase containing exactly one feature class."));
            //            return errors;
            //        }

            //        if (featureClassNames.Count != 1)
            //        {
            //            errors.Add(new ValidationResult("The file geodatabase contained more than one feature class. Please upload a file geodatabase containing exactly one feature class."));
            //            return errors;
            //        }

            //        try
            //        {
            //            var columns = new List<string>
            //            {
            //                "AssessmentNo as ParcelNumber",
            //                "SiteAddress as ParcelAddress",
            //                "Shape_Area as ParcelStagingAreaSquareFeet",
            //                "Shape as ParcelStagingGeometry",
            //                $"{currentPerson.PersonID} as UploadedByPersonID"
            //            };
            //            ogr2OgrCommandLineRunner.ImportFileGdbToMsSql(gdbFile, featureClassNames[0],
            //                "ParcelStaging", columns,
            //                NeptuneWebConfiguration.DatabaseConnectionString, 
            //                true,
            //                Ogr2OgrCommandLineRunner.GEOMETRY_TYPE_MULTIPOLYGON, 
            //                "WHERE AssessmentNo is not null");
            //        }
            //        catch (Ogr2OgrCommandLineException e)
            //        {
            //            if (e.Message.Contains("Unrecognized field name",
            //                    StringComparison.InvariantCultureIgnoreCase))
            //            {
            //                errors.Add(new ValidationResult("The columns in the uploaded file did not match the Parcel schema. The file is invalid and cannot be uploaded."));
            //            }
            //            else
            //            {
            //                errors.Add(new ValidationResult($"There was a problem processing the Feature Class \"{featureClassNames[0]}\". The file may be corrupted or invalid."));
            //                SitkaLogger.Instance.LogDetailedErrorMessage(e);
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            errors.Add(new ValidationResult($"There was a problem processing the Feature Class \"{featureClassNames[0]}\". Feature Classes must contain Polygon and/or Multi-Polygon features."));
            //            SitkaLogger.Instance.LogDetailedErrorMessage(e);
            //        }
            //    }
            //}

            return errors;
        }

        public void UpdateModel(Person currentPerson)
        {
            // todo:
            //BackgroundJob.Schedule(() =>
            //    ScheduledBackgroundJobLaunchHelper.RunParcelLayerUploadBackgroundJob(currentPerson.PersonID), TimeSpan.FromSeconds(30));
        }
    }
}
