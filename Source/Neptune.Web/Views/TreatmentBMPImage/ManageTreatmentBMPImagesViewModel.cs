using System;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.TreatmentBMPImage
{
    public class ManageTreatmentBMPImagesViewModel : FormViewModel
    {
        /// <summary>
        ///  Needed by model binder
        /// </summary>
        public ManageTreatmentBMPImagesViewModel()
        {
            ManagePhotosWithPreviewForm = new ManagePhotosWithPreviewViewModel();
        }

        public ManageTreatmentBMPImagesViewModel(Models.TreatmentBMP treatmentBMP)
        {
            ManagePhotosWithPreviewForm = new ManagePhotosWithPreviewViewModel
            {
                PhotoSimples = treatmentBMP.TreatmentBMPImages.Select(x =>
                    new ManagePhotoWithPreviewPhotoSimple
                    {
                        PrimaryKey = x.TreatmentBMPImageID,
                        Caption = x.Caption,
                        FlagForDeletion = false
                    }).ToList()
            };
        }

        public HttpPostedFileBase Photo { get; set; }
        public ManagePhotosWithPreviewViewModel ManagePhotosWithPreviewForm { get; set; }

        public void UpdateModels(Person currentPerson, Models.TreatmentBMP treatmentBMP)
        {
            // Merge existing photos
            var treatmentBMPAssessmentPhotosToUpdate = ManagePhotosWithPreviewForm.PhotoSimples.Select(x =>
                    x.FlagForDeletion // Exclude from list to update if flagged for deletion
                        ? null
                        : new Models.TreatmentBMPImage(ModelObjectHelpers.NotYetAssignedID,
                            ModelObjectHelpers.NotYetAssignedID, x.Caption, DateTime.Now)
                        {
                            TreatmentBMPImageID = x.PrimaryKey
                        })
                .Where(x => x != null)
                .ToList();
            treatmentBMP.TreatmentBMPImages.Merge(treatmentBMPAssessmentPhotosToUpdate,
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPImages.Local,
                (x, y) => x.TreatmentBMPImageID == y.TreatmentBMPImageID,
                (x, y) => { x.Caption = y.Caption; });

            // Create new photo if it exists
            if (ManagePhotosWithPreviewForm.Photo != null)
            {
                var fileResource = FileResource.CreateNewFromHttpPostedFile(ManagePhotosWithPreviewForm.Photo, currentPerson);
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPImages.Add(
                    new Models.TreatmentBMPImage(fileResource, treatmentBMP, ManagePhotosWithPreviewForm.Caption, DateTime.Now));
            }
        }
    }
}
