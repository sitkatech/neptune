using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentPhotosViewModel : FormViewModel
    {
        public ManagePhotosWithPreviewViewModel ManagePhotosWithPreviewForm { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public AssessmentPhotosViewModel()
        {
            ManagePhotosWithPreviewForm = new ManagePhotosWithPreviewViewModel();
        }

        public AssessmentPhotosViewModel(Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            ManagePhotosWithPreviewForm = new ManagePhotosWithPreviewViewModel
            {
                PhotoSimples = treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Select(x =>
                    new ManagePhotoWithPreviewPhotoSimple
                    {
                        PrimaryKey = x.TreatmentBMPAssessmentPhotoID,
                        Caption = x.Caption,
                        FlagForDeletion = false
                    }).ToList()
            };
        }

        public void UpdateModels(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            // Merge existing photos
            var treatmentBMPAssessmentPhotosToUpdate = ManagePhotosWithPreviewForm.PhotoSimples.Select(x =>
                    x.FlagForDeletion // Exclude from list to update if flagged for deletion
                        ? null
                        : new TreatmentBMPAssessmentPhoto(ModelObjectHelpers.NotYetAssignedID,
                            ModelObjectHelpers.NotYetAssignedID)
                        {
                            TreatmentBMPAssessmentPhotoID = x.PrimaryKey,
                            Caption = x.Caption
                        })
                .Where(x => x != null)
                .ToList();
            treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Merge(treatmentBMPAssessmentPhotosToUpdate,
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessmentPhotos.Local,
                (x, y) => x.TreatmentBMPAssessmentPhotoID == y.TreatmentBMPAssessmentPhotoID,
                (x, y) => { x.Caption = y.Caption; });

            // Create new photo if it exists
            if (ManagePhotosWithPreviewForm.Photo != null)
            {
                var fileResource = FileResource.CreateNewFromHttpPostedFile(ManagePhotosWithPreviewForm.Photo, currentPerson);
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessmentPhotos.Add( 
                    new TreatmentBMPAssessmentPhoto(fileResource, treatmentBMPAssessment) {Caption = ManagePhotosWithPreviewForm.Caption });
            }
        }
    }
}
