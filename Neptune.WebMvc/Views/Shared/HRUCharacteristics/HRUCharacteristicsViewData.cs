using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Views.Shared.HRUCharacteristics
{
    public class HRUCharacteristicsViewData : NeptuneUserControlViewData
    {
        public List<HRUCharacteristicsSummaryDto> HRUCharacteristicsSummaries { get; }

        public HRUCharacteristicsViewData(List<vHRUCharacteristic> hruCharacteristics)
        {
            HRUCharacteristicsSummaries = hruCharacteristics
                .GroupBy(x => x.HRUCharacteristicLandUseCodeDisplayName).Select(x => new HRUCharacteristicsSummaryDto()
                    { Area = x.Sum(y => y.Area).ToString("N2"), ImperviousCover = x.Sum(y => y.ImperviousAcres).ToString("N2"), LandUse = x.Key })
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