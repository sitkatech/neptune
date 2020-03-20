using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using LtInfo.Common.DbSpatial;
using Neptune.Web.Views.Shared.HRUCharacteristics;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public bool CurrentPersonCanManageWaterQualityManagementPlans { get; }
        public string EditWaterQualityManagementPlanTreatmentBmpsUrl { get; }
        public string EditWaterQualityManagementPlanParcelsUrl { get; }
        public string NewWaterQualityManagementPlanDocumentUrl { get; }
        public MapInitJson MapInitJson { get; }
        public ParcelGridSpec ParcelGridSpec { get; }
        public string ParcelGridName { get; }
        public string ParcelGridDataUrl { get; }
        public bool HasSavedWqmpDraft { get; }
        public string BeginWqmpOMVerificationRecordUrl { get; }

        public List<Models.TreatmentBMP> TreatmentBMPs { get; }
        public List<QuickBMP> QuickBMPs { get; }

        public IEnumerable<IGrouping<int, SourceControlBMP>> SourceControlBMPs { get; }
        public List<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; }
        public List<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs  { get; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; }
        public string CalculatedParcelArea {  get; }

        public string TrashCaptureEffectiveness { get; }
        public HRUCharacteristicsViewData HRUCharacteristicsViewData { get; }
        public List<HtmlString> ParameterizationErrors { get; }

        public Models.FieldDefinition FieldDefinitionForPercentOfSiteTreated { get; }
        public Models.FieldDefinition FieldDefinitionForPercentCaptured { get; }
        public Models.FieldDefinition FieldDefinitionForPercentRetained { get; }
        public Models.FieldDefinition FieldDefinitionForAreaWithinWQMP { get; }

        public DetailViewData(Person currentPerson, Models.WaterQualityManagementPlan waterQualityManagementPlan,
            WaterQualityManagementPlanVerify waterQualityManagementPlanVerifyDraft, MapInitJson mapInitJson,
            ParcelGridSpec parcelGridSpec, List<WaterQualityManagementPlanVerify> waterQualityManagementPlanVerifies,
            List<WaterQualityManagementPlanVerifyQuickBMP> waterQualityManagementPlanVerifyQuickBmPs,
            List<WaterQualityManagementPlanVerifyTreatmentBMP> waterQualityManagementPlanVerifyTreatmentBmPs, HRUCharacteristicsViewData hruCharacteristicsViewData)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            PageTitle = WaterQualityManagementPlan.WaterQualityManagementPlanName;
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());

            CurrentPersonCanManageWaterQualityManagementPlans = new WaterQualityManagementPlanManageFeature()
                .HasPermission(currentPerson, waterQualityManagementPlan)
                .HasPermission;
            currentPerson.IsManagerOrAdmin();
            EditWaterQualityManagementPlanTreatmentBmpsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpBmps(WaterQualityManagementPlan));
            EditWaterQualityManagementPlanParcelsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpParcels(WaterQualityManagementPlan));
            NewWaterQualityManagementPlanDocumentUrl =
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
            BeginWqmpOMVerificationRecordUrl = HasSavedWqmpDraft
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

            TreatmentBMPs = waterQualityManagementPlan.TreatmentBMPs.OrderBy(x => x.TreatmentBMPName).ToList();
            QuickBMPs = waterQualityManagementPlan.QuickBMPs.OrderBy(x => x.QuickBMPName).ToList();
            SourceControlBMPs = waterQualityManagementPlan.SourceControlBMPs
                .Where(x => x.SourceControlBMPNote != null || (x.IsPresent != null && x.IsPresent == true))
                .OrderBy(x => x.SourceControlBMPAttributeID)
                .GroupBy(x => x.SourceControlBMPAttribute.SourceControlBMPAttributeCategoryID);
            var calculatedParcelAcres =
                WaterQualityManagementPlan
                    .CalculateParcelAcreageTotal(); // This is 'calculated' by summing parcel recorded acres - not sure that's what's intended by calculated in this case

            // TODO: Never compare floating-point values to zero. We should establish an application-wide error tolerance and use that instead of the direct comparison
            CalculatedParcelArea = calculatedParcelAcres != 0
                ? $"{Math.Round(calculatedParcelAcres, 2).ToString(CultureInfo.InvariantCulture)} acres"
                : "No parcels have been associated with this WQMP";

            TrashCaptureEffectiveness = WaterQualityManagementPlan.TrashCaptureEffectiveness == null
                ? "Not Provided"
                : WaterQualityManagementPlan.TrashCaptureEffectiveness + "%";
            
            FieldDefinitionForPercentOfSiteTreated = Models.FieldDefinition.PercentOfSiteTreated;
            FieldDefinitionForPercentCaptured = Models.FieldDefinition.PercentCaptured;
            FieldDefinitionForPercentRetained = Models.FieldDefinition.PercentRetained;
            FieldDefinitionForAreaWithinWQMP = Models.FieldDefinition.AreaWithinWQMP;
        }

        public double? CalculateAreaWithinWQMP(Models.TreatmentBMP treatmentBMP)
        {
            if (treatmentBMP.Delineation != null &&
                WaterQualityManagementPlan.WaterQualityManagementPlanBoundary != null && treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType != null)
            {
                return treatmentBMP.Delineation.DelineationGeometry.Intersection(WaterQualityManagementPlan
                    .WaterQualityManagementPlanBoundary).Area * DbSpatialHelper.SquareMetersToAcres;
            }

            return null;
        }

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
    }
}
