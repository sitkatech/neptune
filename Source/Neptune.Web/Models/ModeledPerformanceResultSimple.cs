using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security.Notifications;
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

        public bool IsWQMPResult { get; set; }
        public bool IsSimplifiedWQMPResult { get; set; }
        public bool IsResultSetEmpty { get; set; }

        public ModeledPerformanceResultSimple(List<TreatmentBMP> treatmentBMPs)
        {
            var nereidResults = ExtractResults(treatmentBMPs, out var lastDeltaQueue);

            SetDatesAndScalarValues(nereidResults, lastDeltaQueue);
        }

        private static List<NereidResult> ExtractResults(List<TreatmentBMP> treatmentBMP, out DateTime? lastDeltaQueue)
        {
            var nereidResults = HttpRequestStorage.DatabaseEntities.NereidResults.Where(x => x.TreatmentBMPID != null && !x.IsBaselineCondition)
                .ToList().Where(x =>
                    treatmentBMP.Select(y => y.TreatmentBMPID).Contains(x.TreatmentBMPID.GetValueOrDefault())).ToList();

            lastDeltaQueue = HttpRequestStorage.DatabaseEntities.DirtyModelNodes
                .Where(x => x.TreatmentBMPID != null).ToList().OrderByDescending(x => x.CreateDate)
                .FirstOrDefault(x =>
                    treatmentBMP.Select(y => y.TreatmentBMPID).Contains(x.TreatmentBMPID.GetValueOrDefault()))
                ?.CreateDate;
            return nereidResults;
        }

        private void SetDatesAndScalarValues(List<NereidResult> nereidResults, DateTime? lastDeltaQueue)
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

            var fullResults = nereidResults.Select(x => JObject.Parse(x.FullResponse)).ToList();
            WetWeatherInflow = fullResults.Select(x => x.ExtractDoubleValue("runoff_volume_cuft_inflow")).Sum();
            WetWeatherTreated = fullResults.Select(x => x.ExtractDoubleValue("runoff_volume_cuft_treated")).Sum();
            WetWeatherRetained = fullResults.Select(x => x.ExtractDoubleValue("runoff_volume_cuft_retained")).Sum();
            WetWeatherUntreated = (fullResults.Select(x => x.ExtractDoubleValue("runoff_volume_cuft_bypassed")).Sum());
            WetWeatherTSSRemoved = (fullResults.Select(x => x.ExtractDoubleValue("TSS_load_lbs_removed")).Sum() *
                                    PoundsToKilogramsFactor);
            WetWeatherTNRemoved = (fullResults.Select(x => x.ExtractDoubleValue("TN_load_lbs_removed")).Sum() *
                                   PoundsToKilogramsFactor);
            WetWeatherTPRemoved = (fullResults.Select(x => x.ExtractDoubleValue("TP_load_lbs_removed")).Sum() *
                                   PoundsToKilogramsFactor);
            // todo: what is 1e9?????????????????????????
            WetWeatherFCRemoved = (fullResults.Select(x => x.ExtractDoubleValue("FC_load_mpn_removed")).Sum() / 1e9);
            WetWeatherTCuRemoved = (fullResults.Select(x => x.ExtractDoubleValue("TCu_load_lbs_removed")).Sum() *
                                    PoundsToGramsFactor);
            WetWeatherTPbRemoved = (fullResults.Select(x => x.ExtractDoubleValue("TPb_load_lbs_removed")).Sum() *
                                    PoundsToGramsFactor);
            WetWeatherTZnRemoved = (fullResults.Select(x => x.ExtractDoubleValue("TZn_load_lbs_removed")).Sum() *
                                    PoundsToGramsFactor);
            WetWeatherTSSInflow = (fullResults.Select(x => x.ExtractDoubleValue("TSS_load_lbs_inflow")).Sum() *
                                   PoundsToKilogramsFactor);
            WetWeatherTNInflow = (fullResults.Select(x => x.ExtractDoubleValue("TN_load_lbs_inflow")).Sum() *
                                  PoundsToKilogramsFactor);
            WetWeatherTPInflow = (fullResults.Select(x => x.ExtractDoubleValue("TP_load_lbs_inflow")).Sum() *
                                  PoundsToKilogramsFactor);
            WetWeatherFCInflow = (fullResults.Select(x => x.ExtractDoubleValue("FC_load_mpn_inflow")).Sum() / 1e9);
            WetWeatherTCuInflow =
                (fullResults.Select(x => x.ExtractDoubleValue("TCu_load_lbs_inflow")).Sum() * PoundsToGramsFactor);
            WetWeatherTPbInflow =
                (fullResults.Select(x => x.ExtractDoubleValue("TPb_load_lbs_inflow")).Sum() * PoundsToGramsFactor);
            WetWeatherTZnInflow =
                (fullResults.Select(x => x.ExtractDoubleValue("TZn_load_lbs_inflow")).Sum() * PoundsToGramsFactor);

            SummerDryWeatherInflow = fullResults.Select(x => x.ExtractDoubleValue("summer_dry_weather_flow_cuft_inflow")).Sum();
            SummerDryWeatherTreated =
                fullResults.Select(x => x.ExtractDoubleValue("summer_dry_weather_flow_cuft_treated")).Sum();
            SummerDryWeatherRetained =
                fullResults.Select(x => x.ExtractDoubleValue("summer_dry_weather_flow_cuft_retained")).Sum();
            SummerDryWeatherUntreated =
                (fullResults.Select(x => x.ExtractDoubleValue("summer_dry_weather_flow_cuft_bypassed")).Sum());
            SummerDryWeatherTSSRemoved = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTSS_load_lbs_removed")).Sum() *
                                          PoundsToKilogramsFactor);
            SummerDryWeatherTNRemoved = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTN_load_lbs_removed")).Sum() *
                                         PoundsToKilogramsFactor);
            SummerDryWeatherTPRemoved = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTP_load_lbs_removed")).Sum() *
                                         PoundsToKilogramsFactor);
            SummerDryWeatherFCRemoved =
                (fullResults.Select(x => x.ExtractDoubleValue("summer_dwFC_load_mpn_removed")).Sum() / 1e9);
            SummerDryWeatherTCuRemoved = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTCu_load_lbs_removed")).Sum() *
                                          PoundsToGramsFactor);
            SummerDryWeatherTPbRemoved = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTPb_load_lbs_removed")).Sum() *
                                          PoundsToGramsFactor);
            SummerDryWeatherTZnRemoved = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTZn_load_lbs_removed")).Sum() *
                                          PoundsToGramsFactor);
            SummerDryWeatherTSSInflow = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTSS_load_lbs_inflow")).Sum() *
                                         PoundsToKilogramsFactor);
            SummerDryWeatherTNInflow = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTN_load_lbs_inflow")).Sum() *
                                        PoundsToKilogramsFactor);
            SummerDryWeatherTPInflow = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTP_load_lbs_inflow")).Sum() *
                                        PoundsToKilogramsFactor);
            SummerDryWeatherFCInflow =
                (fullResults.Select(x => x.ExtractDoubleValue("summer_dwFC_load_mpn_inflow")).Sum() / 1e9);
            SummerDryWeatherTCuInflow = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTCu_load_lbs_inflow")).Sum() *
                                         PoundsToGramsFactor);
            SummerDryWeatherTPbInflow = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTPb_load_lbs_inflow")).Sum() *
                                         PoundsToGramsFactor);
            SummerDryWeatherTZnInflow = (fullResults.Select(x => x.ExtractDoubleValue("summer_dwTZn_load_lbs_inflow")).Sum() *
                                         PoundsToGramsFactor);

            WinterDryWeatherInflow = fullResults.Select(x => x.ExtractDoubleValue("winter_dry_weather_flow_cuft_inflow")).Sum();
            WinterDryWeatherTreated =
                fullResults.Select(x => x.ExtractDoubleValue("winter_dry_weather_flow_cuft_treated")).Sum();
            WinterDryWeatherRetained =
                fullResults.Select(x => x.ExtractDoubleValue("winter_dry_weather_flow_cuft_retained")).Sum();
            WinterDryWeatherUntreated =
                (fullResults.Select(x => x.ExtractDoubleValue("winter_dry_weather_flow_cuft_bypassed")).Sum());
            WinterDryWeatherTSSRemoved = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTSS_load_lbs_removed")).Sum() *
                                          PoundsToKilogramsFactor);
            WinterDryWeatherTNRemoved = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTN_load_lbs_removed")).Sum() *
                                         PoundsToKilogramsFactor);
            WinterDryWeatherTPRemoved = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTP_load_lbs_removed")).Sum() *
                                         PoundsToKilogramsFactor);
            WinterDryWeatherFCRemoved =
                (fullResults.Select(x => x.ExtractDoubleValue("winter_dwFC_load_mpn_removed")).Sum() / 1e9);
            WinterDryWeatherTCuRemoved = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTCu_load_lbs_removed")).Sum() *
                                          PoundsToGramsFactor);
            WinterDryWeatherTPbRemoved = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTPb_load_lbs_removed")).Sum() *
                                          PoundsToGramsFactor);
            WinterDryWeatherTZnRemoved = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTZn_load_lbs_removed")).Sum() *
                                          PoundsToGramsFactor);
            WinterDryWeatherTSSInflow = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTSS_load_lbs_inflow")).Sum() *
                                         PoundsToKilogramsFactor);
            WinterDryWeatherTNInflow = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTN_load_lbs_inflow")).Sum() *
                                        PoundsToKilogramsFactor);
            WinterDryWeatherTPInflow = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTP_load_lbs_inflow")).Sum() *
                                        PoundsToKilogramsFactor);
            WinterDryWeatherFCInflow =
                (fullResults.Select(x => x.ExtractDoubleValue("winter_dwFC_load_mpn_inflow")).Sum() / 1e9);
            WinterDryWeatherTCuInflow = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTCu_load_lbs_inflow")).Sum() *
                                         PoundsToGramsFactor);
            WinterDryWeatherTPbInflow = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTPb_load_lbs_inflow")).Sum() *
                                         PoundsToGramsFactor);
            WinterDryWeatherTZnInflow = (fullResults.Select(x => x.ExtractDoubleValue("winter_dwTZn_load_lbs_inflow")).Sum() *
                                         PoundsToGramsFactor);

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
            IsWQMPResult = true;
            if (waterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID ==
                WaterQualityManagementPlanModelingApproach.Detailed.WaterQualityManagementPlanModelingApproachID)
            {
                IsSimplifiedWQMPResult = false;
                var nereidResults = ExtractResults(waterQualityManagementPlan.TreatmentBMPs.ToList().Where(x=>x.Delineation?.IsVerified ?? false).ToList(), out var lastDeltaQueue);
                SetDatesAndScalarValues(nereidResults, lastDeltaQueue);
            }
            else
            {
                IsSimplifiedWQMPResult = true;
                var nereidResults = HttpRequestStorage.DatabaseEntities.NereidResults.Where(x =>
                    x.WaterQualityManagementPlanID == waterQualityManagementPlan.WaterQualityManagementPlanID && !x.IsBaselineCondition);
                var lastDeltaQueue = HttpRequestStorage.DatabaseEntities.DirtyModelNodes.FirstOrDefault(x =>
                    x.WaterQualityManagementPlanID == waterQualityManagementPlan.WaterQualityManagementPlanID)?.CreateDate;
                    SetDatesAndScalarValues(nereidResults.ToList(), lastDeltaQueue);
            }

        }

        public ModeledPerformanceResultSimple(TreatmentBMP treatmentBMP): this(new List<TreatmentBMP>{treatmentBMP})
        {
            
        }

        private const double PoundsToKilogramsFactor = 0.453592;
        private const double PoundsToGramsFactor = 453.592;
    }
}