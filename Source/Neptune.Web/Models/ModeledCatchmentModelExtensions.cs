/*-----------------------------------------------------------------------
<copyright file="ModeledCatchmentModelExtensions.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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

using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class ModeledCatchmentModelExtensions
    {
        public static readonly UrlTemplate<int> DetailJurisdictionUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<int> MapSummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(t => t.SummaryForMap(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));

        public static string GetJurisdictionSummaryUrl(this ModeledCatchment modeledCatchment)
        {
            if (modeledCatchment == null) { return ""; }
            return DetailJurisdictionUrlTemplate.ParameterReplace(modeledCatchment.StormwaterJurisdictionID);
        }
       

        public static string GetMapSummaryUrl(this ModeledCatchment modeledCatchment)
        {
            return MapSummaryUrlTemplate.ParameterReplace(modeledCatchment.ModeledCatchmentID);
        }

        public static string GetDeleteUrl(this ModeledCatchment bmpRegistration)
        {
            return DeleteUrlTemplate.ParameterReplace(bmpRegistration.ModeledCatchmentID);
        }


    }
}
