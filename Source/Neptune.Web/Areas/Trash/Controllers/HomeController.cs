using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Controllers;
using Neptune.Web.Security.Shared;
using Neptune.Web.Areas.Trash.Views.Home;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Map;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.JurisdictionControls;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class HomeController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var treatmentBmps = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList()
                .Where(x => x.CanView(CurrentPerson)).ToList();
            var mapInitJson = new SearchMapInitJson("StormwaterIndexMap",
                StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJsonForTrashMap(treatmentBmps, false));
            var jurisdictionLayerGeoJson =
                mapInitJson.Layers.Single(x => x.LayerName == MapInitJsonHelpers.CountyCityLayerName);
            jurisdictionLayerGeoJson.LayerOpacity = 0;
            jurisdictionLayerGeoJson.LayerInitialVisibility = LayerInitialVisibility.Show;

            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TrashHomePage);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, mapInitJson, HttpRequestStorage.DatabaseEntities.TreatmentBMPs, TrashCaptureStatusType.All);

            return RazorView<Index, IndexViewData>(viewData);
        }   
    }
}
