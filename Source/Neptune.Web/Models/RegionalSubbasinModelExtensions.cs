using System;
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
    public static class RegionalSubbasinModelExtensions
    {
        private static readonly UrlTemplate<int> DetailUrlTemplate =
            new UrlTemplate<int>(SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(c =>
                c.Detail(UrlTemplate.Parameter1Int)));

        public static string GetDetailUrl(this RegionalSubbasin regionalSubbasin)
        {
            return DetailUrlTemplate.ParameterReplace(regionalSubbasin.RegionalSubbasinID);
        }

        public static HtmlString GetDisplayNameAsUrl(this RegionalSubbasin regionalSubbasin)
        {

            return new HtmlString($"<a href='{DetailUrlTemplate.ParameterReplace(regionalSubbasin.RegionalSubbasinID)}'>{regionalSubbasin.Watershed} - {regionalSubbasin.DrainID}</a>"); 
        }

        public static List<int> TraceUpstreamCatchmentsReturnIDList(this RegionalSubbasin regionalSubbasin)
        {
            var idList = new List<int>();

            var lookingAt = regionalSubbasin.GetRegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment();
            while (lookingAt.Any())
            {
                var ints = lookingAt.Select(x => x.RegionalSubbasinID);
                idList.AddRange(ints);
                lookingAt = lookingAt.SelectMany(x =>
                    x.GetRegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment()).ToList();
            }

            return idList;
        }

        public static List<RegionalSubbasin> GetRegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment(
            this RegionalSubbasin regionalSubbasin)
        {
            return HttpRequestStorage.DatabaseEntities.RegionalSubbasins
                .Where(x => x.OCSurveyDownstreamCatchmentID == regionalSubbasin.OCSurveyCatchmentID).ToList();
        }

        public static FeatureCollection TraceBackboneDownstream(this Neighborhood regionalSubbasin)
        {
            var backboneDownstream = new List<BackboneSegment>();

            var lookingAt = regionalSubbasin.BackboneSegments;

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

        public static FeatureCollection GetStormshedViaBackboneTrace(this Neighborhood regionalSubbasin)
        {
            var backboneAccumulated = new List<BackboneSegment>();

            var startingPoint = regionalSubbasin.BackboneSegments;

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

            var regionalSubbasinsInStormshed = backboneAccumulated.Select(x=>x.Neighborhood).Distinct();

            var featureCollection = new FeatureCollection();
            featureCollection.Features.AddRange(regionalSubbasinsInStormshed.Select(x =>
            {
                var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionCheck(x.NeighborhoodGeometry);
                feature.Properties.Add("NeighborhoodID", x.NeighborhoodID);
                return feature;
            }));

            return featureCollection;
        }

        public static IEnumerable<TreatmentBMP> GetTreatmentBMPs(this RegionalSubbasin regionalSubbasin)
        {
            return HttpRequestStorage.DatabaseEntities.TreatmentBMPs
                .Where(x => regionalSubbasin.CatchmentGeometry.Contains(x.LocationPoint));
        }
    }
}
