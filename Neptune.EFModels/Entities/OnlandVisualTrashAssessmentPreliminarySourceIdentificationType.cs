namespace Neptune.EFModels.Entities
{
    public partial class OnlandVisualTrashAssessmentPreliminarySourceIdentificationType
    {
        public string GetDisplay()
        {
            if (PreliminarySourceIdentificationType.IsOther())
            {
                return $"Other: {ExplanationIfTypeIsOther}";
            }

            return PreliminarySourceIdentificationType.PreliminarySourceIdentificationTypeDisplayName;
        }
    }
}