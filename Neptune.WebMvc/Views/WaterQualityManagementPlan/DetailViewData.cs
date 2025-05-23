﻿using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using System.Globalization;
using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Shared.HRUCharacteristics;
using Neptune.WebMvc.Views.Shared.ModeledPerformance;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public bool CurrentPersonCanManageWaterQualityManagementPlans { get; }
        public string EditInventoriedBMPsUrl { get; }
        public string EditSimplifiedStructuralBMPsUrl { get; }
        public string EditSourceControlBMPsUrl { get; }
        public string EditParcelsUrl { get; }
        public string RefineAreaUrl { get; }
        public string NewDocumentUrl { get; }
        public MapInitJson MapInitJson { get; }
        public ParcelGridSpec ParcelGridSpec { get; }
        public string ParcelGridName { get; }
        public string ParcelGridDataUrl { get; }
        public bool HasSavedWqmpDraft { get; }
        public string BeginOMVerificationRecordUrl { get; }
        public string EditModelingApproachUrl { get; }

        public List<EFModels.Entities.TreatmentBMP> TreatmentBMPs { get; }
        public List<QuickBMP> QuickBMPs { get; }
        public QuickBMPGridSpec QuickBMPGridSpec { get; }
        public string QuickBMPGridName { get; }
        public string QuickBMPGridDataUrl { get; }
        public string WqmpModelingApproachUrl { get; }
        public IEnumerable<IGrouping<int, SourceControlBMP>> SourceControlBMPs { get; }
        public Dictionary<int, EFModels.Entities.Delineation?> TreatmentBMPDelineationsDict { get; }

        public List<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; }
        public List<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; }
        public List<WaterQualityManagementPlanModelingApproach> WaterQualityManagementPlanModelingApproaches { get; }
        public string CalculatedWQMPAcreage { get; }

        public string TrashCaptureEffectiveness { get; }
        public HRUCharacteristicsViewData HRUCharacteristicsViewData { get; }
        public List<HtmlString> ParameterizationErrors { get; }

        public FieldDefinitionType FieldDefinitionForPercentOfSiteTreated { get; }
        public FieldDefinitionType FieldDefinitionForPercentCaptured { get; }
        public FieldDefinitionType FieldDefinitionForPercentRetained { get; }
        public FieldDefinitionType FieldDefinitionForFullyParameterized { get; }
        public FieldDefinitionType FieldDefinitionForDelineationStatus { get; }
        public FieldDefinitionType FieldDefinitionForDryWeatherFlowOverride { get; }

        public ModeledPerformanceViewData ModeledPerformanceViewData { get; }

        public bool AnyDetailedBMPsNotFullyParameterized { get; }
        public bool AllDetailedBMPsNotFullyParameterized { get; }
        public bool UsesDetailedModelingApproach { get; }
        public bool AllSimpleBMPsNotFullyParameterized { get; set; }
        public bool AnySimpleBMPsNotFullyParameterized { get; set; }

        public string EditUrl { get; }
        public string EditNotesUrl { get; }
        public UrlTemplate<int> StormwaterJurisdictionDetailUrlTemplate { get; }
        public UrlTemplate<int> TreatmentBMPDetailUrlTemplate { get; }
        public UrlTemplate<int> VerifyDetailUrlTemplate { get; }
        public UrlTemplate<int> VerifyDeleteUrlTemplate { get; }
        public UrlTemplate<int> DocumentEditUrlTemplate { get; }
        public UrlTemplate<int> DocumentDeleteUrlTemplate { get; }
        public bool HasWaterQualityManagementPlanBoundary { get; }
        public List<EFModels.Entities.WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan,
            WaterQualityManagementPlanVerify? waterQualityManagementPlanVerifyDraft, MapInitJson mapInitJson,
            List<EFModels.Entities.TreatmentBMP> treatmentBMPs,
            ParcelGridSpec parcelGridSpec, List<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifies,
            List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBmPs,
            List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBmPs,
            HRUCharacteristicsViewData hruCharacteristicsViewData,
            List<WaterQualityManagementPlanModelingApproach> waterQualityManagementPlanModelingApproaches,
            ModeledPerformanceViewData modeledPerformanceViewData,
            IEnumerable<IGrouping<int, SourceControlBMP>> sourceControlBMPs, List<QuickBMP> quickBMPs, QuickBMPGridSpec quickBMPGridSpec, bool hasWaterQualityManagementPlanBoundary, Dictionary<int, EFModels.Entities.Delineation?> treatmentBMPDelineationsDict, List<EFModels.Entities.WaterQualityManagementPlanDocument> waterQualityManagementPlanDocuments, double calculatedWqmpAcreage)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            PageTitle = WaterQualityManagementPlan.WaterQualityManagementPlanName;
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());

            CurrentPersonCanManageWaterQualityManagementPlans = new WaterQualityManagementPlanManageFeature()
                .HasPermission(currentPerson, waterQualityManagementPlan)
                .HasPermission;
            WqmpModelingApproachUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.WqmpModelingOptions());

            EditUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.Edit(WaterQualityManagementPlan));
            EditNotesUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.EditNotes(WaterQualityManagementPlan));
            EditInventoriedBMPsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.EditTreatmentBMPs(WaterQualityManagementPlan));
            EditSimplifiedStructuralBMPsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.EditSimplifiedStructuralBMPs(WaterQualityManagementPlan));
            EditSourceControlBMPsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.EditSourceControlBMPs(WaterQualityManagementPlan));
            EditModelingApproachUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.EditModelingApproach(WaterQualityManagementPlan));
            EditParcelsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.EditParcels(WaterQualityManagementPlan));
            RefineAreaUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.RefineArea(WaterQualityManagementPlan));
            NewDocumentUrl = SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(LinkGenerator, x => x.New(waterQualityManagementPlan));
            TreatmentBMPDetailUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.Detail(UrlTemplate.Parameter1Int)));
            VerifyDetailUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.WqmpVerify(UrlTemplate.Parameter1Int)));
            VerifyDeleteUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.DeleteVerify(UrlTemplate.Parameter1Int)));
            DocumentEditUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.Edit(UrlTemplate.Parameter1Int)));
            DocumentDeleteUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.Delete(UrlTemplate.Parameter1Int)));
            StormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));


            MapInitJson = mapInitJson;

            ParameterizationErrors = CheckForParameterizationErrors(waterQualityManagementPlan);

            ParcelGridSpec = parcelGridSpec;
            ParcelGridName = "parcelGrid";
            ParcelGridDataUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, c =>
                c.ParcelsForWaterQualityManagementPlanGridData(waterQualityManagementPlan));

            HasSavedWqmpDraft = waterQualityManagementPlanVerifyDraft != null &&
                                waterQualityManagementPlanVerifyDraft.IsDraft;
            BeginOMVerificationRecordUrl = HasSavedWqmpDraft
                ? SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, c =>
                    c.EditWqmpVerifyModal(waterQualityManagementPlanVerifyDraft))
                : SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, c =>
                    c.NewWqmpVerify(waterQualityManagementPlan));

            WaterQualityManagementPlanVerifies = waterQualityManagementPlanVerifies;
            WaterQualityManagementPlanVerifyQuickBMPs = waterQualityManagementPlanVerifyQuickBmPs;
            WaterQualityManagementPlanVerifyTreatmentBMPs = waterQualityManagementPlanVerifyTreatmentBmPs;
            HRUCharacteristicsViewData = hruCharacteristicsViewData;

            TreatmentBMPs = treatmentBMPs;
            QuickBMPs = quickBMPs;
            QuickBMPGridSpec = quickBMPGridSpec;
            QuickBMPGridName = "quickBMPGrid";
            QuickBMPGridDataUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, c =>
                c.SimplifiedStructuralBMPsForWaterQualityManagementPlanGridData(waterQualityManagementPlan));

            HasWaterQualityManagementPlanBoundary = hasWaterQualityManagementPlanBoundary;
            SourceControlBMPs = sourceControlBMPs;
            TreatmentBMPDelineationsDict = treatmentBMPDelineationsDict;
            WaterQualityManagementPlanDocuments = waterQualityManagementPlanDocuments;

            // TODO: Never compare floating-point values to zero. We should establish an application-wide error tolerance and use that instead of the direct comparison
            CalculatedWQMPAcreage = calculatedWqmpAcreage != 0
                ? $"{Math.Round(calculatedWqmpAcreage, 2).ToString(CultureInfo.InvariantCulture)} acres"
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
            FieldDefinitionForDryWeatherFlowOverride = FieldDefinitionType.DryWeatherFlowOverrideID;

            UsesDetailedModelingApproach = WaterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID ==
                                           WaterQualityManagementPlanModelingApproach.Detailed
                                               .WaterQualityManagementPlanModelingApproachID;

            if (UsesDetailedModelingApproach)
            {
                AnyDetailedBMPsNotFullyParameterized = TreatmentBMPs.Any(x => !x.IsFullyParameterized(treatmentBMPDelineationsDict[x.TreatmentBMPID]));
                AllDetailedBMPsNotFullyParameterized = TreatmentBMPs.All(x => !x.IsFullyParameterized(treatmentBMPDelineationsDict[x.TreatmentBMPID]));
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

        //There are more errors to come I believe, that's why this is producing a list
        public List<HtmlString> CheckForParameterizationErrors(EFModels.Entities.WaterQualityManagementPlan waterQualityManagement)
        {
            var parameterizationErrors = new List<HtmlString>();

            if (!waterQualityManagement.TreatmentBMPs.Any() && !waterQualityManagement.QuickBMPs.Any() && waterQualityManagement.WaterQualityManagementPlanModelingApproachID != (int)WaterQualityManagementPlanModelingApproachEnum.Simplified)
            {
                parameterizationErrors.Add(new HtmlString("This WQMP is not associated with any inventoried Treatment BMPs and does not have any other treatment area assigned.No load reduction will be calculated for this WQMP."));
            }

            return parameterizationErrors;
        }

        public string DisplayDryWeatherFlowOverride(EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            return treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP != null ? treatmentBMP.TreatmentBMPModelingAttributeTreatmentBMP?.DryWeatherFlowOverride?.DryWeatherFlowOverrideDisplayName : DryWeatherFlowOverride.No.DryWeatherFlowOverrideDisplayName;
        }

        public string DisplayDryWeatherFlowOverride(QuickBMP quickBMP)
        {
            return quickBMP.DryWeatherFlowOverride != null ? quickBMP.DryWeatherFlowOverride?.DryWeatherFlowOverrideDisplayName : DryWeatherFlowOverride.No.DryWeatherFlowOverrideDisplayName;
        }
    }
}
