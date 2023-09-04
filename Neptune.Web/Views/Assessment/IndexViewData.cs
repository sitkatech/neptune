/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.Assessment
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly TreatmentBMPAssessmentGridSpec BMPAssessmentGridSpec;
        public readonly string BMPAssessmentGridName;
        public readonly string BMPAssessmentGridDataUrl;

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.NeptunePage neptunePage, IEnumerable<EFModels.Entities.TreatmentBMPAssessmentObservationType> treatmentBMPAssessmentObservationTypes)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Treatment BMP Assessments";
            PageTitle = "All Assessments";

            BMPAssessmentGridSpec = new TreatmentBMPAssessmentGridSpec(currentPerson, treatmentBMPAssessmentObservationTypes, linkGenerator) { ObjectNameSingular = "BMP Assessment", ObjectNamePlural = "BMP Assessments", SaveFiltersInCookie = true };
            BMPAssessmentGridName = "bmpAssessmentGrid";
            BMPAssessmentGridDataUrl = SitkaRoute<AssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.TreatmentBMPAssessmentsGridJsonData());
        }
    }
}
