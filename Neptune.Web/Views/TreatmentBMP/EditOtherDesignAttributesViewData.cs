/*-----------------------------------------------------------------------
<copyright file="EditViewData.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditOtherDesignAttributesViewData : NeptuneViewData
    {
        public string ParentDetailUrl { get; set; }
        public Shared.EditAttributes.EditAttributesViewData EditAttributesViewData { get; }

        public EditOtherDesignAttributesViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP, string parentDetailUrl, Views.Shared.EditAttributes.EditAttributesViewData editAttributesViewData) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            SubEntityName = treatmentBMP.TreatmentBMPName;
            SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));
            PageTitle = $"Edit {FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabel()} Other Design Attributes";
            EditAttributesViewData = editAttributesViewData;
            ParentDetailUrl = parentDetailUrl;
        }
    }
}
