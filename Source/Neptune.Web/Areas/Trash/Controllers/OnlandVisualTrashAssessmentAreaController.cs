using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Trash.Views;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentArea;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using FeatureCollection = GeoJSON.Net.Feature.FeatureCollection;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentAreaController : NeptuneBaseController
    {
        [HttpGet]
        [OnlandVisualTrashAssessmentAreaViewFeature]
        public ViewResult Detail(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var geoJsonFeatureCollection =
                new List<OnlandVisualTrashAssessmentArea> { onlandVisualTrashAssessmentArea }
                    .ToGeoJsonFeatureCollection();

            var assessmentAreaLayerGeoJson = new LayerGeoJson("assessmentArea", geoJsonFeatureCollection,
                "#ffff00", .5m,
                LayerInitialVisibility.Show);

            var transectLineLayerGeoJson = onlandVisualTrashAssessmentArea.GetTransectLineLayerGeoJson();

            var mapInitJson = new OVTAAreaMapInitJson("ovtaAreaMap", assessmentAreaLayerGeoJson, transectLineLayerGeoJson);
            var viewData = new DetailViewData(CurrentPerson,
                onlandVisualTrashAssessmentArea, mapInitJson);

            return RazorView<Detail, DetailViewData>(viewData);
        }

        [NeptuneViewFeature]
        [HttpGet]
        public ContentResult FindByName()
        {
            return new ContentResult();
        }

        [NeptuneViewFeature]
        [HttpPost]
        public JsonResult FindByName(FindAssessmentAreaByNameViewModel viewModel)
        {
            var searchString = viewModel.SearchTerm.Trim();
            var jurisdictionID = viewModel.JurisdictionID;
            var allOnlandVisualTrashAssessmentAreasMatchingSearchString =
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.Where(
                    x => x.StormwaterJurisdictionID == jurisdictionID &&
                         x.OnlandVisualTrashAssessmentAreaName.Contains(searchString)).ToList();

            var listItems = allOnlandVisualTrashAssessmentAreasMatchingSearchString
                .OrderBy(x => x.OnlandVisualTrashAssessmentAreaName).Take(20).Select(x =>
                {
                    var listItem = new ListItem(x.OnlandVisualTrashAssessmentAreaName,
                        x.OnlandVisualTrashAssessmentAreaID.ToString(CultureInfo.InvariantCulture));
                    return listItem;
                }).ToList();

            return Json(listItems, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public PartialViewResult TrashMapAssetPanel(
            OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var viewData = new TrashMapAssetPanelViewData(CurrentPerson, onlandVisualTrashAssessmentArea);
            return RazorPartialView<TrashMapAssetPanel, TrashMapAssetPanelViewData>(viewData);
        }



        [HttpGet]
        [OnlandVisualTrashAssessmentAreaViewFeature]
        public PartialViewResult NewAssessment(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID);
            return ViewNewAssessment(onlandVisualTrashAssessmentArea, viewModel);
        }

        private PartialViewResult ViewNewAssessment(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"You are about to begin a new OVTA for Assessment Area: {onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName}. This will create a new Assessment record and allow you to start entering Assessment Observations on the next page.";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentAreaViewFeature]
        public ActionResult NewAssessment(
            OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewNewAssessment(onlandVisualTrashAssessmentArea, viewModel);
            }

            var onlandVisualTrashAssessment = new OnlandVisualTrashAssessment(CurrentPerson, DateTime.Now,
                OnlandVisualTrashAssessmentStatus.InProgress)
            {
                OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID,
                AssessingNewArea = false,
                StormwaterJurisdictionID = onlandVisualTrashAssessmentArea.StormwaterJurisdictionID
            };

            HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.Add(onlandVisualTrashAssessment);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult(
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.RecordObservations(onlandVisualTrashAssessment)));
        }
    }

    public class FindAssessmentAreaByNameViewModel
    {
        public string SearchTerm { get; set; }
        public int JurisdictionID { get; set; }
    }
}
