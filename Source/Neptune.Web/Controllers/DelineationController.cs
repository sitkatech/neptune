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

using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Delineation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using JetBrains.Annotations;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;
using Microsoft.SqlServer.Types;
using Neptune.Web.Views.Shared;

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

            var delineationMapInitJson = new DelineationMapInitJson("delineationMap",
                CurrentPerson.GetTreatmentBmpsPersonCanManage(), CurrentPerson.GetBoundingBox());
            var viewData = new DelineationMapViewData(CurrentPerson, neptunePage, delineationMapInitJson, treatmentBMP);
            return RazorView<DelineationMap, DelineationMapViewData>(viewData);
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

            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(treatmentBMP.Delineation.DelineationGeometry);
            feature.Properties.Add("Area", treatmentBMP.GetDelineationAreaString());
            feature.Properties.Add("DelineationType",
                treatmentBMP.Delineation?.DelineationType.DelineationTypeDisplayName ?? "No delineation provided");

            return Content(JObject.FromObject(feature).ToString(Formatting.None));
        }

        [HttpPost]
        [TreatmentBMPEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey,
            ForTreatmentBMPViewModel viewModel)
        {
            var geom = viewModel.WellKnownText == DbGeometryToGeoJsonHelper.POLYGON_EMPTY
                ? null
                : DbGeometry.FromText(viewModel.WellKnownText, MapInitJson.CoordinateSystemId).ToSqlGeometry()
                    .MakeValid().ToDbGeometry();

            geom = geom?.FixSrid();


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
                if (geom != null)
                {
                    treatmentBMPDelineation.DelineationGeometry = geom;
                    treatmentBMPDelineation.DelineationTypeID =
                        delineationType.DelineationTypeID;
                    treatmentBMPDelineation.IsVerified = false;
                }
                else
                {
                    treatmentBMP.DelineationID = null;
                    HttpRequestStorage.DatabaseEntities.SaveChanges();
                    HttpRequestStorage.DatabaseEntities.Delineations.DeleteDelineation(treatmentBMPDelineation);
                }
            }
            else
            {
                if (geom == null)
                {
                    return Json(new {success = true});
                }

                var delineation = new Delineation(geom, delineationType.DelineationTypeID, false);
                HttpRequestStorage.DatabaseEntities.Delineations.Add(delineation);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                treatmentBMP.DelineationID = delineation.DelineationID;
            }



            return Json(new {success = true});
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

            treatmentBMP.DelineationID = null;
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            HttpRequestStorage.DatabaseEntities.Delineations.DeleteDelineation(delineation);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
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

            delineation.TreatmentBMP.DelineationID = null;
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            HttpRequestStorage.DatabaseEntities.Delineations.DeleteDelineation(delineation);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay("Successfully deleted the delineation.");
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
    }

    public class ChangeDelineationStatusViewModel
    {
        public bool IsVerified { get; set; }
    }

    public class ForTreatmentBMPViewModel
    {
        public string WellKnownText { get; set; }
        public string DelineationType { get; set; }
    }

    public class MapDeleteViewModel
    {
        // s formality
    }
}
