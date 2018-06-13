using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class IndexViewData : NeptuneViewData
    {
        public WaterQualityManagementPlanIndexGridSpec IndexGridSpec { get; }
        public string IndexGridName { get; }
        public string IndexGridDataUrl { get; }
        public string NewWaterQualityManagementPlanUrl { get; }
        public bool CurrentPersonCanCreate { get; }

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage,
            WaterQualityManagementPlanIndexGridSpec indexGridSpec) : base(currentPerson, StormwaterBreadCrumbEntity.WaterQualityManagementPlan, neptunePage)
        {
            var waterQualityManagementPlanPluralized = Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            PageTitle = $"All {waterQualityManagementPlanPluralized}";
            EntityName = $"{waterQualityManagementPlanPluralized}";

            IndexGridSpec = indexGridSpec;
            IndexGridName = "waterQualityManagementPlanIndexGrid";
            IndexGridDataUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.WaterQualityManagementPlanIndexGridData());

            NewWaterQualityManagementPlanUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.New());
            CurrentPersonCanCreate = new WaterQualityManagementPlanCreateFeature().HasPermissionByPerson(currentPerson);
        }
    }
}
