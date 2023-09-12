using Neptune.Web.Models;

namespace Neptune.EFModels.Entities;

public partial class FieldVisit : IAuditableEntity
{
    public string GetAuditDescriptionString() => "Field Visit deleted";

    public TreatmentBMPAssessment GetInitialAssessment()
    {
        return TreatmentBMPAssessments.SingleOrDefault(x => x.TreatmentBMPAssessmentType == TreatmentBMPAssessmentType.Initial);
    }

    public TreatmentBMPAssessment GetPostMaintenanceAssessment()
    {
        return TreatmentBMPAssessments.SingleOrDefault(x => x.TreatmentBMPAssessmentType == TreatmentBMPAssessmentType.PostMaintenance);
    }

    public void DeleteFull(NeptuneDbContext dbContext)
    {
        // todo:
        throw new NotImplementedException();
    }
}