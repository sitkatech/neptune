using System;
using System.Linq;
using Neptune.Web.Common;
using Newtonsoft.Json.Linq;

namespace Neptune.Web.Models
{
    public class TreatmentBMPSimple
    {
        public int TreatmentBMPID { get; set; }
        public string DisplayName { get; set; }
        public string TreatmentBMPTypeName { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public TreatmentBMPSimple()
        {
        }

        public TreatmentBMPSimple(TreatmentBMP treatmentBMP)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            DisplayName = treatmentBMP.TreatmentBMPName;
            TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName;
        }

        public TreatmentBMPSimple(int treatmentBMPID, string displayName, string treatmentBMPTypeName)
        {
            TreatmentBMPID = treatmentBMPID;
            DisplayName = displayName;
            TreatmentBMPTypeName = treatmentBMPTypeName;
        }
    }

    public class TreatmentBMPModelResultSimple
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
        
        public TreatmentBMPModelResultSimple(TreatmentBMP treatmentBMP)
        {
            var nereidResult = HttpRequestStorage.DatabaseEntities.NereidResults.Single(x=>x.TreatmentBMPID == treatmentBMP.TreatmentBMPID);
            var fullResults = JObject.Parse(nereidResult.FullResponse);

            LastUpdated = $"{nereidResult.LastUpdate.Value.ToShortDateString()} {nereidResult.LastUpdate.Value.ToShortTimeString()}";

            WetWeatherInflow = fullResults["runoff_volume_cuft_inflow"].Value<double>().RoundToSignificantDigits(3);
            WetWeatherTreated = fullResults["runoff_volume_cuft_treated"].Value<double>().RoundToSignificantDigits(3);
            WetWeatherRetained = fullResults["runoff_volume_cuft_retained"].Value<double>().RoundToSignificantDigits(3);
            WetWeatherUntreated = (fullResults["runoff_volume_cuft_bypassed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(3);
            WetWeatherTSSRemoved = (fullResults["TSS_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherTNRemoved = (fullResults["TN_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherTPRemoved = (fullResults["TP_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherFCRemoved = (fullResults["FC_load_mpn_removed"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            WetWeatherTCuRemoved = (fullResults["TCu_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WetWeatherTPbRemoved = (fullResults["TPb_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WetWeatherTZnRemoved = (fullResults["TZn_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WetWeatherTSSInflow = (fullResults["TSS_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherTNInflow = (fullResults["TN_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherTPInflow = (fullResults["TP_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherFCInflow = (fullResults["FC_load_mpn_inflow"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            WetWeatherTCuInflow = (fullResults["TCu_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WetWeatherTPbInflow = (fullResults["TPb_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WetWeatherTZnInflow = (fullResults["TZn_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);

            SummerDryWeatherInflow = fullResults["summer_dry_weather_flow_cuft_inflow"].Value<double>().RoundToSignificantDigits(3);
            SummerDryWeatherTreated = fullResults["summer_dry_weather_flow_cuft_treated"].Value<double>().RoundToSignificantDigits(3);
            SummerDryWeatherRetained = fullResults["summer_dry_weather_flow_cuft_retained"].Value<double>().RoundToSignificantDigits(3);
            SummerDryWeatherUntreated = (fullResults["summer_dry_weather_flow_cuft_bypassed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(3);
            SummerDryWeatherTSSRemoved = (fullResults["summer_dwTSS_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTNRemoved = (fullResults["summer_dwTN_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTPRemoved = (fullResults["summer_dwTP_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherFCRemoved = (fullResults["summer_dwFC_load_mpn_removed"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            SummerDryWeatherTCuRemoved = (fullResults["summer_dwTCu_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTPbRemoved = (fullResults["summer_dwTPb_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTZnRemoved = (fullResults["summer_dwTZn_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTSSInflow = (fullResults["summer_dwTSS_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTNInflow = (fullResults["summer_dwTN_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTPInflow = (fullResults["summer_dwTP_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherFCInflow = (fullResults["summer_dwFC_load_mpn_inflow"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            SummerDryWeatherTCuInflow = (fullResults["summer_dwTCu_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTPbInflow = (fullResults["summer_dwTPb_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTZnInflow = (fullResults["summer_dwTZn_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);

            WinterDryWeatherInflow = fullResults["winter_dry_weather_flow_cuft_inflow"].Value<double>().RoundToSignificantDigits(3);
            WinterDryWeatherTreated = fullResults["winter_dry_weather_flow_cuft_treated"].Value<double>().RoundToSignificantDigits(3);
            WinterDryWeatherRetained = fullResults["winter_dry_weather_flow_cuft_retained"].Value<double>().RoundToSignificantDigits(3);
            WinterDryWeatherUntreated = (fullResults["winter_dry_weather_flow_cuft_bypassed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(3);
            WinterDryWeatherTSSRemoved = (fullResults["winter_dwTSS_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTNRemoved = (fullResults["winter_dwTN_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTPRemoved = (fullResults["winter_dwTP_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherFCRemoved = (fullResults["winter_dwFC_load_mpn_removed"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            WinterDryWeatherTCuRemoved = (fullResults["winter_dwTCu_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTPbRemoved = (fullResults["winter_dwTPb_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTZnRemoved = (fullResults["winter_dwTZn_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTSSInflow = (fullResults["winter_dwTSS_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTNInflow = (fullResults["winter_dwTN_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTPInflow = (fullResults["winter_dwTP_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherFCInflow = (fullResults["winter_dwFC_load_mpn_inflow"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            WinterDryWeatherTCuInflow = (fullResults["winter_dwTCu_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTPbInflow = (fullResults["winter_dwTPb_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTZnInflow = (fullResults["winter_dwTZn_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);

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
        private const double PoundsToKilogramsFactor = 0.453592;
        private const double PoundsToGramsFactor = 453.592;
    }

    public static class SigFigHelper
    {


        public static double RoundToSignificantDigits(this double d, int digits)
        {
            if (d == 0)
                return 0;

            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            return (double)((decimal) scale * (decimal) Math.Round(d / scale, digits));
        }
    }
}