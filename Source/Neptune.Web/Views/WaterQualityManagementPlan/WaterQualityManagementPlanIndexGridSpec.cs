using System.Web;
using LtInfo.Common;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class WaterQualityManagementPlanIndexGridSpec : GridSpec<Models.WaterQualityManagementPlan>
    {
        public WaterQualityManagementPlanIndexGridSpec(Person currentPerson)
        {
            var waterQualityManagementPlanManageFeature = new WaterQualityManagementPlanManageFeature();
            var waterQualityManagementPlanDeleteFeature = new WaterQualityManagementPlanDeleteFeature();
            
            var currentUserCanManage = waterQualityManagementPlanManageFeature.HasPermissionByPerson(currentPerson);

            ObjectNameSingular = Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            SaveFiltersInCookie = true;

            if (currentUserCanManage)
            {
                Add(string.Empty, x =>
                    {
                        var userHasDeletePermission = waterQualityManagementPlanDeleteFeature.HasPermission(currentPerson, x).HasPermission;
                        return DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), userHasDeletePermission);
                    }, 26,
                    DhtmlxGridColumnFilterType.None);
                Add(string.Empty,
                    x =>
                    {
                        var userCanEdit = waterQualityManagementPlanManageFeature.HasPermission(currentPerson, x).HasPermission;
                        var modalDialogForm = new ModalDialogForm(x.GetEditUrl(), ModalDialogFormHelper.DefaultDialogWidth,
                            $"Edit {Models.FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabel()}");
                        return DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(modalDialogForm, userCanEdit);
                    },
                    26, DhtmlxGridColumnFilterType.None);
            }

            Add("Name", x => x.GetNameAsUrl(), 300);
            Add("Jurisdiction", x => x.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 150);
            Add("Priority", x => x.WaterQualityManagementPlanPriority.WaterQualityManagementPlanPriorityDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Status", x => x.WaterQualityManagementPlanStatus.WaterQualityManagementPlanStatusDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Development Type", x => x.WaterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Land Use", x => x.WaterQualityManagementPlanLandUse.WaterQualityManagementPlanLandUseDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Permit Term", x => x.WaterQualityManagementPlanPermitTerm?.WaterQualityManagementPlanPermitTermDisplayName, 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Approval Date", x => x.ApprovalDate, 120);
            Add("Date of Construction", x => x.DateOfContruction, 120);
            Add("Hydromodification Applies", x => x.HydromodificationApplies?.HydromodificationAppliesDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Hydrologic Subarea", x => x.HydrologicSubarea?.HydrologicSubareaDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Maintenance Contact Name", x => x.MaintenanceContactName, 100);
            Add("Maintenance Contact Organization", x => x.MaintenanceContactOrganization, 120);
            Add("Maintenance Contact Address", x => x.MaintenanceContactAddressToString(), 200);
            Add("Maintenance Contact Phone", x => x.MaintenanceContactPhone, 70);
            Add("# of Treatment BMPs", x => x.TreatmentBMPs.Count, 100);
            Add("# of Documents", x => x.WaterQualityManagementPlanDocuments.Count, 100);
            Add(Models.FieldDefinition.HasAllRequiredDocuments.ToGridHeaderString(),
                x => x.HasAllRequiredDocuments()
                    ? BootstrapHtmlHelpers.MakeGlyphIconWithHiddenText("glyphicon-ok-circle", "Yes")
                    : new HtmlString("<span style='display:none;'>No</span>")
                , 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
        }
    }
}
