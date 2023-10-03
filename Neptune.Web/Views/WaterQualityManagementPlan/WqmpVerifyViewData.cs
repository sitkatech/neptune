using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class WqmpVerifyViewData : NeptuneViewData
    {
        public WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; }
        public List<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs  { get; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; }
        public UrlTemplate<int> TreatmentBMPDetailUrlTemplate { get; }

        public WqmpVerifyViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBmPs, List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBmPs)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = $"{waterQualityManagementPlanVerify.WaterQualityManagementPlan.WaterQualityManagementPlanName} Verification {waterQualityManagementPlanVerify.LastEditedDate.ToShortDateString()}";
            SubEntityName = waterQualityManagementPlanVerify.WaterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(waterQualityManagementPlanVerify.WaterQualityManagementPlan.WaterQualityManagementPlanID));
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());

            WaterQualityManagementPlanVerify = waterQualityManagementPlanVerify;
            WaterQualityManagementPlanVerifyQuickBMPs = waterQualityManagementPlanVerifyQuickBmPs;
            WaterQualityManagementPlanVerifyTreatmentBMPs = waterQualityManagementPlanVerifyTreatmentBmPs;
            TreatmentBMPDetailUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.Detail(UrlTemplate.Parameter1Int)));

        }
    }
}
