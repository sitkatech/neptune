using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.FieldVisit
{
    public sealed class PhotosViewModel : ManagePhotosWithPreviewViewModel
    {
        /// <summary>
        /// Needed by model binder
        /// </summary>
        public PhotosViewModel()
        {
        }

        public PhotosViewModel(EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            PhotoSimples = treatmentBMP.TreatmentBMPImages.Select(x =>
                new ManagePhotoWithPreviewPhotoDto
                {
                    PrimaryKey = x.TreatmentBMPImageID,
                    Caption = x.Caption,
                    FlagForDeletion = false
                }).ToList();
        }

        public void UpdateModel(Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP, NeptuneDbContext dbContext)
        {
            // Merge existing photos
            var photoSimples = PhotoSimples ??
                               new List<ManagePhotoWithPreviewPhotoDto>();
            var treatmentBMPAssessmentPhotosToUpdate = photoSimples.Select(x =>
                    x.FlagForDeletion // Exclude from list to update if flagged for deletion
                        ? null
                        : new TreatmentBMPImage{
                            UploadDate = DateTime.Now,
                            Caption = x.Caption,
                            TreatmentBMPImageID = x.PrimaryKey
                        })
                .Where(x => x != null)
                .ToList();
            treatmentBMP.TreatmentBMPImages.Merge(treatmentBMPAssessmentPhotosToUpdate,
                dbContext.TreatmentBMPImages,
                (x, y) => x.TreatmentBMPImageID == y.TreatmentBMPImageID,
                (x, y) => { x.Caption = y.Caption; });

            // Create new photo if it exists
            if (Photo != null)
            {
                //todo:
                //var fileResource = FileResource.CreateNewFromHttpPostedFile(Photo, currentPerson);
                //dbContext.TreatmentBMPImages.Add(
                //    new EFModels.Entities.TreatmentBMPImage
                //    {
                //        FileResource = fileResource, TreatmentBMP = treatmentBMP, UploadDate = DateTime.Now,
                //        Caption = Caption
                //    });
            }
        }
    }
}
