using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpBmpsViewModel : FormViewModel, IValidatableObject
    {
        public IEnumerable<int> TreatmentBmpIDs { get; set; }
        public List<QuickBMPSimple> QuickBmpSimples { get; set; }

        public List<SourceControlBMPSimple> SourceControlBMPSimples { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpBmpsViewModel()
        {
        }

        public EditWqmpBmpsViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan, List<SourceControlBMPAttribute> sourceControlBMPAttributes)
        {
            TreatmentBmpIDs = waterQualityManagementPlan.TreatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
            QuickBmpSimples = waterQualityManagementPlan.QuickBMPs.Select(x => new QuickBMPSimple(x)).ToList();
            SourceControlBMPSimples = waterQualityManagementPlan.SourceControlBMPs.Select(x => new SourceControlBMPSimple(x)).ToList();

            if (!SourceControlBMPSimples.Any())
            {
                foreach (var sourceControlBMPAttribute in sourceControlBMPAttributes)
                {
                    SourceControlBMPSimples.Add(new SourceControlBMPSimple(sourceControlBMPAttribute));
                }
            }

            SourceControlBMPSimples = SourceControlBMPSimples.OrderBy(x => x.SourceControlBMPAttributeID).ToList();
        }

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan, List<QuickBMPSimple> quickBMPSimples, List<SourceControlBMPSimple> sourceControlBMPSimple)
        {
            waterQualityManagementPlan.TreatmentBMPs.ToList().ForEach(x => { x.WaterQualityManagementPlan = null; });
            TreatmentBmpIDs = TreatmentBmpIDs ?? new List<int>();

            var quickBMPsInDatabase = HttpRequestStorage.DatabaseEntities.QuickBMPs.Local;
            var quickBMPsToUpdate = quickBMPSimples != null ? quickBMPSimples.Select(x => new QuickBMP(x, waterQualityManagementPlan.WaterQualityManagementPlanID)).ToList() : new List<QuickBMP>();

            waterQualityManagementPlan.QuickBMPs.ToList().Merge(quickBMPsToUpdate, quickBMPsInDatabase,
                (x, y) => x.WaterQualityManagementPlanID == y.WaterQualityManagementPlanID &&
                          x.QuickBMPID == y.QuickBMPID, (x, y) =>
                {
                    x.QuickBMPName = y.QuickBMPName;
                    x.QuickBMPNote = y.QuickBMPNote;
                    x.TreatmentBMPTypeID = y.TreatmentBMPTypeID;
                    x.PercentOfSiteTreated = y.PercentOfSiteTreated;
                    x.PercentCaptured = y.PercentCaptured;
                    x.PercentRetained = y.PercentRetained;
                });


            var sourceControlBMPsInDatabase = HttpRequestStorage.DatabaseEntities.SourceControlBMPs.Local;
            var sourceControlBMPsToUpdate = sourceControlBMPSimple?.Select(x => new SourceControlBMP(x, waterQualityManagementPlan.WaterQualityManagementPlanID)).ToList();

            waterQualityManagementPlan.SourceControlBMPs.ToList().Merge(sourceControlBMPsToUpdate, sourceControlBMPsInDatabase, (x, y) => x.SourceControlBMPID == y.SourceControlBMPID,
                (x, y) =>
                {
                    x.IsPresent = y.IsPresent;
                    x.SourceControlBMPNote = y.SourceControlBMPNote;
                });


            HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => TreatmentBmpIDs.Contains(x.TreatmentBMPID))
                .ToList()
                .ForEach(x => { x.WaterQualityManagementPlan = waterQualityManagementPlan; });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            var quickBMPNoteMaxCharacterLength = QuickBMP.FieldLengths.QuickBMPNote;
            var sourceControlBMPNoteMaxCharacterLength = SourceControlBMP.FieldLengths.SourceControlBMPNote;
            var quickBmpWithDuplicateNames = QuickBmpSimples?.Duplicates(x => x.DisplayName).Select(x => x.DisplayName).Distinct();


            foreach (var quickBmpWithDuplicateName in quickBmpWithDuplicateNames ?? new List<string>())
            {
                validationResults.Add(new ValidationResult($"\"{quickBmpWithDuplicateName}\" has already been used Under Other Strucural BMPs. Make sure that all names are unique."));
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
                    validationResults.Add(new ValidationResult("An Other Structural BMP is missing a name."));
                }

                if (quickBMPSimple.QuickTreatmentBMPTypeID <= 0)
                {
                    validationResults.Add(new ValidationResult("An Other Structural BMP is missing a Treatment Type."));
                }
            }

            if (quickBmpSimples.Any(x => x.PercentOfSiteTreated < 0 || x.PercentOfSiteTreated > 100))
            {
                validationResults.Add(new ValidationResult("Percent of Site Treated in Other Structural BMPs needs to be between 0 and 100."));
            }

            if (quickBmpSimples.Any(x => x.PercentCaptured < 0 || x.PercentCaptured > 100))
            {
                validationResults.Add(new ValidationResult("Percent Captured in Other Structural BMPs needs to be between 0 and 100."));
            }

            if (quickBmpSimples.Any(x => x.PercentRetained < 0 || x.PercentRetained > 100))
            {
                validationResults.Add(new ValidationResult("Percent Retained in Other Structural BMPs needs to be between 0 and 100."));
            }

            if (quickBmpSimples.Any(x => x.PercentOfSiteTreated.HasValue) && quickBmpSimples.Sum(x => x.PercentOfSiteTreated) != 100)
            {
                validationResults.Add(new ValidationResult("The Percent of Site Treated exceeds 100 percent, please correct any errors before saving."));
            }

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
