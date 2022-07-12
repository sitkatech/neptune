using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;
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

        public AssessmentPhotosViewModel(Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            if (treatmentBMPAssessment != null)
            {
                PhotoSimples = treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Select(x =>
                    new ManagePhotoWithPreviewPhotoSimple
                    {
                        PrimaryKey = x.TreatmentBMPAssessmentPhotoID,
                        Caption = x.Caption,
                        FlagForDeletion = false
                    }).ToList();
            }
        }

        public void UpdateModels(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            // Merge existing photos
            var photoSimples = PhotoSimples ??
                               new List<ManagePhotoWithPreviewPhotoSimple>();
            var treatmentBMPAssessmentPhotosToUpdate = photoSimples.Select(x =>
                    x.FlagForDeletion // Exclude from list to update if flagged for deletion
                        ? null
                        : new TreatmentBMPAssessmentPhoto(ModelObjectHelpers.NotYetAssignedID,
                            ModelObjectHelpers.NotYetAssignedID)
                        {
                            Caption = x.Caption,
                            TreatmentBMPAssessmentPhotoID = x.PrimaryKey,
                        })
                .Where(x => x != null)
                .ToList();
            treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Merge(treatmentBMPAssessmentPhotosToUpdate,
                HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentPhotos.Local,
                (x, y) => x.TreatmentBMPAssessmentPhotoID == y.TreatmentBMPAssessmentPhotoID,
                (x, y) => { x.Caption = y.Caption; });

            // Create new photo if it exists
            if (Photo != null)
            {
                // for now, setting arbitrary-ish (800) max height and width that roughly corresponds with the largest rendered size on the detail page
                var resizedImage =
                    ImageHelper.ScaleImage(
                        FileResource.ConvertHttpPostedFileToByteArray(Photo), 800, 800);

                var resizedImageBytes = ImageHelper.ImageToByteArrayAndCompress(resizedImage);

                var fileResource = FileResource.CreateNewResizedImageFileResource(Photo, resizedImageBytes, currentPerson);

                new TreatmentBMPAssessmentPhoto(fileResource, treatmentBMPAssessment) {Caption = Caption};
            }
        }
    }
}
