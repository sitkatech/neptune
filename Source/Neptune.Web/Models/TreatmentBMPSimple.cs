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

        public TreatmentBMPModelResultSimple(TreatmentBMP treatmentBMP)
        {
            var nereidResult = JObject.Parse(HttpRequestStorage.DatabaseEntities.NereidResults.Single(x=>x.TreatmentBMPID == treatmentBMP.TreatmentBMPID).FullResponse);

            WetWeatherInflow = nereidResult["runoff_volume_cuft_inflow"].Value<double>().RoundToSignificantDigits(3);
            WetWeatherTreated = nereidResult["runoff_volume_cuft_treated"].Value<double>().RoundToSignificantDigits(3);
            WetWeatherRetained = nereidResult["runoff_volume_cuft_retained"].Value<double>().RoundToSignificantDigits(3);
            WetWeatherUntreated = (nereidResult["runoff_volume_cuft_bypassed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(3);
            WetWeatherTSSRemoved = (nereidResult["TSS_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherTNRemoved = (nereidResult["TN_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherTPRemoved = (nereidResult["TP_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherFCRemoved = (nereidResult["FC_load_mpn_removed"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            WetWeatherTCuRemoved = (nereidResult["TCu_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WetWeatherTPbRemoved = (nereidResult["TPb_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WetWeatherTZnRemoved = (nereidResult["TZn_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WetWeatherTSSInflow = (nereidResult["TSS_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherTNInflow = (nereidResult["TN_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherTPInflow = (nereidResult["TP_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WetWeatherFCInflow = (nereidResult["FC_load_mpn_inflow"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            WetWeatherTCuInflow = (nereidResult["TCu_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WetWeatherTPbInflow = (nereidResult["TPb_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WetWeatherTZnInflow = (nereidResult["TZn_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);

            SummerDryWeatherInflow = nereidResult["summer_dry_weather_flow_cuft_inflow"].Value<double>().RoundToSignificantDigits(3);
            SummerDryWeatherTreated = nereidResult["summer_dry_weather_flow_cuft_treated"].Value<double>().RoundToSignificantDigits(3);
            SummerDryWeatherRetained = nereidResult["summer_dry_weather_flow_cuft_retained"].Value<double>().RoundToSignificantDigits(3);
            SummerDryWeatherUntreated = (nereidResult["summer_dry_weather_flow_cuft_bypassed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(3);
            SummerDryWeatherTSSRemoved = (nereidResult["summer_dwTSS_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTNRemoved = (nereidResult["summer_dwTN_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTPRemoved = (nereidResult["summer_dwTP_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherFCRemoved = (nereidResult["summer_dwFC_load_mpn_removed"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            SummerDryWeatherTCuRemoved = (nereidResult["summer_dwTCu_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTPbRemoved = (nereidResult["summer_dwTPb_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTZnRemoved = (nereidResult["summer_dwTZn_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTSSInflow = (nereidResult["summer_dwTSS_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTNInflow = (nereidResult["summer_dwTN_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTPInflow = (nereidResult["summer_dwTP_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherFCInflow = (nereidResult["summer_dwFC_load_mpn_inflow"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            SummerDryWeatherTCuInflow = (nereidResult["summer_dwTCu_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTPbInflow = (nereidResult["summer_dwTPb_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            SummerDryWeatherTZnInflow = (nereidResult["summer_dwTZn_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);

            WinterDryWeatherInflow = nereidResult["winter_dry_weather_flow_cuft_inflow"].Value<double>().RoundToSignificantDigits(3);
            WinterDryWeatherTreated = nereidResult["winter_dry_weather_flow_cuft_treated"].Value<double>().RoundToSignificantDigits(3);
            WinterDryWeatherRetained = nereidResult["winter_dry_weather_flow_cuft_retained"].Value<double>().RoundToSignificantDigits(3);
            WinterDryWeatherUntreated = (nereidResult["winter_dry_weather_flow_cuft_bypassed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(3);
            WinterDryWeatherTSSRemoved = (nereidResult["winter_dwTSS_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTNRemoved = (nereidResult["winter_dwTN_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTPRemoved = (nereidResult["winter_dwTP_load_lbs_removed"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherFCRemoved = (nereidResult["winter_dwFC_load_mpn_removed"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            WinterDryWeatherTCuRemoved = (nereidResult["winter_dwTCu_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTPbRemoved = (nereidResult["winter_dwTPb_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTZnRemoved = (nereidResult["winter_dwTZn_load_lbs_removed"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTSSInflow = (nereidResult["winter_dwTSS_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTNInflow = (nereidResult["winter_dwTN_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTPInflow = (nereidResult["winter_dwTP_load_lbs_inflow"].Value<double>() * PoundsToKilogramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherFCInflow = (nereidResult["winter_dwFC_load_mpn_inflow"].Value<double>() / 1e9).RoundToSignificantDigits(2);
            WinterDryWeatherTCuInflow = (nereidResult["winter_dwTCu_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTPbInflow = (nereidResult["winter_dwTPb_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);
            WinterDryWeatherTZnInflow = (nereidResult["winter_dwTZn_load_lbs_inflow"].Value<double>() * PoundsToGramsFactor).RoundToSignificantDigits(2);

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