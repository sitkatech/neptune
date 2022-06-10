using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static class vProjectLoadRemovingResultExtensionMethods
    {
        public static ProjectLoadReducingResultDto AsDto(this vProjectLoadReducingResult result)
        {
            var toReturn = new ProjectLoadReducingResultDto()
            {
                TreatmentBMPID = result.TreatmentBMPID,
                TreatmentBMPName = result.TreatmentBMPName,
                ProjectID = result.ProjectID,
                ProjectName = result.ProjectName,

                EffectiveAreaAcres = result.EffectiveAreaAcres,
                DesignStormDepth85thPercentile = result.DesignStormDepth85thPercentile,
                DesignVolume85thPercentile = result.DesignVolume85thPercentile,

                WetWeatherInflow = result.WetWeatherInflow ?? 0,
                WetWeatherTreated = result.WetWeatherTreated ?? 0,
                WetWeatherRetained = result.WetWeatherRetained ?? 0,
                WetWeatherUntreated = result.WetWeatherUntreated ?? 0,
                WetWeatherTSSReduced = result.WetWeatherTSSReduced ?? 0,
                WetWeatherTNReduced = result.WetWeatherTNReduced ?? 0,
                WetWeatherTPReduced = result.WetWeatherTPReduced ?? 0,
                WetWeatherFCReduced = result.WetWeatherFCReduced ?? 0,
                WetWeatherTCuReduced = result.WetWeatherTCuReduced ?? 0,
                WetWeatherTPbReduced = result.WetWeatherTPbReduced ?? 0,
                WetWeatherTZnReduced = result.WetWeatherTZnReduced ?? 0,
                WetWeatherTSSInflow = result.WetWeatherTSSInflow ?? 0,
                WetWeatherTNInflow = result.WetWeatherTNInflow ?? 0,
                WetWeatherTPInflow = result.WetWeatherTPInflow ?? 0,
                WetWeatherFCInflow = result.WetWeatherFCInflow ?? 0,
                WetWeatherTCuInflow = result.WetWeatherTCuInflow ?? 0,
                WetWeatherTPbInflow = result.WetWeatherTPbInflow ?? 0,
                WetWeatherTZnInflow = result.WetWeatherTZnInflow ?? 0,
                SummerDryWeatherInflow = result.SummerDryWeatherInflow ?? 0,
                SummerDryWeatherTreated = result.SummerDryWeatherTreated ?? 0,
                SummerDryWeatherRetained = result.SummerDryWeatherRetained ?? 0,
                SummerDryWeatherUntreated = result.SummerDryWeatherUntreated ?? 0,
                SummerDryWeatherTSSReduced = result.SummerDryWeatherTSSReduced ?? 0,
                SummerDryWeatherTNReduced = result.SummerDryWeatherTNReduced ?? 0,
                SummerDryWeatherTPReduced = result.SummerDryWeatherTPReduced ?? 0,
                SummerDryWeatherFCReduced = result.SummerDryWeatherFCReduced ?? 0,
                SummerDryWeatherTCuReduced = result.SummerDryWeatherTCuReduced ?? 0,
                SummerDryWeatherTPbReduced = result.SummerDryWeatherTPbReduced ?? 0,
                SummerDryWeatherTZnReduced = result.SummerDryWeatherTZnReduced ?? 0,
                SummerDryWeatherTSSInflow = result.SummerDryWeatherTSSInflow ?? 0,
                SummerDryWeatherTNInflow = result.SummerDryWeatherTNInflow ?? 0,
                SummerDryWeatherTPInflow = result.SummerDryWeatherTPInflow ?? 0,
                SummerDryWeatherFCInflow = result.SummerDryWeatherFCInflow ?? 0,
                SummerDryWeatherTCuInflow = result.SummerDryWeatherTCuInflow ?? 0,
                SummerDryWeatherTPbInflow = result.SummerDryWeatherTPbInflow ?? 0,
                SummerDryWeatherTZnInflow = result.SummerDryWeatherTZnInflow ?? 0,
                WinterDryWeatherInflow = result.WinterDryWeatherInflow ?? 0,
                WinterDryWeatherTreated = result.WinterDryWeatherTreated ?? 0,
                WinterDryWeatherRetained = result.WinterDryWeatherRetained ?? 0,
                WinterDryWeatherUntreated = result.WinterDryWeatherUntreated ?? 0,
                WinterDryWeatherTSSReduced = result.WinterDryWeatherTSSReduced ?? 0,
                WinterDryWeatherTNReduced = result.WinterDryWeatherTNReduced ?? 0,
                WinterDryWeatherTPReduced = result.WinterDryWeatherTPReduced ?? 0,
                WinterDryWeatherFCReduced = result.WinterDryWeatherFCReduced ?? 0,
                WinterDryWeatherTCuReduced = result.WinterDryWeatherTCuReduced ?? 0,
                WinterDryWeatherTPbReduced = result.WinterDryWeatherTPbReduced ?? 0,
                WinterDryWeatherTZnReduced = result.WinterDryWeatherTZnReduced ?? 0,
                WinterDryWeatherTSSInflow = result.WinterDryWeatherTSSInflow ?? 0,
                WinterDryWeatherTNInflow = result.WinterDryWeatherTNInflow ?? 0,
                WinterDryWeatherTPInflow = result.WinterDryWeatherTPInflow ?? 0,
                WinterDryWeatherFCInflow = result.WinterDryWeatherFCInflow ?? 0,
                WinterDryWeatherTCuInflow = result.WinterDryWeatherTCuInflow ?? 0,
                WinterDryWeatherTPbInflow = result.WinterDryWeatherTPbInflow ?? 0,
                WinterDryWeatherTZnInflow = result.WinterDryWeatherTZnInflow ?? 0,
            };

            toReturn.DryWeatherInflow = toReturn.SummerDryWeatherInflow + toReturn.WinterDryWeatherInflow;
            toReturn.DryWeatherTreated = toReturn.SummerDryWeatherTreated + toReturn.WinterDryWeatherTreated;
            toReturn.DryWeatherRetained = toReturn.SummerDryWeatherRetained + toReturn.WinterDryWeatherRetained;
            toReturn.DryWeatherUntreated = toReturn.SummerDryWeatherUntreated + toReturn.WinterDryWeatherUntreated;
            toReturn.DryWeatherTSSReduced = toReturn.SummerDryWeatherTSSReduced + toReturn.WinterDryWeatherTSSReduced;
            toReturn.DryWeatherTNReduced = toReturn.SummerDryWeatherTNReduced + toReturn.WinterDryWeatherTNReduced;
            toReturn.DryWeatherTPReduced = toReturn.SummerDryWeatherTPReduced + toReturn.WinterDryWeatherTPReduced;
            toReturn.DryWeatherFCReduced = toReturn.SummerDryWeatherFCReduced + toReturn.WinterDryWeatherFCReduced;
            toReturn.DryWeatherTCuReduced = toReturn.SummerDryWeatherTCuReduced + toReturn.WinterDryWeatherTCuReduced;
            toReturn.DryWeatherTPbReduced = toReturn.SummerDryWeatherTPbReduced + toReturn.WinterDryWeatherTPbReduced;
            toReturn.DryWeatherTZnReduced = toReturn.SummerDryWeatherTZnReduced + toReturn.WinterDryWeatherTZnReduced;
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
            toReturn.TotalTSSReduced = toReturn.DryWeatherTSSReduced + toReturn.WetWeatherTSSReduced;
            toReturn.TotalTNReduced = toReturn.DryWeatherTNReduced + toReturn.WetWeatherTNReduced;
            toReturn.TotalTPReduced = toReturn.DryWeatherTPReduced + toReturn.WetWeatherTPReduced;
            toReturn.TotalFCReduced = toReturn.DryWeatherFCReduced + toReturn.WetWeatherFCReduced;
            toReturn.TotalTCuReduced = toReturn.DryWeatherTCuReduced + toReturn.WetWeatherTCuReduced;
            toReturn.TotalTPbReduced = toReturn.DryWeatherTPbReduced + toReturn.WetWeatherTPbReduced;
            toReturn.TotalTZnReduced = toReturn.DryWeatherTZnReduced + toReturn.WetWeatherTZnReduced;
            toReturn.TotalTSSInflow = toReturn.DryWeatherTSSInflow + toReturn.WetWeatherTSSInflow;
            toReturn.TotalTNInflow = toReturn.DryWeatherTNInflow + toReturn.WetWeatherTNInflow;
            toReturn.TotalTPInflow = toReturn.DryWeatherTPInflow + toReturn.WetWeatherTPInflow;
            toReturn.TotalFCInflow = toReturn.DryWeatherFCInflow + toReturn.WetWeatherFCInflow;
            toReturn.TotalTCuInflow = toReturn.DryWeatherTCuInflow + toReturn.WetWeatherTCuInflow;
            toReturn.TotalTPbInflow = toReturn.DryWeatherTPbInflow + toReturn.WetWeatherTPbInflow;
            toReturn.TotalTZnInflow = toReturn.DryWeatherTZnInflow + toReturn.WetWeatherTZnInflow;

            return toReturn;
        }
    }
}