using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.FieldVisit
{
    public sealed class AssessmentPhotosViewModel : ManagePhotosWithPreviewViewModel
    {
        /// <summary>
        /// Needed by model binder
        /// </summary>
        public AssessmentPhotosViewModel()
        {
        }

        public AssessmentPhotosViewModel(EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            if (treatmentBMPAssessment != null)
            {
                PhotoSimples = treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Select(x =>
                    new ManagePhotoWithPreviewPhotoDto
                    {
                        PrimaryKey = x.TreatmentBMPAssessmentPhotoID,
                        Caption = x.Caption,
                        FlagForDeletion = false
                    }).ToList();
            }
        }

        public void UpdateModel(Person currentPerson, EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment, NeptuneDbContext dbContext)
        {
            // Merge existing photos
            var photoSimples = PhotoSimples ??
                               new List<ManagePhotoWithPreviewPhotoDto>();
            var treatmentBMPAssessmentPhotosToUpdate = photoSimples.Select(x =>
                    x.FlagForDeletion // Exclude from list to update if flagged for deletion
                        ? null
                        : new TreatmentBMPAssessmentPhoto
                        {
                            Caption = x.Caption,
                            TreatmentBMPAssessmentPhotoID = x.PrimaryKey,
                        })
                .Where(x => x != null)
                .ToList();
            treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Merge(treatmentBMPAssessmentPhotosToUpdate,
                dbContext.TreatmentBMPAssessmentPhotos,
                (x, y) => x.TreatmentBMPAssessmentPhotoID == y.TreatmentBMPAssessmentPhotoID,
                (x, y) => { x.Caption = y.Caption; });

            // Create new photo if it exists
            if (Photo != null)
            {
                //todo:
                // for now, setting arbitrary-ish (800) max height and width that roughly corresponds with the largest rendered size on the detail page
                //var resizedImage =
                //    ImageHelper.ScaleImage(
                //        FileResource.ConvertHttpPostedFileToByteArray(Photo), 800, 800);

                //var resizedImageBytes = ImageHelper.ImageToByteArrayAndCompress(resizedImage);

                //var fileResource = FileResource.CreateNewResizedImageFileResource(Photo, resizedImageBytes, currentPerson);

                //new TreatmentBMPAssessmentPhoto{FileResource = fileResource, TreatmentBMPAssessment = treatmentBMPAssessment, Caption = Caption};
            }
        }
    }
}
