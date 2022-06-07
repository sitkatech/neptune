using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration;

namespace Hippocamp.Models.DataTransferObjects
{
    public class TreatmentBMPModeledResultSimpleDto
    {
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public double WetWeatherInflow { get; set; }
        public double WetWeatherTreated { get; set; }
        public double WetWeatherRetained { get; set; }
        public double WetWeatherUntreated { get; set; }
        public double WetWeatherTSSRemoved { get; set; }
        public double WetWeatherTNRemoved { get; set; }
        public double WetWeatherTPRemoved { get; set; }
        public double WetWeatherFCRemoved { get; set; }
        public double WetWeatherTCuRemoved { get; set; }
        public double WetWeatherTPbRemoved { get; set; }
        public double WetWeatherTZnRemoved { get; set; }
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
        public double SummerDryWeatherTSSRemoved { get; set; }
        public double SummerDryWeatherTNRemoved { get; set; }
        public double SummerDryWeatherTPRemoved { get; set; }
        public double SummerDryWeatherFCRemoved { get; set; }
        public double SummerDryWeatherTCuRemoved { get; set; }
        public double SummerDryWeatherTPbRemoved { get; set; }
        public double SummerDryWeatherTZnRemoved { get; set; }
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
        public double WinterDryWeatherTSSRemoved { get; set; }
        public double WinterDryWeatherTNRemoved { get; set; }
        public double WinterDryWeatherTPRemoved { get; set; }
        public double WinterDryWeatherFCRemoved { get; set; }
        public double WinterDryWeatherTCuRemoved { get; set; }
        public double WinterDryWeatherTPbRemoved { get; set; }
        public double WinterDryWeatherTZnRemoved { get; set; }
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
        public double DryWeatherTSSRemoved { get; set;}
        public double DryWeatherTNRemoved { get; set;}
        public double DryWeatherTPRemoved { get; set;}
        public double DryWeatherFCRemoved { get; set;}
        public double DryWeatherTCuRemoved { get; set;}
        public double DryWeatherTPbRemoved { get; set;}
        public double DryWeatherTZnRemoved { get; set;}
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
        public double TotalTSSRemoved { get; set;}
        public double TotalTNRemoved { get; set;}
        public double TotalTPRemoved { get; set;}
        public double TotalFCRemoved { get; set;}
        public double TotalTCuRemoved { get; set;}
        public double TotalTPbRemoved { get; set;}
        public double TotalTZnRemoved { get; set;}
        public double TotalTSSInflow { get; set;}
        public double TotalTNInflow { get; set;}
        public double TotalTPInflow { get; set;}
        public double TotalFCInflow { get; set;}
        public double TotalTCuInflow { get; set;}
        public double TotalTPbInflow { get; set;}
        public double TotalTZnInflow { get; set;}
    }

    public sealed class TreatmentBMPModeledResultMap : ClassMap<TreatmentBMPModeledResultSimpleDto>
    {
        public TreatmentBMPModeledResultMap()
        {
            Map(m => m.TreatmentBMPName).Name("Treatment BMP Name");
            Map(m => m.ProjectID).Name("Project ID");
            Map(m => m.ProjectName).Name("Project Name");

            Map(m => m.WetWeatherInflow).Name("WetWeatherInflow (cu-ft/yr)");
            Map(m => m.WetWeatherTreated).Name("WetWeatherTreated (cu-ft/yr)");
            Map(m => m.WetWeatherRetained).Name("WetWeatherRetained (cu-ft/yr)");
            Map(m => m.WetWeatherUntreated).Name("WetWeatherUntreated (cu-ft/yr)");
            Map(m => m.WetWeatherTSSRemoved).Name("WetWeatherTSSRemoved (kg)");
            Map(m => m.WetWeatherTNRemoved).Name("WetWeatherTNRemoved (kg)");
            Map(m => m.WetWeatherTPRemoved).Name("WetWeatherTPRemoved (kg)");
            Map(m => m.WetWeatherFCRemoved).Name("WetWeatherFCRemoved (billion CFUs)");
            Map(m => m.WetWeatherTCuRemoved).Name("WetWeatherTCuRemoved (g)");
            Map(m => m.WetWeatherTPbRemoved).Name("WetWeatherTPbRemoved (g)");
            Map(m => m.WetWeatherTZnRemoved).Name("WetWeatherTZnRemoved (g)");
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
            Map(m => m.DryWeatherTSSRemoved).Name("DryWeatherTSSRemoved (kg)");
            Map(m => m.DryWeatherTNRemoved).Name("DryWeatherTNRemoved (kg)");
            Map(m => m.DryWeatherTPRemoved).Name("DryWeatherTPRemoved (kg)");
            Map(m => m.DryWeatherFCRemoved).Name("DryWeatherFCRemoved (billion CFUs)");
            Map(m => m.DryWeatherTCuRemoved).Name("DryWeatherTCuRemoved (g)");
            Map(m => m.DryWeatherTPbRemoved).Name("DryWeatherTPbRemoved (g)");
            Map(m => m.DryWeatherTZnRemoved).Name("DryWeatherTZnRemoved (g)");
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
            Map(m => m.TotalTSSRemoved).Name("TotalTSSRemoved (kg)");
            Map(m => m.TotalTNRemoved).Name("TotalTNRemoved (kg)");
            Map(m => m.TotalTPRemoved).Name("TotalTPRemoved (kg)");
            Map(m => m.TotalFCRemoved).Name("TotalFCRemoved (billion CFUs)");
            Map(m => m.TotalTCuRemoved).Name("TotalTCuRemoved (g)");
            Map(m => m.TotalTPbRemoved).Name("TotalTPbRemoved (g)");
            Map(m => m.TotalTZnRemoved).Name("TotalTZnRemoved (g)");
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