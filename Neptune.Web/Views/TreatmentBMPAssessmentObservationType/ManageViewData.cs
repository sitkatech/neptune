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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
    public class ManageViewData : NeptuneViewData
    {
        public readonly TreatmentBMPAssessmentObservationTypeGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;
        public readonly string NewObservationTypeUrl;

        public ManageViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.NeptunePage neptunePage)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Observation Type";
            PageTitle = "All Observation Types";

            NewObservationTypeUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, t => t.New());
            GridSpec = new TreatmentBMPAssessmentObservationTypeGridSpec(linkGenerator, currentPerson)
            {
                ObjectNameSingular = "Observation Type",
                ObjectNamePlural = $"Observation Types",
                SaveFiltersInCookie = true                
            };

            GridName = "observationTypeGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(linkGenerator, tc => tc.ObservationTypeGridJsonData());
        }
    }
}
