﻿using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;
using Neptune.Web.Services;
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

        public async Task UpdateModel(Person currentPerson, EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment,
            NeptuneDbContext dbContext, FileResourceService fileResourceService)
        {
            // Merge existing photos
            var photoSimples = PhotoSimples ??
                               new List<ManagePhotoWithPreviewPhotoDto>();

            var treatmentBMPAssessmentPhotoIDsToDelete = PhotoSimples?.Where(x => x.FlagForDeletion).Select(x => x.PrimaryKey).ToList() 
                                                         ?? new List<int>();
            var fileResourcesToDelete = dbContext.TreatmentBMPAssessmentPhotos
                .Include(x => x.FileResource)
                .Where(x => treatmentBMPAssessmentPhotoIDsToDelete.Contains(x.TreatmentBMPAssessmentPhotoID)).Select(x => x.FileResource).ToList();
            foreach (var fileResource in fileResourcesToDelete)
            {
                await fileResourceService.DeleteBlobForFileResource(fileResource);
            }

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
                var fileResource = await fileResourceService.CreateNewFromIFormFile(Photo, currentPerson);
                var newPhoto = new TreatmentBMPAssessmentPhoto { FileResource = fileResource, TreatmentBMPAssessment = treatmentBMPAssessment, Caption = Caption };
                await dbContext.TreatmentBMPAssessmentPhotos.AddAsync(newPhoto);

            }
        }
    }
}
