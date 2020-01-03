using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neptune.Web.Models
{
    public static class NetworkCatchmentModelExtensions
    {
        private static readonly UrlTemplate<int> DetailUrlTemplate =
            new UrlTemplate<int>(SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(c =>
                c.Detail(UrlTemplate.Parameter1Int)));

        public static string GetDetailUrl(this NetworkCatchment networkCatchment)
        {
            return DetailUrlTemplate.ParameterReplace(networkCatchment.NetworkCatchmentID);
        }

        public static HtmlString GetDisplayNameAsUrl(this NetworkCatchment networkCatchment)
        {

            return new HtmlString($"<a href='{DetailUrlTemplate.ParameterReplace(networkCatchment.NetworkCatchmentID)}'>{networkCatchment.Watershed} - {networkCatchment.DrainID}</a>"); 
        }

        public static List<int> TraceUpstreamCatchmentsReturnIDList(this NetworkCatchment networkCatchment)
        {
            var idList = new List<int>();

            var lookingAt = networkCatchment.GetNetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment();
            while (lookingAt.Any())
            {
                var ints = lookingAt.Select(x => x.NetworkCatchmentID);
                idList.AddRange(ints);
                lookingAt = lookingAt.SelectMany(x =>
                    x.GetNetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment()).ToList();
            }

            return idList;
        }

        public static List<NetworkCatchment> GetNetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment(
            this NetworkCatchment networkCatchment)
        {
            return HttpRequestStorage.DatabaseEntities.NetworkCatchments
                .Where(x => x.OCSurveyDownstreamCatchmentID == networkCatchment.OCSurveyCatchmentID).ToList();
        }

        public static FeatureCollection TraceBackboneDownstream(this Neighborhood networkCatchment)
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
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(x.BackboneSegmentGeometry);
                feature.Properties.Add("dummy", "dummy");
                return feature;
            }));

            return featureCollection;
        }

        public static FeatureCollection GetStormshedViaBackboneTrace(this Neighborhood networkCatchment)
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

            var networkCatchmentsInStormshed = backboneAccumulated.Select(x=>x.Neighborhood).Distinct();

            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(networkCatchmentsInStormshed.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(x.NeighborhoodGeometry);
                feature.Properties.Add("NeighborhoodID", x.NeighborhoodID);
                return feature;
            }));

            return featureCollection;
        }
    }
}
