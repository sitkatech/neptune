using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditSourceControlBMPsViewData : NeptuneViewData
    {
        public string WaterQualityManagementPlanDetailUrl { get; }

        public EditSourceControlBMPsViewData(Person currentPerson, Models.WaterQualityManagementPlan waterQualityManagementPlan) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
            PageTitle = "Edit Source Control BMPs";

            WaterQualityManagementPlanDetailUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
        }
    }
}
