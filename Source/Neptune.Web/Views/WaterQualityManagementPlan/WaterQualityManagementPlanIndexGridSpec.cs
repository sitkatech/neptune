using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class WaterQualityManagementPlanIndexGridSpec : GridSpec<Models.WaterQualityManagementPlan>
    {
        public WaterQualityManagementPlanIndexGridSpec(bool currentUserCanManage)
        {
            ObjectNameSingular = Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            SaveFiltersInCookie = true;

            if (currentUserCanManage)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true), 26,
                    DhtmlxGridColumnFilterType.None);
                Add(string.Empty,
                    x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditUrl(), ModalDialogFormHelper.DefaultDialogWidth, $"Edit {Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabel()}"), true),
                    26, DhtmlxGridColumnFilterType.None);
            }

            Add("Name", x => x.GetNameAsUrl(), 300);
            Add("Maintenance Contact User", x => x.MaintenanceUserPerson.GetFullNameFirstLastAsUrl(), 150, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Maintenance Contact Organization", x => x.MaintenanceOrganziation.GetDisplayNameAsUrl(), 150, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Priority", x => x.WaterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Status", x => x.WaterQualityManagementPlanStatus.WaterQualityManagementPlanStatusDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Development Type", x => x.WaterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Land Use", x => x.WaterQualityManagementPlanLandUse.WaterQualityManagementPlanLandUseDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("# of Treatment BMPs", x => x.TreatmentBMPs.Count, 50);
            Add("# of Documents", x => x.WaterQualityManagementPlanDocuments.Count, 50);
        }
    }
}
