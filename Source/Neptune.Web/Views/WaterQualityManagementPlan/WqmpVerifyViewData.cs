using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class WqmpVerifyViewData : NeptuneViewData
    {
        public WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; }
        public List<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs  { get; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; }

        public WqmpVerifyViewData(Person currentPerson, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBmPs, List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBmPs)
            : base(currentPerson, StormwaterBreadCrumbEntity.WaterQualityManagementPlan)
        {
            PageTitle = $"{waterQualityManagementPlanVerify.WaterQualityManagementPlan.WaterQualityManagementPlanName} Verification {waterQualityManagementPlanVerify.LastEditedDate.ToShortDateString()}";
            SubEntityName = waterQualityManagementPlanVerify.WaterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlanVerify.WaterQualityManagementPlan.WaterQualityManagementPlanPriorityID));
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());

            WaterQualityManagementPlanVerify = waterQualityManagementPlanVerify;
            WaterQualityManagementPlanVerifyQuickBMPs = waterQualityManagementPlanVerifyQuickBmPs;
            WaterQualityManagementPlanVerifyTreatmentBMPs = waterQualityManagementPlanVerifyTreatmentBmPs;
        }
    }
}
