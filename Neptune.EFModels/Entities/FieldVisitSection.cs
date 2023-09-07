/*-----------------------------------------------------------------------
<copyright file="FieldVisitSection.cs" company="Tahoe Regional Planning Agency">
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

namespace Neptune.EFModels.Entities
{
    public abstract partial class FieldVisitSection
    {
        public abstract bool ExpandMenu(FieldVisit fieldVisit);
    }

    public partial class FieldVisitSectionInventory
    {
        public override bool ExpandMenu(FieldVisit fieldVisit)
        {
            return fieldVisit.InventoryUpdated;
        }
    }

    public partial class FieldVisitSectionAssessment
    {
        public override bool ExpandMenu(FieldVisit fieldVisit)
        {
            return fieldVisit.GetInitialAssessment() != null;
        }
    }

    public partial class FieldVisitSectionMaintenance
    {
        public override bool ExpandMenu(FieldVisit fieldVisit)
        {
            return fieldVisit.MaintenanceRecord != null;
        }
    }

    public partial class FieldVisitSectionPostMaintenanceAssessment
    {
        public override bool ExpandMenu(FieldVisit fieldVisit)
        {
            return fieldVisit.GetPostMaintenanceAssessment() != null;
        }
    }

    public partial class FieldVisitSectionVisitSummary
    {
        public override bool ExpandMenu(FieldVisit fieldVisit)
        {
            return false;
        }
    }
}
