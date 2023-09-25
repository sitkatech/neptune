using System.ComponentModel.DataAnnotations;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditSourceControlBMPsViewModel : FormViewModel, IValidatableObject
    {
        public List<SourceControlBMPUpsertDto> SourceControlBMPSimples { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditSourceControlBMPsViewModel()
        {
        }

        public EditSourceControlBMPsViewModel(List<SourceControlBMPUpsertDto> sourceControlBMPUpsertDtos)
        {
            SourceControlBMPSimples = sourceControlBMPUpsertDtos;
        }

        public void UpdateModel(NeptuneDbContext dbContext, EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, ICollection<SourceControlBMP> existingSourceControlBMPs)
        {
            var sourceControlBMPsInDatabase = dbContext.SourceControlBMPs;
            var sourceControlBMPsToUpdate = SourceControlBMPSimples.Select(x => new SourceControlBMP
                {
                    SourceControlBMPID = x.SourceControlBMPID ?? ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue(),
                    WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID,
                    SourceControlBMPAttributeID = x.SourceControlBMPAttributeID,
                    IsPresent = x.IsPresent,
                    SourceControlBMPNote = x.SourceControlBMPNote
                }
            ).ToList();

            existingSourceControlBMPs.Merge(sourceControlBMPsToUpdate, sourceControlBMPsInDatabase, (x, y) => x.SourceControlBMPID == y.SourceControlBMPID,
                (x, y) =>
                {
                    x.IsPresent = y.IsPresent;
                    x.SourceControlBMPNote = y.SourceControlBMPNote;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var sourceControlBMPNoteMaxCharacterLength = SourceControlBMP.FieldLengths.SourceControlBMPNote;

            foreach (var sourceControlBMP in SourceControlBMPSimples)
            {
                var sourceControlBMPNoteCharacterLength = sourceControlBMP.SourceControlBMPNote?.Length;
                if (sourceControlBMPNoteCharacterLength > sourceControlBMPNoteMaxCharacterLength)
                {
                    validationResults.Add(new ValidationResult($"\"{sourceControlBMP.SourceControlBMPAttributeName}\"'s note is too long. Notes have a maximum of {sourceControlBMPNoteMaxCharacterLength} characters and is {sourceControlBMPNoteCharacterLength - sourceControlBMPNoteMaxCharacterLength} over the limit."));
                }
            }

            return validationResults;
        }
    }
}
