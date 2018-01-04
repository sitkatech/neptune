namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentSimple
    {
        public bool IsComplete { get; set; }
        public string AssessmentScore { get; set; }

        public TreatmentBMPAssessmentSimple(Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            IsComplete = treatmentBMPAssessment.IsAssessmentComplete();
            AssessmentScore = IsComplete ? treatmentBMPAssessment.FormattedScore() : null;
        }
    }
}