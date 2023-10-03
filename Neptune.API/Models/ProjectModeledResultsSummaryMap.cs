using CsvHelper.Configuration;

namespace Neptune.Models.DataTransferObjects;

public sealed class ProjectModeledResultsSummaryMap : ClassMap<ProjectModeledResultSummaryDto>
{
    public ProjectModeledResultsSummaryMap()
    {
        Map(m => m.ProjectName).Name("Project Name");
        Map(m => m.Organization).Name("Organization");
        Map(m => m.Jurisdiction).Name("Jurisdiction");
        Map(m => m.AcresTreated).Name("Acres Treated");
        Map(m => m.ImperviousAcresTreated).Name("Impervious Acres Treated");
        Map(m => m.SEA).Name("SEA Score");
        Map(m => m.TPI).Name("TPI Score");
        Map(m => m.DryWeatherWQLRI).Name("Dry Weather WQLRI");
        Map(m => m.WetWeatherWQLRI).Name("Wet Weather WQLRI");

        Map(m => m.WetWeatherInflow).Name("WetWeatherInflow (cu-ft/yr)");
        Map(m => m.WetWeatherTreated).Name("WetWeatherTreated (cu-ft/yr)");
        Map(m => m.WetWeatherRetained).Name("WetWeatherRetained (cu-ft/yr)");
        Map(m => m.WetWeatherUntreated).Name("WetWeatherUntreated (cu-ft/yr)");
        Map(m => m.WetWeatherTSSReduced).Name("WetWeatherTSSReduced (kg)");
        Map(m => m.WetWeatherTNReduced).Name("WetWeatherTNReduced (kg)");
        Map(m => m.WetWeatherTPReduced).Name("WetWeatherTPReduced (kg)");
        Map(m => m.WetWeatherFCReduced).Name("WetWeatherFCReduced (billion CFUs)");
        Map(m => m.WetWeatherTCuReduced).Name("WetWeatherTCuReduced (g)");
        Map(m => m.WetWeatherTPbReduced).Name("WetWeatherTPbReduced (g)");
        Map(m => m.WetWeatherTZnReduced).Name("WetWeatherTZnReduced (g)");
        Map(m => m.WetWeatherTSSInflow).Name("WetWeatherTSSInflow (kg)");
        Map(m => m.WetWeatherTNInflow).Name("WetWeatherTNInflow (kg)");
        Map(m => m.WetWeatherTPInflow).Name("WetWeatherTPInflow (kg)");
        Map(m => m.WetWeatherFCInflow).Name("WetWeatherFCInflow (billion CFUs)");
        Map(m => m.WetWeatherTCuInflow).Name("WetWeatherTCuInflow (g)");
        Map(m => m.WetWeatherTPbInflow).Name("WetWeatherTPbInflow (g)");
        Map(m => m.WetWeatherTZnInflow).Name("WetWeatherTZnInflow (g)");

        Map(m => m.DryWeatherInflow).Name("DryWeatherInflow (cu-ft/yr)");
        Map(m => m.DryWeatherTreated).Name("DryWeatherTreated (cu-ft/yr)");
        Map(m => m.DryWeatherRetained).Name("DryWeatherRetained (cu-ft/yr)");
        Map(m => m.DryWeatherUntreated).Name("DryWeatherUntreated (cu-ft/yr)");
        Map(m => m.DryWeatherTSSReduced).Name("DryWeatherTSSReduced (kg)");
        Map(m => m.DryWeatherTNReduced).Name("DryWeatherTNReduced (kg)");
        Map(m => m.DryWeatherTPReduced).Name("DryWeatherTPReduced (kg)");
        Map(m => m.DryWeatherFCReduced).Name("DryWeatherFCReduced (billion CFUs)");
        Map(m => m.DryWeatherTCuReduced).Name("DryWeatherTCuReduced (g)");
        Map(m => m.DryWeatherTPbReduced).Name("DryWeatherTPbReduced (g)");
        Map(m => m.DryWeatherTZnReduced).Name("DryWeatherTZnReduced (g)");
        Map(m => m.DryWeatherTSSInflow).Name("DryWeatherTSSInflow (kg)");
        Map(m => m.DryWeatherTNInflow).Name("DryWeatherTNInflow (kg)");
        Map(m => m.DryWeatherTPInflow).Name("DryWeatherTPInflow (kg)");
        Map(m => m.DryWeatherFCInflow).Name("DryWeatherFCInflow (billion CFUs)");
        Map(m => m.DryWeatherTCuInflow).Name("DryWeatherTCuInflow (g)");
        Map(m => m.DryWeatherTPbInflow).Name("DryWeatherTPbInflow (g)");
        Map(m => m.DryWeatherTZnInflow).Name("DryWeatherTZnInflow (g)");

        Map(m => m.TotalInflow).Name("TotalInflow (cu-ft/yr)");
        Map(m => m.TotalTreated).Name("TotalTreated (cu-ft/yr)");
        Map(m => m.TotalRetained).Name("TotalRetained (cu-ft/yr)");
        Map(m => m.TotalUntreated).Name("TotalUntreated (cu-ft/yr)");
        Map(m => m.TotalTSSReduced).Name("TotalTSSReduced (kg)");
        Map(m => m.TotalTNReduced).Name("TotalTNReduced (kg)");
        Map(m => m.TotalTPReduced).Name("TotalTPReduced (kg)");
        Map(m => m.TotalFCReduced).Name("TotalFCReduced (billion CFUs)");
        Map(m => m.TotalTCuReduced).Name("TotalTCuReduced (g)");
        Map(m => m.TotalTPbReduced).Name("TotalTPbReduced (g)");
        Map(m => m.TotalTZnReduced).Name("TotalTZnReduced (g)");
        Map(m => m.TotalTSSInflow).Name("TotalTSSInflow (kg)");
        Map(m => m.TotalTNInflow).Name("TotalTNInflow (kg)");
        Map(m => m.TotalTPInflow).Name("TotalTPInflow (kg)");
        Map(m => m.TotalFCInflow).Name("TotalFCInflow (billion CFUs)");
        Map(m => m.TotalTCuInflow).Name("TotalTCuInflow (g)");
        Map(m => m.TotalTPbInflow).Name("TotalTPbInflow (g)");
        Map(m => m.TotalTZnInflow).Name("TotalTZnInflow (g)");
    }

}