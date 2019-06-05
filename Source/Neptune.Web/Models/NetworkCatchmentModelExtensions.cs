using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common.GeoJson;

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

        public static FeatureCollection TraceBackbizzleDownstrizzle(this NetworkCatchment networkCatchment)
        {
            var backbizzleDownstrizzle = new List<BackboneSegment>();

            var lookingAt = networkCatchment.BackboneSegments;

            while (lookingAt.Any())
            {
                backbizzleDownstrizzle.AddRange(lookingAt);

                lookingAt = lookingAt.Select(x =>
                    x.DownstreamBackboneSegment).Distinct().ToList();
            }

            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(backbizzleDownstrizzle.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.BackboneSegmentGeometry);
                return feature;
            }));

            return featureCollection;
        }
    }
}
