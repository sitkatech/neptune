using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.BootstrapWrappers;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Common.ModalDialog;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class IndexGridSpec : GridSpec<WaterQualityManagementPlanDetailedWithTreatmentBMPsAndQuickBMPs>
    {
        public IndexGridSpec(LinkGenerator linkGenerator, Person currentPerson)
        {
            var fieldDefinitionWaterQualityManagementPlan = FieldDefinitionType.WaterQualityManagementPlan;
            var waterQualityManagementPlanLabelSingular =
                fieldDefinitionWaterQualityManagementPlan.GetFieldDefinitionLabel();
            ObjectNameSingular = waterQualityManagementPlanLabelSingular;
            ObjectNamePlural = fieldDefinitionWaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            SaveFiltersInCookie = true;

            var waterQualityManagementPlanManageFeature = new WaterQualityManagementPlanManageFeature();
            var isAnonymousOrUnassigned = currentPerson.IsAnonymousOrUnassigned();

            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
            var editUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Edit(UrlTemplate.Parameter1Int)));
            var verifyDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.WqmpVerify(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            if (!currentPerson.IsAnonymousOrUnassigned())
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(
                        deleteUrlTemplate.ParameterReplace(x.WaterQualityManagementPlan.WaterQualityManagementPlanID),
                        waterQualityManagementPlanManageFeature
                            .HasPermission(currentPerson, x.WaterQualityManagementPlan.StormwaterJurisdictionID).HasPermission), 26,
                    DhtmlxGridColumnFilterType.None);
                Add(string.Empty,
                    x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(
                            editUrlTemplate.ParameterReplace(x.WaterQualityManagementPlan.WaterQualityManagementPlanID),
                            ModalDialogFormHelper.DefaultDialogWidth,
                            $"Edit {waterQualityManagementPlanLabelSingular}"),
                        waterQualityManagementPlanManageFeature.HasPermission(currentPerson, x.WaterQualityManagementPlan.StormwaterJurisdictionID)
                            .HasPermission),
                    26, DhtmlxGridColumnFilterType.None);
            }

            Add("Name", x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlan.WaterQualityManagementPlanID), x.WaterQualityManagementPlan.WaterQualityManagementPlanName), 300, DhtmlxGridColumnFilterType.Text);
            Add("Jurisdiction", x => isAnonymousOrUnassigned ? new HtmlString(x.WaterQualityManagementPlan.StormwaterJurisdictionName) :
                UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlan.StormwaterJurisdictionID), x.WaterQualityManagementPlan.StormwaterJurisdictionName), 150);
            Add("Priority", x => x.WaterQualityManagementPlan.WaterQualityManagementPlanPriorityDisplayName,
                100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Status", x => x.WaterQualityManagementPlan.WaterQualityManagementPlanStatusDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Development Type",
                x => x.WaterQualityManagementPlan.WaterQualityManagementPlanDevelopmentTypeDisplayName,
                100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Land Use", x => x.WaterQualityManagementPlan.WaterQualityManagementPlanLandUseDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Permit Term",
                x => x.WaterQualityManagementPlan.WaterQualityManagementPlanPermitTermDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Approval Date", x => x.WaterQualityManagementPlan.ApprovalDate, 120);
            Add("Date of Construction", x => x.WaterQualityManagementPlan.DateOfContruction, 120);
            Add(FieldDefinitionType.HydromodificationApplies.ToGridHeaderString(), x => x.WaterQualityManagementPlan.HydromodificationAppliesTypeDisplayName, 130,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Hydrologic Subarea", x => x.WaterQualityManagementPlan.HydrologicSubareaName, 120,
                DhtmlxGridColumnFilterType.SelectFilterStrict);

            if (!currentPerson.IsAnonymousOrUnassigned())
            {
                Add("Maintenance Contact Organization", x => x.WaterQualityManagementPlan.MaintenanceContactOrganization, 120);
                Add("Maintenance Contact Name", x => x.WaterQualityManagementPlan.MaintenanceContactName, 100);
                Add("Maintenance Contact Address", x => x.WaterQualityManagementPlan.MaintenanceContactAddressToString(), 200);
                Add("Maintenance Contact Phone", x => x.WaterQualityManagementPlan.MaintenanceContactPhone, 70);
            }
            
            Add("# of Inventoried BMPs", x => x.WaterQualityManagementPlan.TreatmentBMPCount, 100);
            Add("# of Simplified BMPs", x => x.WaterQualityManagementPlan.QuickBMPCount, 100);
            Add("Modeling Approach", x => x.WaterQualityManagementPlan.WaterQualityManagementPlanModelingApproachDisplayName,
                100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.FullyParameterized.ToGridHeaderString("Fully Parameterized?"), x =>
            {
                if (x.WaterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID ==
                    WaterQualityManagementPlanModelingApproach.Detailed.WaterQualityManagementPlanModelingApproachID)
                {
                    return x.TreatmentBMPsWithModelingAttributesAndDelineation.Any(y => y.TreatmentBMP.IsFullyParameterized(y.Delineation)) ? new HtmlString("Yes") : new HtmlString("No");
                }

                return x.QuickBMPs.Any(y => y.IsFullyParameterized())
                        ? new HtmlString("Yes")
                        : new HtmlString("No");
            }, 120);
            Add("# of Documents", x => x.WaterQualityManagementPlan.DocumentCount, 100);
            Add(FieldDefinitionType.HasAllRequiredDocuments.ToGridHeaderString(),
                x => (x.WaterQualityManagementPlan.HasRequiredDocuments ?? false)
                    ? BootstrapHtmlHelpers.MakeGlyphIconWithHiddenText("glyphicon-ok-circle", "Yes")
                    : new HtmlString("<span style='display:none;'>No</span>")
                , 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Record Number", x => x.WaterQualityManagementPlan.RecordNumber, 150);
            Add("Recorded Parcel Acreage", x => x.WaterQualityManagementPlan.RecordedWQMPAreaInAcres, 100);
            Add("Calculated WQMP Acreage", x => Math.Round(x.WaterQualityManagementPlan.CalculatedWQMPAcreage ?? 0, 1), 100);
            Add("Associated APNs", x => x.WaterQualityManagementPlan.AssociatedAPNs, 200);
            Add("Latest O&M Verification",
                x =>
                {
                    var verificationDate = x.WaterQualityManagementPlan.VerificationDate;
                    if(!verificationDate.HasValue)
                    {
                        return new HtmlString("N/A");
                    }

                    if (currentPerson.IsAnonymousOrUnassigned())
                    {
                        return new HtmlString(verificationDate.ToStringDate());
                    }

                    return new HtmlString(
                        $"<a href=\"{verifyDetailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlan.WaterQualityManagementPlanVerifyID.Value)}\" alt=\"{verificationDate}\" title=\"{verificationDate}\" >{verificationDate}</a>");
                }, 100);
            Add(FieldDefinitionType.TrashCaptureStatus.ToGridHeaderString(),
                x => x.WaterQualityManagementPlan.TrashCaptureStatusTypeDisplayName, 130,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
 