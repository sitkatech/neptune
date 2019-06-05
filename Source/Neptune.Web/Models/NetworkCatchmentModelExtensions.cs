using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Models
{
    public static class NetworkCatchmentModelExtensions
    {
        public static List<int> TraceUpstreamCatchmentsReturnIDList(this NetworkCatchment networkCatchment)
        {
            var idList = new List<int>();

            var lookingAt = networkCatchment.NetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment;
            while (lookingAt.Any())
            {
                var ints = lookingAt.Select(x => x.NetworkCatchmentID);
                idList.AddRange(ints);
                lookingAt = lookingAt.SelectMany(x =>
                    x.NetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment).ToList();
            }

            return idList;
        }

        public static List<BackboneSegment> TraceBackbizzleDownstrizzle(this NetworkCatchment networkCatchment)
        {
            var backbizzleDownstrizzle = new List<BackboneSegment>();

            var lookingAt = networkCatchment.BackboneSegments;

            while (lookingAt.Any())
            {
                backbizzleDownstrizzle.AddRange(lookingAt);

                lookingAt = lookingAt.Select(x =>
                    x.DownstreamBackboneSegment).Distinct().ToList();
            }

            return backbizzleDownstrizzle.Select(x=>x.BackboneSegmentGeometry).Select();
        }
    }
}
