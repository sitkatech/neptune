using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class NewViewData : NeptuneViewData
    {
        public Models.TreatmentBMP TreatmentBMP { get; }
        public string SubmitUrl { get; }
        public RegionalSubbasinRevisionRequestMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
        public string MapFormID { get; }

        public NewViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP, RegionalSubbasinRevisionRequestMapInitJson mapInitJson) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMP = treatmentBMP;
            MapInitJson = mapInitJson;
            GeoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            MapFormID = "revisionRequestHiddenInputContainer";
            SubmitUrl =
                SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(x => x.New(treatmentBMP));
            EntityName = "Regional Subbasin";
            PageTitle = "Revision";
        }
    }

    public class RegionalSubbasinRevisionRequestMapInitJson : MapInitJson
    {
        public LayerGeoJson CentralizedDelineationLayerGeoJson { get; }

        public RegionalSubbasinRevisionRequestMapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers,
            BoundingBox boundingBox, LayerGeoJson centralizedDelineationLayerGeoJson) : base(mapDivID, zoomLevel, layers, boundingBox)
        {
            CentralizedDelineationLayerGeoJson = centralizedDelineationLayerGeoJson;
        }
    }
}