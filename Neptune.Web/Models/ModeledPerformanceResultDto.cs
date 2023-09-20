using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Models
{
    public class ModeledPerformanceResultDto
    {
        public double EffectiveAreaAcres { get; set; }
        public double DesignStormDepth85thPercentile { get; set; }
        public double DesignVolume85thPercentile { get; set; }
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
        public double DryWeatherInflow { get; set; }
        public double DryWeatherTreated { get; set; }
        public double DryWeatherRetained { get; set; }
        public double DryWeatherUntreated { get; set; }
        public double DryWeatherTSSReduced { get; set; }
        public double DryWeatherTNReduced { get; set; }
        public double DryWeatherTPReduced { get; set; }
        public double DryWeatherFCReduced { get; set; }
        public double DryWeatherTCuReduced { get; set; }
        public double DryWeatherTPbReduced { get; set; }
        public double DryWeatherTZnReduced { get; set; }
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
        public double TotalTSSReduced { get; set; }
        public double TotalTNReduced { get; set; }
        public double TotalTPReduced { get; set; }
        public double TotalFCReduced { get; set; }
        public double TotalTCuReduced { get; set; }
        public double TotalTPbReduced { get; set; }
        public double TotalTZnReduced { get; set; }
        public double TotalTSSInflow { get; set; }
        public double TotalTNInflow { get; set; }
        public double TotalTPInflow { get; set; }
        public double TotalFCInflow { get; set; }
        public double TotalTCuInflow { get; set; }
        public double TotalTPbInflow { get; set; }
        public double TotalTZnInflow { get; set; }

        public string LastUpdated { get; set; }

        public bool Outdated { get; set; }

        public bool IsWQMPResult { get; set; }
        public bool IsSimplifiedWQMPResult { get; set; }
        public bool IsResultSetEmpty { get; set; }

        public ModeledPerformanceResultDto(NeptuneDbContext dbContext, List<TreatmentBMP> treatmentBMPs)
        {
            var nereidResults = ExtractResults(dbContext, treatmentBMPs, out var lastDeltaQueue);

            SetDatesAndScalarValues(nereidResults, lastDeltaQueue);
        }

        public ModeledPerformanceResultDto(NeptuneDbContext dbContext, WaterQualityManagementPlan waterQualityManagementPlan)
        {
            IsWQMPResult = true;
            if (waterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID ==
                WaterQualityManagementPlanModelingApproach.Detailed.WaterQualityManagementPlanModelingApproachID)
            {
                IsSimplifiedWQMPResult = false;
                var treatmentBmps = Delineations.ListByTreatmentBMPIDList(dbContext,
                    waterQualityManagementPlan.TreatmentBMPs.Select(x => x.TreatmentBMPID)).Where(x => x.IsVerified).Select(x => x.TreatmentBMP).ToList();
                var nereidResults = ExtractResults(dbContext, treatmentBmps, out var lastDeltaQueue);
                SetDatesAndScalarValues(nereidResults, lastDeltaQueue);
            }
            else
            {
                IsSimplifiedWQMPResult = true;
                var nereidResults = dbContext.vLoadReducingResults.AsNoTracking().Where(x => x.WaterQualityManagementPlanID != null &&
                    x.WaterQualityManagementPlanID == waterQualityManagementPlan.WaterQualityManagementPlanID && !x.IsBaselineCondition);
                var lastDeltaQueue = dbContext.DirtyModelNodes.AsNoTracking().FirstOrDefault(x =>
                    x.WaterQualityManagementPlanID == waterQualityManagementPlan.WaterQualityManagementPlanID)?.CreateDate;
                SetDatesAndScalarValues(nereidResults.ToList(), lastDeltaQueue);
            }

        }

        public ModeledPerformanceResultDto(NeptuneDbContext dbContext, TreatmentBMP treatmentBMP) : this(dbContext, new List<TreatmentBMP> { treatmentBMP })
        {

        }

        private static List<vLoadReducingResult> ExtractResults(NeptuneDbContext dbContext, List<TreatmentBMP> treatmentBMP, out DateTime? lastDeltaQueue)
        {
            var nereidResults = dbContext.vLoadReducingResults.AsNoTracking().Where(x => x.TreatmentBMPID != null && !x.IsBaselineCondition &&
                    treatmentBMP.Select(y => y.TreatmentBMPID).Contains(x.TreatmentBMPID.Value)).ToList();

            lastDeltaQueue = dbContext.DirtyModelNodes.AsNoTracking()
                .Where(x => x.TreatmentBMPID != null).ToList().OrderByDescending(x => x.CreateDate)
                .FirstOrDefault(x =>
                    treatmentBMP.Select(y => y.TreatmentBMPID).Contains(x.TreatmentBMPID.GetValueOrDefault()))
                ?.CreateDate;
            return nereidResults;
        }

        private void SetDatesAndScalarValues(List<vLoadReducingResult> nereidResults, DateTime? lastDeltaQueue)
        {
            if (!nereidResults.Any())
            {
                IsResultSetEmpty = true;
                return;
            }

            // nereidResults should never ever be empty so this should never ever be a problem
            // ReSharper disable once PossibleInvalidOperationException
            var nereidResultLastUpdate = nereidResults.Select(x => x.LastUpdate).Max().Value;

            LastUpdated = $"{nereidResultLastUpdate.ToShortDateString()} {nereidResultLastUpdate.ToShortTimeString()}";

            Outdated = lastDeltaQueue != null && lastDeltaQueue.Value > nereidResultLastUpdate;

            EffectiveAreaAcres = nereidResults.Sum(x => x.EffectiveAreaAcres ?? 0);
            EffectiveAreaAcres = nereidResults.Sum(x => x.EffectiveAreaAcres ?? 0);
            DesignStormDepth85thPercentile = nereidResults.Sum(x => x.DesignStormDepth85thPercentile ?? 0);
            DesignVolume85thPercentile = nereidResults.Sum(x => x.DesignVolume85thPercentile ?? 0);
            WetWeatherInflow = nereidResults.Sum(x => x.WetWeatherInflow ?? 0);
            WetWeatherTreated = nereidResults.Sum(x => x.WetWeatherTreated ?? 0);
            WetWeatherRetained = nereidResults.Sum(x => x.WetWeatherRetained ?? 0);
            WetWeatherUntreated = nereidResults.Sum(x => x.WetWeatherUntreated ?? 0);
            WetWeatherTSSReduced = nereidResults.Sum(x => x.WetWeatherTSSReduced ?? 0);
            WetWeatherTNReduced = nereidResults.Sum(x => x.WetWeatherTNReduced ?? 0);
            WetWeatherTPReduced = nereidResults.Sum(x => x.WetWeatherTPReduced ?? 0);
            WetWeatherFCReduced = nereidResults.Sum(x => x.WetWeatherFCReduced ?? 0);
            WetWeatherTCuReduced = nereidResults.Sum(x => x.WetWeatherTCuReduced ?? 0);
            WetWeatherTPbReduced = nereidResults.Sum(x => x.WetWeatherTPbReduced ?? 0);
            WetWeatherTZnReduced = nereidResults.Sum(x => x.WetWeatherTZnReduced ?? 0);
            WetWeatherTSSInflow = nereidResults.Sum(x => x.WetWeatherTSSInflow ?? 0);
            WetWeatherTNInflow = nereidResults.Sum(x => x.WetWeatherTNInflow ?? 0);
            WetWeatherTPInflow = nereidResults.Sum(x => x.WetWeatherTPInflow ?? 0);
            WetWeatherFCInflow = nereidResults.Sum(x => x.WetWeatherFCInflow ?? 0);
            WetWeatherTCuInflow = nereidResults.Sum(x => x.WetWeatherTCuInflow ?? 0);
            WetWeatherTPbInflow = nereidResults.Sum(x => x.WetWeatherTPbInflow ?? 0);
            WetWeatherTZnInflow = nereidResults.Sum(x => x.WetWeatherTZnInflow ?? 0);
            SummerDryWeatherInflow = nereidResults.Sum(x => x.SummerDryWeatherInflow ?? 0);
            SummerDryWeatherTreated = nereidResults.Sum(x => x.SummerDryWeatherTreated ?? 0);
            SummerDryWeatherRetained = nereidResults.Sum(x => x.SummerDryWeatherRetained ?? 0);
            SummerDryWeatherUntreated = nereidResults.Sum(x => x.SummerDryWeatherUntreated ?? 0);
            SummerDryWeatherTSSReduced = nereidResults.Sum(x => x.SummerDryWeatherTSSReduced ?? 0);
            SummerDryWeatherTNReduced = nereidResults.Sum(x => x.SummerDryWeatherTNReduced ?? 0);
            SummerDryWeatherTPReduced = nereidResults.Sum(x => x.SummerDryWeatherTPReduced ?? 0);
            SummerDryWeatherFCReduced = nereidResults.Sum(x => x.SummerDryWeatherFCReduced ?? 0);
            SummerDryWeatherTCuReduced = nereidResults.Sum(x => x.SummerDryWeatherTCuReduced ?? 0);
            SummerDryWeatherTPbReduced = nereidResults.Sum(x => x.SummerDryWeatherTPbReduced ?? 0);
            SummerDryWeatherTZnReduced = nereidResults.Sum(x => x.SummerDryWeatherTZnReduced ?? 0);
            SummerDryWeatherTSSInflow = nereidResults.Sum(x => x.SummerDryWeatherTSSInflow ?? 0);
            SummerDryWeatherTNInflow = nereidResults.Sum(x => x.SummerDryWeatherTNInflow ?? 0);
            SummerDryWeatherTPInflow = nereidResults.Sum(x => x.SummerDryWeatherTPInflow ?? 0);
            SummerDryWeatherFCInflow = nereidResults.Sum(x => x.SummerDryWeatherFCInflow ?? 0);
            SummerDryWeatherTCuInflow = nereidResults.Sum(x => x.SummerDryWeatherTCuInflow ?? 0);
            SummerDryWeatherTPbInflow = nereidResults.Sum(x => x.SummerDryWeatherTPbInflow ?? 0);
            SummerDryWeatherTZnInflow = nereidResults.Sum(x => x.SummerDryWeatherTZnInflow ?? 0);
            WinterDryWeatherInflow = nereidResults.Sum(x => x.WinterDryWeatherInflow ?? 0);
            WinterDryWeatherTreated = nereidResults.Sum(x => x.WinterDryWeatherTreated ?? 0);
            WinterDryWeatherRetained = nereidResults.Sum(x => x.WinterDryWeatherRetained ?? 0);
            WinterDryWeatherUntreated = nereidResults.Sum(x => x.WinterDryWeatherUntreated ?? 0);
            WinterDryWeatherTSSReduced = nereidResults.Sum(x => x.WinterDryWeatherTSSReduced ?? 0);
            WinterDryWeatherTNReduced = nereidResults.Sum(x => x.WinterDryWeatherTNReduced ?? 0);
            WinterDryWeatherTPReduced = nereidResults.Sum(x => x.WinterDryWeatherTPReduced ?? 0);
            WinterDryWeatherFCReduced = nereidResults.Sum(x => x.WinterDryWeatherFCReduced ?? 0);
            WinterDryWeatherTCuReduced = nereidResults.Sum(x => x.WinterDryWeatherTCuReduced ?? 0);
            WinterDryWeatherTPbReduced = nereidResults.Sum(x => x.WinterDryWeatherTPbReduced ?? 0);
            WinterDryWeatherTZnReduced = nereidResults.Sum(x => x.WinterDryWeatherTZnReduced ?? 0);
            WinterDryWeatherTSSInflow = nereidResults.Sum(x => x.WinterDryWeatherTSSInflow ?? 0);
            WinterDryWeatherTNInflow = nereidResults.Sum(x => x.WinterDryWeatherTNInflow ?? 0);
            WinterDryWeatherTPInflow = nereidResults.Sum(x => x.WinterDryWeatherTPInflow ?? 0);
            WinterDryWeatherFCInflow = nereidResults.Sum(x => x.WinterDryWeatherFCInflow ?? 0);
            WinterDryWeatherTCuInflow = nereidResults.Sum(x => x.WinterDryWeatherTCuInflow ?? 0);
            WinterDryWeatherTPbInflow = nereidResults.Sum(x => x.WinterDryWeatherTPbInflow ?? 0);
            WinterDryWeatherTZnInflow = nereidResults.Sum(x => x.WinterDryWeatherTZnInflow ?? 0);

            DryWeatherInflow = SummerDryWeatherInflow + WinterDryWeatherInflow;
            DryWeatherTreated = SummerDryWeatherTreated + WinterDryWeatherTreated;
            DryWeatherRetained = SummerDryWeatherRetained + WinterDryWeatherRetained;
            DryWeatherUntreated = SummerDryWeatherUntreated + WinterDryWeatherUntreated;
            DryWeatherTSSReduced = SummerDryWeatherTSSReduced + WinterDryWeatherTSSReduced;
            DryWeatherTNReduced = SummerDryWeatherTNReduced + WinterDryWeatherTNReduced;
            DryWeatherTPReduced = SummerDryWeatherTPReduced + WinterDryWeatherTPReduced;
            DryWeatherFCReduced = SummerDryWeatherFCReduced + WinterDryWeatherFCReduced;
            DryWeatherTCuReduced = SummerDryWeatherTCuReduced + WinterDryWeatherTCuReduced;
            DryWeatherTPbReduced = SummerDryWeatherTPbReduced + WinterDryWeatherTPbReduced;
            DryWeatherTZnReduced = SummerDryWeatherTZnReduced + WinterDryWeatherTZnReduced;
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
            TotalTSSReduced = DryWeatherTSSReduced + WetWeatherTSSReduced;
            TotalTNReduced = DryWeatherTNReduced + WetWeatherTNReduced;
            TotalTPReduced = DryWeatherTPReduced + WetWeatherTPReduced;
            TotalFCReduced = DryWeatherFCReduced + WetWeatherFCReduced;
            TotalTCuReduced = DryWeatherTCuReduced + WetWeatherTCuReduced;
            TotalTPbReduced = DryWeatherTPbReduced + WetWeatherTPbReduced;
            TotalTZnReduced = DryWeatherTZnReduced + WetWeatherTZnReduced;
            TotalTSSInflow = DryWeatherTSSInflow + WetWeatherTSSInflow;
            TotalTNInflow = DryWeatherTNInflow + WetWeatherTNInflow;
            TotalTPInflow = DryWeatherTPInflow + WetWeatherTPInflow;
            TotalFCInflow = DryWeatherFCInflow + WetWeatherFCInflow;
            TotalTCuInflow = DryWeatherTCuInflow + WetWeatherTCuInflow;
            TotalTPbInflow = DryWeatherTPbInflow + WetWeatherTPbInflow;
            TotalTZnInflow = DryWeatherTZnInflow + WetWeatherTZnInflow;
        }
    }
}