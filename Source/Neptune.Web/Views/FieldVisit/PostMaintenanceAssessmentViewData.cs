using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class PostMaintenanceAssessmentViewData : FieldVisitSectionViewData
    {
        public Models.TreatmentBMPAssessment PostMaintenanceAssessment { get; }

        public PostMaintenanceAssessmentViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit, Models.FieldVisitSection.PostMaintenanceAssessment)
        {
            PostMaintenanceAssessment = fieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.PostMaintenance);
        }
    }
}