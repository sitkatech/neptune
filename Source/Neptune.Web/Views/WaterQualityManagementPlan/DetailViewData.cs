using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Neptune.Web.Views.Shared.HRUCharacteristics;
using Neptune.Web.Views.Shared.ModeledPerformance;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public bool CurrentPersonCanManageWaterQualityManagementPlans { get; }
        public string EditInventoriedBMPsUrl { get; }
        public string EditSimplifiedStructuralBMPsUrl { get; }
        public string EditSourceControlBMPsUrl { get; }
        public string EditParcelsUrl { get; }
        public string EditWqmpBoundaryUrl { get; }
        public string NewDocumentUrl { get; }
        public MapInitJson MapInitJson { get; }
        public ParcelGridSpec ParcelGridSpec { get; }
        public string ParcelGridName { get; }
        public string ParcelGridDataUrl { get; }
        public bool HasSavedWqmpDraft { get; }
        public string BeginOMVerificationRecordUrl { get; }
        public string EditModelingApproachUrl { get; }

        public List<Models.TreatmentBMP> TreatmentBMPs { get; }
        public List<QuickBMP> QuickBMPs { get; }

        public IEnumerable<IGrouping<int, SourceControlBMP>> SourceControlBMPs { get; }
        public List<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; }
        public List<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; }
        public List<WaterQualityManagementPlanModelingApproach> WaterQualityManagementPlanModelingApproaches { get; }
        public string CalculatedWQMPAcreage { get; }

        public string TrashCaptureEffectiveness { get; }
        public HRUCharacteristicsViewData HRUCharacteristicsViewData { get; }
        public List<HtmlString> ParameterizationErrors { get; }

        public Models.FieldDefinitionType FieldDefinitionForPercentOfSiteTreated { get; }
        public Models.FieldDefinitionType FieldDefinitionForPercentCaptured { get; }
        public Models.FieldDefinitionType FieldDefinitionForPercentRetained { get; }
        public Models.FieldDefinitionType FieldDefinitionForFullyParameterized { get; }
        public Models.FieldDefinitionType FieldDefinitionForDelineationStatus { get; }
        public Models.FieldDefinitionType FieldDefinitionForDryWeatherFlowOverride { get; }

        public ModeledPerformanceViewData ModeledPerformanceViewData { get; }

        public bool AnyDetailedBMPsNotFullyParameterized { get; }
        public bool AllDetailedBMPsNotFullyParameterized { get; }
        public bool UsesDetailedModelingApproach { get; }


        public DetailViewData(Person currentPerson, Models.WaterQualityManagementPlan waterQualityManagementPlan,
            WaterQualityManagementPlanVerify waterQualityManagementPlanVerifyDraft, MapInitJson mapInitJson,
            List<Models.TreatmentBMP> treatmentBMPs,
            ParcelGridSpec parcelGridSpec, List<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifies,
            List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBmPs,
            List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBmPs,
            HRUCharacteristicsViewData hruCharacteristicsViewData,
            List<DryWeatherFlowOverride> dryWeatherFlowOverrides,
            List<WaterQualityManagementPlanModelingApproach> waterQualityManagementPlanModelingApproaches, ModeledPerformanceViewData modeledPerformanceViewData)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            PageTitle = WaterQualityManagementPlan.WaterQualityManagementPlanName;
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());

            CurrentPersonCanManageWaterQualityManagementPlans = new WaterQualityManagementPlanManageFeature()
                .HasPermission(currentPerson, waterQualityManagementPlan)
                .HasPermission;
            currentPerson.IsManagerOrAdmin();
            EditInventoriedBMPsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpBmps(WaterQualityManagementPlan));
            EditSimplifiedStructuralBMPsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditSimplifiedStructuralBMPs(WaterQualityManagementPlan));
            EditSourceControlBMPsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditSourceControlBMPs(WaterQualityManagementPlan));
            EditModelingApproachUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditModelingApproach(WaterQualityManagementPlan));
            EditParcelsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpParcels(WaterQualityManagementPlan));
            EditWqmpBoundaryUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpBoundary(WaterQualityManagementPlan));
            NewDocumentUrl =
                SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(c =>
                    c.New(waterQualityManagementPlan));
            MapInitJson = mapInitJson;

            ParameterizationErrors = CheckForParameterizationErrors(waterQualityManagementPlan);

            ParcelGridSpec = parcelGridSpec;
            ParcelGridName = "parcelGrid";
            ParcelGridDataUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.ParcelsForWaterQualityManagementPlanGridData(waterQualityManagementPlan));

            HasSavedWqmpDraft = waterQualityManagementPlanVerifyDraft != null &&
                                waterQualityManagementPlanVerifyDraft.IsDraft;
            BeginOMVerificationRecordUrl = HasSavedWqmpDraft
                ? SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpVerifyModal(waterQualityManagementPlanVerifyDraft))
                : SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.NewWqmpVerify(waterQualityManagementPlan));

            SourceControlBMPs = waterQualityManagementPlan.SourceControlBMPs
                .Where(x => x.IsPresent == true || x.SourceControlBMPNote != null)
                .OrderBy(x => x.SourceControlBMPAttributeID)
                .GroupBy(x => x.SourceControlBMPAttribute.SourceControlBMPAttributeCategoryID);
            WaterQualityManagementPlanVerifies = waterQualityManagementPlanVerifies;
            WaterQualityManagementPlanVerifyQuickBMPs = waterQualityManagementPlanVerifyQuickBmPs;
            WaterQualityManagementPlanVerifyTreatmentBMPs = waterQualityManagementPlanVerifyTreatmentBmPs;
            HRUCharacteristicsViewData = hruCharacteristicsViewData;

            TreatmentBMPs = treatmentBMPs;
            QuickBMPs = waterQualityManagementPlan.QuickBMPs.OrderBy(x => x.QuickBMPName).ToList();
            SourceControlBMPs = waterQualityManagementPlan.SourceControlBMPs
                .Where(x => x.SourceControlBMPNote != null || (x.IsPresent != null && x.IsPresent == true))
                .OrderBy(x => x.SourceControlBMPAttributeID)
                .GroupBy(x => x.SourceControlBMPAttribute.SourceControlBMPAttributeCategoryID);

            var calculatedWQMPAcreage =
                WaterQualityManagementPlan
                    .CalculateTotalAcreage();

            // TODO: Never compare floating-point values to zero. We should establish an application-wide error tolerance and use that instead of the direct comparison
            CalculatedWQMPAcreage = calculatedWQMPAcreage != 0
                ? $"{Math.Round(calculatedWQMPAcreage, 2).ToString(CultureInfo.InvariantCulture)} acres"
                : "No parcels have been associated with this WQMP";

            TrashCaptureEffectiveness = WaterQualityManagementPlan.TrashCaptureEffectiveness == null
                ? "Not Provided"
                : WaterQualityManagementPlan.TrashCaptureEffectiveness + "%";

            WaterQualityManagementPlanModelingApproaches = waterQualityManagementPlanModelingApproaches;
            ModeledPerformanceViewData = modeledPerformanceViewData;

            FieldDefinitionForPercentOfSiteTreated = FieldDefinitionType.PercentOfSiteTreated;
            FieldDefinitionForPercentCaptured = FieldDefinitionType.PercentCaptured;
            FieldDefinitionForPercentRetained = FieldDefinitionType.PercentRetained;
            FieldDefinitionForFullyParameterized = FieldDefinitionType.FullyParameterized;
            FieldDefinitionForDelineationStatus = FieldDefinitionType.DelineationStatus;
            FieldDefinitionForDryWeatherFlowOverride = FieldDefinitionType.DryWeatherFlowOverride;

            UsesDetailedModelingApproach = WaterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID ==
                                           WaterQualityManagementPlanModelingApproach.Detailed
                                               .WaterQualityManagementPlanModelingApproachID;

            if (UsesDetailedModelingApproach)
            {
                AnyDetailedBMPsNotFullyParameterized = TreatmentBMPs.Any(x => !(x.IsFullyParameterized() && (x.Delineation?.IsVerified ?? false)));
                AllDetailedBMPsNotFullyParameterized = TreatmentBMPs.All(x => !(x.IsFullyParameterized() && (x.Delineation?.IsVerified ?? false)));
                // this is redundant but I just want to make this perfectly clear.
                AnySimpleBMPsNotFullyParameterized = false;
                AllSimpleBMPsNotFullyParameterized = false;
            }
            else
            {
                AnySimpleBMPsNotFullyParameterized = QuickBMPs.Any(x => !x.IsFullyParameterized());
                AllSimpleBMPsNotFullyParameterized = QuickBMPs.All(x => !x.IsFullyParameterized());
                // this is redundant but I just want to make this perfectly clear.
                AnyDetailedBMPsNotFullyParameterized = false;
                    AllDetailedBMPsNotFullyParameterized = false;
            }
        }

        public bool AllSimpleBMPsNotFullyParameterized { get; set; }

        public bool AnySimpleBMPsNotFullyParameterized { get; set; }


        //There are more errors to come I believe, that's why this is producing a list
        public List<HtmlString> CheckForParameterizationErrors(Models.WaterQualityManagementPlan waterQualityManagement)
        {
            var parameterizationErrors = new List<HtmlString>();

            if (!waterQualityManagement.TreatmentBMPs.Any() && !waterQualityManagement.QuickBMPs.Any())
            {
                parameterizationErrors.Add(new HtmlString("This WQMP is not associated with any inventoried Treatment BMPs and does not have any other treatment area assigned.No load reduction will be calculated for this WQMP."));
            }

            return parameterizationErrors;
        }

        public string DisplayDryWeatherFlowOverride(Models.TreatmentBMP treatmentBMP)
        {
            return treatmentBMP.TreatmentBMPModelingAttribute != null ? treatmentBMP.TreatmentBMPModelingAttribute?.DryWeatherFlowOverride?.DryWeatherFlowOverrideDisplayName : DryWeatherFlowOverride.No.DryWeatherFlowOverrideDisplayName;
        }

        public string DisplayDryWeatherFlowOverride(QuickBMP quickBMP)
        {
            return quickBMP.DryWeatherFlowOverride != null ? quickBMP.DryWeatherFlowOverride?.DryWeatherFlowOverrideDisplayName : DryWeatherFlowOverride.No.DryWeatherFlowOverrideDisplayName;
        }
    }
}
