using System.Collections.Generic;
using Neptune.Web.Security;
using Neptune.Web.Views.NetworkCatchment;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Controllers
{
    public class NetworkCatchmentController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var geoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            var networkCatchmentLayerName = NeptuneWebConfiguration.NetworkCatchmentLayerName;

            var viewData = new IndexViewData(CurrentPerson, new NetworkCatchmentMapInitJson("networkCatchmentMap"), geoServerUrl, networkCatchmentLayerName);
            return RazorView<Index, IndexViewData>(viewData);
        }
    }

    public class NetworkCatchmentMapInitJson : MapInitJson
    {
        public NetworkCatchmentMapInitJson(string mapDivID) : base(mapDivID, DefaultZoomLevel, new List<LayerGeoJson>(), BoundingBox.MakeNewDefaultBoundingBox())
        {
        }
    }
}