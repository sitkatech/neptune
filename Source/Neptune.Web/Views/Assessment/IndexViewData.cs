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

using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Assessment
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly TreatmentBMPAssessmentGridSpec BMPAssessmentGridSpec;
        public readonly string BMPAssessmentGridName;
        public readonly string BMPAssessmentGridDataUrl;

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage, IQueryable<Models.TreatmentBMPAssessmentObservationType> allObservationTypes)
            : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Treatment BMP Assessments";
            PageTitle = "All Assessments";

            BMPAssessmentGridSpec = new TreatmentBMPAssessmentGridSpec(currentPerson, allObservationTypes) { ObjectNameSingular = "BMP Assessment", ObjectNamePlural = "BMP Assessments", SaveFiltersInCookie = true };
            BMPAssessmentGridName = "bmpAssessmentGrid";
            BMPAssessmentGridDataUrl = SitkaRoute<AssessmentController>.BuildUrlFromExpression(j => j.TreatmentBMPAssessmentsGridJsonData());           
        }
    }
}
