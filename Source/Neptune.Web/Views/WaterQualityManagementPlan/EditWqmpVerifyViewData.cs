using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpVerifyViewData : NeptuneViewData
    {
        public Models.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public List<WaterQualityManagementPlanVerifyType> WaterQualityManagementPlanVerifyTypes { get; }
        List<WaterQualityManagementPlanVisitStatus> WaterQualityManagementPlanVisitStatuses { get; }
        List<WaterQualityManagementPlanVerifyStatus>  WaterQualityManagementPlanVerifyStatuses { get; }

        public EditWqmpVerifyViewData(Person currentPerson, 
            Models.WaterQualityManagementPlan waterQualityManagementPlan, 
            List<WaterQualityManagementPlanVerifyType>  waterQualityManagementPlanVerifyTypes, 
            List<WaterQualityManagementPlanVisitStatus> waterQualityManagementPlanVisitStatuses,
            List<WaterQualityManagementPlanVerifyStatus>  waterQualityManagementPlanVerifyStatuses) 
            : base(currentPerson, StormwaterBreadCrumbEntity.WaterQualityManagementPlan)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            PageTitle = WaterQualityManagementPlan.WaterQualityManagementPlanName;
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());

            WaterQualityManagementPlanVerifyTypes = waterQualityManagementPlanVerifyTypes;
            WaterQualityManagementPlanVisitStatuses = waterQualityManagementPlanVisitStatuses;
            WaterQualityManagementPlanVerifyStatuses = waterQualityManagementPlanVerifyStatuses;
        }
    }
}
