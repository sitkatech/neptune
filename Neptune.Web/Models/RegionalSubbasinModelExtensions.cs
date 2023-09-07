using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Models
{
    public static class RegionalSubbasinModelExtensions
    {
        //public static HtmlString GetDisplayNameAsUrl(this RegionalSubbasin regionalSubbasin)
        //{

        //    return new HtmlString($"<a href='{DetailUrlTemplate.ParameterReplace(regionalSubbasin.RegionalSubbasinID)}'>{regionalSubbasin.Watershed} - {regionalSubbasin.DrainID}: {regionalSubbasin.RegionalSubbasinID}</a>"); 
        //}

        public static List<int> TraceUpstreamCatchmentsReturnIDList(this RegionalSubbasin regionalSubbasin,
            NeptuneDbContext dbContext)
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
            this RegionalSubbasin regionalSubbasin, NeptuneDbContext dbContext)
        {
            return dbContext.RegionalSubbasins
                .Where(x => x.OCSurveyDownstreamCatchmentID == regionalSubbasin.OCSurveyCatchmentID).ToList();
        }

        public static IEnumerable<TreatmentBMP> GetTreatmentBMPs(this RegionalSubbasin regionalSubbasin, NeptuneDbContext dbContext)
        {
            return TreatmentBMPs.GetNonPlanningModuleBMPs(dbContext)
                .Where(x => regionalSubbasin.CatchmentGeometry.Contains(x.LocationPoint));
        }

        public static IEnumerable<HRUCharacteristic> GetHRUCharacteristics(this RegionalSubbasin regionalSubbasin, NeptuneDbContext dbContext)
        {
            return dbContext.HRUCharacteristics.Where(x =>
                x.LoadGeneratingUnit.RegionalSubbasinID == regionalSubbasin.RegionalSubbasinID);
        }

    }
}
