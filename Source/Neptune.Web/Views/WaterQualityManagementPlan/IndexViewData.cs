using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class IndexViewData : NeptuneViewData
    {
        public WaterQualityManagementPlanIndexGridSpec IndexGridSpec { get; }
        public string IndexGridName { get; }
        public string IndexGridDataUrl { get; }
        public WaterQualityManagementPlanVerificationGridSpec VerificationGridSpec { get; }
        public string VerificationGridName { get; }
        public string VerificationGridDataUrl { get; }
        public string NewWaterQualityManagementPlanUrl { get; }
        public bool CurrentPersonCanCreate { get; }
        public ViewPageContentViewData VerificationNeptunePage { get; }

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage,
            WaterQualityManagementPlanIndexGridSpec indexGridSpec, Models.NeptunePage secondaryNeptunePage, WaterQualityManagementPlanVerificationGridSpec verificationGridSpec) : base(currentPerson, neptunePage)
        {
            var waterQualityManagementPlanPluralized = Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            PageTitle = $"All {waterQualityManagementPlanPluralized}";
            EntityName = $"{waterQualityManagementPlanPluralized}";

            IndexGridSpec = indexGridSpec;
            IndexGridName = "waterQualityManagementPlanIndexGrid";
            IndexGridDataUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.WaterQualityManagementPlanIndexGridData());

            VerificationNeptunePage = new ViewPageContentViewData(secondaryNeptunePage, currentPerson);
            VerificationGridSpec = verificationGridSpec;
            VerificationGridName = "waterQualityManagementPlanVerificationIndexGrid";
            VerificationGridDataUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.WaterQualityManagementPlanVerificationGridData());

            NewWaterQualityManagementPlanUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.New());
            CurrentPersonCanCreate = new WaterQualityManagementPlanCreateFeature().HasPermissionByPerson(currentPerson);
        }

    }
}
