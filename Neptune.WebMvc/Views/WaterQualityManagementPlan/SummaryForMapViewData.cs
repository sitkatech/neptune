/*-----------------------------------------------------------------------
<copyright file="SummaryForMapViewData.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class SummaryForMapViewData : NeptuneViewData
    {

        public EFModels.Entities.WaterQualityManagementPlan WaterQualityManagementPlan{ get; }
        public string StormwaterJurisdictionDetailUrl { get; }
        public string WaterQualityManagementPlanDetailUrl { get; }
        public string WaterQualityManagementPlanOAndMVerificationlUrl { get; }

        public SummaryForMapViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            StormwaterJurisdictionDetailUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(waterQualityManagementPlan.StormwaterJurisdictionID));
            WaterQualityManagementPlanDetailUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator,
                    x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
            WaterQualityManagementPlanOAndMVerificationlUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator,
                    x => x.NewWqmpVerify(waterQualityManagementPlan.WaterQualityManagementPlanID));
        }
    }
}
