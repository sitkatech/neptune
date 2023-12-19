using Neptune.EFModels.Entities;
using Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment.MapInitJson;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RecordObservationsViewData : OVTASectionViewData
    {
        public ViewDataForAngularClass ViewDataForAngular { get; }
        public OVTAObservationsMapInitJson MapInitJson { get; }

        public RecordObservationsViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration,  EFModels.Entities.OnlandVisualTrashAssessment ovta,
            OVTAObservationsMapInitJson mapInitJson, string geoServerUrl)
            : base(httpContext, linkGenerator, currentPerson, webConfiguration, Neptune.EFModels.Entities.OVTASection.RecordObservations, ovta)
        {
            MapInitJson = mapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson, ovta, geoServerUrl, ovta.StormwaterJurisdictionID);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(OVTAObservationsMapInitJson mapInitJson,
                Neptune.EFModels.Entities.OnlandVisualTrashAssessment ovta, string geoServerUrl, int? ovtaStormwaterJurisdictionID)
            {
                MapInitJson = mapInitJson;
                GeoServerUrl = geoServerUrl;
                OVTAStormwaterJurisdictionID = ovtaStormwaterJurisdictionID;
                ovtaID = ovta.OnlandVisualTrashAssessmentID;

            }

            public OVTAObservationsMapInitJson MapInitJson { get; }
            public int ovtaID { get; }
            public string GeoServerUrl { get; }
            public int? OVTAStormwaterJurisdictionID { get; }
        }
    }
}