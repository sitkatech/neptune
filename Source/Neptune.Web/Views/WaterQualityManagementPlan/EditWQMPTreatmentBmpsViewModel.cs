using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpTreatmentBmpsViewModel : FormViewModel, IValidatableObject
    {
        public IEnumerable<int> TreatmentBmpIDs { get; set; }
        public List<QuickBMPSimple> QuickBmpSimples { get; set; }

        public List<SourceControlBMPSimple> SourceControlBMPSimples { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditWqmpTreatmentBmpsViewModel()
        {
        }

        public EditWqmpTreatmentBmpsViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan, List<SourceControlBMPAttribute> sourceControlBMPAttributes)
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

            var quickBMPsInDatabase = HttpRequestStorage.DatabaseEntities.AllQuickBMPs.Local;
            var quickBMPsToUpdate = quickBMPSimples != null ? quickBMPSimples.Select(x => new QuickBMP(x, waterQualityManagementPlan.TenantID, waterQualityManagementPlan.WaterQualityManagementPlanID)).ToList() : new List<QuickBMP>();


            waterQualityManagementPlan.QuickBMPs.ToList().Merge(quickBMPsToUpdate, quickBMPsInDatabase,
                (x, y) => x.WaterQualityManagementPlanID == y.WaterQualityManagementPlanID &&
                          x.QuickBMPID == y.QuickBMPID, (x, y) =>
                {
                    x.QuickBMPName = y.QuickBMPName;
                    x.QuickBMPNote = y.QuickBMPNote;
                    x.TreatmentBMPType = y.TreatmentBMPType;
                });


            var sourceControlBMPsInDatabase = HttpRequestStorage.DatabaseEntities.AllSourceControlBMPs.Local;
            var sourceControlBMPsToUpdate = sourceControlBMPSimple?.Select(x => new SourceControlBMP(x,
                waterQualityManagementPlan.TenantID, waterQualityManagementPlan.WaterQualityManagementPlanID)).ToList();

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
            
            foreach (var quickBMPSimple in QuickBmpSimples ?? new List<QuickBMPSimple>())
            {
                var quickBMPNoteCharacterLength = quickBMPSimple?.QuickBMPNote.Length;
                if (quickBMPNoteCharacterLength != null && quickBMPNoteCharacterLength > quickBMPNoteMaxCharacterLength)
                {
                    validationResults.Add(new ValidationResult($"\"{quickBMPSimple?.DisplayName}\"'s note is too long. Notes have a maximum of {quickBMPNoteMaxCharacterLength} characters and is {quickBMPNoteCharacterLength - quickBMPNoteMaxCharacterLength} over the limit."));

                }
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
