using LtInfo.Common;
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

            return new HtmlString($"<a href='{DetailUrlTemplate.ParameterReplace(regionalSubbasin.RegionalSubbasinID)}'>{regionalSubbasin.Watershed} - {regionalSubbasin.DrainID}: {regionalSubbasin.RegionalSubbasinID}</a>"); 
        }

        public static List<int> TraceUpstreamCatchmentsReturnIDList(this RegionalSubbasin regionalSubbasin,
            DatabaseEntities dbContext)
        {
            var idList = new List<int>();

            var lookingAt = regionalSubbasin.GetRegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment(dbContext);
            while (lookingAt.Any())
            {
                var ints = lookingAt.Select(x => x.RegionalSubbasinID);
                idList.AddRange(ints);
                lookingAt = lookingAt.SelectMany(x =>
                    x.GetRegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment(dbContext)).ToList();
            }

            return idList;
        }

        public static List<RegionalSubbasin> GetRegionalSubbasinsWhereYouAreTheOCSurveyDownstreamCatchment(
            this RegionalSubbasin regionalSubbasin, DatabaseEntities dbContext)
        {
            return dbContext.RegionalSubbasins
                .Where(x => x.OCSurveyDownstreamCatchmentID == regionalSubbasin.OCSurveyCatchmentID).ToList();
        }

        public static IEnumerable<TreatmentBMP> GetTreatmentBMPs(this RegionalSubbasin regionalSubbasin)
        {
            return HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetNonPlanningModuleBMPs()
                .Where(x => regionalSubbasin.CatchmentGeometry.Contains(x.LocationPoint));
        }
    }
}
