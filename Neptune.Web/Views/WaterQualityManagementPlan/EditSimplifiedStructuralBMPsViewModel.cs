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
        public List<QuickBMPSimpleDto> QuickBmpSimples { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditSimplifiedStructuralBMPsViewModel()
        {
        }

        public EditSimplifiedStructuralBMPsViewModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            QuickBmpSimples = waterQualityManagementPlan.QuickBMPs.Select(x => x.AsSimpleDto()).ToList();
        }

        public void UpdateModel(EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, List<QuickBMPSimpleDto> quickBMPSimples, NeptuneDbContext dbContext)
        {
            var quickBMPsInDatabase = dbContext.QuickBMPs;
            var quickBMPsToUpdate = quickBMPSimples != null
                ? quickBMPSimples.Select(x => new QuickBMP
                {
                    QuickBMPID = x.QuickBMPID,
                    WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID,
                    QuickBMPName = x.QuickBMPName,
                    TreatmentBMPTypeID = x.TreatmentBMPTypeID,
                    QuickBMPNote = x.QuickBMPNote,
                    DryWeatherFlowOverrideID = x.DryWeatherFlowOverrideID,
                    PercentOfSiteTreated = x.PercentOfSiteTreated,
                    PercentCaptured = x.PercentCaptured,
                    PercentRetained = x.PercentRetained
                }).ToList()
                : new List<QuickBMP>();

            waterQualityManagementPlan.QuickBMPs.ToList().Merge(quickBMPsToUpdate, quickBMPsInDatabase,
                (x, y) => x.WaterQualityManagementPlanID == y.WaterQualityManagementPlanID &&
                          x.QuickBMPID == y.QuickBMPID, (x, y) =>
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
            var quickBmpWithDuplicateNames = QuickBmpSimples?.Duplicates(x => x.QuickBMPName).Select(x => x.QuickBMPName).Distinct();


            foreach (var quickBmpWithDuplicateName in quickBmpWithDuplicateNames ?? new List<string>())
            {
                validationResults.Add(new ValidationResult($"\"{quickBmpWithDuplicateName}\" has already been used. Make sure that all names are unique."));
            }

            var quickBmpSimples = QuickBmpSimples ?? new List<QuickBMPSimpleDto>();
            foreach (var quickBMPSimple in quickBmpSimples)
            {
                var quickBMPNoteCharacterLength = quickBMPSimple.QuickBMPNote?.Length;
                if (quickBMPNoteCharacterLength != null && quickBMPNoteCharacterLength > quickBMPNoteMaxCharacterLength)
                {
                    validationResults.Add(new ValidationResult($"\"{quickBMPSimple?.QuickBMPName}\"'s note is too long. Notes have a maximum of {quickBMPNoteMaxCharacterLength} characters and is {quickBMPNoteCharacterLength - quickBMPNoteMaxCharacterLength} over the limit."));

                }

                if (quickBMPSimple.QuickBMPName == null)
                {
                    validationResults.Add(new ValidationResult("A Simplified Structural BMP is missing a name."));
                }

                if (quickBMPSimple.TreatmentBMPTypeID <= 0)
                {
                    validationResults.Add(new ValidationResult("A Simplified Structural BMP is missing a Treatment Type."));
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
