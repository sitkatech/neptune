using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.BootstrapWrappers;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.HtmlHelperExtensions;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
{
    public class OnlandVisualTrashAssessmentAreaIndexGridSpec : GridSpec<EFModels.Entities.OnlandVisualTrashAssessmentArea>
    {
        public OnlandVisualTrashAssessmentAreaIndexGridSpec(LinkGenerator linkGenerator, Person currentPerson, ILookup<int, EFModels.Entities.OnlandVisualTrashAssessment>? ovtaAssessmentsLookup)
        {
            var stormwaterJurisdictionDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            var beginOvtaUrlTemplate = new UrlTemplate<int>(SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator, x => x.NewAssessment(UrlTemplate.Parameter1Int)));
            if (currentPerson.IsManagerOrAdmin())
            {
                var deleteUrlTemplate = new UrlTemplate<int>(
                    SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator, x => x.Delete(UrlTemplate.Parameter1Int)));
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.OnlandVisualTrashAssessmentAreaID), true, true), 25, DhtmlxGridColumnFilterType.None);
            }

            if (currentPerson.IsJurisdictionEditorOrManagerOrAdmin())
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeModalDialogLink(
                    BootstrapHtmlHelpers.MakeGlyphIconWithHiddenText("glyphicon-plus", "Reassess this OVTA Area"),
                    beginOvtaUrlTemplate.ParameterReplace(x.OnlandVisualTrashAssessmentAreaID), 500, "Begin OVTA", true, "Begin", "Cancel",
                    new List<string>(), null, null), 30, DhtmlxGridColumnFilterType.None);
            }

            var detailUrlTemplate = new UrlTemplate<int>(
                SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));

            Add("Assessment Area Name",
                x =>
                {
                    if (!new OnlandVisualTrashAssessmentAreaViewFeature()
                            .HasPermission(currentPerson, x).HasPermission)
                    {
                        return new HtmlString(x.OnlandVisualTrashAssessmentAreaName);
                    }

                    return new HtmlString(
                        $"<a href=\"{detailUrlTemplate.ParameterReplace(x.OnlandVisualTrashAssessmentAreaID)}\" alt=\"{x.OnlandVisualTrashAssessmentAreaName}\" title=\"{x.OnlandVisualTrashAssessmentAreaName}\" >{x.OnlandVisualTrashAssessmentAreaName}</a>");
                }, 170,
                DhtmlxGridColumnFilterType.Html);

            Add(FieldDefinitionType.BaselineScore.ToGridHeaderString(), x => x.GetBaselineScoreAsHtmlString(), 150,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(FieldDefinitionType.ProgressScore.ToGridHeaderString(), x => x.GetProgressScoreAsHtmlString(), 150,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Number of Assessments Completed", x => ovtaAssessmentsLookup?[x.OnlandVisualTrashAssessmentAreaID].Count(), 170,
                DhtmlxGridColumnAggregationType.Total);
            Add("Last Assessment Date", x => ovtaAssessmentsLookup?[x.OnlandVisualTrashAssessmentAreaID].Where(y =>
                y.OnlandVisualTrashAssessmentStatusID == (int)OnlandVisualTrashAssessmentStatusEnum.Complete).Max(y => y.CompletedDate), 120, DhtmlxGridColumnFormatType.Date);
            Add(FieldDefinitionType.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => UrlTemplate.MakeHrefString(stormwaterJurisdictionDetailUrlTemplate.ParameterReplace(x.StormwaterJurisdictionID), x.StormwaterJurisdiction.GetOrganizationDisplayName()), 170);
            Add("Description", x => x.AssessmentAreaDescription, 170);
        }
    }
}