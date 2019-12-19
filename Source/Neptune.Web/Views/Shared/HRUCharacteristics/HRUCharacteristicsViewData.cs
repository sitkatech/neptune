using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared.HRUCharacteristics
{
    public class HRUCharacteristicsViewData : NeptuneUserControlViewData
    {
        public IHaveHRUCharacteristics EntityWithHRUCharacteristics { get; }
        public List<HRUCharacteristicsSummarySimple> HRUCharacteristicsSummaries { get; }

        public HRUCharacteristicsViewData(IHaveHRUCharacteristics entityWithHRUCharacteristics)
        {
            EntityWithHRUCharacteristics = entityWithHRUCharacteristics;

            HRUCharacteristicsSummaries = EntityWithHRUCharacteristics.HRUCharacteristics
                .GroupBy(x => x.HRUCharacteristicLandUseCode).Select(x => new HRUCharacteristicsSummarySimple()
                    { Area = x.Sum(y => y.Area).ToString("N2"), ImperviousCover = x.Sum(y => y.ImperviousAcres).ToString("N2"), LandUse = x.Key.HRUCharacteristicLandUseCodeDisplayName })
                .ToList();

            HRUCharacteristicsTotal = new HRUCharacteristicsSummarySimple
            {
                LandUse = "Total",
                Area = EntityWithHRUCharacteristics.HRUCharacteristics.Sum(x=>x.Area).ToString("N2"),
                ImperviousCover = EntityWithHRUCharacteristics.HRUCharacteristics.Sum(x=>x.ImperviousAcres).ToString("N2"),

            };
        }

        public HRUCharacteristicsSummarySimple HRUCharacteristicsTotal { get; set; }
    }
}