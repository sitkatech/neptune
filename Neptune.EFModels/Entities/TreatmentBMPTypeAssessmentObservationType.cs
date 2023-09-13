namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPTypeAssessmentObservationType : IHaveASortOrder
    {
        public string GetDisplayName()
        {
            return TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;
        }

        public int GetID()
        {
            return TreatmentBMPTypeAssessmentObservationTypeID;
        }
    }
}