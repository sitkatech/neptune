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

using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditAttributesViewData : NeptuneViewData
    {
        public Models.TreatmentBMP TreatmentBMP { get; }
        public List<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; set; }

        public EditAttributesViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP,
            CustomAttributeTypePurpose customAttributeTypePurpose) : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP)
        {
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Index());
            EntityUrl = treatmentBMPIndexUrl;
            SubEntityName = treatmentBMP.TreatmentBMPName;
            SubEntityUrl = treatmentBMP.GetDetailUrl();
            TreatmentBMP = treatmentBMP;
            PageTitle = $"Edit {Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabel()} Attributes";

            TreatmentBMPTypeCustomAttributeTypes = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Where(x=>x.CustomAttributeType.CustomAttributeTypePurposeID == customAttributeTypePurpose.CustomAttributeTypePurposeID)
                .OrderBy(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();
        }
    }
}
