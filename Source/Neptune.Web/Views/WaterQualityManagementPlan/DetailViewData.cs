using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMP;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class DetailViewData : NeptuneViewData
    {
        public Models.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public bool CurrentPersonCanManageWaterQualityManagementPlans { get; }
        public string EditWaterQualityManagementPlanTreatmentBmpsUrl { get; }
        public string EditWaterQualityManagementPlanParcelsUrl { get; }
        public string NewWaterQualityManagementPlanDocumentUrl { get; }
        public TreatmentBMPGridSpec TreatmentBmpGridSpec { get; }
        public string TreatmentBmpGridName { get; }
        public string TreatmentBmpGridDataUrl { get; }
        public MapInitJson MapInitJson { get; }
        public ParcelGridSpec ParcelGridSpec { get; }
        public string ParcelGridName { get; }
        public string ParcelGridDataUrl { get; }
        public string EditWqmpOMVerificationRecordUrl { get; }

        public DetailViewData(Person currentPerson, Models.WaterQualityManagementPlan waterQualityManagementPlan,
            TreatmentBMPGridSpec treatmentBMPGridSpec, MapInitJson mapInitJson, ParcelGridSpec parcelGridSpec)
            : base(currentPerson, StormwaterBreadCrumbEntity.WaterQualityManagementPlan)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            PageTitle = WaterQualityManagementPlan.WaterQualityManagementPlanName;
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());

            CurrentPersonCanManageWaterQualityManagementPlans = new WaterQualityManagementPlanManageFeature()
                .HasPermission(currentPerson, waterQualityManagementPlan)
                .HasPermission;
            EditWaterQualityManagementPlanTreatmentBmpsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpTreatmentBmps(WaterQualityManagementPlan));
            EditWaterQualityManagementPlanParcelsUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                    c.EditWqmpParcels(WaterQualityManagementPlan));
            NewWaterQualityManagementPlanDocumentUrl =
                SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(c =>
                    c.New(waterQualityManagementPlan));
            TreatmentBmpGridSpec = treatmentBMPGridSpec;
            TreatmentBmpGridName = "treatmentBmpGrid";
            TreatmentBmpGridDataUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.TreatmentBmpsForWaterQualityManagementPlanGridData(waterQualityManagementPlan));
            MapInitJson = mapInitJson;
            ParcelGridSpec = parcelGridSpec;
            ParcelGridName = "parcelGrid";
            ParcelGridDataUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.ParcelsForWaterQualityManagementPlanGridData(waterQualityManagementPlan));
            EditWqmpOMVerificationRecordUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.EditWqmpOMVerificationRecord(waterQualityManagementPlan));
        }
    }
}
