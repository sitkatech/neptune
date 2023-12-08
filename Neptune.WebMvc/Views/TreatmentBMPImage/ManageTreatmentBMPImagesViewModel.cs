using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Services;
using Neptune.WebMvc.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.WebMvc.Views.TreatmentBMPImage
{
    public sealed class ManageTreatmentBMPImagesViewModel : ManagePhotosWithPreviewViewModel
    {
        /// <summary>
        ///  Needed by model binder
        /// </summary>
        public ManageTreatmentBMPImagesViewModel()
        {
        }

        public ManageTreatmentBMPImagesViewModel(IEnumerable<EFModels.Entities.TreatmentBMPImage> treatmentBMPImages)
        {
            PhotoSimples = treatmentBMPImages.Select(x =>
                new ManagePhotoWithPreviewPhotoDto()
                {
                    PrimaryKey = x.TreatmentBMPImageID,
                    Caption = x.Caption,
                    FlagForDeletion = false
                }).ToList();
        }

        public async Task UpdateModel(Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP,
            NeptuneDbContext dbContext, FileResourceService fileResourceService, ICollection<EFModels.Entities.TreatmentBMPImage> existingTreatmentBMPImages)
        {
            // Merge existing photos
            var photoSimples = PhotoSimples ??
                               new List<ManagePhotoWithPreviewPhotoDto>();

            // take care of blob deletion first
            var treatmentBMPImageIDsToDelete = PhotoSimples?.Where(x => x.FlagForDeletion).Select(x => x.PrimaryKey).ToList()
                                                         ?? new List<int>();
            var fileResourcesToDelete = dbContext.TreatmentBMPImages
                .Include(x => x.FileResource)
                .Where(x => treatmentBMPImageIDsToDelete.Contains(x.TreatmentBMPImageID)).Select(x => x.FileResource).ToList();
            foreach (var fileResource in fileResourcesToDelete)
            {
                await fileResourceService.DeleteBlobForFileResource(fileResource);
            }

            var treatmentBMPImagesToUpdate = photoSimples.Select(x =>
                    x.FlagForDeletion // Exclude from list to update if flagged for deletion
                        ? null
                        : new EFModels.Entities.TreatmentBMPImage
                        {
                            UploadDate = DateOnly.FromDateTime(DateTime.Now),
                            Caption = x.Caption,
                            TreatmentBMPImageID = x.PrimaryKey
                        })
                .Where(x => x != null)
                .ToList();
            existingTreatmentBMPImages.Merge(treatmentBMPImagesToUpdate,
                dbContext.TreatmentBMPImages,
                (x, y) => x.TreatmentBMPImageID == y.TreatmentBMPImageID,
                (x, y) => { x.Caption = y.Caption; });

            // Create new photo if it exists
            if (Photo != null)
            {
                var fileResource = await fileResourceService.CreateNewFromIFormFile(Photo, currentPerson);
                dbContext.TreatmentBMPImages.Add(
                    new EFModels.Entities.TreatmentBMPImage
                    {
                        FileResource = fileResource,
                        TreatmentBMP = treatmentBMP,
                        UploadDate = DateOnly.FromDateTime(DateTime.Now),
                        Caption = Caption
                    });
            }
        }
    }
}
