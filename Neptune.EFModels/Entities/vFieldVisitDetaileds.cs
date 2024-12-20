﻿/*-----------------------------------------------------------------------
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
    public static class vFieldVisitDetaileds
    {
        public static List<vFieldVisitDetailed> GetProvisionalFieldVisits(NeptuneDbContext dbContext, IEnumerable<int> stormwaterJurisdictionIDsPersonCanView)
        {
            return dbContext.vFieldVisitDetaileds.AsNoTracking()
                .Where(x => x.IsFieldVisitVerified == false && stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).OrderByDescending(x => x.VisitDate).ToList();
        }

        public static List<vFieldVisitDetailed> GetProvisionalFieldVisits(NeptuneDbContext dbContext, Person currentPerson)
        {
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(dbContext, currentPerson);
            return GetProvisionalFieldVisits(dbContext, stormwaterJurisdictionIDsPersonCanView);
        }

        public static List<vFieldVisitDetailed> ListForStormwaterJurisdictionIDs(NeptuneDbContext dbContext, IEnumerable<int> stormwaterJurisdictionIDsPersonCanView)
        {
            return dbContext.vFieldVisitDetaileds.AsNoTracking()
                .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).OrderByDescending(x => x.VisitDate).ToList();
        }


        public static List<vFieldVisitDetailed> ListByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            return dbContext.vFieldVisitDetaileds.AsNoTracking().Where(x => x.TreatmentBMPID == treatmentBMPID)
                .OrderByDescending(x => x.VisitDate)
                .ToList();
        }
    }
}
