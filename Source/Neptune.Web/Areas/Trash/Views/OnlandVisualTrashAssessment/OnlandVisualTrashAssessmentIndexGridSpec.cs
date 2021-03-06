﻿using System.Collections.Generic;
using System.Web;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OnlandVisualTrashAssessmentIndexGridSpec : GridSpec<Models.OnlandVisualTrashAssessment>
    {
        public OnlandVisualTrashAssessmentIndexGridSpec(Person currentPerson, bool showName)
        {
            Add(string.Empty, x => x.GetDetailUrlForGrid(currentPerson), 40, DhtmlxGridColumnFilterType.None);

            if (currentPerson.IsManagerOrAdmin())
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 25, DhtmlxGridColumnFilterType.None);
            }

            if (currentPerson.IsJurisdictionEditorOrManagerOrAdmin())
            {
                Add(string.Empty, x => x.GetEditUrlForGrid(currentPerson), 80, DhtmlxGridColumnFilterType.None);
                Add(string.Empty, x => x.OnlandVisualTrashAssessmentArea != null
                    ? DhtmlxGridHtmlHelpers.MakeModalDialogLink(
                        BootstrapHtmlHelpers.MakeGlyphIconWithHiddenText("glyphicon-plus", "Reassess this OVTA Area")
                            .ToString(),
                        x.OnlandVisualTrashAssessmentArea.GetBeginOVTAUrl(), 500, "Begin OVTA", true, "Begin", "Cancel",
                        new List<string>(), null, null)
                    : new HtmlString(""), 30, DhtmlxGridColumnFilterType.None);
            }

            if (showName)
            {
                Add("Assessment Area Name",
                    x => x.OnlandVisualTrashAssessmentArea?.GetDisplayNameAsDetailUrl(currentPerson) ?? new HtmlString("Not Set"), 170,
                    DhtmlxGridColumnFilterType.Html);
            }

            Add(FieldDefinition.AssessmentScore.ToGridHeaderString(), x => x.OnlandVisualTrashAssessmentScore?.OnlandVisualTrashAssessmentScoreDisplayName, 100,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Assessment Type", x => x.ToBaselineProgress(), 75,
                DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Last Assessment Date", x => x.CompletedDate, 120, DhtmlxGridColumnFormatType.Date);
            Add("Status", x => x.OnlandVisualTrashAssessmentStatus.OnlandVisualTrashAssessmentStatusDisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(FieldDefinition.Jurisdiction.ToGridHeaderString("Jurisdiction"), x => x.StormwaterJurisdiction?.GetDisplayNameAsDetailUrl() ?? new HtmlString("Not Set"), 170);

            Add("Created By", x => x.CreatedByPerson.GetFullNameFirstLastAsUrl(), 115,
                DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Created On", x => x.CreatedDate, 120, DhtmlxGridColumnFormatType.Date);
        }
    }
}
