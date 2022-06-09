using Hippocamp.Models.DataTransferObjects;
using Newtonsoft.Json.Linq;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ProjectNereidResultExtensionMethods
    {
        public static TreatmentBMPModeledResultSimpleDto AsTreatmentBMPModeledResultSimpleDto(this vProjectModelingResult result)
        {
            var parsedResults = JObject.Parse(result.FullResponse);
            var toReturn = new TreatmentBMPModeledResultSimpleDto()
            {
                TreatmentBMPID = result.TreatmentBMPID.Value,
                TreatmentBMPName = result.TreatmentBMPName,
                ProjectID = result.ProjectID,
                ProjectName = result.ProjectName,

                WetWeatherInflow = result.WetWeatherInflow ?? 0,
                WetWeatherTreated = result.WetWeatherTreated ?? 0,
                WetWeatherRetained = result.WetWeatherRetained ?? 0,
                WetWeatherUntreated = result.WetWeatherUntreated ?? 0,
                WetWeatherTSSRemoved = result.WetWeatherTSSRemoved ?? 0,
                WetWeatherTNRemoved = result.WetWeatherTNRemoved ?? 0,
                WetWeatherTPRemoved = result.WetWeatherTPRemoved ?? 0,
                WetWeatherFCRemoved = result.WetWeatherFCRemoved ?? 0,
                WetWeatherTCuRemoved = result.WetWeatherTCuRemoved ?? 0,
                WetWeatherTPbRemoved = result.WetWeatherTPbRemoved ?? 0,
                WetWeatherTZnRemoved = result.WetWeatherTZnRemoved ?? 0,
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
                SummerDryWeatherTSSRemoved = result.SummerDryWeatherTSSRemoved ?? 0,
                SummerDryWeatherTNRemoved = result.SummerDryWeatherTNRemoved ?? 0,
                SummerDryWeatherTPRemoved = result.SummerDryWeatherTPRemoved ?? 0,
                SummerDryWeatherFCRemoved = result.SummerDryWeatherFCRemoved ?? 0,
                SummerDryWeatherTCuRemoved = result.SummerDryWeatherTCuRemoved ?? 0,
                SummerDryWeatherTPbRemoved = result.SummerDryWeatherTPbRemoved ?? 0,
                SummerDryWeatherTZnRemoved = result.SummerDryWeatherTZnRemoved ?? 0,
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
                WinterDryWeatherTSSRemoved = result.WinterDryWeatherTSSRemoved ?? 0,
                WinterDryWeatherTNRemoved = result.WinterDryWeatherTNRemoved ?? 0,
                WinterDryWeatherTPRemoved = result.WinterDryWeatherTPRemoved ?? 0,
                WinterDryWeatherFCRemoved = result.WinterDryWeatherFCRemoved ?? 0,
                WinterDryWeatherTCuRemoved = result.WinterDryWeatherTCuRemoved ?? 0,
                WinterDryWeatherTPbRemoved = result.WinterDryWeatherTPbRemoved ?? 0,
                WinterDryWeatherTZnRemoved = result.WinterDryWeatherTZnRemoved ?? 0,
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

        public static TreatmentBMPModeledResultSimpleDto AsTreatmentBMPModeledResultSimpleDtoWithTreatmentBMPFields(this vProjectModelingResult result, TreatmentBMP treatmentBMP)
        {
            var toReturn = result.AsTreatmentBMPModeledResultSimpleDto();
            toReturn.TreatmentBMPName = treatmentBMP.TreatmentBMPName;

            return toReturn;
        }
    }
}