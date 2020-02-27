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

using Hangfire;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GeoJson;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Security;
using Neptune.Web.Views.Delineation;
using Neptune.Web.Views.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Mvc;

namespace Neptune.Web.Controllers
{
    public class DelineationController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult DelineationMap(int? treatmentBMPID)
        {
            var treatmentBMP = treatmentBMPID.HasValue
                ? HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(
                    treatmentBMPID.Value)
                : null;

            if (treatmentBMP != null)
            {
                var permissionCheckResult = new TreatmentBMPEditFeature().HasPermission(CurrentPerson, treatmentBMP);
                Check.Assert(permissionCheckResult.HasPermission, permissionCheckResult.PermissionDeniedMessage);
            }

            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.DelineationMap);
            var stormwaterJurisdictionsPersonCanView = CurrentPerson.GetStormwaterJurisdictionsPersonCanView().ToList();
            var stormwaterJurisdictionIDsPersonCanView = stormwaterJurisdictionsPersonCanView.Select(x => x.StormwaterJurisdictionID);
            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Include(x => x.Delineations).Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            var delineationMapInitJson = new DelineationMapInitJson("delineationMap", treatmentBMPs, BoundingBox.GetBoundingBox(stormwaterJurisdictionsPersonCanView));
            var bulkUploadTreatmentBMPDelineationsUrl = SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(x => x.UpdateDelineationGeometry());
            var viewData = new DelineationMapViewData(CurrentPerson, neptunePage, delineationMapInitJson, treatmentBMP, bulkUploadTreatmentBMPDelineationsUrl);
            return RazorView<DelineationMap, DelineationMapViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult DelineationReconciliationReport()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.DelineationReconciliationReport);
            var regionalSubbasinsLastUpdated = HttpRequestStorage.DatabaseEntities.RegionalSubbasins.Max(x => x.LastUpdate);
            var viewData = new DelineationReconciliationReportViewData(CurrentPerson, neptunePage, regionalSubbasinsLastUpdated);
            return RazorView<DelineationReconciliationReport, DelineationReconciliationReportViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<Delineation> DelineationsMisalignedWithRegionalSubbasinsGridJsonData()
        {
            var gridSpec = new MisalignedDelineationGridSpec();
            var delineations = HttpRequestStorage.DatabaseEntities.Delineations.Where(x => x.HasDiscrepancies).ToList().Where(x => x.TreatmentBMP.CanView(CurrentPerson)).OrderBy(x => x.TreatmentBMP.TreatmentBMPName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Delineation>(delineations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<Delineation> DelineationsOverlappingEachOtherGridJsonData()
        {
            var gridSpec = new DelineationOverlapsDelineationGridSpec();
            var delineations = HttpRequestStorage.DatabaseEntities.Delineations.Where(x => x.DelineationOverlaps.Any()).ToList().Where(x => x.TreatmentBMP.CanView(CurrentPerson)).ToList().OrderBy(x => x.TreatmentBMP.TreatmentBMPName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Delineation>(delineations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [TreatmentBMPEditFeature]
        public ContentResult ForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            if (treatmentBMP.Delineation == null)
            {
                // should be 400 tbh
                return Content(JObject.FromObject(new {noDelineation = true}).ToString(Formatting.None));
            }

            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithNoReproject(treatmentBMP.Delineation.DelineationGeometry4326);
            feature.Properties.Add("Area", treatmentBMP.GetDelineationAreaString());
            feature.Properties.Add("DelineationType",
                treatmentBMP.Delineation?.DelineationType.DelineationTypeDisplayName ?? "No delineation provided");

            feature.Properties.Add("DelineationStatus",
                treatmentBMP.Delineation?.IsVerified ?? false ? "Verified" : "Provisional");

            return Content(JObject.FromObject(feature).ToString(Formatting.None));
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            ForTreatmentBMPViewModel viewModel)
        {
            DbGeometry geom4326;

            if (viewModel.WellKnownText.Count == 1)
            {
                geom4326 = viewModel.WellKnownText[0] == DbGeometryToGeoJsonHelper.POLYGON_EMPTY
                    ? null
                    : DbGeometry.FromText(viewModel.WellKnownText[0], CoordinateSystemHelper.WGS_1984_SRID).ToSqlGeometry()
                        .MakeValid().ToDbGeometry().FixSrid(CoordinateSystemHelper.WGS_1984_SRID);
            }
            else
            {
                geom4326 = viewModel.WellKnownText
                    .Select(x =>
                        DbGeometry.FromText(x, CoordinateSystemHelper.WGS_1984_SRID).ToSqlGeometry().MakeValid()
                            .ToDbGeometry().FixSrid(CoordinateSystemHelper.WGS_1984_SRID)).ToList()
                    .UnionListGeometries();
            }

           

            DbGeometry geom2771 = null;

            // like all POSTs from the browser, transform to State Plane 
            if (geom4326 != null)
            {
                geom2771 = CoordinateSystemHelper.ProjectWebMercatorToCaliforniaStatePlaneVI(geom4326);
            }

            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPDelineation = treatmentBMP.Delineation;

            // todo: validate on the view model, not here
            if (!Enum.TryParse(viewModel.DelineationType, out DelineationTypeEnum delineationTypeEnum))
            {
                // todo: really should return a 400 bad request
                return Json(new {error = "Invalid Delineation Type"});
            }

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
                    HttpRequestStorage.DatabaseEntities.Delineations.DeleteDelineation(treatmentBMPDelineation);

                }
            }
            else
            {
                if (geom4326 == null)
                {
                    return Json(new {success = true});
                }

                var delineation =
                    new Delineation(geom2771, delineationType.DelineationTypeID, false, treatmentBMP.TreatmentBMPID,
                        DateTime.Now, false) {DelineationGeometry4326 = geom4326};
                HttpRequestStorage.DatabaseEntities.Delineations.Add(delineation);
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return Json(new {success = true, delineationID = treatmentBMP.Delineation.DelineationID});
        }

        [HttpGet]
        public ContentResult ChangeDelineationStatus(DelineationPrimaryKey delineationPrimaryKey)
        {
            return new ContentResult();
        }

        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ChangeDelineationStatus(DelineationPrimaryKey delineationPrimaryKey,
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

            return Json(new {success = true});
        }

        [HttpGet]
        [TreatmentBMPEditFeature]
        public ContentResult MapDelete(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            return new ContentResult();
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        public ActionResult MapDelete(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, MapDeleteViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var delineation = treatmentBMP.Delineation;

            if (delineation == null)
            {
                throw new SitkaRecordNotFoundException(
                    $"No delineation found for Treatment BMP {treatmentBMPPrimaryKey}");
            }

            HttpRequestStorage.DatabaseEntities.Delineations.DeleteDelineation(delineation);

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay("The Delineation was successfully deleted.");

            return Json(new {success = true});
        }

        [HttpGet]
        [DelineationDeleteFeature]
        public PartialViewResult Delete(DelineationPrimaryKey delineationPrimaryKey)
        {
            var delineation = delineationPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(delineation.DelineationID);
            return ViewDeleteDelineation(delineation, viewModel);
        }

        [HttpPost]
        [DelineationDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(DelineationPrimaryKey delineationPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var delineation = delineationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteDelineation(delineation, viewModel);
            }
            
            HttpRequestStorage.DatabaseEntities.Delineations.DeleteDelineation(delineation);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay("The Delineation was successfully deleted.");


            return new ModalDialogFormJsonResult(
                SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(c => c.Index()));
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

            BackgroundJob.Schedule(() => ScheduledBackgroundJobLaunchHelper.RunDelineationDiscrepancyCheckerJob(), TimeSpan.FromSeconds(1));
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
        // a formality
    }
}
