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

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
{
    public class OnlandVisualTrashAssessmentIndexGridSpec : GridSpec<Neptune.EFModels.Entities.OnlandVisualTrashAssessment>
    {
        public OnlandVisualTrashAssessmentIndexGridSpec(LinkGenerator linkGenerator, Person currentPerson, bool showName)
        {
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var detailUrlTemplate =
                new UrlTemplate<int>(
                    SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var userDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var beginOvtaUrlTemplate = new UrlTemplate<int>(SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator, x => x.NewAssessment(UrlTemplate.Parameter1Int)));

            Add(string.Empty, x =>
            {
                if (x.OnlandVisualTrashAssessmentStatus == OnlandVisualTrashAssessmentStatus.Complete)
                {
                    return new HtmlString(
                        $"<a class='gridButton' href='{detailUrlTemplate.ParameterReplace(x.OnlandVisualTrashAssessmentID)}'>View</a>");
                }

                return new HtmlString("");
            }, 40, DhtmlxGridColumnFilterType.None);

            if (currentPerson.IsManagerOrAdmin())
            {
                var deleteUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.OnlandVisualTrashAssessmentID), true, true), 25, DhtmlxGridColumnFilterType.None);
            }

            if (currentPerson.IsJurisdictionEditorOrManagerOrAdmin())
            {
                var editUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x =>
                            x.RecordObservations(UrlTemplate.Parameter1Int)));
                var editStatusUrlTemplate = new UrlTemplate<int>(
                    SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x =>
                        x.EditStatusToAllowEdit(UrlTemplate.Parameter1Int)));
                Add(string.Empty, x =>
                {
                    var userCanEdit = new OnlandVisualTrashAssessmentEditStatusFeature()
                        .HasPermission(currentPerson, x)
                        .HasPermission;
                    if (!userCanEdit) return new HtmlString(string.Empty);

                    return x.OnlandVisualTrashAssessmentStatus ==
                           OnlandVisualTrashAssessmentStatus.Complete
                        ? ModalDialogFormHelper.ModalDialogFormLink("Return to Edit", editStatusUrlTemplate.ParameterReplace(x.OnlandVisualTrashAssessmentID),
                            $"Return to Edit On-land Visual Trash Assessment for {x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName}",
                            500, "Continue", "Cancel", new List<string> { "gridButton" },
                            null, null) : DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(editUrlTemplate.ParameterReplace(x.OnlandVisualTrashAssessmentID));
                }, 80, DhtmlxGridColumnFilterType.None);
                Add(string.Empty, x => x.OnlandVisualTrashAssessmentArea != null
                    ? DhtmlxGridHtmlHelpers.MakeModalDialogLink(
                        BootstrapHtmlHelpers.MakeGlyphIconWithHiddenText("glyphicon-plus",
                            "Reassess this OVTA Area"),
                        beginOvtaUrlTemplate.ParameterReplace(x.OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID), 500, "Begin OVTA", true, "Begin",
                        "Cancel",
                        new List<string>(), null, null)
                    : new HtmlString(""), 30, DhtmlxGridColumnFilterType.None);
            }

            if (showName)
            {
                var assessmentAreaDetailUrlTemplate = new UrlTemplate<int>(
                    SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
                Add("Assessment Area Name",
                    x =>
                    {
                        var onlandVisualTrashAssessmentArea = x.OnlandVisualTrashAssessmentArea;
                        if (onlandVisualTrashAssessmentArea == null)
                        {
                            return new HtmlString("Not Set");
                        }
                        if (!new OnlandVisualTrashAssessmentAreaViewFeature()
                                .HasPermission(currentPerson, onlandVisualTrashAssessmentArea).HasPermission)
                        {
                            return new HtmlString(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName);
                        }

                        return new HtmlString(
                            $"<a href=\"{assessmentAreaDetailUrlTemplate.ParameterReplace(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID)}\" alt=\"{onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName}\" title=\"{onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName}\" >{onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName}</a>");
                    }, 170,
                    DhtmlxGridColumnFilterType.Html);
            }

            Add(FieldDefinitionType.AssessmentScore.ToGridHeaderString(), x => x.OnlandVisualTrashAssessmentScore?.OnlandVisualTrashAssessmentScoreDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Assessment Type", x => x.ToBaselineProgress(), 75,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Last Assessment Date", x => x.CompletedDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Status", x => x.OnlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString("Jurisdiction"), x =>
                UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.StormwaterJurisdiction.GetOrganizationDisplayName()), 170);

            Add("Created By", x => UrlTemplate.MakeHrefString(userDetailUrlTemplate.ParameterReplace(x.CreatedByPersonID), x.CreatedByPerson.GetFullNameFirstLast()), 115,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Created On", x => x.CreatedDate, 120, DhtmlxGridColumnFormatType.Date);
        }
    }
}
