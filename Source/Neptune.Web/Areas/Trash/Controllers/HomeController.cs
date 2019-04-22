using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using MoreLinq;
using Neptune.Web.Controllers;
using Neptune.Web.Areas.Trash.Views.Home;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class HomeController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var treatmentBmps = CurrentPerson.GetTreatmentBmpsPersonCanManage();
            var treatmentBMPLayerGeoJson = TrashModuleMapInitJson.MakeTreatmentBMPLayerGeoJsonForTrashMap(treatmentBmps, false);

            var parcels = HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlanParcels.Select(x => x.Parcel)
                .AsEnumerable().DistinctBy(x => x.ParcelID).ToList();
            var parcelLayerGeoJson = TrashModuleMapInitJson.MakeParcelLayerGeoJsonForTrashMap(parcels, false);

            var boundingBox = CurrentPerson.GetBoundingBox();
            var ovtaBasedMapInitJson = new TrashModuleMapInitJson("ovtaBasedResultsMap", treatmentBMPLayerGeoJson, parcelLayerGeoJson, boundingBox);
            var areaBasedMapInitJson = new StormwaterMapInitJson("areaBasedResultsMap", boundingBox);
            var loadBasedMapInitJson= new StormwaterMapInitJson("loadBasedResultsMap", boundingBox);

            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TrashHomePage);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, ovtaBasedMapInitJson, areaBasedMapInitJson,
                loadBasedMapInitJson, treatmentBmps, TrashCaptureStatusType.All, parcels);

            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RefreshTrashGeneratingUnits()
        {
            return ViewRefreshTrashGeneratingUnits(new ConfirmDialogFormViewModel());
        }

        private PartialViewResult ViewRefreshTrashGeneratingUnits(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = "This operation will take several minutes to run. Updated data will not be available until the operation is complete.";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public async Task<ActionResult> RefreshTrashGeneratingUnits(ConfirmDialogFormViewModel viewModel)
        {
            HttpRequestStorage.DatabaseEntities.Database.CommandTimeout = 600;
            await HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommandAsync("execute dbo.pRebuildTrashGeneratingUnitTable");

            return new ModalDialogFormJsonResult();
        }
    }
}
