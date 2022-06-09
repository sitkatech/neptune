using Hippocamp.Models.DataTransferObjects;
using Newtonsoft.Json.Linq;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ProjectNereidResultExtensionMethods
    {
        public static TreatmentBMPModeledResultSimpleDto AsTreatmentBMPModeledResultSimpleDto(this ProjectNereidResult result)
        {
            var parsedResults = JObject.Parse(result.FullResponse);
            var toReturn = new TreatmentBMPModeledResultSimpleDto()
            {
                TreatmentBMPID = result.TreatmentBMPID.Value,
                ProjectID = result.ProjectID,
                ProjectName = result.Project.ProjectName,

                WetWeatherInflow = parsedResults.ExtractDoubleValue("runoff_volume_cuft_inflow"),
                WetWeatherTreated = parsedResults.ExtractDoubleValue("runoff_volume_cuft_treated"),
                WetWeatherRetained = parsedResults.ExtractDoubleValue("runoff_volume_cuft_retained"),
                WetWeatherUntreated = (parsedResults.ExtractDoubleValue("runoff_volume_cuft_bypassed")),
                WetWeatherTSSRemoved = (parsedResults.ExtractDoubleValue("TSS_load_lbs_removed") * PoundsToKilogramsFactor),
                WetWeatherTNRemoved = (parsedResults.ExtractDoubleValue("TN_load_lbs_removed") * PoundsToKilogramsFactor),
                WetWeatherTPRemoved = (parsedResults.ExtractDoubleValue("TP_load_lbs_removed") * PoundsToKilogramsFactor),
                // todo: what is 1e9?????????????????????????
                WetWeatherFCRemoved = (parsedResults.ExtractDoubleValue("FC_load_mpn_removed") / 1e9),
                WetWeatherTCuRemoved = (parsedResults.ExtractDoubleValue("TCu_load_lbs_removed") * PoundsToGramsFactor),
                WetWeatherTPbRemoved = (parsedResults.ExtractDoubleValue("TPb_load_lbs_removed") * PoundsToGramsFactor),
                WetWeatherTZnRemoved = (parsedResults.ExtractDoubleValue("TZn_load_lbs_removed") * PoundsToGramsFactor),
                WetWeatherTSSInflow = (parsedResults.ExtractDoubleValue("TSS_load_lbs_inflow") * PoundsToKilogramsFactor),
                WetWeatherTNInflow = (parsedResults.ExtractDoubleValue("TN_load_lbs_inflow") * PoundsToKilogramsFactor),
                WetWeatherTPInflow = (parsedResults.ExtractDoubleValue("TP_load_lbs_inflow") * PoundsToKilogramsFactor),
                WetWeatherFCInflow = (parsedResults.ExtractDoubleValue("FC_load_mpn_inflow") / 1e9),
                WetWeatherTCuInflow = (parsedResults.ExtractDoubleValue("TCu_load_lbs_inflow") * PoundsToGramsFactor),
                WetWeatherTPbInflow = (parsedResults.ExtractDoubleValue("TPb_load_lbs_inflow") * PoundsToGramsFactor),
                WetWeatherTZnInflow = (parsedResults.ExtractDoubleValue("TZn_load_lbs_inflow") * PoundsToGramsFactor),

                SummerDryWeatherInflow = parsedResults.ExtractDoubleValue("summer_dry_weather_flow_cuft_inflow"),
                SummerDryWeatherTreated = parsedResults.ExtractDoubleValue("summer_dry_weather_flow_cuft_treated"),
                SummerDryWeatherRetained = parsedResults.ExtractDoubleValue("summer_dry_weather_flow_cuft_retained"),
                SummerDryWeatherUntreated = (parsedResults.ExtractDoubleValue("summer_dry_weather_flow_cuft_bypassed")),
                SummerDryWeatherTSSRemoved = (parsedResults.ExtractDoubleValue("summer_dwTSS_load_lbs_removed") * PoundsToKilogramsFactor),
                SummerDryWeatherTNRemoved = (parsedResults.ExtractDoubleValue("summer_dwTN_load_lbs_removed") * PoundsToKilogramsFactor),
                SummerDryWeatherTPRemoved = (parsedResults.ExtractDoubleValue("summer_dwTP_load_lbs_removed") * PoundsToKilogramsFactor),
                SummerDryWeatherFCRemoved = (parsedResults.ExtractDoubleValue("summer_dwFC_load_mpn_removed") / 1e9),
                SummerDryWeatherTCuRemoved = (parsedResults.ExtractDoubleValue("summer_dwTCu_load_lbs_removed") * PoundsToGramsFactor),
                SummerDryWeatherTPbRemoved = (parsedResults.ExtractDoubleValue("summer_dwTPb_load_lbs_removed") * PoundsToGramsFactor),
                SummerDryWeatherTZnRemoved = (parsedResults.ExtractDoubleValue("summer_dwTZn_load_lbs_removed") * PoundsToGramsFactor),
                SummerDryWeatherTSSInflow = (parsedResults.ExtractDoubleValue("summer_dwTSS_load_lbs_inflow") * PoundsToKilogramsFactor),
                SummerDryWeatherTNInflow = (parsedResults.ExtractDoubleValue("summer_dwTN_load_lbs_inflow") * PoundsToKilogramsFactor),
                SummerDryWeatherTPInflow = (parsedResults.ExtractDoubleValue("summer_dwTP_load_lbs_inflow") * PoundsToKilogramsFactor),
                SummerDryWeatherFCInflow = (parsedResults.ExtractDoubleValue("summer_dwFC_load_mpn_inflow") / 1e9),
                SummerDryWeatherTCuInflow = (parsedResults.ExtractDoubleValue("summer_dwTCu_load_lbs_inflow") * PoundsToGramsFactor),
                SummerDryWeatherTPbInflow = (parsedResults.ExtractDoubleValue("summer_dwTPb_load_lbs_inflow") * PoundsToGramsFactor),
                SummerDryWeatherTZnInflow = (parsedResults.ExtractDoubleValue("summer_dwTZn_load_lbs_inflow") * PoundsToGramsFactor),

                WinterDryWeatherInflow = parsedResults.ExtractDoubleValue("winter_dry_weather_flow_cuft_inflow"),
                WinterDryWeatherTreated = parsedResults.ExtractDoubleValue("winter_dry_weather_flow_cuft_treated"),
                WinterDryWeatherRetained = parsedResults.ExtractDoubleValue("winter_dry_weather_flow_cuft_retained"),
                WinterDryWeatherUntreated = (parsedResults.ExtractDoubleValue("winter_dry_weather_flow_cuft_bypassed")),
                WinterDryWeatherTSSRemoved = (parsedResults.ExtractDoubleValue("winter_dwTSS_load_lbs_removed") * PoundsToKilogramsFactor),
                WinterDryWeatherTNRemoved = (parsedResults.ExtractDoubleValue("winter_dwTN_load_lbs_removed") * PoundsToKilogramsFactor),
                WinterDryWeatherTPRemoved = (parsedResults.ExtractDoubleValue("winter_dwTP_load_lbs_removed") * PoundsToKilogramsFactor),
                WinterDryWeatherFCRemoved = (parsedResults.ExtractDoubleValue("winter_dwFC_load_mpn_removed") / 1e9),
                WinterDryWeatherTCuRemoved = (parsedResults.ExtractDoubleValue("winter_dwTCu_load_lbs_removed") * PoundsToGramsFactor),
                WinterDryWeatherTPbRemoved = (parsedResults.ExtractDoubleValue("winter_dwTPb_load_lbs_removed") * PoundsToGramsFactor),
                WinterDryWeatherTZnRemoved = (parsedResults.ExtractDoubleValue("winter_dwTZn_load_lbs_removed") * PoundsToGramsFactor),
                WinterDryWeatherTSSInflow = (parsedResults.ExtractDoubleValue("winter_dwTSS_load_lbs_inflow") * PoundsToKilogramsFactor),
                WinterDryWeatherTNInflow = (parsedResults.ExtractDoubleValue("winter_dwTN_load_lbs_inflow") * PoundsToKilogramsFactor),
                WinterDryWeatherTPInflow = (parsedResults.ExtractDoubleValue("winter_dwTP_load_lbs_inflow") * PoundsToKilogramsFactor),
                WinterDryWeatherFCInflow = (parsedResults.ExtractDoubleValue("winter_dwFC_load_mpn_inflow") / 1e9),
                WinterDryWeatherTCuInflow = (parsedResults.ExtractDoubleValue("winter_dwTCu_load_lbs_inflow") * PoundsToGramsFactor),
                WinterDryWeatherTPbInflow = (parsedResults.ExtractDoubleValue("winter_dwTPb_load_lbs_inflow") * PoundsToGramsFactor),
                WinterDryWeatherTZnInflow = (parsedResults.ExtractDoubleValue("winter_dwTZn_load_lbs_inflow") * PoundsToGramsFactor)
            };

            toReturn.DryWeatherInflow = toReturn.SummerDryWeatherInflow + toReturn.WinterDryWeatherInflow;
            toReturn.DryWeatherTreated = toReturn.SummerDryWeatherTreated + toReturn.WinterDryWeatherTreated;
            toReturn.DryWeatherRetained = toReturn.SummerDryWeatherRetained + toReturn.WinterDryWeatherRetained;
            toReturn.DryWeatherUntreated = toReturn.SummerDryWeatherUntreated + toReturn.WinterDryWeatherUntreated;
            toReturn.DryWeatherTSSRemoved = toReturn.SummerDryWeatherTSSRemoved + toReturn.WinterDryWeatherTSSRemoved;
            toReturn.DryWeatherTNRemoved = toReturn.SummerDryWeatherTNRemoved + toReturn.WinterDryWeatherTNRemoved;
            toReturn.DryWeatherTPRemoved = toReturn.SummerDryWeatherTPRemoved + toReturn.WinterDryWeatherTPRemoved;
            toReturn.DryWeatherFCRemoved = toReturn.SummerDryWeatherFCRemoved + toReturn.WinterDryWeatherFCRemoved;
            toReturn.DryWeatherTCuRemoved = toReturn.SummerDryWeatherTCuRemoved + toReturn.WinterDryWeatherTCuRemoved;
            toReturn.DryWeatherTPbRemoved = toReturn.SummerDryWeatherTPbRemoved + toReturn.WinterDryWeatherTPbRemoved;
            toReturn.DryWeatherTZnRemoved = toReturn.SummerDryWeatherTZnRemoved + toReturn.WinterDryWeatherTZnRemoved;
            toReturn.DryWeatherTSSInflow = toReturn.SummerDryWeatherTSSInflow + toReturn.WinterDryWeatherTSSInflow;
            toReturn.DryWeatherTNInflow = toReturn.SummerDryWeatherTNInflow + toReturn.WinterDryWeatherTNInflow;
            toReturn.DryWeatherTPInflow = toReturn.SummerDryWeatherTPInflow + toReturn.WinterDryWeatherTPInflow;
            toReturn.DryWeatherFCInflow = toReturn.SummerDryWeatherFCInflow + toReturn.WinterDryWeatherFCInflow;
            toReturn.DryWeatherTCuInflow = toReturn.SummerDryWeatherTCuInflow + toReturn.WinterDryWeatherTCuInflow;
            toReturn.DryWeatherTPbInflow = toReturn.SummerDryWeatherTPbInflow + toReturn.WinterDryWeatherTPbInflow;
            toReturn.DryWeatherTZnInflow = toReturn.SummerDryWeatherTZnInflow + toReturn.WinterDryWeatherTZnInflow;

            toReturn.TotalInflow = toReturn.DryWeatherInflow + toReturn.WetWeatherInflow;
            toReturn.TotalTreated = toReturn.DryWeatherTreated + toReturn.WetWeatherTreated;
            toReturn.TotalRetained = toReturn.DryWeatherRetained + toReturn.WetWeatherRetained;
            toReturn.TotalUntreated = toReturn.DryWeatherUntreated + toReturn.WetWeatherUntreated;
            toReturn.TotalTSSRemoved = toReturn.DryWeatherTSSRemoved + toReturn.WetWeatherTSSRemoved;
            toReturn.TotalTNRemoved = toReturn.DryWeatherTNRemoved + toReturn.WetWeatherTNRemoved;
            toReturn.TotalTPRemoved = toReturn.DryWeatherTPRemoved + toReturn.WetWeatherTPRemoved;
            toReturn.TotalFCRemoved = toReturn.DryWeatherFCRemoved + toReturn.WetWeatherFCRemoved;
            toReturn.TotalTCuRemoved = toReturn.DryWeatherTCuRemoved + toReturn.WetWeatherTCuRemoved;
            toReturn.TotalTPbRemoved = toReturn.DryWeatherTPbRemoved + toReturn.WetWeatherTPbRemoved;
            toReturn.TotalTZnRemoved = toReturn.DryWeatherTZnRemoved + toReturn.WetWeatherTZnRemoved;
            toReturn.TotalTSSInflow = toReturn.DryWeatherTSSInflow + toReturn.WetWeatherTSSInflow;
            toReturn.TotalTNInflow = toReturn.DryWeatherTNInflow + toReturn.WetWeatherTNInflow;
            toReturn.TotalTPInflow = toReturn.DryWeatherTPInflow + toReturn.WetWeatherTPInflow;
            toReturn.TotalFCInflow = toReturn.DryWeatherFCInflow + toReturn.WetWeatherFCInflow;
            toReturn.TotalTCuInflow = toReturn.DryWeatherTCuInflow + toReturn.WetWeatherTCuInflow;
            toReturn.TotalTPbInflow = toReturn.DryWeatherTPbInflow + toReturn.WetWeatherTPbInflow;
            toReturn.TotalTZnInflow = toReturn.DryWeatherTZnInflow + toReturn.WetWeatherTZnInflow;

            return toReturn;
        }

        public static TreatmentBMPModeledResultSimpleDto AsTreatmentBMPModeledResultSimpleDtoWithTreatmentBMPFields(this ProjectNereidResult result, TreatmentBMP treatmentBMP)
        {
            var toReturn = result.AsTreatmentBMPModeledResultSimpleDto();
            toReturn.TreatmentBMPName = treatmentBMP.TreatmentBMPName;

            return toReturn;
        }

        public static double ExtractDoubleValue(this JObject jobject, string key)
        {
            return jobject[key]?.Value<double>() ?? 0;
        }

        private const double PoundsToKilogramsFactor = 0.453592;
        private const double PoundsToGramsFactor = 453.592;
    }
}