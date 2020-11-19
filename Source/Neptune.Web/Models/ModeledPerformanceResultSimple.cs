using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Newtonsoft.Json.Linq;

namespace Neptune.Web.Models
{
    public class ModeledPerformanceResultSimple
    {
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
        public double DryWeatherInflow { get; set; }
        public double DryWeatherTreated { get; set; }
        public double DryWeatherRetained { get; set; }
        public double DryWeatherUntreated { get; set; }
        public double DryWeatherTSSRemoved { get; set; }
        public double DryWeatherTNRemoved { get; set; }
        public double DryWeatherTPRemoved { get; set; }
        public double DryWeatherFCRemoved { get; set; }
        public double DryWeatherTCuRemoved { get; set; }
        public double DryWeatherTPbRemoved { get; set; }
        public double DryWeatherTZnRemoved { get; set; }
        public double DryWeatherTSSInflow { get; set; }
        public double DryWeatherTNInflow { get; set; }
        public double DryWeatherTPInflow { get; set; }
        public double DryWeatherFCInflow { get; set; }
        public double DryWeatherTCuInflow { get; set; }
        public double DryWeatherTPbInflow { get; set; }
        public double DryWeatherTZnInflow { get; set; }
        public double TotalInflow { get; set; }
        public double TotalTreated { get; set; }
        public double TotalRetained { get; set; }
        public double TotalUntreated { get; set; }
        public double TotalTSSRemoved { get; set; }
        public double TotalTNRemoved { get; set; }
        public double TotalTPRemoved { get; set; }
        public double TotalFCRemoved { get; set; }
        public double TotalTCuRemoved { get; set; }
        public double TotalTPbRemoved { get; set; }
        public double TotalTZnRemoved { get; set; }
        public double TotalTSSInflow { get; set; }
        public double TotalTNInflow { get; set; }
        public double TotalTPInflow { get; set; }
        public double TotalFCInflow { get; set; }
        public double TotalTCuInflow { get; set; }
        public double TotalTPbInflow { get; set; }
        public double TotalTZnInflow { get; set; }

        public string LastUpdated { get; set; }

        public bool Outdated { get; set; }

        public ModeledPerformanceResultSimple(List<TreatmentBMP> treatmentBMP)
        {
            var nereidResults = HttpRequestStorage.DatabaseEntities.NereidResults.Where(x => x.TreatmentBMPID != null)
                .ToList().Where(x =>
                    treatmentBMP.Select(y => y.TreatmentBMPID).Contains(x.TreatmentBMPID.GetValueOrDefault())).ToList();
            var fullResults = nereidResults.Select(x=>JObject.Parse(x.FullResponse)).ToList();

            var nereidResultLastUpdate = nereidResults.Select(x=>x.LastUpdate).Max().Value;
            
            LastUpdated = $"{nereidResultLastUpdate.ToShortDateString()} {nereidResultLastUpdate.ToShortTimeString()}";

            var lastDeltaQueue = HttpRequestStorage.DatabaseEntities.DirtyModelNodes
                .Where(x => x.TreatmentBMPID != null).ToList().OrderByDescending(x => x.CreateDate)
                .FirstOrDefault(x =>
                    treatmentBMP.Select(y => y.TreatmentBMPID).Contains(x.TreatmentBMPID.GetValueOrDefault()))
                ?.CreateDate;

            Outdated = lastDeltaQueue != null && lastDeltaQueue.Value > nereidResultLastUpdate;

            WetWeatherInflow = fullResults.ExtractDoubleValue("runoff_volume_cuft_inflow");
            WetWeatherTreated = fullResults.ExtractDoubleValue("runoff_volume_cuft_treated");
            WetWeatherRetained = fullResults.ExtractDoubleValue("runoff_volume_cuft_retained");
            WetWeatherUntreated = (fullResults.ExtractDoubleValue("runoff_volume_cuft_bypassed"));
            WetWeatherTSSRemoved = (fullResults.ExtractDoubleValue("TSS_load_lbs_removed") * PoundsToKilogramsFactor);
            WetWeatherTNRemoved = (fullResults.ExtractDoubleValue("TN_load_lbs_removed") * PoundsToKilogramsFactor);
            WetWeatherTPRemoved = (fullResults.ExtractDoubleValue("TP_load_lbs_removed") * PoundsToKilogramsFactor);
            WetWeatherFCRemoved = (fullResults.ExtractDoubleValue("FC_load_mpn_removed") / 1e9);
            WetWeatherTCuRemoved = (fullResults.ExtractDoubleValue("TCu_load_lbs_removed") * PoundsToGramsFactor);
            WetWeatherTPbRemoved = (fullResults.ExtractDoubleValue("TPb_load_lbs_removed") * PoundsToGramsFactor);
            WetWeatherTZnRemoved = (fullResults.ExtractDoubleValue("TZn_load_lbs_removed") * PoundsToGramsFactor);
            WetWeatherTSSInflow = (fullResults.ExtractDoubleValue("TSS_load_lbs_inflow") * PoundsToKilogramsFactor);
            WetWeatherTNInflow = (fullResults.ExtractDoubleValue("TN_load_lbs_inflow") * PoundsToKilogramsFactor);
            WetWeatherTPInflow = (fullResults.ExtractDoubleValue("TP_load_lbs_inflow") * PoundsToKilogramsFactor);
            WetWeatherFCInflow = (fullResults.ExtractDoubleValue("FC_load_mpn_inflow") / 1e9);
            WetWeatherTCuInflow = (fullResults.ExtractDoubleValue("TCu_load_lbs_inflow") * PoundsToGramsFactor);
            WetWeatherTPbInflow = (fullResults.ExtractDoubleValue("TPb_load_lbs_inflow") * PoundsToGramsFactor);
            WetWeatherTZnInflow = (fullResults.ExtractDoubleValue("TZn_load_lbs_inflow") * PoundsToGramsFactor);

            SummerDryWeatherInflow = fullResults.ExtractDoubleValue("summer_dry_weather_flow_cuft_inflow");
            SummerDryWeatherTreated = fullResults.ExtractDoubleValue("summer_dry_weather_flow_cuft_treated");
            SummerDryWeatherRetained = fullResults.ExtractDoubleValue("summer_dry_weather_flow_cuft_retained");
            SummerDryWeatherUntreated = (fullResults.ExtractDoubleValue("summer_dry_weather_flow_cuft_bypassed"));
            SummerDryWeatherTSSRemoved = (fullResults.ExtractDoubleValue("summer_dwTSS_load_lbs_removed") * PoundsToKilogramsFactor);
            SummerDryWeatherTNRemoved = (fullResults.ExtractDoubleValue("summer_dwTN_load_lbs_removed") * PoundsToKilogramsFactor);
            SummerDryWeatherTPRemoved = (fullResults.ExtractDoubleValue("summer_dwTP_load_lbs_removed") * PoundsToKilogramsFactor);
            SummerDryWeatherFCRemoved = (fullResults.ExtractDoubleValue("summer_dwFC_load_mpn_removed") / 1e9);
            SummerDryWeatherTCuRemoved = (fullResults.ExtractDoubleValue("summer_dwTCu_load_lbs_removed") * PoundsToGramsFactor);
            SummerDryWeatherTPbRemoved = (fullResults.ExtractDoubleValue("summer_dwTPb_load_lbs_removed") * PoundsToGramsFactor);
            SummerDryWeatherTZnRemoved = (fullResults.ExtractDoubleValue("summer_dwTZn_load_lbs_removed") * PoundsToGramsFactor);
            SummerDryWeatherTSSInflow = (fullResults.ExtractDoubleValue("summer_dwTSS_load_lbs_inflow") * PoundsToKilogramsFactor);
            SummerDryWeatherTNInflow = (fullResults.ExtractDoubleValue("summer_dwTN_load_lbs_inflow") * PoundsToKilogramsFactor);
            SummerDryWeatherTPInflow = (fullResults.ExtractDoubleValue("summer_dwTP_load_lbs_inflow") * PoundsToKilogramsFactor);
            SummerDryWeatherFCInflow = (fullResults.ExtractDoubleValue("summer_dwFC_load_mpn_inflow") / 1e9);
            SummerDryWeatherTCuInflow = (fullResults.ExtractDoubleValue("summer_dwTCu_load_lbs_inflow") * PoundsToGramsFactor);
            SummerDryWeatherTPbInflow = (fullResults.ExtractDoubleValue("summer_dwTPb_load_lbs_inflow") * PoundsToGramsFactor);
            SummerDryWeatherTZnInflow = (fullResults.ExtractDoubleValue("summer_dwTZn_load_lbs_inflow") * PoundsToGramsFactor);

            WinterDryWeatherInflow = fullResults.ExtractDoubleValue("winter_dry_weather_flow_cuft_inflow");
            WinterDryWeatherTreated = fullResults.ExtractDoubleValue("winter_dry_weather_flow_cuft_treated");
            WinterDryWeatherRetained = fullResults.ExtractDoubleValue("winter_dry_weather_flow_cuft_retained");
            WinterDryWeatherUntreated = (fullResults.ExtractDoubleValue("winter_dry_weather_flow_cuft_bypassed"));
            WinterDryWeatherTSSRemoved = (fullResults.ExtractDoubleValue("winter_dwTSS_load_lbs_removed") * PoundsToKilogramsFactor);
            WinterDryWeatherTNRemoved = (fullResults.ExtractDoubleValue("winter_dwTN_load_lbs_removed") * PoundsToKilogramsFactor);
            WinterDryWeatherTPRemoved = (fullResults.ExtractDoubleValue("winter_dwTP_load_lbs_removed") * PoundsToKilogramsFactor);
            WinterDryWeatherFCRemoved = (fullResults.ExtractDoubleValue("winter_dwFC_load_mpn_removed") / 1e9);
            WinterDryWeatherTCuRemoved = (fullResults.ExtractDoubleValue("winter_dwTCu_load_lbs_removed") * PoundsToGramsFactor);
            WinterDryWeatherTPbRemoved = (fullResults.ExtractDoubleValue("winter_dwTPb_load_lbs_removed") * PoundsToGramsFactor);
            WinterDryWeatherTZnRemoved = (fullResults.ExtractDoubleValue("winter_dwTZn_load_lbs_removed") * PoundsToGramsFactor);
            WinterDryWeatherTSSInflow = (fullResults.ExtractDoubleValue("winter_dwTSS_load_lbs_inflow") * PoundsToKilogramsFactor);
            WinterDryWeatherTNInflow = (fullResults.ExtractDoubleValue("winter_dwTN_load_lbs_inflow") * PoundsToKilogramsFactor);
            WinterDryWeatherTPInflow = (fullResults.ExtractDoubleValue("winter_dwTP_load_lbs_inflow") * PoundsToKilogramsFactor);
            WinterDryWeatherFCInflow = (fullResults.ExtractDoubleValue("winter_dwFC_load_mpn_inflow") / 1e9);
            WinterDryWeatherTCuInflow = (fullResults.ExtractDoubleValue("winter_dwTCu_load_lbs_inflow") * PoundsToGramsFactor);
            WinterDryWeatherTPbInflow = (fullResults.ExtractDoubleValue("winter_dwTPb_load_lbs_inflow") * PoundsToGramsFactor);
            WinterDryWeatherTZnInflow = (fullResults.ExtractDoubleValue("winter_dwTZn_load_lbs_inflow") * PoundsToGramsFactor);

            DryWeatherInflow = SummerDryWeatherInflow + WinterDryWeatherInflow;
            DryWeatherTreated = SummerDryWeatherTreated + WinterDryWeatherTreated;
            DryWeatherRetained = SummerDryWeatherRetained + WinterDryWeatherRetained;
            DryWeatherUntreated = SummerDryWeatherUntreated + WinterDryWeatherUntreated;
            DryWeatherTSSRemoved = SummerDryWeatherTSSRemoved + WinterDryWeatherTSSRemoved;
            DryWeatherTNRemoved = SummerDryWeatherTNRemoved + WinterDryWeatherTNRemoved;
            DryWeatherTPRemoved = SummerDryWeatherTPRemoved + WinterDryWeatherTPRemoved;
            DryWeatherFCRemoved = SummerDryWeatherFCRemoved + WinterDryWeatherFCRemoved;
            DryWeatherTCuRemoved = SummerDryWeatherTCuRemoved + WinterDryWeatherTCuRemoved;
            DryWeatherTPbRemoved = SummerDryWeatherTPbRemoved + WinterDryWeatherTPbRemoved;
            DryWeatherTZnRemoved = SummerDryWeatherTZnRemoved + WinterDryWeatherTZnRemoved;
            DryWeatherTSSInflow = SummerDryWeatherTSSInflow + WinterDryWeatherTSSInflow;
            DryWeatherTNInflow = SummerDryWeatherTNInflow + WinterDryWeatherTNInflow;
            DryWeatherTPInflow = SummerDryWeatherTPInflow + WinterDryWeatherTPInflow;
            DryWeatherFCInflow = SummerDryWeatherFCInflow + WinterDryWeatherFCInflow;
            DryWeatherTCuInflow = SummerDryWeatherTCuInflow + WinterDryWeatherTCuInflow;
            DryWeatherTPbInflow = SummerDryWeatherTPbInflow + WinterDryWeatherTPbInflow;
            DryWeatherTZnInflow = SummerDryWeatherTZnInflow + WinterDryWeatherTZnInflow;

            TotalInflow = DryWeatherInflow + WetWeatherInflow;
            TotalTreated = DryWeatherTreated + WetWeatherTreated;
            TotalRetained = DryWeatherRetained + WetWeatherRetained;
            TotalUntreated = DryWeatherUntreated + WetWeatherUntreated;
            TotalTSSRemoved = DryWeatherTSSRemoved + WetWeatherTSSRemoved;
            TotalTNRemoved = DryWeatherTNRemoved + WetWeatherTNRemoved;
            TotalTPRemoved = DryWeatherTPRemoved + WetWeatherTPRemoved;
            TotalFCRemoved = DryWeatherFCRemoved + WetWeatherFCRemoved;
            TotalTCuRemoved = DryWeatherTCuRemoved + WetWeatherTCuRemoved;
            TotalTPbRemoved = DryWeatherTPbRemoved + WetWeatherTPbRemoved;
            TotalTZnRemoved = DryWeatherTZnRemoved + WetWeatherTZnRemoved;
            TotalTSSInflow = DryWeatherTSSInflow + WetWeatherTSSInflow;
            TotalTNInflow = DryWeatherTNInflow + WetWeatherTNInflow;
            TotalTPInflow = DryWeatherTPInflow + WetWeatherTPInflow;
            TotalFCInflow = DryWeatherFCInflow + WetWeatherFCInflow;
            TotalTCuInflow = DryWeatherTCuInflow + WetWeatherTCuInflow;
            TotalTPbInflow = DryWeatherTPbInflow + WetWeatherTPbInflow;
            TotalTZnInflow = DryWeatherTZnInflow + WetWeatherTZnInflow;
        }

        public ModeledPerformanceResultSimple(WaterQualityManagementPlan waterQualityManagementPlan)
        {
            // todo
        }

        private const double PoundsToKilogramsFactor = 0.453592;
        private const double PoundsToGramsFactor = 453.592;
    }
}