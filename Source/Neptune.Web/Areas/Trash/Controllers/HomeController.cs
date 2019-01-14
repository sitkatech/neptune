using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Controllers;
using Neptune.Web.Areas.Trash.Views.Home;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;

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
            var treatmentBMPLayerGeoJson = TrashModuleMapInitJson.MakeTreatmentBMPLayerGeoJsonForTrashMap(treatmentBmps, false);

            var parcels = HttpRequestStorage.DatabaseEntities.Parcels.ToList();
            var parcelLayerGeoJson = TrashModuleMapInitJson.MakeParcelLayerGeoJsonForTrashMap(parcels, false);

            var mapInitJson = new TrashModuleMapInitJson("StormwaterIndexMap", treatmentBMPLayerGeoJson, parcelLayerGeoJson);

            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TrashHomePage);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, mapInitJson, HttpRequestStorage.DatabaseEntities.TreatmentBMPs, TrashCaptureStatusType.All);

            return RazorView<Index, IndexViewData>(viewData);
        }   
    }
}
