using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.Common.DesignByContract;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.DelineationGeometry;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment.MapInitJson;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessmentArea;
using Neptune.WebMvc.Views.Shared;
using Detail = Neptune.WebMvc.Views.OnlandVisualTrashAssessmentArea.Detail;
using DetailViewData = Neptune.WebMvc.Views.OnlandVisualTrashAssessmentArea.DetailViewData;

namespace Neptune.WebMvc.Controllers
{
    //[Area("Trash")]
    //[Route("[area]/[controller]/[action]", Name = "[area]_[controller]_[action]")]
    public class OnlandVisualTrashAssessmentAreaController : NeptuneBaseController<OnlandVisualTrashAssessmentAreaController>
    {
        private readonly AzureBlobStorageService _azureBlobStorageService;
        private readonly GDALAPIService _gdalApiService;

        public OnlandVisualTrashAssessmentAreaController(NeptuneDbContext dbContext, ILogger<OnlandVisualTrashAssessmentAreaController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, AzureBlobStorageService azureBlobStorageService, GDALAPIService gdalApiService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _azureBlobStorageService = azureBlobStorageService;
            _gdalApiService = gdalApiService;
        }

        [HttpGet("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public ViewResult Detail([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = OnlandVisualTrashAssessmentAreas.GetByID(_dbContext, onlandVisualTrashAssessmentAreaPrimaryKey);
            var geoJsonFeatureCollection = new List<OnlandVisualTrashAssessmentArea> { onlandVisualTrashAssessmentArea }.ToGeoJsonFeatureCollection();

            var observationsLayerGeoJson = OnlandVisualTrashAssessments.GetTransectBackingAssessment(_dbContext, onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID)?.OnlandVisualTrashAssessmentObservations.MakeObservationsLayerGeoJson();

            var assessmentAreaLayerGeoJson = new LayerGeoJson("assessmentArea", geoJsonFeatureCollection, "#ffff00", 0.5f, LayerInitialVisibility.Show);

            var transectLineLayerGeoJson = onlandVisualTrashAssessmentArea.GetTransectLineLayerGeoJson();

            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var boundingBoxDto = new BoundingBoxDto(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry4326);
            var mapInitJson = new OVTAAreaMapInitJson("ovtaAreaMap", assessmentAreaLayerGeoJson, transectLineLayerGeoJson, observationsLayerGeoJson, layerGeoJsons, boundingBoxDto);
            var newUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(_linkGenerator, x => x.NewAssessment(onlandVisualTrashAssessmentArea));
            var editDetailsUrl =
                SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(_linkGenerator, x => x.EditBasics(onlandVisualTrashAssessmentArea));
            var confirmEditLocationUrl =
                SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(_linkGenerator, x => x.ConfirmEditLocation(onlandVisualTrashAssessmentArea));
            var onlandVisualTrashAssessments = OnlandVisualTrashAssessments
                .ListByOnlandVisualTrashAssessmentAreaID(_dbContext,
                    onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID).Where(x =>
                    x.OnlandVisualTrashAssessmentStatusID == (int)OnlandVisualTrashAssessmentStatusEnum.Complete)
                .ToList();
            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration,
                onlandVisualTrashAssessmentArea, mapInitJson, newUrl, editDetailsUrl, confirmEditLocationUrl, _webConfiguration.MapServiceUrl, onlandVisualTrashAssessments);

            return RazorView<Detail, DetailViewData>(viewData);
        }

        private PartialViewResult ViewEditBasics(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, EditBasicsViewModel viewModel)
        {
            var viewData = new EditBasicsViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, onlandVisualTrashAssessmentArea);
            return RazorPartialView<EditBasics, EditBasicsViewData, EditBasicsViewModel>(viewData, viewModel);
        }

        [HttpGet("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public PartialViewResult EditBasics([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var viewModel = new EditBasicsViewModel(onlandVisualTrashAssessmentArea);
            return ViewEditBasics(onlandVisualTrashAssessmentArea, viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public async Task<ActionResult> EditBasics([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, EditBasicsViewModel viewModel)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditBasics(onlandVisualTrashAssessmentArea, viewModel);
            }

            viewModel.UpdateModel(onlandVisualTrashAssessmentArea);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Successfully updated OVTA Area details");

            var redirectUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(_linkGenerator,
                    x => x.Detail(onlandVisualTrashAssessmentArea));
            return new ModalDialogFormJsonResult(redirectUrl);
        }

        [HttpGet("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public ActionResult ConfirmEditLocation([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;

            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID);
            return ViewConfirmEditLocationOnlandVisualTrashAssessmentArea(onlandVisualTrashAssessmentArea, viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public ActionResult ConfirmEditLocation([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
           return new ModalDialogFormJsonResult(SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(_linkGenerator, x => x.EditLocation(onlandVisualTrashAssessmentAreaPrimaryKey)));
        }

        private PartialViewResult ViewConfirmEditLocationOnlandVisualTrashAssessmentArea(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, ConfirmDialogFormViewModel viewModel)
        {
            var n = OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(_dbContext, onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID).Count;

            var confirmMessage = (n < 2) ? "Any changes you make to the Assessment Area will apply to all future assessments" : $"Any changes you make to the Assessment Area will apply to the {n} Assessments associated with this area. Proceed?";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);

            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        private ViewResult ViewEditLocation(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, EditLocationViewModel viewModel)
        {
            var assessmentAreaLayerGeoJson = onlandVisualTrashAssessmentArea.GetAssessmentAreaLayerGeoJson();
            var transectLineLayerGeoJson = onlandVisualTrashAssessmentArea.GetTransectLineLayerGeoJson();
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList();
            var boundingBoxDto = new BoundingBoxDto(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry4326);
            var refineAssessmentAreaMapInitJson = new RefineAssessmentAreaMapInitJson("refineAssessmentAreaMap", null, assessmentAreaLayerGeoJson, transectLineLayerGeoJson, layerGeoJsons, boundingBoxDto);

            var viewData = new EditLocationViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.EditOVTAArea), onlandVisualTrashAssessmentArea, refineAssessmentAreaMapInitJson);
            return RazorView<EditLocation, EditLocationViewData, EditLocationViewModel>(viewData, viewModel);
        }

        [HttpGet("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public ViewResult EditLocation([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var viewModel = new EditLocationViewModel();
            return ViewEditLocation(onlandVisualTrashAssessmentArea, viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public async Task<ActionResult> EditLocation([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, EditLocationViewModel viewModel)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditLocation(onlandVisualTrashAssessmentArea, viewModel);
            }

            await viewModel.UpdateModel(_dbContext, onlandVisualTrashAssessmentArea);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Successfully updated OVTA Area location");

            return Redirect(
                SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(_linkGenerator, x =>
                    x.Detail(onlandVisualTrashAssessmentAreaPrimaryKey)));
        }

        [HttpGet("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public JsonResult ParcelsViaTransect([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;


            return Json(
                new
                {
                    ParcelIDs = ParcelGeometries
                        .GetIntersected(_dbContext, onlandVisualTrashAssessmentArea.TransectLine)
                        .Select(x => x.ParcelID).ToList()
                });
        }


        [HttpGet("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public PartialViewResult Delete([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID);
            return ViewDeleteOnlandVisualTrashAssessmentArea(onlandVisualTrashAssessmentArea, viewModel);
        }

        [HttpPost("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public async Task<ActionResult> Delete([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;

            var assessmentCount = OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(_dbContext, onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID).Count;
            Check.Assert(assessmentCount == 0, $"The Assessment Area {onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName} cannot be deleted because it has {assessmentCount} Assessment(s) which must be deleted first.");

            if (!ModelState.IsValid)
            {
                return ViewDeleteOnlandVisualTrashAssessmentArea(onlandVisualTrashAssessmentArea, viewModel);
            }

            await onlandVisualTrashAssessmentArea.DeleteFull(_dbContext);

            SetMessageForDisplay(
                $"Successfully deleted the assessment area, {onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName}.");
            return new ModalDialogFormJsonResult(SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(_linkGenerator, x => x.Index()));
        }

        private PartialViewResult ViewDeleteOnlandVisualTrashAssessmentArea(OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessmentCount = OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(_dbContext, onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID).Count;
            string confirmMessage;
            bool canProceed;

            if (onlandVisualTrashAssessmentCount != 0)
            {
                confirmMessage = $"The Assessment Area {onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName} has {onlandVisualTrashAssessmentCount} Assessment(s). You must first delete all associated Assessments before you can delete the Assessment Area.";
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
                _dbContext.OnlandVisualTrashAssessmentAreas.Where(
                    x => x.StormwaterJurisdictionID == jurisdictionID &&
                         x.OnlandVisualTrashAssessmentAreaName.Contains(searchString)).ToList();

            var listItems = allOnlandVisualTrashAssessmentAreasMatchingSearchString
                .OrderBy(x => x.OnlandVisualTrashAssessmentAreaName).Take(20).Select(x =>
                {
                    var listItem = new SelectListItem(x.OnlandVisualTrashAssessmentAreaName,
                        x.OnlandVisualTrashAssessmentAreaID.ToString(CultureInfo.InvariantCulture));
                    return listItem;
                }).ToList();

            return Json(listItems);
        }

        [HttpGet("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [NeptuneViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public PartialViewResult TrashMapAssetPanel([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;
            var mostRecentAssessmentCompletedDate = OnlandVisualTrashAssessments.ListByOnlandVisualTrashAssessmentAreaID(_dbContext, onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID).Where(x => x.OnlandVisualTrashAssessmentStatusID == (int) OnlandVisualTrashAssessmentStatusEnum.Complete).Max(x => x.CompletedDate);
            var viewData = new TrashMapAssetPanelViewData(_linkGenerator, CurrentPerson, onlandVisualTrashAssessmentArea, mostRecentAssessmentCompletedDate);
            return RazorPartialView<TrashMapAssetPanel, TrashMapAssetPanelViewData>(viewData);
        }

        [HttpGet("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public PartialViewResult NewAssessment([FromRoute] OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
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

        [HttpPost("{onlandVisualTrashAssessmentAreaPrimaryKey}")]
        [OnlandVisualTrashAssessmentAreaEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("onlandVisualTrashAssessmentAreaPrimaryKey")]
        public async Task<ActionResult> NewAssessment([FromRoute]
            OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var onlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentAreaPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewNewAssessment(onlandVisualTrashAssessmentArea, viewModel);
            }

            var onlandVisualTrashAssessment = new OnlandVisualTrashAssessment{
                CreatedByPersonID = CurrentPerson.PersonID, 
                CreatedDate = DateTime.UtcNow,
                StormwaterJurisdictionID = onlandVisualTrashAssessmentArea.StormwaterJurisdictionID,
                OnlandVisualTrashAssessmentStatusID = (int) OnlandVisualTrashAssessmentStatusEnum.InProgress, 
                IsProgressAssessment = false, IsTransectBackingAssessment = false,
                OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID,
                AssessingNewArea = false
            };

            await _dbContext.OnlandVisualTrashAssessments.AddAsync(onlandVisualTrashAssessment);
            await _dbContext.SaveChangesAsync();

            return new ModalDialogFormJsonResult(
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(_linkGenerator, x => x.RecordObservations(onlandVisualTrashAssessment)));
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ActionResult BulkUploadOVTAAreas()
        {
            var bulkUploadTrashScreenVisitViewModel = new BulkUploadOVTAAreasViewModel() { AreaName = "OVTAAreaName" };

            return ViewBulkUploadOTVAAreas(bulkUploadTrashScreenVisitViewModel);
        }

        private ViewResult ViewBulkUploadOTVAAreas(
            BulkUploadOVTAAreasViewModel bulkUploadTrashScreenVisitViewModel)
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.UploadOVTAs);
            var newGisUploadUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(_linkGenerator, x => x.BulkUploadOVTAAreas());
            var approveGisUploadUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(_linkGenerator, x => x.ApproveOVTAAreaGisUpload());
            var bulkUploadTrashScreenVisitViewData = new BulkUploadOVTAAreasViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson,
                newGisUploadUrl, approveGisUploadUrl, StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson), null);

            return RazorView<BulkUploadOVTAAreas, BulkUploadOVTAAreasViewData,
                BulkUploadOVTAAreasViewModel>(bulkUploadTrashScreenVisitViewData,
                bulkUploadTrashScreenVisitViewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        public async Task<IActionResult> BulkUploadOVTAAreas(BulkUploadOVTAAreasViewModel viewModel)
        {
            var file = viewModel.FileResourceData;
            var blobName = Guid.NewGuid().ToString();
            await _azureBlobStorageService.UploadToBlobStorage(await FileStreamHelpers.StreamToBytes(file), blobName, ".gdb");
            var featureClassNames = await _gdalApiService.OgrInfoGdbToFeatureClassInfo(file);

            if (featureClassNames.Count == 0)
            {
                ModelState.AddModelError("FileResourceData",
                    "The file geodatabase contained no feature class. Please upload a file geodatabase containing exactly one feature class.");
            }
            else if (featureClassNames.Count > 1)
            {
                ModelState.AddModelError("FileResourceData",
                    "The file geodatabase contained more than one feature class. Please upload a file geodatabase containing exactly one feature class.");
            }

            var featureClassName = featureClassNames.Single().LayerName;
            //if (!OgrInfoCommandLineRunner.ConfirmAttributeExistsOnFeatureClass(
            //        new FileInfo(NeptuneWebConfiguration.OgrInfoExecutable),
            //        gdbFile,
            //        Ogr2OgrCommandLineRunner.DefaultTimeOut, featureClassName, TreatmentBMPNameField))
            //{
            //    errors.Add(new ValidationResult($"The feature class in the file geodatabase does not have an attribute named {TreatmentBMPNameField}. Please double-check the attribute name you entered and try again."));
            //    return errors;
            //}

            if (!ModelState.IsValid)
            {
                return ViewUpdateOVTAAreaGeometryErrors(viewModel);
            }

            try
            {
                var columns = new List<string>
                {
                    $"{viewModel.StormwaterJurisdictionID} as StormwaterJurisdictionID",
                    $"{viewModel.AreaName} as AreaName",
                    $"Description",
                    $"{CurrentPerson.PersonID} as UploadedByPersonID"
                };

                var apiRequest = new GdbToGeoJsonRequestDto()
                {
                    BlobContainer = AzureBlobStorageService.BlobContainerName,
                    CanonicalName = blobName,
                    GdbLayerOutputs = new()
                    {
                        new()
                        {
                            Columns = columns,
                            FeatureLayerName = featureClassName,
                            NumberOfSignificantDigits = 4,
                            Filter = "",
                            CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
                        }
                    }
                };
                var geoJson = await _gdalApiService.Ogr2OgrGdbToGeoJson(apiRequest);
                var ovtaAreaStagings = await GeoJsonSerializer.DeserializeFromFeatureCollectionWithCCWCheck<OnlandVisualTrashAssessmentAreaStaging>(geoJson,
                    GeoJsonSerializer.DefaultSerializerOptions, Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
                // todo: Run MakeValid "update dbo.DelineationStaging set Geometry = Geometry.MakeValid() where Geometry.STIsValid() = 0";

                var validOVTAAreaStagings = ovtaAreaStagings.Where(x => x.Geometry is { IsValid: true, Area: > 0 }).ToList();
                if (validOVTAAreaStagings.Any())
                {
                    await _dbContext.OnlandVisualTrashAssessmentAreaStagings.Where(x => x.UploadedByPersonID == CurrentPerson.PersonID).ExecuteDeleteAsync();
                    _dbContext.OnlandVisualTrashAssessmentAreaStagings.AddRange(validOVTAAreaStagings);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unrecognized field name",
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    ModelState.AddModelError("",
                        "The columns in the uploaded file did not match the OVTA area schema. The file is invalid and cannot be uploaded.");
                }
                else
                {
                    ModelState.AddModelError("",
                        $"There was a problem processing the Feature Class \"{featureClassName}\". The file may be corrupted or invalid.");
                }
                return ViewUpdateOVTAAreaGeometryErrors(viewModel);
            }


            return RedirectToAction(new SitkaRoute<OnlandVisualTrashAssessmentAreaController>(_linkGenerator, x => x.ApproveOVTAAreaGisUpload()));
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ActionResult ApproveOVTAAreaGisUpload()
        {
            var viewModel = new ApproveOVTAAreaGisUploadViewModel();
            return ViewApproveOVTAAreaGisUpload(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        public async Task<ActionResult> ApproveOVTAAreaGisUpload(ApproveOVTAAreaGisUploadViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewBulkUploadOTVAAreas(new BulkUploadOVTAAreasViewModel());
            }

            var onlandVisualTrashAssessmentAreaStagings = _dbContext.OnlandVisualTrashAssessmentAreaStagings.Where(x => x.UploadedByPersonID == CurrentPerson.PersonID).ToList();

            //// Will break if there are multiple batches of staged uploads, which is precisely what we want to happen. 
            var stormwaterJurisdictionID = onlandVisualTrashAssessmentAreaStagings.Select(x => x.StormwaterJurisdictionID).Distinct().Single();
            var stormwaterJurisdiction = StormwaterJurisdictions.GetByID(_dbContext, stormwaterJurisdictionID);
            var stormwaterJurisdictionName = stormwaterJurisdiction.GetOrganizationDisplayName();

            var ovtaAreaNames = _dbContext.OnlandVisualTrashAssessmentAreas.AsNoTracking()
                .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID)
                .Select(x => x.OnlandVisualTrashAssessmentAreaName).ToList();

            var ovtaAreaStaging = _dbContext.OnlandVisualTrashAssessmentAreaStagings.AsNoTracking().Where(x =>
                x.StormwaterJurisdictionID == stormwaterJurisdictionID &&
                x.UploadedByPersonID == CurrentPerson.PersonID).ToList();

            var duplicateOVTAAreaNames = ovtaAreaStaging.Select(x => x.AreaName).Intersect(ovtaAreaNames).ToList();

            foreach (var ovtaArea in ovtaAreaStaging)
            {
                if (duplicateOVTAAreaNames.Contains(ovtaArea.AreaName))
                {
                    var existingOVTA = _dbContext.OnlandVisualTrashAssessmentAreas.Single(x =>
                        x.OnlandVisualTrashAssessmentAreaName == ovtaArea.AreaName &&
                        x.StormwaterJurisdictionID == stormwaterJurisdictionID);

                    existingOVTA.OnlandVisualTrashAssessmentAreaGeometry = ovtaArea.Geometry;
                    existingOVTA.OnlandVisualTrashAssessmentAreaGeometry4326 = ovtaArea.Geometry.ProjectTo4326();
                }
                else
                {
                    var newOVTA = new OnlandVisualTrashAssessmentArea()
                    {
                        OnlandVisualTrashAssessmentAreaName = ovtaArea.AreaName,
                        AssessmentAreaDescription = ovtaArea.Description,
                        StormwaterJurisdictionID = ovtaArea.StormwaterJurisdictionID,
                        OnlandVisualTrashAssessmentAreaGeometry = ovtaArea.Geometry,
                        OnlandVisualTrashAssessmentAreaGeometry4326 = ovtaArea.Geometry.ProjectTo4326()
                    };

                    _dbContext.OnlandVisualTrashAssessmentAreas.AddRange(newOVTA);
                }
            }

            var successfulUploadCount = (int?)ovtaAreaStaging.Count;

            SetMessageForDisplay($"{successfulUploadCount} OVTA areas were successfully uploaded for Jurisdiction {stormwaterJurisdictionName}");

            await _dbContext.SaveChangesAsync();

            await _dbContext.OnlandVisualTrashAssessmentAreaStagings.Where(x => x.UploadedByPersonID == CurrentPerson.PersonID).ExecuteDeleteAsync();

            return RedirectToAction(new SitkaRoute<OnlandVisualTrashAssessmentController>(_linkGenerator, x => x.Index()));
        }

        private PartialViewResult ViewApproveOVTAAreaGisUpload(ApproveOVTAAreaGisUploadViewModel viewModel)
        {
            var onlandVisualTrashAssessmentAreaStagings = _dbContext.OnlandVisualTrashAssessmentAreaStagings.Include(x => x.StormwaterJurisdiction)
                .Where(x => x.UploadedByPersonID == CurrentPerson.PersonID).ToList();

            var ovtaAreaUploadGisReportFromStaging = OVTAAreaUploadGisReportJsonResult.GetOVTAAreaUploadGisReportFromStaging(_dbContext, CurrentPerson, onlandVisualTrashAssessmentAreaStagings);

            var viewData = new ApproveOVTAAreaGisUploadViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, ovtaAreaUploadGisReportFromStaging);
            return RazorPartialView<ApproveOVTAAreaGisUpload, ApproveOVTAAreaGisUploadViewData, ApproveOVTAAreaGisUploadViewModel>(viewData, viewModel);

        }

        private PartialViewResult ViewUpdateOVTAAreaGeometryErrors(BulkUploadOVTAAreasViewModel viewModel)
        {
            var viewData = new BulkUploadOVTAAreasViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson,
                null, null, StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson), null);
            return RazorPartialView<UpdateOVTAAreaGeometryErrors, BulkUploadOVTAAreasViewData, BulkUploadOVTAAreasViewModel>(viewData, viewModel);
        }

    }


    public class FindAssessmentAreaByNameViewModel
    {
        public string SearchTerm { get; set; }
        public int JurisdictionID { get; set; }
    }
}
