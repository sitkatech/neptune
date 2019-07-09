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

        public static FeatureCollection TraceBackboneDownstream(this NetworkCatchment networkCatchment)
        {
            var backboneDownstream = new List<BackboneSegment>();

            var lookingAt = networkCatchment.BackboneSegments;

            while (lookingAt.Any())
            {
                backboneDownstream.AddRange(lookingAt);

                lookingAt = lookingAt.Where(x=>x.DownstreamBackboneSegment != null).Select(x =>x.DownstreamBackboneSegment).Distinct().ToList();
            }

            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(backboneDownstream.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.BackboneSegmentGeometry);
                feature.Properties.Add("dummy", "dummy");
                return feature;
            }));

            return featureCollection;
        }

        public static FeatureCollection GetStormshedViaBackboneTrace(this NetworkCatchment networkCatchment)
        {
            var backboneAccumulated = new List<BackboneSegment>();

            var startingPoint = networkCatchment.BackboneSegments;

            var lookingAt = startingPoint.Where(x => x.BackboneSegmentTypeID != BackboneSegmentType.Channel.BackboneSegmentTypeID).ToList();

            while (lookingAt.Any())
            {
                backboneAccumulated.AddRange(lookingAt);

                var downFromHere = lookingAt.Where(x => x.DownstreamBackboneSegment != null)
                    .Select(x => x.DownstreamBackboneSegment)
                    .Where(x => x.BackboneSegmentTypeID != BackboneSegmentType.Channel.BackboneSegmentTypeID).Distinct()
                    .Except(backboneAccumulated);

                var upFromHere = lookingAt.SelectMany(x => x.BackboneSegmentsWhereYouAreTheDownstreamBackboneSegment)
                    .Where(x => x.BackboneSegmentTypeID != BackboneSegmentType.Channel.BackboneSegmentTypeID).Distinct()
                    .Except(backboneAccumulated);

                lookingAt = upFromHere.Union(downFromHere).ToList();
            }

            var networkCatchmentsInStormshed = backboneAccumulated.Select(x=>x.NetworkCatchment).Distinct();

            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(networkCatchmentsInStormshed.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(x.CatchmentGeometry);
                feature.Properties.Add("NetworkCatchmentID", x.NetworkCatchmentID);
                return feature;
            }));

            return featureCollection;
        }
    }
}
