using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpOMVerificationRecordViewData : NeptuneViewData
    {
        public Models.WaterQualityManagementPlan WaterQualityManagementPlan { get; }

        public EditWqmpOMVerificationRecordViewData(Person currentPerson, Models.WaterQualityManagementPlan waterQualityManagementPlan) : base(currentPerson, StormwaterBreadCrumbEntity.WaterQualityManagementPlan)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            PageTitle = WaterQualityManagementPlan.WaterQualityManagementPlanName;
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());
        }
    }
}
