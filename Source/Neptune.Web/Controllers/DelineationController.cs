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
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Security.Shared;
using System.Web;
using LtInfo.Common.GeoJson;
using LtInfo.Common.Mvc;
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
        [NeptuneViewFeature]
        public ViewResult DelineationMap()
        {
            var delineationMapInitJson = new DelineationMapInitJson("delineationMap", HttpRequestStorage.DatabaseEntities.TreatmentBMPs);
            var viewData = new DelineationMapViewData(CurrentPerson, delineationMapInitJson);
            return RazorView<DelineationMap, DelineationMapViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ContentResult ForTreatmentBMP(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;

            if (treatmentBMP.Delineation == null)
            {
                // should be 400 tbh
                return Content("{}");
            }

            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(treatmentBMP.Delineation.DelineationGeometry);

            return Content(JObject.FromObject(feature).ToString(Formatting.None));
        }
    }
}

namespace Neptune.Web.Views.Delineation
{
    public class DelineationMapViewData : NeptuneViewData
    {
        public DelineationMapViewData(Person currentPerson, StormwaterMapInitJson mapInitJson) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            MapInitJson = mapInitJson;
            EntityName = "Delineation";
            PageTitle = "Delineation Map";
            GeoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
        }

        public StormwaterMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
    }

    public abstract class DelineationMap : TypedWebViewPage<DelineationMapViewData>
    {

    }

    public class DelineationMapInitJson : StormwaterMapInitJson
    {
        public LayerGeoJson TreatmentBMPLayerGeoJson { get; set; }

        public DelineationMapInitJson(string mapDivID, DbSet<Models.TreatmentBMP> databaseEntitiesTreatmentBMPs) : base(mapDivID)
        {
            TreatmentBMPLayerGeoJson = MakeTreatmentBMPLayerGeoJson(databaseEntitiesTreatmentBMPs, true, false);
        }
    }
}
