/*-----------------------------------------------------------------------
<copyright file="WaterQualityManagementPlanVerify.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency">
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
    public static partial class DatabaseContextExtensions
    {

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(t => t.DeleteVerify(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            return DeleteUrlTemplate.ParameterReplace(waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);
        }


        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(t => t.WqmpVerify(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            return DetailUrlTemplate.ParameterReplace(waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID);
        }



    }

}
