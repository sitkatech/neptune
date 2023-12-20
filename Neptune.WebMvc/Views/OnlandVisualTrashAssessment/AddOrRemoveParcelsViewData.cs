using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment.MapInitJson;
using Neptune.WebMvc.Views.Shared.Trash;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
{
    public class AddOrRemoveParcelsViewData: OVTASectionViewData
    {
        public AddOrRemoveParcelsMapIntJson OVTASummaryMapInitJson { get; }
        public ViewDataForAngularClass ViewDataForAngular { get; set; }
        public string RefreshUrl { get; set; }
        public bool RequireRefresh { get; set; }

        public AddOrRemoveParcelsViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration,  Neptune.EFModels.Entities.OVTASection ovtaSection, Neptune.EFModels.Entities.OnlandVisualTrashAssessment ovta, AddOrRemoveParcelsMapIntJson ovtaSummaryMapInitJson, string geoServerUrl) : base(httpContext, linkGenerator, currentPerson, webConfiguration, ovtaSection, ovta)
        {
            OVTASummaryMapInitJson = ovtaSummaryMapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(ovtaSummaryMapInitJson, ovta.IsDraftGeometryManuallyRefined.GetValueOrDefault(), geoServerUrl);
            RefreshUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator,
                    x => x.RefreshParcels(ovta));
            RequireRefresh = ovta.IsDraftGeometryManuallyRefined.GetValueOrDefault();
        }

        public class ViewDataForAngularClass: TrashModuleMapViewDataForAngularBaseClass
        {
            public ViewDataForAngularClass(AddOrRemoveParcelsMapIntJson mapInitJson,
                bool isDraftGeometryManuallyRefined, string geoServerUrl) : base(geoServerUrl)
            {
                MapInitJson = mapInitJson;
                IsDraftGeometryManuallyRefined = isDraftGeometryManuallyRefined;
            }

            public AddOrRemoveParcelsMapIntJson MapInitJson { get; }
            public bool IsDraftGeometryManuallyRefined { get; }
        }
    }
}
