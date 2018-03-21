﻿/*-----------------------------------------------------------------------
<copyright file="AssessmentInformationViewData.cs" company="Tahoe Regional Planning Agency">
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

using System.Collections.Generic;
using System.Web.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class AssessmentInformationViewData : AssessmentSectionViewData
    {
        public readonly IEnumerable<SelectListItem> JurisdictionPeople;
        public readonly IEnumerable<SelectListItem> AssessmentTypes;

        public const string ThisSectionName = "Assessment Information";
        public AssessmentInformationViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBmpAssessment, IEnumerable<SelectListItem> jurisdictionPeople, IEnumerable<SelectListItem> assessmentTypes)
            : base(currentPerson, treatmentBmpAssessment, ThisSectionName)
        {
            JurisdictionPeople = jurisdictionPeople;
            AssessmentTypes = assessmentTypes;
        }  
    }
}
