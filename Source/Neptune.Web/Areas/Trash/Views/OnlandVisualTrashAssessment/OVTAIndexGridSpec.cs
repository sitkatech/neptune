using System.Collections.Generic;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTAIndexGridSpec : GridSpec<Models.OnlandVisualTrashAssessment>
    {
        public OVTAIndexGridSpec(Person currentPerson, bool showDelete, bool showEdit, bool showName)
        {
            if (showDelete)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), new OnlandVisualTrashAssessmentDeleteFeature().HasPermission(currentPerson, x).HasPermission, true), 30, DhtmlxGridColumnFilterType.None);
            }
            if (showEdit)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(x.GetEditUrl(), new OnlandVisualTrashAssessmentViewFeature().HasPermission(currentPerson, x).HasPermission), 30, DhtmlxGridColumnFilterType.None);

            }

            Add(string.Empty, x => (x.OnlandVisualTrashAssessmentArea != null
                ? DhtmlxGridHtmlHelpers.MakeModalDialogLink(
                    BootstrapHtmlHelpers.MakeGlyphIconWithHiddenText("glyphicon-plus", "Reassess this OVTA Area").ToString(),
                    x.OnlandVisualTrashAssessmentArea.GetBeginOVTAUrl(), 500, "Begin OVTA",
                    new OnlandVisualTrashAssessmentAreaViewFeature()
                        .HasPermission(currentPerson, x.OnlandVisualTrashAssessmentArea).HasPermission, "Begin", "Cancel",
                    new List<string>(), null, null)
                : new HtmlString("")), 30, DhtmlxGridColumnFilterType.None);

            Add(FieldDefinition.OVTAScore.ToGridHeaderString(), x => x.OnlandVisualTrashAssessmentScore?.OnlandVisualTrashAssessmentScoreDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Completed On", x => x.CompletedDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Status", x => x.OnlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => x.StormwaterJurisdiction?.GetDisplayNameAsDetailUrl() ?? new HtmlString("Not Set"), 170);

            if (showName)
            {
                Add("Assessment Area Name",
                    x => x.OnlandVisualTrashAssessmentArea?.GetDisplayNameAsDetailUrl(currentPerson) ?? new HtmlString("Not Set"), 170,
                    DhtmlxGridColumnFilterType.SelectFilterStrict);
            }

            Add("Created By", x => x.CreatedByPerson.GetFullNameFirstLastAsUrl(), 90,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Created On", x => x.CreatedDate, 120, DhtmlxGridColumnFormatType.Date);
        }
    }
}
