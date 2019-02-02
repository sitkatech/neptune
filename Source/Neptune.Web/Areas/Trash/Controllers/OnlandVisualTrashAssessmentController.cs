using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Newtonsoft.Json.Linq;
using Index = Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment.Index;
using IndexViewData = Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment.IndexViewData;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson, NeptunePage.GetNeptunePageByPageType(NeptunePageType.OVTAIndex));
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessment> OVTAGridJsonData()
        {
            var treatmentBMPs = GetOVTAsAndGridSpec(out var gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<OnlandVisualTrashAssessment>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<OnlandVisualTrashAssessment> GetOVTAsAndGridSpec(out OVTAIndexGridSpec gridSpec, Person currentPerson)
        {
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            gridSpec = new OVTAIndexGridSpec(currentPerson, showDelete, showEdit);
            return HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.ToList();
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Instructions(int? ovtaID) // route "overloaded" so we can handle revisiting and starting anew with the same route.
        {
            var viewModel = new InstructionsViewModel();

            var onlandVisualTrashAssessment = ovtaID.HasValue
                ? HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.GetOnlandVisualTrashAssessment(
                    ovtaID.Value)
                : null;

            return ViewInstructions(viewModel, onlandVisualTrashAssessment);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Instructions(int? ovtaID, InstructionsViewModel viewModel)
        {
            OnlandVisualTrashAssessment onlandVisualTrashAssessment;
            if (ovtaID.HasValue)
            {
                onlandVisualTrashAssessment =
                    HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.GetOnlandVisualTrashAssessment(
                        ovtaID.Value);
            }
            else
            {
                onlandVisualTrashAssessment = new OnlandVisualTrashAssessment(CurrentPerson, DateTime.Now);
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.Add(onlandVisualTrashAssessment);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }

            return RedirectToAppropriateStep(viewModel, Models.OVTASection.Instructions, onlandVisualTrashAssessment);
        }

        private ViewResult ViewInstructions(InstructionsViewModel viewModel, OnlandVisualTrashAssessment ovta)
        {
            var viewData = new InstructionsViewData(CurrentPerson,
                NeptunePage.GetNeptunePageByPageType(NeptunePageType.OVTAInstructions), ovta);
            return RazorView<Instructions, InstructionsViewData, InstructionsViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult InitiateOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new InitiateOVTAViewModel(onlandVisualTrashAssessment);

            return ViewInitiateOVTA(onlandVisualTrashAssessment, viewModel);
        }

        private ViewResult ViewInitiateOVTA(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            InitiateOVTAViewModel viewModel)
        {
            var stormwaterJurisdictionsPersonCanEdit = CurrentPerson.GetStormwaterJurisdictionsPersonCanEdit().ToList();

            // do not offer a drop-down menu if the user can only edit one jurisdiction
            var defaultJurisdiction = stormwaterJurisdictionsPersonCanEdit.Count == 1 ? stormwaterJurisdictionsPersonCanEdit.Single() : null;

            var jurisdictionsSelectList = stormwaterJurisdictionsPersonCanEdit
                .ToSelectListWithDisabledEmptyFirstRow(j => j.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture),
                    j => j.GetOrganizationDisplayName(), "Choose a Jurisdiction");

            var onlandVisualTrashAssessmentAreas = HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.ToList()
                .Where(x => stormwaterJurisdictionsPersonCanEdit.Contains(x.StormwaterJurisdiction)).ToList();

            var mapInitJson = new SelectOVTAAreaMapInitJson("selectOVTAAreaMap", SelectOVTAAreaMapInitJson.MakeAssessmentAreasLayerGeoJson(onlandVisualTrashAssessmentAreas));

            var viewData = new InitiateOVTAViewData(CurrentPerson,
                onlandVisualTrashAssessment, jurisdictionsSelectList, mapInitJson, onlandVisualTrashAssessmentAreas, defaultJurisdiction);
            return RazorView<InitiateOVTA, InitiateOVTAViewData, InitiateOVTAViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult InitiateOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, InitiateOVTAViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewInitiateOVTA(onlandVisualTrashAssessment, viewModel);
            }

            viewModel.UpdateModel(onlandVisualTrashAssessment);

            return RedirectToAppropriateStep(viewModel, Models.OVTASection.InitiateOVTA, onlandVisualTrashAssessment);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult RecordObservations(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new RecordObservationsViewModel(onlandVisualTrashAssessment);
            return ViewRecordObservations(onlandVisualTrashAssessment, viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RecordObservations(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, RecordObservationsViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewRecordObservations(onlandVisualTrashAssessment, viewModel);
            }

            var allOnlandVisualTrashAssessmentObservations = HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentObservations;
            viewModel.UpdateModel(onlandVisualTrashAssessment, allOnlandVisualTrashAssessmentObservations.Local);

            return RedirectToAppropriateStep(viewModel, Models.OVTASection.RecordObservations, onlandVisualTrashAssessmentPrimaryKey.EntityObject);
        }

        private ViewResult ViewRecordObservations(OnlandVisualTrashAssessment onlandVisualTrashAssessment, RecordObservationsViewModel viewModel)
        {
            var observationsLayerGeoJson = OVTAObservationsMapInitJson.MakeObservationsLayerGeoJson(onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations);

            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea != null
                ? SelectOVTAAreaMapInitJson.MakeAssessmentAreasLayerGeoJson(
                    new List<OnlandVisualTrashAssessmentArea>
                    {
                            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea
                    })
                : null;

            var ovtaObservationsMapInitJson = new OVTAObservationsMapInitJson("observationsMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson);


            var viewData = new RecordObservationsViewData(CurrentPerson,
                onlandVisualTrashAssessment, ovtaObservationsMapInitJson);
            return RazorView<RecordObservations, RecordObservationsViewData, RecordObservationsViewModel>(viewData,
                viewModel);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult FinalizeOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            var viewModel = new FinalizeOVTAViewModel(onlandVisualTrashAssessment);

            return ViewFinalizeOVTA(onlandVisualTrashAssessment, viewModel);
        }

        private ViewResult ViewFinalizeOVTA(OnlandVisualTrashAssessment onlandVisualTrashAssessment,
            FinalizeOVTAViewModel viewModel)
        {
            OVTASummaryMapInitJson ovtaSummaryMapInitJson;
            var observationsLayerGeoJson =
                OVTAObservationsMapInitJson.MakeObservationsLayerGeoJson(onlandVisualTrashAssessment
                    .OnlandVisualTrashAssessmentObservations);
            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea == null)
            {
                var parcels = onlandVisualTrashAssessment.GetParcelsViaTransect();
                var assessmentAreaLayerGeoJson = new LayerGeoJson("parcels", parcels.ToGeoJsonFeatureCollection(),
                    "#ffff00", .5m,
                    LayerInitialVisibility.Show);
                ovtaSummaryMapInitJson = new OVTASummaryMapInitJson("summaryMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson);
            }
            else
            {
                var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea;
                var assessmentAreaLayerGeoJson = new LayerGeoJson("parcels", new List<OnlandVisualTrashAssessmentArea> { onlandVisualTrashAssessmentArea }.ToGeoJsonFeatureCollection(),
                    "#ffff00", .5m,
                    LayerInitialVisibility.Show);
                ovtaSummaryMapInitJson = new OVTASummaryMapInitJson("summaryMap", observationsLayerGeoJson, assessmentAreaLayerGeoJson);
            }

            var viewData = new FinalizeOVTAViewData(CurrentPerson,
                onlandVisualTrashAssessment, ovtaSummaryMapInitJson);
            return RazorView<FinalizeOVTA, FinalizeOVTAViewData, FinalizeOVTAViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneViewFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult FinalizeOVTA(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, FinalizeOVTAViewModel viewModel)
        {
            var onlandVisualTrashAssessment = onlandVisualTrashAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewFinalizeOVTA(onlandVisualTrashAssessment, viewModel);
            }

            if (onlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea == null)
            {
                var assessmentAreaGeometry = onlandVisualTrashAssessment.GetAreaViaTransect();
                var onlandVisualTrashAssessmentArea = new OnlandVisualTrashAssessmentArea(viewModel.AssessmentAreaName, onlandVisualTrashAssessment.StormwaterJurisdiction, assessmentAreaGeometry);

                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.Add(
                    onlandVisualTrashAssessmentArea);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                onlandVisualTrashAssessment.OnlandVisualTrashAssessmentAreaID =
                    onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID;
            }
            else
            {
                viewModel.UpdateModel(onlandVisualTrashAssessment);
            }

            return Redirect(SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index()));
        }

        private ActionResult RedirectToAppropriateStep(OnlandVisualTrashAssessmentViewModel viewModel, Models.OVTASection ovtaSection, OnlandVisualTrashAssessment ovta)
        {
            return Redirect(viewModel.AutoAdvance
                ? ovtaSection.GetNextSection().GetSectionUrl(ovta)
                : ovtaSection.GetSectionUrl(ovta));
        }

        // photo handling
        // todo: move this to a separate controller

        [HttpGet]
        [NeptuneViewFeature]
        public ContentResult StageObservationPhoto(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey)
        {
            return Content("");
        }

        [HttpPost]
        [NeptuneViewFeature]
        public ActionResult StageObservationPhoto(OnlandVisualTrashAssessmentPrimaryKey onlandVisualTrashAssessmentPrimaryKey, ObservationPhotoStagingSimple opss)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFile(opss.Photo, CurrentPerson);

            var staging = new OnlandVisualTrashAssessmentObservationPhotoStaging(fileResource,
                onlandVisualTrashAssessmentPrimaryKey.EntityObject);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return Json(new
            {
                PhotoStagingID = staging.OnlandVisualTrashAssessmentObservationPhotoStagingID,
                PhotoStagingUrl = staging.FileResource.GetFileResourceUrl()
            });
        }



        [HttpGet]
        [NeptuneViewFeature]
        public ActionResult DeleteObservationPhoto()
        {
            return Content("");
        }

        [HttpPost]
        [NeptuneViewFeature]
        public ActionResult DeleteObservationPhoto(DeleteObservationPhotoSimple dopss)
        {
            if (dopss.IsStagedPhoto)
            {
                var onlandVisualTrashAssessmentObservationPhotoStaging =
                    ((OnlandVisualTrashAssessmentObservationPhotoStagingPrimaryKey) dopss.ID).EntityObject;
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentObservationPhotoStagings
                    .DeleteOnlandVisualTrashAssessmentObservationPhotoStaging(
                        onlandVisualTrashAssessmentObservationPhotoStaging);
            }
            else
            {
                var onlandVisualTrashAssessmentObservationPhoto =
                    ((OnlandVisualTrashAssessmentObservationPhotoPrimaryKey)dopss.ID).EntityObject;
                HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentObservationPhotos
                    .DeleteOnlandVisualTrashAssessmentObservationPhoto(
                        onlandVisualTrashAssessmentObservationPhoto);
            }
            return Content("");
        }
    }

    public class DeleteObservationPhotoSimple
    {
        public int ID { get; set; }

        public bool IsStagedPhoto { get; set; }
    }

    public class ObservationPhotoStagingSimple
    {
        public HttpPostedFileBase Photo { get; set; }
    }
}
