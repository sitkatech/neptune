using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration;

namespace Hippocamp.Models.DataTransferObjects
{
    public class ProjectLoadReducingResultDto
    {
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public double? EffectiveAreaAcres { get; set; }
        public double? DesignStormDepth85thPercentile { get; set; }
        public double? DesignVolume85thPercentile { get; set; }
        public double WetWeatherInflow { get; set; }
        public double WetWeatherTreated { get; set; }
        public double WetWeatherRetained { get; set; }
        public double WetWeatherUntreated { get; set; }
        public double WetWeatherTSSReduced { get; set; }
        public double WetWeatherTNReduced { get; set; }
        public double WetWeatherTPReduced { get; set; }
        public double WetWeatherFCReduced { get; set; }
        public double WetWeatherTCuReduced { get; set; }
        public double WetWeatherTPbReduced { get; set; }
        public double WetWeatherTZnReduced { get; set; }
        public double WetWeatherTSSInflow { get; set; }
        public double WetWeatherTNInflow { get; set; }
        public double WetWeatherTPInflow { get; set; }
        public double WetWeatherFCInflow { get; set; }
        public double WetWeatherTCuInflow { get; set; }
        public double WetWeatherTPbInflow { get; set; }
        public double WetWeatherTZnInflow { get; set; }
        public double SummerDryWeatherInflow { get; set; }
        public double SummerDryWeatherTreated { get; set; }
        public double SummerDryWeatherRetained { get; set; }
        public double SummerDryWeatherUntreated { get; set; }
        public double SummerDryWeatherTSSReduced { get; set; }
        public double SummerDryWeatherTNReduced { get; set; }
        public double SummerDryWeatherTPReduced { get; set; }
        public double SummerDryWeatherFCReduced { get; set; }
        public double SummerDryWeatherTCuReduced { get; set; }
        public double SummerDryWeatherTPbReduced { get; set; }
        public double SummerDryWeatherTZnReduced { get; set; }
        public double SummerDryWeatherTSSInflow { get; set; }
        public double SummerDryWeatherTNInflow { get; set; }
        public double SummerDryWeatherTPInflow { get; set; }
        public double SummerDryWeatherFCInflow { get; set; }
        public double SummerDryWeatherTCuInflow { get; set; }
        public double SummerDryWeatherTPbInflow { get; set; }
        public double SummerDryWeatherTZnInflow { get; set; }
        public double WinterDryWeatherInflow { get; set; }
        public double WinterDryWeatherTreated { get; set; }
        public double WinterDryWeatherRetained { get; set; }
        public double WinterDryWeatherUntreated { get; set; }
        public double WinterDryWeatherTSSReduced { get; set; }
        public double WinterDryWeatherTNReduced { get; set; }
        public double WinterDryWeatherTPReduced { get; set; }
        public double WinterDryWeatherFCReduced { get; set; }
        public double WinterDryWeatherTCuReduced { get; set; }
        public double WinterDryWeatherTPbReduced { get; set; }
        public double WinterDryWeatherTZnReduced { get; set; }
        public double WinterDryWeatherTSSInflow { get; set; }
        public double WinterDryWeatherTNInflow { get; set; }
        public double WinterDryWeatherTPInflow { get; set; }
        public double WinterDryWeatherFCInflow { get; set; }
        public double WinterDryWeatherTCuInflow { get; set; }
        public double WinterDryWeatherTPbInflow { get; set; }
        public double WinterDryWeatherTZnInflow { get; set; }
        public double DryWeatherInflow { get; set;}
        public double DryWeatherTreated { get; set;}
        public double DryWeatherRetained { get; set;}
        public double DryWeatherUntreated { get; set;}
        public double DryWeatherTSSReduced { get; set;}
        public double DryWeatherTNReduced { get; set;}
        public double DryWeatherTPReduced { get; set;}
        public double DryWeatherFCReduced { get; set;}
        public double DryWeatherTCuReduced { get; set;}
        public double DryWeatherTPbReduced { get; set;}
        public double DryWeatherTZnReduced { get; set;}
        public double DryWeatherTSSInflow { get; set;}
        public double DryWeatherTNInflow { get; set;}
        public double DryWeatherTPInflow { get; set;}
        public double DryWeatherFCInflow { get; set;}
        public double DryWeatherTCuInflow { get; set;}
        public double DryWeatherTPbInflow { get; set;}
        public double DryWeatherTZnInflow { get; set;}

        public double TotalInflow { get; set;}
        public double TotalTreated { get; set;}
        public double TotalRetained { get; set;}
        public double TotalUntreated { get; set;}
        public double TotalTSSReduced { get; set;}
        public double TotalTNReduced { get; set;}
        public double TotalTPReduced { get; set;}
        public double TotalFCReduced { get; set;}
        public double TotalTCuReduced { get; set;}
        public double TotalTPbReduced { get; set;}
        public double TotalTZnReduced { get; set;}
        public double TotalTSSInflow { get; set;}
        public double TotalTNInflow { get; set;}
        public double TotalTPInflow { get; set;}
        public double TotalFCInflow { get; set;}
        public double TotalTCuInflow { get; set;}
        public double TotalTPbInflow { get; set;}
        public double TotalTZnInflow { get; set;}
    }

    public sealed class ProjectLoadRemovingResultMap : ClassMap<ProjectLoadReducingResultDto>
    {
        public ProjectLoadRemovingResultMap()
        {
            Map(m => m.TreatmentBMPName).Name("Treatment BMP Name");
            Map(m => m.ProjectID).Name("Project ID");
            Map(m => m.ProjectName).Name("Project Name");

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
}