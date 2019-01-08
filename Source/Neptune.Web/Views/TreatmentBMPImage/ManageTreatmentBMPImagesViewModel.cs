using System;
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.TreatmentBMPImage
{
    public sealed class ManageTreatmentBMPImagesViewModel : ManagePhotosWithPreviewViewModel
    {
        /// <summary>
        ///  Needed by model binder
        /// </summary>
        public ManageTreatmentBMPImagesViewModel()
        {
        }

        public ManageTreatmentBMPImagesViewModel(Models.TreatmentBMP treatmentBMP)
        {
            PhotoSimples = treatmentBMP.TreatmentBMPImages.Select(x =>
                new ManagePhotoWithPreviewPhotoSimple
                {
                    PrimaryKey = x.TreatmentBMPImageID,
                    Caption = x.Caption,
                    FlagForDeletion = false
                }).ToList();
        }

        public void UpdateModels(Person currentPerson, Models.TreatmentBMP treatmentBMP)
        {
            // Merge existing photos
            var photoSimples = PhotoSimples ??
                               new List<ManagePhotoWithPreviewPhotoSimple>();
            var treatmentBMPAssessmentPhotosToUpdate = photoSimples.Select(x =>
                    x.FlagForDeletion // Exclude from list to update if flagged for deletion
                        ? null
                        : new Models.TreatmentBMPImage(ModelObjectHelpers.NotYetAssignedID,
                            ModelObjectHelpers.NotYetAssignedID, DateTime.Now)
                        {
                            Caption = x.Caption,
                            TreatmentBMPImageID = x.PrimaryKey
                        })
                .Where(x => x != null)
                .ToList();
            treatmentBMP.TreatmentBMPImages.Merge(treatmentBMPAssessmentPhotosToUpdate,
                HttpRequestStorage.DatabaseEntities.TreatmentBMPImages.Local,
                (x, y) => x.TreatmentBMPImageID == y.TreatmentBMPImageID,
                (x, y) => { x.Caption = y.Caption; });

            // Create new photo if it exists
            if (Photo != null)
            {
                var fileResource = FileResource.CreateNewFromHttpPostedFile(Photo, currentPerson);
                HttpRequestStorage.DatabaseEntities.TreatmentBMPImages.Add(
                    new Models.TreatmentBMPImage(fileResource, treatmentBMP, DateTime.Now) { Caption = Caption });
            }
        }
    }
}
