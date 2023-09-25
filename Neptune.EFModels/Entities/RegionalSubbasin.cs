using System.Collections.Generic;
using System.Linq;

namespace Neptune.EFModels.Entities
{
    public partial class RegionalSubbasin
    {
        public List<RegionalSubbasin> GetRegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment(
            NeptuneDbContext dbContext)
        {
            return dbContext.RegionalSubbasins
                .Where(x => x.OCSurveyDownstreamCatchmentID == OCSurveyCatchmentID).ToList();
        }

        public List<int> TraceUpstreamCatchmentsReturnIDList(NeptuneDbContext dbContext)
        {
            var idList = new List<int>();

            var lookingAt = GetRegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment(dbContext);
            while (lookingAt.Any())
            {
                var ints = lookingAt.Select(x => x.RegionalSubbasinID);
                idList.AddRange(ints);
                lookingAt = lookingAt.SelectMany(x =>
                    x.GetRegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment(dbContext)).ToList();
            }

            return idList;
        }
    }
}