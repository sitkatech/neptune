using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Neptune.Web.Views.Shared.HRUCharacteristics
{
    public abstract class HRUCharacteristics : TypedWebPartialViewPage<HRUCharacteristicsViewData>
    {


        public static void RenderPartialView(HtmlHelper html, HRUCharacteristicsViewData viewData)
        {
            html.RenderRazorSitkaPartial<HRUCharacteristics, HRUCharacteristicsViewData>(viewData);
        }
    }

    public class HRUCharacteristicsViewData : NeptuneUserControlViewData
    {
        public IHaveHRUCharacteristics EntityWithHRUCharacteristics { get; }
        public List<HRUCharacteristicsSummarySimple> HRUCharacteristicsSummaries { get; }

        public HRUCharacteristicsViewData(IHaveHRUCharacteristics entityWithHRUCharacteristics)
        {
            EntityWithHRUCharacteristics = entityWithHRUCharacteristics;

            HRUCharacteristicsSummaries = EntityWithHRUCharacteristics.HRUCharacteristics
                .GroupBy(x => x.LSPCLandUseDescription).Select(x => new HRUCharacteristicsSummarySimple()
                { Area = x.Sum(y => y.Area).ToString("N2"), ImperviousCover = x.Sum(y => y.ImperviousAcres).ToString("N2"), LandUse = x.Key })
                .ToList();
        }
    }
}