using System.Collections.Generic;
using System.Linq;
using Hippocamp.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class StormwaterJurisdictions
    {
        private static IQueryable<StormwaterJurisdiction> getStormwaterJurisdictionsImpl(HippocampDbContext dbContext)
        {
            return dbContext.StormwaterJurisdictions
                .Include(x => x.Organization);
        }

        public static List<StormwaterJurisdictionSimpleDto> ListByIDsAsSimpleDto(HippocampDbContext dbContext, List<int> stormwaterJurisdictionIDs)
        {
            return getStormwaterJurisdictionsImpl(dbContext)
                .Where(x => stormwaterJurisdictionIDs.Contains(x.StormwaterJurisdictionID))
                .Select(x => x.AsSimpleDto())
                .ToList();
        }
    }
}