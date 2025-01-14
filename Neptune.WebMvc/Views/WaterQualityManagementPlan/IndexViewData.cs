using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Shared;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class IndexViewData : NeptuneViewData
    {
        public IndexGridSpec IndexGridSpec { get; }
        public string IndexGridName { get; }
        public string IndexGridDataUrl { get; }
        public WaterQualityManagementPlanVerificationGridSpec VerificationGridSpec { get; }
        public string VerificationGridName { get; }
        public string VerificationGridDataUrl { get; }
        public string NewWaterQualityManagementPlanUrl { get; }
        public bool CurrentPersonCanCreate { get; }
        public ViewPageContentViewData VerificationNeptunePage { get; }
        public string BulkUploadWQMPUrl { get; }
        public string BulkUploadSimplifiedBMPs { get; }
        public string BulkWqmpBoundaryFromAPNs { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage neptunePage,
            IndexGridSpec indexGridSpec, EFModels.Entities.NeptunePage secondaryNeptunePage, WaterQualityManagementPlanVerificationGridSpec verificationGridSpec) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            var waterQualityManagementPlanPluralized = FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            PageTitle = $"All {waterQualityManagementPlanPluralized}";
            EntityName = $"{waterQualityManagementPlanPluralized}";

            IndexGridSpec = indexGridSpec;
            IndexGridName = "waterQualityManagementPlanIndexGrid";
            IndexGridDataUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.WaterQualityManagementPlanIndexGridData());

            VerificationNeptunePage = new ViewPageContentViewData(linkGenerator, secondaryNeptunePage, currentPerson);
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
            BulkUploadSimplifiedBMPs =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.UploadSimplifiedBMPs());
            BulkWqmpBoundaryFromAPNs =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.UploadWqmpBoundaryFromAPNs());
        }

    }
}
