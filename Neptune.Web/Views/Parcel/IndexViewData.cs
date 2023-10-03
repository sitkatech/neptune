/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;

namespace Neptune.Web.Views.Parcel
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly HtmlString IntroNarrativeContent;
        public readonly MapInitJson MapInitJson;
        public readonly string FindParcelByAddressUrl;
        public readonly string FindParcelByApnUrl;
        public readonly string ParcelSummaryForMapUrl;
        public readonly string GeoserverUrl;
        public readonly string ParcelLayerUploadUrl;

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.NeptunePage neptunePage,
            string introNarrativeContent,
            MapInitJson mapInitJson,
            string findParcelByAddressUrl,
            string findParcelByApnUrl,
            string geoserverUrl,
            string parcelSummaryForMapUrl,
            string parcelLayerUploadUrl) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            IntroNarrativeContent = new HtmlString(introNarrativeContent);
            EntityName = "Parcels";
            PageTitle = "All Parcels";

            MapInitJson = mapInitJson;
            FindParcelByAddressUrl = findParcelByAddressUrl;
            FindParcelByApnUrl = findParcelByApnUrl;
            GeoserverUrl = geoserverUrl;
            ParcelSummaryForMapUrl = parcelSummaryForMapUrl;
            ParcelLayerUploadUrl = parcelLayerUploadUrl;
        }
    }
}
