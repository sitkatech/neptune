/*-----------------------------------------------------------------------
<copyright file="TreatmentBMP.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency">
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

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static List<TreatmentBMP> GetProvisionalTreatmentBMPs(this IQueryable<TreatmentBMP> treatmentBMPs, Person currentPerson)
        {
            return treatmentBMPs.GetNonPlanningModuleBMPs().Where(x => x.InventoryIsVerified == false).ToList().Where(x => x.CanView(currentPerson)).OrderBy(x => x.TreatmentBMPName).ToList();
        }

        public static IQueryable<TreatmentBMP> GetNonPlanningModuleBMPs(this IQueryable<TreatmentBMP> treatmentBMPs)
        {
            return treatmentBMPs.Where(x => x.ProjectID == null);
        }
    }

}
