namespace Hippocamp.Models.DataTransferObjects;

public class ProjectGrantScoreDto
{
    public int ProjectID { get; set; }
    public double? ProjectArea { get; set; }
    public double? PollutantVolume { get; set; }
    public double? PollutantMetals { get; set; }
    public double? PollutantBacteria { get; set; }
    public double? PollutantNutrients { get; set; }
    public double? PollutantTSS { get; set; }
    public double? TPI { get; set; }
    public double? SEA { get; set; }
    public double? DryWeatherWQLRI { get; set; }
    public double? WetWeatherWQLRI { get; set; }
    public string Watersheds { get; set; }
}