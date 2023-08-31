﻿/*-----------------------------------------------------------------------
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

namespace Neptune.Web.Views.Shared.EditAttributes
{
    public class EditAttributesViewData : NeptuneViewData
    {
        public List<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; }
        public string ParentDetailUrl { get; set; }
        public bool IsSubForm { get; }
        public bool MissingRequiredAttributes { get; }

        public EditAttributesViewData(Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP,
            CustomAttributeTypePurpose customAttributeTypePurpose, bool isSubForm, bool missingRequiredAttributes, LinkGenerator linkGenerator, HttpContext httpContext) : base(currentPerson, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            IsSubForm = isSubForm;
            MissingRequiredAttributes = missingRequiredAttributes;
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            EntityUrl = treatmentBMPIndexUrl;
            SubEntityName = treatmentBMP.TreatmentBMPName;
            SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));
            PageTitle = $"Edit {FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabel()} Attributes";

            ParentDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));

            TreatmentBMPTypeCustomAttributeTypes = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes
                .Where(x => x.CustomAttributeType.CustomAttributeTypePurposeID ==
                            customAttributeTypePurpose.CustomAttributeTypePurposeID)
                .ToList().OrderBy(x => x.SortOrder).ThenBy(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();
        }

        public EditAttributesViewData(Person currentPerson, EFModels.Entities.FieldVisit fieldVisit, bool isSubForm, bool missingRequiredAttributes, LinkGenerator linkGenerator, HttpContext httpContext) : base(
            currentPerson, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            IsSubForm = isSubForm;
            MissingRequiredAttributes = missingRequiredAttributes;
            TreatmentBMPTypeCustomAttributeTypes = fieldVisit.TreatmentBMP.TreatmentBMPType
                .TreatmentBMPTypeCustomAttributeTypes.Where(x =>
                    x.CustomAttributeType.CustomAttributeTypePurposeID !=
                    CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).ToList().OrderBy(x =>
                    x.CustomAttributeType.CustomAttributeTypePurpose.CustomAttributeTypePurposeDisplayName)
                .ThenBy(x => x.SortOrder).ThenBy(x => x.CustomAttributeType.CustomAttributeTypeName).ToList();
        }
    }
}