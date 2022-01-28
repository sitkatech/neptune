using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class StormwaterJurisdictions
    {
        private static IQueryable<StormwaterJurisdiction> GetStormwaterJurisdictionsImpl(HippocampDbContext dbContext)
        {
            return dbContext.StormwaterJurisdictions
                .Include(x => x.Organization);
        }

        public static List<StormwaterJurisdictionSimpleDto> ListByIDsAsSimpleDto(HippocampDbContext dbContext, List<int> stormwaterJurisdictionIDs)
        {
            return GetStormwaterJurisdictionsImpl(dbContext)
                .Where(x => stormwaterJurisdictionIDs.Contains(x.StormwaterJurisdictionID))
                .OrderBy(x => x.Organization.OrganizationName)
                .Select(x => x.AsSimpleDto())
                .ToList();
        }
    }
}