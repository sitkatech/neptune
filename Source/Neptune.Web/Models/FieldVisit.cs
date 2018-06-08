/*-----------------------------------------------------------------------
<copyright file="FieldVisit.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class FieldVisit : IAuditableEntity
    {
        public TreatmentBMPAssessment GetAssessmentByType(FieldVisitAssessmentType fieldVisitAssessmentType)
        {
            return fieldVisitAssessmentType == FieldVisitAssessmentType.Initial
                ? InitialAssessment
                : PostMaintenanceAssessment;
        }

        public string AuditDescriptionString => $"Field Visit deleted";

        public void DetachMaintenanceRecord()
        {
            MaintenanceRecordID = null;
        }

        public void DetachPostMaintenanceAssessment()
        {
            PostMaintenanceAssessmentID = null;
        }

        public void DetachInitialAssessment()
        {
            InitialAssessmentID = null;
        }

        public bool RequiredAttributeDoesNotHaveValue()
        {
            // pick-many-from-list attributes will not be posted at all if none of their options are selected, so we need to check if any of the required custom attribute types are missing from the list of custom attributes
            return TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Any(x =>
                       x.CustomAttributeType.CustomAttributeTypePurposeID !=
                       CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID &&
                       x.CustomAttributeType.IsRequired &&
                       !TreatmentBMP.CustomAttributes.Select(y => y.CustomAttributeTypeID)
                           .Contains(x.CustomAttributeTypeID)) ||
                   TreatmentBMP.CustomAttributes.Any(x =>
                       x.CustomAttributeType.CustomAttributeTypePurposeID !=
                       CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID &&
                       x.CustomAttributeType.IsRequired &&
                       (x.CustomAttributeValues == null ||
                        x.CustomAttributeValues.All(y => string.IsNullOrEmpty(y.AttributeValue)))
                   );
        }
    }
}