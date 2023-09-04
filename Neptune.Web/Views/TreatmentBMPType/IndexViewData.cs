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
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class IndexViewData : NeptuneViewData
    {

        public List<EFModels.Entities.TreatmentBMPType> TreatmentBMPTypes { get; }
        public UrlTemplate<int> DetailUrlTemplate { get; }
        public UrlTemplate<int> TreatmentBMPAssessmentObservationTypeDetailUrlTemplate { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            NeptunePage neptunePage,
            List<EFModels.Entities.TreatmentBMPType> treatmentBMPTypes)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabelPluralized()}";
            PageTitle = "All BMP Types";

            TreatmentBMPTypes = treatmentBMPTypes;
            DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            TreatmentBMPAssessmentObservationTypeDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int))); // todo: need to fix to right controller
        }
    }
}
