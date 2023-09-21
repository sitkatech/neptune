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
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities
{
    public static class Delineations
    {
        public static IQueryable<Delineation> GetImpl(NeptuneDbContext dbContext)
        {
            return dbContext.Delineations
                    .Include(x => x.TreatmentBMP)
                ;
        }

        public static Delineation GetByIDWithChangeTracking(NeptuneDbContext dbContext,
            int delineationID)
        {
            var delineation = GetImpl(dbContext)
                .SingleOrDefault(x => x.DelineationID == delineationID);
            Check.RequireNotNull(delineation,
                $"Delineation with ID {delineationID} not found!");
            return delineation;
        }

        public static Delineation GetByIDWithChangeTracking(NeptuneDbContext dbContext,
            DelineationPrimaryKey delineationPrimaryKey)
        {
            return GetByIDWithChangeTracking(dbContext, delineationPrimaryKey.PrimaryKeyValue);
        }

        public static Delineation GetByID(NeptuneDbContext dbContext, int delineationID)
        {
            var delineation = GetImpl(dbContext).AsNoTracking()
                .SingleOrDefault(x => x.DelineationID == delineationID);
            Check.RequireNotNull(delineation,
                $"Delineation with ID {delineationID} not found!");
            return delineation;
        }

        public static Delineation GetByID(NeptuneDbContext dbContext,
            DelineationPrimaryKey delineationPrimaryKey)
        {
            return GetByID(dbContext, delineationPrimaryKey.PrimaryKeyValue);
        }

        public static Delineation? GetByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            var delineation = GetImpl(dbContext).AsNoTracking()
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            return delineation;
        }

        public static Delineation? GetByTreatmentBMPIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            var delineation = GetImpl(dbContext)
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            return delineation;
        }

        public static List<Delineation> ListByTreatmentBMPIDList(NeptuneDbContext dbContext, IEnumerable<int> treatmentBMPIDList)
        {
            return GetImpl(dbContext).AsNoTracking()
                .Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID)).OrderBy(x => x.DelineationID).ToList();
        }

        public static List<Delineation> GetProvisionalBMPDelineations(NeptuneDbContext dbContext, Person currentPerson)
        {
            return GetImpl(dbContext).AsNoTracking()
                .Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.TreatmentBMPType)
                .Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Where(x => x.IsVerified == false).ToList()
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