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
        public bool CurrentPersonCanManage { get; }
        public string NewWaterQualityManagementPlanDocumentUrl { get; }
        public TreatmentBMPGridSpec TreatmentBmpGridSpec { get; }
        public string TreatmentBmpGridName { get; }
        public string TreatmentBmpGridDataUrl { get; }
        public MapInitJson MapInitJson { get; }

        public DetailViewData(Person currentPerson, Models.WaterQualityManagementPlan waterQualityManagementPlan,
            TreatmentBMPGridSpec treatmentBMPGridSpec, MapInitJson mapInitJson)
            : base(currentPerson, StormwaterBreadCrumbEntity.WaterQualityManagementPlan)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            PageTitle = WaterQualityManagementPlan.WaterQualityManagementPlanName;
            EntityName = $"{Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x => x.Index());

            CurrentPersonCanManage = new WaterQualityManagementPlanManageFeature()
                .HasPermission(currentPerson, waterQualityManagementPlan).HasPermission;
            NewWaterQualityManagementPlanDocumentUrl =
                SitkaRoute<WaterQualityManagementPlanDocumentController>.BuildUrlFromExpression(c =>
                    c.New(waterQualityManagementPlan));
            TreatmentBmpGridSpec = treatmentBMPGridSpec;
            TreatmentBmpGridName = "treatmentBmpGrid";
            TreatmentBmpGridDataUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.TreatmentBmpsForWaterQualityManagementPlanGridData(waterQualityManagementPlan));
            MapInitJson = mapInitJson;
        }
    }
}
