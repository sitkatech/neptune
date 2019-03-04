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
        public DelineationMapViewData(Person currentPerson, Models.NeptunePage neptunePage, StormwaterMapInitJson mapInitJson, Models.TreatmentBMP initialTreatmentBMP) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            MapInitJson = mapInitJson;
            IsInitialTreatmentBMPProvided = initialTreatmentBMP != null;
            InitialTreatmentBMPID = initialTreatmentBMP?.TreatmentBMPID;
            EntityName = "Delineation";
            PageTitle = "Delineation Map";
            GeoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
        }

        public int? InitialTreatmentBMPID { get; }

        public StormwaterMapInitJson MapInitJson { get; }
        public bool IsInitialTreatmentBMPProvided { get; }
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
            TreatmentBMPLayerGeoJson = MakeTreatmentBMPLayerGeoJson(databaseEntitiesTreatmentBMPs,
                (feature, treatmentBMP) =>
                {
                    if (treatmentBMP.Delineation != null)
                    {
                        feature.Properties.Add("DelineationURL", treatmentBMP.GetDelineationUrl());
                    }
                }, false);
        }
    }
}
