using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models;

public static class FieldVisitSectionModelExtensions
{
    public static string GetSectionUrl(this FieldVisitSection fieldVisitSection,
        EFModels.Entities.FieldVisit fieldVisit, LinkGenerator linkGenerator)
    {
        var fieldVisitSectionEnum = fieldVisitSection.ToEnum;
        switch (fieldVisitSectionEnum)
        {
            case FieldVisitSectionEnum.Inventory:
                return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Inventory(fieldVisit));
            case FieldVisitSectionEnum.Assessment:
                return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Assessment(fieldVisit));
            case FieldVisitSectionEnum.Maintenance:
                return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Maintain(fieldVisit));
            case FieldVisitSectionEnum.PostMaintenanceAssessment:
                return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.PostMaintenanceAssessment(fieldVisit));
            case FieldVisitSectionEnum.VisitSummary:
                return SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.VisitSummary(fieldVisit));
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static IEnumerable<FieldVisitSubsectionData> GetSubsections(this FieldVisitSection fieldVisitSection,
        EFModels.Entities.FieldVisit fieldVisit, LinkGenerator linkGenerator)
    {
        var fieldVisitSectionEnum = fieldVisitSection.ToEnum;
        switch (fieldVisitSectionEnum)
        {
            case FieldVisitSectionEnum.Inventory:
                return new[]
                {
                    new FieldVisitSubsectionData
                    {
                        SubsectionName = "Location",
                        SubsectionUrl =
                            SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator,
                                x => x.Location(fieldVisit))
                    },
                    new FieldVisitSubsectionData
                    {
                        SubsectionName = "Photos",
                        SubsectionUrl =
                            SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator,
                                x => x.Photos(fieldVisit))
                    },
                    new FieldVisitSubsectionData
                    {
                        SubsectionName = "Attributes",
                        SubsectionUrl =
                            SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator,
                                x => x.Attributes(fieldVisit))
                    }
                };
                break;
            case FieldVisitSectionEnum.Assessment:
                return GetAssessmentSubsections(linkGenerator, fieldVisit, TreatmentBMPAssessmentTypeEnum.Initial);
            case FieldVisitSectionEnum.Maintenance:
                return new List<FieldVisitSubsectionData>
                {
                    new()
                    {
                        SubsectionName = "Edit Maintenance Record",
                        SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator,
                            x => x.EditMaintenanceRecord(fieldVisit))
                    }
                };
            case FieldVisitSectionEnum.PostMaintenanceAssessment:
                return GetAssessmentSubsections(linkGenerator, fieldVisit, TreatmentBMPAssessmentTypeEnum.PostMaintenance);
            case FieldVisitSectionEnum.VisitSummary:
                return new List<FieldVisitSubsectionData>();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static IEnumerable<FieldVisitSubsectionData> GetAssessmentSubsections(LinkGenerator linkGenerator,
        EFModels.Entities.FieldVisit fieldVisit, TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum)
    {
        var treatmentBMP = fieldVisit.TreatmentBMP;
        var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(treatmentBMPAssessmentTypeEnum);

        var assessmentSubsections = new List<FieldVisitSubsectionData>
        {
            new()
            {
                SubsectionName = "Observations",
                SubsectionUrl =
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, c =>
                        c.Observations(fieldVisit, treatmentBMPAssessmentTypeEnum)),
                SectionCompletionStatusIndicator =
                    treatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.All(x =>
                        treatmentBMPAssessment.IsObservationComplete(x.TreatmentBMPAssessmentObservationType))
                        ? new HtmlString(
                            "<span class='glyphicon glyphicon-ok field-validation-success text-left' style='color: #5cb85c; margin-right: 4px'></span>")
                        : new HtmlString(
                            "<span class='glyphicon glyphicon-exclamation-sign field-validation-warning text-left' style='margin-right: 4px'></span>")
            },
            new()
            {
                SubsectionName = "Photos",
                SubsectionUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, c =>
                    c.AssessmentPhotos(fieldVisit, treatmentBMPAssessmentTypeEnum)),
                SectionCompletionStatusIndicator = treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Any()
                    ? new HtmlString(
                        "<span class='glyphicon glyphicon-ok field-validation-success text-left' style='color: #5cb85c; margin-right: 4px'></span>")
                    : new HtmlString("<span style=\"width: 19px; display: inline-block;\"></span>")
            }
        };

        return assessmentSubsections;
    }
}