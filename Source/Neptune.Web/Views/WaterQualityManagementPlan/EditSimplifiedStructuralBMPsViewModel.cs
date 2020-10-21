using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditSimplifiedStructuralBMPsViewModel : FormViewModel, IValidatableObject
    {
        public List<QuickBMPSimple> QuickBmpSimples { get; set; }

        public List<SourceControlBMPSimple> SourceControlBMPSimples { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditSimplifiedStructuralBMPsViewModel()
        {
        }

        public EditSimplifiedStructuralBMPsViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan)
        {
            QuickBmpSimples = waterQualityManagementPlan.QuickBMPs.Select(x => new QuickBMPSimple(x)).ToList();
        }

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan, List<QuickBMPSimple> quickBMPSimples)
        {
            var quickBMPsInDatabase = HttpRequestStorage.DatabaseEntities.QuickBMPs.Local;
            var quickBMPsToUpdate = quickBMPSimples != null ? quickBMPSimples.Select(x => new QuickBMP(x, waterQualityManagementPlan.WaterQualityManagementPlanID)).ToList() : new List<QuickBMP>();

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
            var quickBmpWithDuplicateNames = QuickBmpSimples?.Duplicates(x => x.DisplayName).Select(x => x.DisplayName).Distinct();


            foreach (var quickBmpWithDuplicateName in quickBmpWithDuplicateNames ?? new List<string>())
            {
                validationResults.Add(new ValidationResult($"\"{quickBmpWithDuplicateName}\" has already been used. Make sure that all names are unique."));
            }

            var quickBmpSimples = QuickBmpSimples ?? new List<QuickBMPSimple>();
            foreach (var quickBMPSimple in quickBmpSimples)
            {
                var quickBMPNoteCharacterLength = quickBMPSimple.QuickBMPNote?.Length;
                if (quickBMPNoteCharacterLength != null && quickBMPNoteCharacterLength > quickBMPNoteMaxCharacterLength)
                {
                    validationResults.Add(new ValidationResult($"\"{quickBMPSimple?.DisplayName}\"'s note is too long. Notes have a maximum of {quickBMPNoteMaxCharacterLength} characters and is {quickBMPNoteCharacterLength - quickBMPNoteMaxCharacterLength} over the limit."));

                }

                if (quickBMPSimple.DisplayName == null)
                {
                    validationResults.Add(new ValidationResult("A Simplified Structural BMP is missing a name."));
                }

                if (quickBMPSimple.QuickTreatmentBMPTypeID <= 0)
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
