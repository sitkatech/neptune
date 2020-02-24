using System;
using System.Web;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using Microsoft.Ajax.Utilities;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class WaterQualityManagementPlanIndexGridSpec : GridSpec<Models.WaterQualityManagementPlan>
    {
        public WaterQualityManagementPlanIndexGridSpec(Person currentPerson)
        {
            var fieldDefinitionWaterQualityManagementPlan = Models.FieldDefinition.WaterQualityManagementPlan;
            var waterQualityManagementPlanLabelSingular =
                fieldDefinitionWaterQualityManagementPlan.GetFieldDefinitionLabel();
            ObjectNameSingular = waterQualityManagementPlanLabelSingular;
            ObjectNamePlural = fieldDefinitionWaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            SaveFiltersInCookie = true;

            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(),
                    true), 26,
                DhtmlxGridColumnFilterType.None);
            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditUrl(),
                        ModalDialogFormHelper.DefaultDialogWidth,
                        $"Edit {waterQualityManagementPlanLabelSingular}"),
                    true),
                26, DhtmlxGridColumnFilterType.None);
            Add("Name", x => x.GetDisplayNameAsUrl(), 300, DhtmlxGridColumnFilterType.Text);
            Add("Jurisdiction", x => x.StormwaterJurisdiction.GetDisplayNameAsDetailUrl(), 150);
            Add("Priority", x => x.WaterQualityManagementPlanPriority?.WaterQualityManagementPlanPriorityDisplayName,
                100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Status", x => x.WaterQualityManagementPlanStatus?.WaterQualityManagementPlanStatusDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Development Type",
                x => x.WaterQualityManagementPlanDevelopmentType?.WaterQualityManagementPlanDevelopmentTypeDisplayName,
                100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Land Use", x => x.WaterQualityManagementPlanLandUse?.WaterQualityManagementPlanLandUseDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Permit Term",
                x => x.WaterQualityManagementPlanPermitTerm?.WaterQualityManagementPlanPermitTermDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Approval Date", x => x.ApprovalDate, 120);
            Add("Date of Construction", x => x.DateOfContruction, 120);
            Add("Hydromodification Applies", x => x.HydromodificationApplies?.HydromodificationAppliesDisplayName, 120,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Hydrologic Subarea", x => x.HydrologicSubarea?.HydrologicSubareaName, 120,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
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
            Add("Record Number", x => x.RecordNumber, 150);
            Add("Recorded Parcel Acreage", x => x.RecordedWQMPAreaInAcres, 100);
            Add("Calculated Parcel Acreage", x => Math.Round(x.CalculateParcelAcreageTotal(), 1).ToString(), 100);
            Add("Latest O&M Verification",
                x => new HtmlString(!x.GetLatestOandMVerificationDate().IsNullOrWhiteSpace()
                    ? $"<a href=\"{x.GetLatestOandMVerificationUrl()}\" alt=\"{x.GetLatestOandMVerificationDate()}\" title=\"{x.GetLatestOandMVerificationDate()}\" >{x.GetLatestOandMVerificationDate()}</a>"
                    : "N/A"), 100);
            Add(Models.FieldDefinition.TrashCaptureStatus.ToGridHeaderString(),
                x => x.TrashCaptureStatusType.TrashCaptureStatusTypeDisplayName, 130,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
 