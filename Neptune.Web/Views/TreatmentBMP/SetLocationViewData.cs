/*-----------------------------------------------------------------------
<copyright file="EditLocationViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Views.Shared.Location;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class SetLocationViewData : NeptuneViewData
    {
        public SetLocationViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            EFModels.Entities.TreatmentBMP treatmentBMP, EditLocationViewData editLocationViewData) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.FindABMP());
            EntityUrl = treatmentBMPIndexUrl;
            if (treatmentBMP != null)
            {
                SubEntityName = treatmentBMP.TreatmentBMPName;
                SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));
                TreatmentBMP = treatmentBMP;
            }

            PageTitle = "Edit Location";
            TreatmentBMPDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));

            EditLocationViewData = editLocationViewData;
        }

        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }
        public string TreatmentBMPDetailUrl { get; }
        public EditLocationViewData EditLocationViewData { get; }
    }
}