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

using System;
using System.Data.Entity.Spatial;
using System.Net;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Security.Shared;
using System.Web;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Delineation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Neptune.Web.Controllers
{
    public class DelineationController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult DelineationMap(int? treatmentBMPID)
        {
            var treatmentBMP = treatmentBMPID.HasValue
                ? HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(
                    treatmentBMPID.Value)
                : null;
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.DelineationMap);

            var delineationMapInitJson = new DelineationMapInitJson("delineationMap", HttpRequestStorage.DatabaseEntities.TreatmentBMPs);
            var viewData = new DelineationMapViewData(CurrentPerson, neptunePage, delineationMapInitJson, treatmentBMP);
            return RazorView<DelineationMap, DelineationMapViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ContentResult ForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            if (treatmentBMP.Delineation == null)
            {
                // should be 400 tbh
                return Content(JObject.FromObject(new {noDelineation = true}).ToString(Formatting.None));
            }

            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(treatmentBMP.Delineation.DelineationGeometry);

            return Content(JObject.FromObject(feature).ToString(Formatting.None));
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, ForTreatmentBMPViewModel viewModel)
        {
            var geom = viewModel.WellKnownText == DbGeometryToGeoJsonHelper.POLYGON_EMPTY ? null : DbGeometry.FromText(viewModel.WellKnownText, 4326).ToSqlGeometry().MakeValid().ToDbGeometry();
            
            if (geom != null)
            {
                // make sure the SRID is 4326 before we save
                var wkt = geom.ToString();

                if (wkt.IndexOf("MULTIPOLYGON", StringComparison.InvariantCulture) > -1)
                {
                    wkt = wkt.Substring(wkt.IndexOf("MULTIPOLYGON", StringComparison.InvariantCulture));
                }
                else
                {
                    wkt = wkt.Substring(wkt.IndexOf("POLYGON", StringComparison.InvariantCulture));
                }

                geom = DbGeometry.FromText(wkt, 4326);
            }


            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPDelineation = treatmentBMP.Delineation;

            if (!Enum.TryParse(viewModel.DelineationType, out DelineationTypeEnum delineationTypeEnum))
            {
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

                var delineation = new Delineation(geom, delineationType.DelineationTypeID);
                HttpRequestStorage.DatabaseEntities.Delineations.Add(delineation);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                treatmentBMP.DelineationID = delineation.DelineationID;
            }

            

            return Json(new {success = true});
        }
    }

    public class ForTreatmentBMPViewModel
    {
        public string WellKnownText { get; set; }
        public string DelineationType { get; set; }
    }
}
