using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class TreatmentBMPExtensionMethods
{
    public static TreatmentBMPDisplayDto AsDisplayDto(this TreatmentBMP treatmentBMP)
    {
        var treatmentBMPSimpleDto = new TreatmentBMPDisplayDto()
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID,
            DisplayName = treatmentBMP.TreatmentBMPName,
            TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName
        };
        return treatmentBMPSimpleDto;
    }

    public static WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto AsWaterQualityManagementPlanVerifyTreatmentBMPSimpleDto(this TreatmentBMP treatmentBMP)
    {
        var waterQualityManagementPlanVerifyTreatmentBMPSimpleDto = new WaterQualityManagementPlanVerifyTreatmentBMPSimpleDto()
        {
            TreatmentBMPName = treatmentBMP.TreatmentBMPName,
            TreatmentBMPID = treatmentBMP.TreatmentBMPID,
            TreatmentBMPType = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName,
        };
        //var mostRecentFieldVisit = treatmentBMP.FieldVisit.Where(x => x.FieldVisitStatus == FieldVisitStatus.Complete).OrderByDescending(x => x.VisitDate).FirstOrDefault();
        //waterQualityManagementPlanVerifyTreatmentBMPSimpleDto.FieldVisiLastVisitedtDate = mostRecentFieldVisit?.VisitDate.ToShortDateString();
        //waterQualityManagementPlanVerifyTreatmentBMPSimpleDto.FieldVisitMostRecentScore = mostRecentFieldVisit?.GetPostMaintenanceAssessment() != null ? mostRecentFieldVisit.GetPostMaintenanceAssessment().FormattedScore() : mostRecentFieldVisit?.GetInitialAssessment()?.FormattedScore();

        return waterQualityManagementPlanVerifyTreatmentBMPSimpleDto;
    }
}