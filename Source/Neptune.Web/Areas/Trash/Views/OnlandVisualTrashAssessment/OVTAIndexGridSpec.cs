using System.Web;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTAIndexGridSpec : GridSpec<Models.OnlandVisualTrashAssessment>
    {
        public OVTAIndexGridSpec(Person currentPerson, bool showDelete, bool showEdit)
        {
            if (showDelete)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap("", false, true), 30, DhtmlxGridColumnFilterType.None);
            }
            if (showEdit)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(x.GetEditUrl(), x.OnlandVisualTrashAssessmentStatus == OnlandVisualTrashAssessmentStatus.InProgress), 30, DhtmlxGridColumnFilterType.None);
            }

            Add("Created By", x => x.CreatedByPerson.GetFullNameFirstLastAsUrl(), 90,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Created On", x => x.CreatedDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Completed On", x => x.CompletedDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Score", x => x.OnlandVisualTrashAssessmentScore?.OnlandVisualTrashAssessmentScoreDisplayName, 60,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => x.StormwaterJurisdiction?.GetDisplayNameAsDetailUrl() ?? new HtmlString("Not Set"), 170);
            Add("Assessment Area Name", x => x.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName ?? "Not Set", 170, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Status", x=>x.OnlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
