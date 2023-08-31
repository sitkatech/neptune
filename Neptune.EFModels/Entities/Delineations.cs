/*-----------------------------------------------------------------------
<copyright file="FieldVisit.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public static class Delineations
    {
        public static List<Delineation> GetProvisionalBMPDelineations(NeptuneDbContext dbContext, Person currentPerson)
        {
            return dbContext.Delineations.AsNoTracking()
                .Include(x => x.TreatmentBMP).ThenInclude(x => x.TreatmentBMPType)
                .Include(x => x.TreatmentBMP).ThenInclude(x => x.StormwaterJurisdiction)
                .Where(x => x.IsVerified == false).AsEnumerable()
                .Where(x => x.TreatmentBMP.CanView(currentPerson))
                .OrderBy(x => x.TreatmentBMP.TreatmentBMPName).ToList();
        }

        public static void MarkAsVerified(Delineation delineation, Person currentPerson)
        {
            delineation.IsVerified = true;
            delineation.DateLastVerified = DateTime.Now;
            delineation.VerifiedByPersonID = currentPerson.PersonID;
        }
    }
}
