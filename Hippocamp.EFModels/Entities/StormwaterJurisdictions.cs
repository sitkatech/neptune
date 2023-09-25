using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Neptune.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class StormwaterJurisdictions
    {
        private static IQueryable<StormwaterJurisdiction> GetStormwaterJurisdictionsImpl(NeptuneDbContext dbContext)
        {
            return dbContext.StormwaterJurisdictions
                .Include(x => x.Organization)
                .AsNoTracking();
        }

        public static List<StormwaterJurisdictionSimpleDto> ListByIDsAsSimpleDto(NeptuneDbContext dbContext, List<int> stormwaterJurisdictionIDs)
        {
            return GetStormwaterJurisdictionsImpl(dbContext)
                .Where(x => stormwaterJurisdictionIDs.Contains(x.StormwaterJurisdictionID))
                .OrderBy(x => x.Organization.OrganizationName)
                .Select(x => x.AsSimpleDto())
                .ToList();
        }

        public static BoundingBoxDto GetBoundingBoxDtoByJurisdictionID(NeptuneDbContext dbContext, int stormwaterJurisdictionID)
        {
            var stormwaterJurisdictionGeometry = dbContext.StormwaterJurisdictionGeometries
                .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID)
                .Select(x => x.Geometry4326);

            return new BoundingBoxDto(stormwaterJurisdictionGeometry);
        }

        public static BoundingBoxDto GetBoundingBoxDtoByPersonID(NeptuneDbContext dbContext, int personID)
        {
            var person = People.GetByID(dbContext, personID);
            var jurisdictions = dbContext.StormwaterJurisdictionGeometries;
            if (person.RoleID != (int)RoleEnum.Admin || person.RoleID != (int)RoleEnum.SitkaAdmin)
            {
                var jurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, personID);
                jurisdictions.Where(x => jurisdictionIDs.Contains(x.StormwaterJurisdictionID));
            }
            return new BoundingBoxDto(jurisdictions.Select(x => x.Geometry4326).ToList());
        }
    }
}