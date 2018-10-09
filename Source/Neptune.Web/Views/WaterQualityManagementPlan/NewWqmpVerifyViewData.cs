using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System.Web.Mvc;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class NewWqmpVerifyViewData : NeptuneViewData
    {
        public Models.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public List<WaterQualityManagementPlanVerifyType> WaterQualityManagementPlanVerifyTypes { get; }
        public List<WaterQualityManagementPlanVisitStatus> WaterQualityManagementPlanVisitStatuses { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanVerifyStatuses { get; }
        public string WaterQualityManagementPlanUrl { get; }

        public NewWqmpVerifyViewData(Person currentPerson, 
            Models.WaterQualityManagementPlan waterQualityManagementPlan, 
            List<WaterQualityManagementPlanVerifyType>  waterQualityManagementPlanVerifyTypes, 
            List<WaterQualityManagementPlanVisitStatus> waterQualityManagementPlanVisitStatuses,
            List<WaterQualityManagementPlanVerifyStatus>  waterQualityManagementPlanVerifyStatuses) 
            : base(currentPerson, StormwaterBreadCrumbEntity.WaterQualityManagementPlan)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            PageTitle = "New Water Quality Management Plan O&M Verification";

            SubEntityName = WaterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));

            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());

            WaterQualityManagementPlanVerifyTypes = waterQualityManagementPlanVerifyTypes;
            WaterQualityManagementPlanVisitStatuses = waterQualityManagementPlanVisitStatuses;
            WaterQualityManagementPlanVerifyStatuses = waterQualityManagementPlanVerifyStatuses.OrderBy(x => x.WaterQualityManagementPlanVerifyStatusName)
                .ToSelectListWithEmptyFirstRow(x => x.WaterQualityManagementPlanVerifyStatusID.ToString(), x => x.WaterQualityManagementPlanVerifyStatusName, "Status Not Set");

            WaterQualityManagementPlanUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.Detail(waterQualityManagementPlan.WaterQualityManagementPlanPriorityID));
        }
    }
}