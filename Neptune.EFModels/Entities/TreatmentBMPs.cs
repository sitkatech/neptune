﻿/*-----------------------------------------------------------------------
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

using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities
{
    public static class TreatmentBMPs
    {
        public static List<TreatmentBMP> GetProvisionalTreatmentBMPs(NeptuneDbContext dbContext, Person currentPerson)
        {
            return GetNonPlanningModuleBMPs(dbContext).Where(x => x.InventoryIsVerified == false).ToList().Where(x => x.CanView(currentPerson)).OrderBy(x => x.TreatmentBMPName).ToList();
        }

        public static IQueryable<TreatmentBMP> GetNonPlanningModuleBMPs(NeptuneDbContext dbContext)
        {
            return GetImpl(dbContext).AsNoTracking().Where(x => x.ProjectID == null);
        }

        private static IQueryable<TreatmentBMP> GetImpl(NeptuneDbContext dbContext)
        {
            return dbContext.TreatmentBMPs
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Include(x => x.OwnerOrganization)
                .Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP)
                .Include(x => x.UpstreamBMP)
                ;
        }

        public static TreatmentBMP GetByIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            var treatmentBMP = GetImpl(dbContext)
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            Check.RequireNotNull(treatmentBMP, $"TreatmentBMP with ID {treatmentBMPID} not found!");
            return treatmentBMP;
        }

        public static TreatmentBMP GetByIDWithChangeTracking(NeptuneDbContext dbContext, TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            return GetByIDWithChangeTracking(dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue);
        }

        public static TreatmentBMP GetByID(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            var treatmentBMP = GetImpl(dbContext).AsNoTracking()
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            Check.RequireNotNull(treatmentBMP, $"TreatmentBMP with ID {treatmentBMPID} not found!");
            return treatmentBMP;
        }

        public static TreatmentBMP GetByID(NeptuneDbContext dbContext, TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            return GetByID(dbContext, treatmentBMPPrimaryKey.PrimaryKeyValue);
        }

        public static List<TreatmentBMP> List(NeptuneDbContext dbContext)
        {
            return GetImpl(dbContext).AsNoTracking().OrderBy(x => x.TreatmentBMPName).ToList();
        }

        public static List<TreatmentBMP> ListWithModelingAttributes(NeptuneDbContext dbContext)
        {
            return dbContext.TreatmentBMPs
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Include(x => x.OwnerOrganization)
                .Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP)
                .AsNoTracking().OrderBy(x => x.TreatmentBMPName).ToList();
        }

        public static Dictionary<int, int> ListCountByTreatmentBMPType(NeptuneDbContext dbContext)
        {
            return dbContext.TreatmentBMPs.AsNoTracking().GroupBy(x => x.TreatmentBMPTypeID).Select(x => new { x.Key, Count = x.Count()})
                .ToDictionary(x => x.Key, x => x.Count);
        }

        public static Dictionary<int, int> ListCountByStormwaterJurisdiction(NeptuneDbContext dbContext)
        {
            return dbContext.TreatmentBMPs.AsNoTracking().GroupBy(x => x.StormwaterJurisdictionID).Select(x => new { x.Key, Count = x.Count()})
                .ToDictionary(x => x.Key, x => x.Count);
        }

        public static TreatmentBMP GetByIDForFeatureContextCheck(NeptuneDbContext dbContext, int treatmentBMPID)
        {
            var treatmentBMP = dbContext.TreatmentBMPs
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization).AsNoTracking()
                .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
            Check.RequireNotNull(treatmentBMP, $"TreatmentBMP with ID {treatmentBMPID} not found!");
            return treatmentBMP;
        }

        public static List<TreatmentBMP> ListByStormwaterJurisdictionID(NeptuneDbContext dbContext, int stormwaterJurisdictionID)
        {
            return GetImpl(dbContext).AsNoTracking()
                .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID).ToList();
        }


        public static List<TreatmentBMP> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
        {
            return GetImpl(dbContext).AsNoTracking()
                .Where(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID).ToList();
        }

        public static List<TreatmentBMP> ListByTreatmentBMPIDList(NeptuneDbContext dbContext, List<int> treatmentBMPIDList)
        {
            return GetImpl(dbContext).AsNoTracking()
                .Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID)).ToList();
        }

        public static List<TreatmentBMP> ListByTreatmentBMPIDListWithChangeTracking(NeptuneDbContext dbContext, List<int> treatmentBMPIDList)
        {
            return GetImpl(dbContext).Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID)).ToList();
        }
    }
}
