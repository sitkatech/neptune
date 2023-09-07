using Neptune.EFModels.Entities;
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
        public string BulkUploadWQMPUrl { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.NeptunePage neptunePage,
            WaterQualityManagementPlanIndexGridSpec indexGridSpec, EFModels.Entities.NeptunePage secondaryNeptunePage, WaterQualityManagementPlanVerificationGridSpec verificationGridSpec) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            var waterQualityManagementPlanPluralized = FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            PageTitle = $"All {waterQualityManagementPlanPluralized}";
            EntityName = $"{waterQualityManagementPlanPluralized}";

            IndexGridSpec = indexGridSpec;
            IndexGridName = "waterQualityManagementPlanIndexGrid";
            IndexGridDataUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.WaterQualityManagementPlanIndexGridData());

            VerificationNeptunePage = new ViewPageContentViewData(secondaryNeptunePage, currentPerson, linkGenerator);
            VerificationGridSpec = verificationGridSpec;
            VerificationGridName = "waterQualityManagementPlanVerificationIndexGrid";
            VerificationGridDataUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.WaterQualityManagementPlanVerificationGridData());

            NewWaterQualityManagementPlanUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,x => x.New());
            CurrentPersonCanCreate = new WaterQualityManagementPlanCreateFeature().HasPermissionByPerson(currentPerson);
            BulkUploadWQMPUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                x.UploadWqmps());
        }

    }
}
