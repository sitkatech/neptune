using System.ComponentModel.DataAnnotations;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditSimplifiedStructuralBMPsViewModel : FormViewModel, IValidatableObject
    {
        public List<QuickBMPUpsertDto> QuickBMPs { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditSimplifiedStructuralBMPsViewModel()
        {
        }

        public EditSimplifiedStructuralBMPsViewModel(List<QuickBMPUpsertDto> quickBMPUpsertDtos)
        {
            QuickBMPs = quickBMPUpsertDtos;
        }

        public void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, NeptuneDbContext dbContext, List<QuickBMP> existingQuickBMPs)
        {
            var quickBMPsInDatabase = dbContext.QuickBMPs;
            var quickBMPsToUpdate = QuickBMPs != null
                ? QuickBMPs.Select(x => new QuickBMP
                {
                    WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID,
                    QuickBMPName = x.QuickBMPName,
                    TreatmentBMPTypeID = x.TreatmentBMPTypeID.Value,
                    QuickBMPNote = x.QuickBMPNote,
                    DryWeatherFlowOverrideID = x.DryWeatherFlowOverrideID,
                    PercentOfSiteTreated = x.PercentOfSiteTreated,
                    PercentCaptured = x.PercentCaptured,
                    PercentRetained = x.PercentRetained
                }).ToList()
                : new List<QuickBMP>();

            existingQuickBMPs.Merge(quickBMPsToUpdate, quickBMPsInDatabase,
                (x, y) => x.WaterQualityManagementPlanID == y.WaterQualityManagementPlanID &&
                          x.QuickBMPName == y.QuickBMPName, (x, y) =>
                {
                    x.QuickBMPName = y.QuickBMPName;
                    x.QuickBMPNote = y.QuickBMPNote;
                    x.DryWeatherFlowOverrideID = y.DryWeatherFlowOverrideID;
                    x.TreatmentBMPTypeID = y.TreatmentBMPTypeID;
                    x.PercentOfSiteTreated = y.PercentOfSiteTreated;
                    x.PercentCaptured = y.PercentCaptured;
                    x.PercentRetained = y.PercentRetained;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var quickBMPNoteMaxCharacterLength = QuickBMP.FieldLengths.QuickBMPNote;
            var quickBmpWithDuplicateNames = QuickBMPs?.Duplicates(x => x.QuickBMPName).Select(x => x.QuickBMPName).Distinct();


            foreach (var quickBmpWithDuplicateName in quickBmpWithDuplicateNames ?? new List<string>())
            {
                validationResults.Add(new ValidationResult($"\"{quickBmpWithDuplicateName}\" has already been used. Make sure that all names are unique."));
            }

            var quickBmpSimples = QuickBMPs ?? new List<QuickBMPUpsertDto>();
            foreach (var quickBMPSimple in quickBmpSimples)
            {
                var quickBMPNoteCharacterLength = quickBMPSimple.QuickBMPNote?.Length;
                if (quickBMPNoteCharacterLength != null && quickBMPNoteCharacterLength > quickBMPNoteMaxCharacterLength)
                {
                    validationResults.Add(new ValidationResult($"\"{quickBMPSimple?.QuickBMPName}\"'s note is too long. Notes have a maximum of {quickBMPNoteMaxCharacterLength} characters and is {quickBMPNoteCharacterLength - quickBMPNoteMaxCharacterLength} over the limit."));

                }
            }

            if (quickBmpSimples.Any(x => x.PercentRetained > x.PercentCaptured))
            {
                validationResults.Add(new ValidationResult("Percent Captured needs to be greater than Percent Retained."));
            }

            if (quickBmpSimples.Any(x => x.PercentOfSiteTreated < 0 || x.PercentOfSiteTreated > 100))
            {
                validationResults.Add(new ValidationResult("Percent of Site Treated needs to be between 0 and 100."));
            }

            if (quickBmpSimples.Any(x => x.PercentCaptured < 0 || x.PercentCaptured > 100))
            {
                validationResults.Add(new ValidationResult("Percent Captured needs to be between 0 and 100."));
            }

            if (quickBmpSimples.Any(x => x.PercentRetained < 0 || x.PercentRetained > 100))
            {
                validationResults.Add(new ValidationResult("Percent Retained needs to be between 0 and 100."));
            }

            if (quickBmpSimples.Any(x => x.PercentOfSiteTreated.HasValue) && quickBmpSimples.Sum(x => x.PercentOfSiteTreated) > 100)
            {
                validationResults.Add(new ValidationResult("The Percent of Site Treated exceeds 100 percent, please correct any errors before saving."));
            }

            return validationResults;
        }
    }
}
