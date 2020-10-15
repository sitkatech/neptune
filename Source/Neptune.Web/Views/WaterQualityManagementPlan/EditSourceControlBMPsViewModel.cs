using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditSourceControlBMPsViewModel : FormViewModel, IValidatableObject
    {
        public List<SourceControlBMPSimple> SourceControlBMPSimples { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public EditSourceControlBMPsViewModel()
        {
        }

        public EditSourceControlBMPsViewModel(Models.WaterQualityManagementPlan waterQualityManagementPlan, List<SourceControlBMPAttribute> sourceControlBMPAttributes)
        {
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

        public void UpdateModels(Models.WaterQualityManagementPlan waterQualityManagementPlan, List<SourceControlBMPSimple> sourceControlBMPSimple)
        {
            var sourceControlBMPsInDatabase = HttpRequestStorage.DatabaseEntities.SourceControlBMPs.Local;
            var sourceControlBMPsToUpdate = sourceControlBMPSimple?.Select(x => new SourceControlBMP(x, waterQualityManagementPlan.WaterQualityManagementPlanID)).ToList();

            waterQualityManagementPlan.SourceControlBMPs.ToList().Merge(sourceControlBMPsToUpdate, sourceControlBMPsInDatabase, (x, y) => x.SourceControlBMPID == y.SourceControlBMPID,
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
