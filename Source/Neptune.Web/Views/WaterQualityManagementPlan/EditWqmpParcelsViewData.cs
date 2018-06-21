using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpParcelsViewData : NeptuneViewData
    {
        public Models.WaterQualityManagementPlan WaterQualityManagementPlan { get; }

        public EditWqmpParcelsViewData(Person currentPerson,
            Models.WaterQualityManagementPlan waterQualityManagementPlan) : base(currentPerson, StormwaterBreadCrumbEntity.WaterQualityManagementPlan)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c => c.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = waterQualityManagementPlan.GetDetailUrl();
            PageTitle = "Edit Associated Parcels";
        }
    }
}
