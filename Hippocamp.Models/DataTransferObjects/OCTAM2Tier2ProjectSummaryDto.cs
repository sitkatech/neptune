namespace Hippocamp.Models.DataTransferObjects
{
    public class ProjectHRUCharacteristicsSummaryDto : ProjectSimpleDto
    {
        public double Area { get; set; }
        public double ImperviousAcres { get; set; }
        public double? TPI { get; set; }
        public double? SEA { get; set; }
        public double? DryWeatherWQLRI { get; set; }
        public double? WetWeatherWQLRI { get; set; }
    }
}