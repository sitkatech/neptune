using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.RegionalSubbasinRevisionRequest
{
    public class NewViewData : NeptuneViewData
    {
        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }
        public string SubmitUrl { get; }
        public RegionalSubbasinRevisionRequestMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
        public string MapFormID { get; }
        public EFModels.Entities.RegionalSubbasinRevisionRequest ExistingOpenRequest { get; set; }
        public string TreatmentBMPDetailUrl { get; }
        public string TreatmentBMPDelineationMapUrl { get; }
        public string? ExistingOpenRequestDetailUrl { get; }

        public NewViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP, RegionalSubbasinRevisionRequestMapInitJson mapInitJson, string geoServerUrl) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            TreatmentBMP = treatmentBMP;
            MapInitJson = mapInitJson;
            GeoServerUrl = geoServerUrl;
            MapFormID = "revisionRequestHiddenInputContainer";
            SubmitUrl =
                SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator, x => x.New(treatmentBMP));
            EntityName = "Regional Subbasin";
            EntityUrl = SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = "Revision";
            ExistingOpenRequest = treatmentBMP.RegionalSubbasinRevisionRequests.SingleOrDefault(x => x.RegionalSubbasinRevisionRequestStatusID == RegionalSubbasinRevisionRequestStatus.Open.RegionalSubbasinRevisionRequestStatusID);

            TreatmentBMPDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP.TreatmentBMPID));
            TreatmentBMPDelineationMapUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationMap(treatmentBMP.TreatmentBMPID));
            ExistingOpenRequestDetailUrl = ExistingOpenRequest == null ? string.Empty : SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(ExistingOpenRequest.RegionalSubbasinRevisionRequestID));

        }
    }

    public class RegionalSubbasinRevisionRequestMapInitJson : MapInitJson
    {
        public LayerGeoJson CentralizedDelineationLayerGeoJson { get; }

        public RegionalSubbasinRevisionRequestMapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers,
            BoundingBoxDto boundingBox, LayerGeoJson centralizedDelineationLayerGeoJson) : base(mapDivID, zoomLevel, layers, boundingBox)
        {
            CentralizedDelineationLayerGeoJson = centralizedDelineationLayerGeoJson;
        }
    }
}
