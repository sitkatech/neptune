namespace Neptune.EFModels.Entities;

public partial class FieldVisit
{
    public TreatmentBMPAssessment? GetAssessmentByType(TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum)
    {
        return TreatmentBMPAssessments.SingleOrDefault(x => x.TreatmentBMPAssessmentTypeID == (int)treatmentBMPAssessmentTypeEnum);
    }

    public TreatmentBMPAssessment? GetInitialAssessment()
    {
        return GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.Initial);
    }

    public TreatmentBMPAssessment? GetPostMaintenanceAssessment()
    {
        return GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.PostMaintenance);
    }

    public void DeleteFull(NeptuneDbContext dbContext)
    {
        // todo: deletefull
        throw new NotImplementedException("Deleting of Field Visit not implemented yet!");
    }
}