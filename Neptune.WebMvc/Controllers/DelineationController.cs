/*-----------------------------------------------------------------------
<copyright file="DelineationController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Delineation;
using Neptune.WebMvc.Views.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.MvcResults;
using Microsoft.Extensions.Options;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels;
using Neptune.EFModels.Nereid;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Services.Filters;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.WebMvc.Controllers
{
    public class DelineationController : NeptuneBaseController<DelineationController>
    {
        public DelineationController(NeptuneDbContext dbContext, ILogger<DelineationController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult DelineationMap(int? treatmentBMPID)
        {
            var treatmentBMP = treatmentBMPID.HasValue
                ? TreatmentBMPs.GetByID(_dbContext, treatmentBMPID.Value)
                : null;

            if (treatmentBMP != null)
            {
                var permissionCheckResult = new TreatmentBMPEditFeature().HasPermission(CurrentPerson, treatmentBMP);
                Check.Assert(permissionCheckResult.HasPermission, permissionCheckResult.PermissionDeniedMessage);
            }

            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.DelineationMap);
            var stormwaterJurisdictionIDs = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson).ToList();
            BoundingBoxDto boundingBoxDto;
            if (stormwaterJurisdictionIDs.Any())
            {
                var geometries = StormwaterJurisdictionGeometries
                    .ListByStormwaterJurisdictionIDList(_dbContext, stormwaterJurisdictionIDs)
                    .Select(x => x.Geometry4326);
                boundingBoxDto = new BoundingBoxDto(geometries);
            }
            else
            {
                boundingBoxDto = new BoundingBoxDto();
            }

            var treatmentBMPs = TreatmentBMPs.GetNonPlanningModuleBMPs(_dbContext).Include(x => x.Delineation).Where(x => stormwaterJurisdictionIDs.Contains(x.StormwaterJurisdictionID)).ToList();
            var delineationMapInitJson = new DelineationMapInitJson("delineationMap", treatmentBMPs, boundingBoxDto, MapInitJsonHelpers.GetJurisdictionMapLayers(_dbContext).ToList(), _linkGenerator);
            var bulkUploadTreatmentBMPDelineationsUrl = ""; // todo: SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(_linkGenerator, x => x.UpdateDelineationGeometry());
            var stormwaterJurisdictionCqlFilter = CurrentPerson.GetStormwaterJurisdictionCqlFilter(_dbContext);
            var viewData = new DelineationMapViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, delineationMapInitJson, treatmentBMP, bulkUploadTreatmentBMPDelineationsUrl, stormwaterJurisdictionCqlFilter, _webConfiguration.ParcelMapServiceUrl, _webConfiguration.AutoDelineateServiceUrl);
            return RazorView<DelineationMap, DelineationMapViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult DelineationReconciliationReport()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.DelineationReconciliationReport);
            var regionalSubbasinsLastUpdated = _dbContext.RegionalSubbasins.Max(x => x.LastUpdate);
            var viewData = new DelineationReconciliationReportViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, regionalSubbasinsLastUpdated);
            return RazorView<DelineationReconciliationReport, DelineationReconciliationReportViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<Delineation> DelineationsMisalignedWithRegionalSubbasinsGridJsonData()
        {
            var gridSpec = new MisalignedDelineationGridSpec(_linkGenerator);
            var delineations = Delineations.ListHavingDiscrepancies(_dbContext, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Delineation>(delineations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<Delineation> DelineationsOverlappingEachOtherGridJsonData()
        {
            var gridSpec = new DelineationOverlapsDelineationGridSpec(_linkGenerator);
            var delineations = Delineations.ListHavingOverlaps(_dbContext, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Delineation>(delineations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public JsonResult ForTreatmentBMP([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var delineation = Delineations.GetByTreatmentBMPID(_dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue);
            if (delineation == null)
            {
                // should be 400 tbh
                return Json(new {noDelineation = true});
            }

            var attributesTable = new AttributesTable
            {
                { "Area", delineation.GetDelineationAreaString() },
                { "DelineationType", delineation.DelineationType.DelineationTypeDisplayName ?? "No delineation provided" },
                { "DelineationStatus", delineation.IsVerified ? "Verified" : "Provisional" }
            };
            var feature = new Feature(delineation.DelineationGeometry4326, attributesTable);

            return Json(feature);
        }

        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> ForTreatmentBMP([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            ForTreatmentBMPViewModel viewModel)
        {
            if (!Enum.TryParse(viewModel.DelineationType, out DelineationTypeEnum delineationTypeEnum))
            {
                // todo: really should return a 400 bad request
                return Json(new {error = "Invalid Delineation Type"});
            }

            Geometry geom4326;
            if (viewModel.WellKnownText.Count == 1)
            {
                geom4326 = viewModel.WellKnownText[0] == GeometryHelper.POLYGON_EMPTY
                    ? null
                    : GeometryHelper.FromWKT(viewModel.WellKnownText[0], Proj4NetHelper.WEB_MERCATOR).Buffer(0);
            }
            else
            {
                geom4326 = viewModel.WellKnownText
                    .Select(x =>
                        GeometryHelper.FromWKT(x, Proj4NetHelper.WEB_MERCATOR).Buffer(0)).ToList()
                    .UnionListGeometries();
            }

            Geometry geom2771 = null;

            // like all POSTs from the browser, transform to State Plane 
            if (geom4326 != null)
            {
                geom2771 = geom4326.ProjectTo2771();
            }

            var treatmentBMP =  treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPDelineation = Delineations.GetByTreatmentBMPIDWithChangeTracking(_dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue);

            // for queueing the LGU job
            var newShape = geom2771;
            var oldShape = treatmentBMPDelineation?.DelineationGeometry;

            var delineationType = DelineationType.ToType(delineationTypeEnum);

            if (treatmentBMPDelineation != null)
            {
                
                if (geom4326 != null)
                {
                    treatmentBMPDelineation.DelineationGeometry = geom2771;
                    treatmentBMPDelineation.DelineationGeometry4326 = geom4326;
                    treatmentBMPDelineation.DelineationTypeID =
                        delineationType.DelineationTypeID;
                    treatmentBMPDelineation.IsVerified = false;
                    treatmentBMPDelineation.DateLastModified = DateTime.Now;
                }
                else
                {
                    await treatmentBMPDelineation.DeleteDelineation(_dbContext);
                }
            }
            else
            {
                if (geom4326 == null)
                {
                    return Json(new {success = true});
                }

                var delineation = new Delineation()
                {
                    TreatmentBMPID = treatmentBMPPrimaryKey.PrimaryKeyValue,
                    DelineationGeometry = geom2771,
                    DelineationGeometry4326 = geom4326,
                    DelineationTypeID = delineationType.DelineationTypeID,
                    DateLastModified = DateTime.Now,
                    IsVerified = false,
                    HasDiscrepancies = false
                };
                await _dbContext.Delineations.AddAsync(delineation);
            }

            await _dbContext.SaveChangesAsync();

            var treatmentBMPType = TreatmentBMPTypes.GetByIDWithChangeTracking(_dbContext, treatmentBMP.TreatmentBMPTypeID);
            if (!(newShape == null & oldShape == null) && treatmentBMPType.TreatmentBMPModelingType != null)
            {
                await ModelingEngineUtilities.QueueLGURefreshForArea(oldShape, newShape, _dbContext);
            }

            return Json(new {success = true, delineationID = treatmentBMP.Delineation.DelineationID});
        }

        [HttpGet("{delineationPrimaryKey}")]
        [DelineationDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("delineationPrimaryKey")]
        public ContentResult ChangeDelineationStatus([FromRoute] DelineationPrimaryKey delineationPrimaryKey)
        {
            return new ContentResult();
        }

        [HttpPost("{delineationPrimaryKey}")]
        [DelineationDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("delineationPrimaryKey")]
        public async Task<IActionResult> ChangeDelineationStatus([FromRoute] DelineationPrimaryKey delineationPrimaryKey,
            ChangeDelineationStatusViewModel viewModel)
        {
            var delineation = delineationPrimaryKey.EntityObject;
            // todo: learn how to send a 400 or other error code with Json
            if (!ModelState.IsValid)
            {
                return Json(new {success = false});
            }

            delineation.IsVerified = viewModel.IsVerified;
            delineation.DateLastVerified = DateTime.Now;
            delineation.VerifiedByPersonID = CurrentPerson.PersonID;
            await _dbContext.SaveChangesAsync();

            // if verifying delineation, execute model at the delineation
            // if de-verifying, execute model at its BMP
            if (delineation.IsVerified)
            {
                await NereidUtilities.MarkDelineationDirty(delineation, _dbContext);
            }
            else
            {
                await NereidUtilities.MarkTreatmentBMPDirty(delineation.TreatmentBMP, _dbContext);
            }

            return Json(new {success = true});
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        public ContentResult MapDelete([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            return new ContentResult();
        }


        // todo: this and the other delete should share some of their code
        [HttpPost("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPEditFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public async Task<IActionResult> MapDelete([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, MapDeleteViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var delineation = treatmentBMP.Delineation;
            var isDelineationDistributed = delineation?.DelineationType == DelineationType.Distributed;
            var geometry = delineation?.DelineationGeometry;

            if (delineation == null)
            {
                throw new SitkaRecordNotFoundException(
                    $"No delineation found for Treatment BMP {treatmentBMPPrimaryKey}");
            }

            await delineation.DeleteDelineation(_dbContext);

            if (isDelineationDistributed)
            {
                await ModelingEngineUtilities.QueueLGURefreshForArea(geometry, null, _dbContext);
            }

            SetMessageForDisplay("The Delineation was successfully deleted.");

            return Json(new {success = true});
        }

        [HttpGet("{delineationPrimaryKey}")]
        [DelineationDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("delineationPrimaryKey")]
        public PartialViewResult Delete([FromRoute] DelineationPrimaryKey delineationPrimaryKey)
        {
            var delineation = delineationPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(delineation.DelineationID);
            return ViewDeleteDelineation(delineation, viewModel);
        }

        [HttpPost("{delineationPrimaryKey}")]
        [DelineationDeleteFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("delineationPrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] DelineationPrimaryKey delineationPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var delineation = delineationPrimaryKey.EntityObject;
            var geometry = delineation.DelineationGeometry;
            var isDelineationDistributed = delineation.DelineationType == DelineationType.Distributed;


            if (!ModelState.IsValid)
            {
                return ViewDeleteDelineation(delineation, viewModel);
            }

            await delineation.DeleteDelineation(_dbContext);

            SetMessageForDisplay("The Delineation was successfully deleted.");

            if (isDelineationDistributed)
            {
                await ModelingEngineUtilities.QueueLGURefreshForArea(geometry, null, _dbContext);
            }

            return new ModalDialogFormJsonResult(
                SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(_linkGenerator, x => x.Index()));
        }

        private PartialViewResult ViewDeleteDelineation(Delineation delineation, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage =
                $"Are you sure you want to delete the delineation for '{delineation.TreatmentBMP.TreatmentBMPName}'?";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }


        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult CheckForDiscrepancies()
        {
            return ViewCheckForDiscrepancies(new ConfirmDialogFormViewModel());
        }


        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult CheckForDiscrepancies(ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewCheckForDiscrepancies(viewModel);
            }

            //todo: hangfire
            //BackgroundJob.Schedule(() => ScheduledBackgroundJobLaunchHelper.RunDelineationDiscrepancyCheckerJob(), TimeSpan.FromSeconds(1));
            SetMessageForDisplay("The job to check BMP delineation discrepancies and overlaps has been queued. Please check back in a few minutes to see the new results.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewCheckForDiscrepancies(ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to check for discrepancies between ALL centralized and distributed delineations and the most recent Regional Subbasin Layer?<br /><br />This can take a little while to run.";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
    }

    public class ChangeDelineationStatusViewModel
    {
        public bool IsVerified { get; set; }
    }

    public class ForTreatmentBMPViewModel
    {
        public List<string> WellKnownText { get; set; }
        public string DelineationType { get; set; }
    }

    public class MapDeleteViewModel
    {
        // a formality -- in order to have a post I needed a get, so I needed a ViewModel to overload the method with.
    }
}
