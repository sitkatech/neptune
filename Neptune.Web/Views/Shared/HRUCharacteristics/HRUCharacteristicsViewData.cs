using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.Shared.HRUCharacteristics
{
    public class HRUCharacteristicsViewData : NeptuneUserControlViewData
    {
        public List<HRUCharacteristicsSummaryDto> HRUCharacteristicsSummaries { get; }

        public HRUCharacteristicsViewData(List<HRUCharacteristic> hruCharacteristics)
        {
            HRUCharacteristicsSummaries = hruCharacteristics
                .GroupBy(x => x.HRUCharacteristicLandUseCode).Select(x => new HRUCharacteristicsSummaryDto()
                    { Area = x.Sum(y => y.Area).ToString("N2"), ImperviousCover = x.Sum(y => y.ImperviousAcres).ToString("N2"), LandUse = x.Key.HRUCharacteristicLandUseCodeDisplayName })
                .ToList();

            HRUCharacteristicsTotal = new HRUCharacteristicsSummaryDto
            {
                LandUse = "Total",
                Area = hruCharacteristics.Sum(x=>x.Area).ToString("N2"),
                ImperviousCover = hruCharacteristics.Sum(x=>x.ImperviousAcres).ToString("N2"),

            };
        }

        public HRUCharacteristicsSummaryDto HRUCharacteristicsTotal { get; set; }
    }
}