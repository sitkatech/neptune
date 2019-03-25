using System;

namespace Neptune.Web.Models
{
    public partial class OnlandVisualTrashAssessmentPreliminarySourceIdentificationType
    {
        public string GetDisplay()
        {
            if (PreliminarySourceIdentificationType.IsOther())
            {
                return ExplanationIfTypeIsOther;
            }

            return PreliminarySourceIdentificationType.PreliminarySourceIdentificationTypeDisplayName;
        }
    }
}