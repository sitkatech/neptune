using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.BootstrapWrappers;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Common.ModalDialog;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class IndexGridSpec : GridSpec<vWaterQualityManagementPlanDetailedWithTreatmentBMPsAndQuickBMPs>
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
                        deleteUrlTemplate.ParameterReplace(x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanID),
                        waterQualityManagementPlanManageFeature
                            .HasPermission(currentPerson, x.vWaterQualityManagementPlanDetailed.StormwaterJurisdictionID).HasPermission), 26,
                    DhtmlxGridColumnFilterType.None);
                Add(string.Empty,
                    x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(
                            editUrlTemplate.ParameterReplace(x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanID),
                            ModalDialogFormHelper.DefaultDialogWidth,
                            $"Edit {waterQualityManagementPlanLabelSingular}"),
                        waterQualityManagementPlanManageFeature.HasPermission(currentPerson, x.vWaterQualityManagementPlanDetailed.StormwaterJurisdictionID)
                            .HasPermission),
                    26, DhtmlxGridColumnFilterType.None);
            }

            Add("Name", x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanID), x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanName), 300, DhtmlxGridColumnFilterType.Text);
            Add("Jurisdiction", x => isAnonymousOrUnassigned ? new HtmlString(x.vWaterQualityManagementPlanDetailed.StormwaterJurisdictionName) :
                UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.vWaterQualityManagementPlanDetailed.StormwaterJurisdictionID), x.vWaterQualityManagementPlanDetailed.StormwaterJurisdictionName), 150);
            Add("Priority", x => x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanPriorityDisplayName,
                100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Status", x => x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanStatusDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Development Type",
                x => x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanDevelopmentTypeDisplayName,
                100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Land Use", x => x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanLandUseDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Permit Term",
                x => x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanPermitTermDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Approval Date", x => x.vWaterQualityManagementPlanDetailed.ApprovalDate, 120);
            Add("Date of Construction", x => x.vWaterQualityManagementPlanDetailed.DateOfContruction, 120);
            Add(FieldDefinitionType.HydromodificationApplies.ToGridHeaderString(), x => x.vWaterQualityManagementPlanDetailed.HydromodificationAppliesTypeDisplayName, 130,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Hydrologic Subarea", x => x.vWaterQualityManagementPlanDetailed.HydrologicSubareaName, 120,
                DhtmlxGridColumnFilterType.SelectFilterStrict);

            if (!currentPerson.IsAnonymousOrUnassigned())
            {
                Add("Maintenance Contact Organization", x => x.vWaterQualityManagementPlanDetailed.MaintenanceContactOrganization, 120);
                Add("Maintenance Contact Name", x => x.vWaterQualityManagementPlanDetailed.MaintenanceContactName, 100);
                Add("Maintenance Contact Address", x => x.vWaterQualityManagementPlanDetailed.MaintenanceContactAddressToString(), 200);
                Add("Maintenance Contact Phone", x => x.vWaterQualityManagementPlanDetailed.MaintenanceContactPhone, 70);
            }
            
            Add("# of Inventoried BMPs", x => x.vWaterQualityManagementPlanDetailed.TreatmentBMPCount, 100);
            Add("# of Simplified BMPs", x => x.vWaterQualityManagementPlanDetailed.QuickBMPCount, 100);
            Add("Modeling Approach", x => x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanModelingApproachDisplayName,
                100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.FullyParameterized.ToGridHeaderString("Fully Parameterized?"), x =>
            {
                if (x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanModelingApproachID ==
                    WaterQualityManagementPlanModelingApproach.Detailed.WaterQualityManagementPlanModelingApproachID)
                {
                    return x.TreatmentBMPsWithModelingAttributesAndDelineation.Any(y => y.TreatmentBMP.IsFullyParameterized(y.Delineation)) ? new HtmlString("Yes") : new HtmlString("No");
                }

                return x.QuickBMPs.Any(y => y.IsFullyParameterized())
                        ? new HtmlString("Yes")
                        : new HtmlString("No");
            }, 120);
            Add("# of Documents", x => x.vWaterQualityManagementPlanDetailed.DocumentCount, 100);
            Add(FieldDefinitionType.HasAllRequiredDocuments.ToGridHeaderString(),
                x => (x.vWaterQualityManagementPlanDetailed.HasRequiredDocuments ?? false)
                    ? BootstrapHtmlHelpers.MakeGlyphIconWithHiddenText("glyphicon-ok-circle", "Yes")
                    : new HtmlString("<span style='display:none;'>No</span>")
                , 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Record Number", x => x.vWaterQualityManagementPlanDetailed.RecordNumber, 150);
            Add("Recorded Parcel Acreage", x => x.vWaterQualityManagementPlanDetailed.RecordedWQMPAreaInAcres, 100);
            Add("Calculated WQMP Acreage", x => Math.Round(x.vWaterQualityManagementPlanDetailed.CalculatedWQMPAcreage ?? 0, 1), 100);
            Add("Associated APNs", x => x.vWaterQualityManagementPlanDetailed.AssociatedAPNs, 200);
            Add("Latest O&M Verification",
                x =>
                {
                    var verificationDate = x.vWaterQualityManagementPlanDetailed.VerificationDate;
                    if(!verificationDate.HasValue)
                    {
                        return new HtmlString("N/A");
                    }

                    if (currentPerson.IsAnonymousOrUnassigned())
                    {
                        return new HtmlString(verificationDate.ToStringDate());
                    }

                    return new HtmlString(
                        $"<a href=\"{verifyDetailUrlTemplate.ParameterReplace(x.vWaterQualityManagementPlanDetailed.WaterQualityManagementPlanVerifyID.Value)}\" alt=\"{verificationDate}\" title=\"{verificationDate}\" >{verificationDate}</a>");
                }, 100);
            Add(FieldDefinitionType.TrashCaptureStatus.ToGridHeaderString(),
                x => x.vWaterQualityManagementPlanDetailed.TrashCaptureStatusTypeDisplayName, 130,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
 