using System.Linq;
using System.Web.Mvc;
using MoreLinq;
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


            // don't even think about touching the actual parcel tabel, unless you want to materialize half a million big geometry bois
            var parcels = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanParcels.Select(x => x.Parcel)
                .AsEnumerable().DistinctBy(x => x.ParcelID).ToList();
            var parcelLayerGeoJson = TrashModuleMapInitJson.MakeParcelLayerGeoJsonForTrashMap(parcels, false);


            var stormwaterJurisdictionsPersonCanEdit = CurrentPerson.GetStormwaterJurisdictionsPersonCanEdit().ToList();
            // todo: create bounding box from stormwater jurisdictions

            var mapInitJson = new TrashModuleMapInitJson("StormwaterIndexMap", treatmentBMPLayerGeoJson, parcelLayerGeoJson);

            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TrashHomePage);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, mapInitJson, treatmentBmps, TrashCaptureStatusType.All, parcels);

            return RazorView<Index, IndexViewData>(viewData);
        }   
    }
}
