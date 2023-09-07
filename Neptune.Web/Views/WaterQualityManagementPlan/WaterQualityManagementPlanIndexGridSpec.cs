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
    public class WaterQualityManagementPlanIndexGridSpec : GridSpec<EFModels.Entities.WaterQualityManagementPlan>
    {
        public WaterQualityManagementPlanIndexGridSpec(LinkGenerator linkGenerator, Person currentPerson)
        {
            var fieldDefinitionWaterQualityManagementPlan = FieldDefinitionType.WaterQualityManagementPlan;
            var waterQualityManagementPlanLabelSingular =
                fieldDefinitionWaterQualityManagementPlan.GetFieldDefinitionLabel();
            ObjectNameSingular = waterQualityManagementPlanLabelSingular;
            ObjectNamePlural = fieldDefinitionWaterQualityManagementPlan.GetFieldDefinitionLabelPluralized();
            SaveFiltersInCookie = true;

            var waterQualityManagementPlanDeleteFeature = new WaterQualityManagementPlanDeleteFeature();
            var qualityManagementPlanManageFeature = new WaterQualityManagementPlanManageFeature();
            var isAnonymousOrUnassigned = currentPerson.IsAnonymousOrUnassigned();

            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
            var editUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Edit(UrlTemplate.Parameter1Int)));
            var verifyDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.WqmpVerify(UrlTemplate.Parameter1Int)));
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));


            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.WaterQualityManagementPlanID),
                    waterQualityManagementPlanDeleteFeature.HasPermission(currentPerson, x).HasPermission), 26,
                DhtmlxGridColumnFilterType.None);
            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(editUrlTemplate.ParameterReplace(x.WaterQualityManagementPlanID),
                        ModalDialogFormHelper.DefaultDialogWidth,
                        $"Edit {waterQualityManagementPlanLabelSingular}"),
                    qualityManagementPlanManageFeature.HasPermission(currentPerson, x).HasPermission),
                26, DhtmlxGridColumnFilterType.None);
            Add("Name", x => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(x.WaterQualityManagementPlanID), x.WaterQualityManagementPlanName), 300, DhtmlxGridColumnFilterType.Text);
            Add("Jurisdiction", x => isAnonymousOrUnassigned ? new HtmlString(x.StormwaterJurisdiction.GetOrganizationDisplayName()) :
                UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.StormwaterJurisdiction.GetOrganizationDisplayName()), 150);
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
            Add(FieldDefinitionType.HydromodificationApplies.ToGridHeaderString(), x => x.HydromodificationAppliesType?.HydromodificationAppliesTypeDisplayName, 130,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Hydrologic Subarea", x => x.HydrologicSubarea?.HydrologicSubareaName, 120,
                DhtmlxGridColumnFilterType.SelectFilterStrict);

            if (!currentPerson.IsAnonymousOrUnassigned())
            {
                Add("Maintenance Contact Organization", x => x.MaintenanceContactOrganization, 120);
                Add("Maintenance Contact Name", x => x.MaintenanceContactName, 100);
                Add("Maintenance Contact Address", x => x.MaintenanceContactAddressToString(), 200);
                Add("Maintenance Contact Phone", x => x.MaintenanceContactPhone, 70);
            }
            
            Add("# of Inventoried BMPs", x => currentPerson.GetInventoriedBMPsForWQMP(x).Count(), 100);
            Add("# of Simplified BMPs", x => x.QuickBMPs.Count, 100);
            Add("Modeling Approach", x => x.WaterQualityManagementPlanModelingApproach.WaterQualityManagementPlanModelingApproachDisplayName,
                100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.FullyParameterized.ToGridHeaderString("Fully Parameterized?"), x => x.IsFullyParameterized() ? new HtmlString("Yes") : new HtmlString("No"), 120);
            Add("# of Documents", x => x.WaterQualityManagementPlanDocuments.Count, 100);
            Add(FieldDefinitionType.HasAllRequiredDocuments.ToGridHeaderString(),
                x => x.HasAllRequiredDocuments()
                    ? BootstrapHtmlHelpers.MakeGlyphIconWithHiddenText("glyphicon-ok-circle", "Yes")
                    : new HtmlString("<span style='display:none;'>No</span>")
                , 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict, DhtmlxGridColumnAlignType.Center);
            Add("Record Number", x => x.RecordNumber, 150);
            Add("Recorded Parcel Acreage", x => x.RecordedWQMPAreaInAcres, 100);
            Add("Calculated WQMP Acreage", x => Math.Round(x.CalculateTotalAcreage(), 1), 100);
            Add("Associated APNs", x => string.Join(", ", x.WaterQualityManagementPlanParcels.Select(y => y.Parcel.ParcelNumber)), 200);
            Add("Latest O&M Verification",
                x =>
                {
                    var verificationDate = x.GetLatestOandMVerificationDate();
                    if(string.IsNullOrWhiteSpace(verificationDate))
                    {
                        return new HtmlString("N/A");
                    }

                    if (currentPerson.IsAnonymousOrUnassigned())
                    {
                        return new HtmlString(verificationDate);
                    }

                    var waterQualityManagementPlanVerify = x.WaterQualityManagementPlanVerifies.Single(y =>
                        y.LastEditedDate == x.WaterQualityManagementPlanVerifies.Select(z => z.LastEditedDate).Max());
                    return new HtmlString(
                        $"<a href=\"{verifyDetailUrlTemplate.ParameterReplace(waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID)}\" alt=\"{verificationDate}\" title=\"{verificationDate}\" >{verificationDate}</a>");
                }, 100);
            Add(FieldDefinitionType.TrashCaptureStatus.ToGridHeaderString(),
                x => x.TrashCaptureStatusType.TrashCaptureStatusTypeDisplayName, 130,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
 