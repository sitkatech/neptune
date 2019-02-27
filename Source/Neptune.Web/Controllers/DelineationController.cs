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
using System.Net;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Security.Shared;
using System.Web;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Delineation;

namespace Neptune.Web.Controllers
{
    public class DelineationController : NeptuneBaseController
    {
        public ViewResult DelineationMap()
        {
            var viewData = new DelineationMapViewData(CurrentPerson, new StormwaterMapInitJson("delineationMap"));
            return RazorView<DelineationMap, DelineationMapViewData>(viewData);
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
}
