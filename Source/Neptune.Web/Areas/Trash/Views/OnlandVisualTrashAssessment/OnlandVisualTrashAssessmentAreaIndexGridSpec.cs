using System.Collections.Generic;
using System.Web;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OnlandVisualTrashAssessmentAreaIndexGridSpec : GridSpec<Models.OnlandVisualTrashAssessmentArea>
    {
        public OnlandVisualTrashAssessmentAreaIndexGridSpec(Person currentPerson)
        {
            Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeModalDialogLink(
                BootstrapHtmlHelpers.MakeGlyphIconWithHiddenText("glyphicon-plus", "Reassess this OVTA Area").ToString(),
                x.GetBeginOVTAUrl(), 500, "Begin OVTA",
                new OnlandVisualTrashAssessmentAreaViewFeature()
                    .HasPermission(currentPerson, x).HasPermission, "Begin", "Cancel",
                new List<string>(), null, null), 30, DhtmlxGridColumnFilterType.None);

            Add("Assessment Area Name",
                x => x.GetDisplayNameAsDetailUrl(currentPerson) ?? new HtmlString("Not Set"), 170,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);

            Add(FieldDefinition.OVTAScore.ToGridHeaderString(), x => x.GetScoreAsHtmlString(), 150,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Number of Assessments Completed", x => x.OnlandVisualTrashAssessments.Count, 170,
                DhtmlxGridColumnAggregationType.Total);
            Add("Last Assessment Date", x => x.GetLastAssessmentDate(), 120, DhtmlxGridColumnFormatType.Date);
            Add(FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => x.StormwaterJurisdiction?.GetDisplayNameAsDetailUrl() ?? new HtmlString("Not Set"), 170);
        }
    }
}