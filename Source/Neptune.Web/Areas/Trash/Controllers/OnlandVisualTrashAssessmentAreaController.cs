using LtInfo.Common.DesignByContract;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentArea;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Neptune.Web.Security.Shared;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentAreaController : NeptuneBaseController
    {
        [HttpGet]
        [AnonymousUnclassifiedFeature]
        [OnlandVisualTrashAssessmentAreaViewFeature]
        public ViewResult Detail(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var geoJsonFeatureCollection =
                new List<OnlandVisualTrashAssessmentArea> { onlandVisualTrashAssessmentArea }
                    .ToGeoJsonFeatureCollection();


            var observationsLayerGeoJson = onlandVisualTrashAssessmentArea.GetTransectBackingAssessment()?.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();


            var assessmentAreaLayerGeoJson = new LayerGeoJson("assessmentArea", geoJsonFeatureCollection,
                "#ffff00", .5m,
                LayerInitialVisibility.Show);

            var transectLineLayerGeoJson = onlandVisualTrashAssessmentArea.GetTransectLineLayerGeoJson();

            var mapInitJson = new OVTAAreaMapInitJson("ovtaAreaMap", assessmentAreaLayerGeoJson, transectLineLayerGeoJson, observationsLayerGeoJson);
            var newUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x => x.NewAssessment(onlandVisualTrashAssessmentArea));
            var editDetailsUrl =
                SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x => x.EditBasics(onlandVisualTrashAssessmentArea));
            var confirmEditLocationUrl =
                SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x => x.ConfirmEditLocation(onlandVisualTrashAssessmentArea));
            var viewData = new Views.OnlandVisualTrashAssessmentArea.DetailViewData(CurrentPerson,
                onlandVisualTrashAssessmentArea, mapInitJson, newUrl, editDetailsUrl, confirmEditLocationUrl);

            return RazorView<Views.OnlandVisualTrashAssessmentArea.Detail, Views.OnlandVisualTrashAssessmentArea.DetailViewData>(viewData);
        }

        private PartialViewResult ViewEditBasics(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, EditBasicsViewModel viewModel)
        {
            var viewData = new EditBasicsViewData(CurrentPerson, onlandVisualTrashAssessmentArea);
            return RazorPartialView<EditBasics, EditBasicsViewData, EditBasicsViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public PartialViewResult EditBasics(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var viewModel = new EditBasicsViewModel(onlandVisualTrashAssessmentArea);
            return ViewEditBasics(onlandVisualTrashAssessmentArea, viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditBasics(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, EditBasicsViewModel viewModel)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditBasics(onlandVisualTrashAssessmentArea, viewModel);
            }

            viewModel.UpdateModel(onlandVisualTrashAssessmentArea);
            SetMessageForDisplay("Successfully updated OVTA Area details");

            return new ModalDialogFormJsonResult(onlandVisualTrashAssessmentArea.GetDetailUrl());
        }


        [HttpGet]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        public ActionResult ConfirmEditLocation(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;

            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID);
            return ViewConfirmEditLocationOnlandVisualTrashAssessmentArea(onlandVisualTrashAssessmentArea, viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        public ActionResult ConfirmEditLocation(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
           return new ModalDialogFormJsonResult(SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(c => c.EditLocation(onlandVisualTrashAssessmentAreaPrimaryKey)));
        }

        private PartialViewResult ViewConfirmEditLocationOnlandVisualTrashAssessmentArea(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, ConfirmDialogFormViewModel viewModel)
        {
            var n = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Count;

            var confirmMessage = (n < 2) ? "Any changes you make to the Assessment Area will apply to all future assessments" : $"Any changes you make to the Assessment Area will apply to the {n} Assessments associated with this area. Proceed?";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);

            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        private ViewResult ViewEditLocation(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, EditLocationViewModel viewModel)
        {
            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessmentArea.GetAssessmentAreaLayerGeoJson();
            var transectLineLayerGeoJson = onlandVisualTrashAssessmentArea.GetTransectLineLayerGeoJson();
            var refineAssessmentAreaMapInitJson = new RefineAssessmentAreaMapInitJson("refineAssessmentAreaMap", null, assessmentAreaLayerGeoJson, transectLineLayerGeoJson);

            var viewData = new EditLocationViewData(CurrentPerson, onlandVisualTrashAssessmentArea, refineAssessmentAreaMapInitJson);
            return RazorView<EditLocation, EditLocationViewData, EditLocationViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ViewResult EditLocation(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var viewModel = new EditLocationViewModel(onlandVisualTrashAssessmentArea);
            return ViewEditLocation(onlandVisualTrashAssessmentArea, viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditLocation(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, EditLocationViewModel viewModel)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditLocation(onlandVisualTrashAssessmentArea, viewModel);
            }

            viewModel.UpdateModel(onlandVisualTrashAssessmentArea);

            SetMessageForDisplay("Successfully updated OVTA Area location");

            return Redirect(
                SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x =>
                    x.Detail(onlandVisualTrashAssessmentAreaPrimaryKey)));
        }

        [HttpGet]
        [OnlandVisualTrashAssessmentAreaViewFeature]
        public JsonResult ParcelsViaTransect(
            OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;


            return Json(new {ParcelIDs = HttpRequestStorage.DatabaseEntities.Parcels.Where(x=>x.ParcelGeometry.Intersects(onlandVisualTrashAssessmentArea.TransectLine)).Select(x=>x.ParcelID).ToList()}, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [OnlandVisualTrashAssessmentAreaDeleteFeature]
        public PartialViewResult Delete(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID);
            return ViewDeleteOnlandVisualTrashAssessmentArea(onlandVisualTrashAssessmentArea, viewModel);
        }

        [HttpPost]
        [OnlandVisualTrashAssessmentAreaDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;

            var assessmentCount = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Count;
            Check.Assert(assessmentCount == 0, $"The Assessment Area {onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName} cannot be deleted because it has {assessmentCount} Assessment(s) which must be deleted first.");

            if (!ModelState.IsValid)
            {
                return ViewDeleteOnlandVisualTrashAssessmentArea(onlandVisualTrashAssessmentArea, viewModel);
            }

            onlandVisualTrashAssessmentArea.Delete(HttpRequestStorage.DatabaseEntities);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay(
                $"Successfully deleted the assessment area, {onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName}.");
            return new ModalDialogFormJsonResult(SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(c => c.Index()));
        }

        private PartialViewResult ViewDeleteOnlandVisualTrashAssessmentArea(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessmentAreaCount =
                onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Count;
            string confirmMessage;
            bool canProceed;

            if (onlandVisualTrashAssessmentAreaCount != 0)
            {
                confirmMessage = $"The Assessment Area {onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName} has {onlandVisualTrashAssessmentAreaCount} Assessment(s). You must first delete all associated Assessments before you can delete the Assessment Area.";
                canProceed = false;
            }
            else
            {
                confirmMessage = $"Are you sure you want to delete the assessment area {onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName}?";
                canProceed = true;
            }
            var viewData = new ConfirmDialogFormViewData(confirmMessage, canProceed);


            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [NeptuneViewAndRequiresJurisdictionsFeature]
        [HttpGet]
        public ContentResult FindByName()
        {
            return new ContentResult();
        }

        [NeptuneViewAndRequiresJurisdictionsFeature]
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
        [OnlandVisualTrashAssessmentAreaEditFeature]
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
        [OnlandVisualTrashAssessmentAreaEditFeature]
        public ActionResult NewAssessment(
            OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewNewAssessment(onlandVisualTrashAssessmentArea, viewModel);
            }

            var onlandVisualTrashAssessment = new OnlandVisualTrashAssessment(CurrentPerson, DateTime.Now, onlandVisualTrashAssessmentArea.StormwaterJurisdiction,
                OnlandVisualTrashAssessmentStatus.InProgress, false, false)
            {
                OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID,
                AssessingNewArea = false
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
