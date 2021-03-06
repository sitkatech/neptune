﻿using System.Web;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentArea
{
    public class TrashMapAssetPanelViewData
    {
        public Models.OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea { get; }
        public bool UserHasViewDetailsPermission { get; }
        public HtmlString ScoreHtmlString { get; set; }

        public TrashMapAssetPanelViewData(Person currentPerson, Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            OnlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentArea;
            UserHasViewDetailsPermission = new OnlandVisualTrashAssessmentAreaViewFeature().HasPermission(currentPerson, onlandVisualTrashAssessmentArea).HasPermission;
            ScoreHtmlString = new HtmlString(OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore != null
                ? OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore
                    .OnlandVisualTrashAssessmentScoreDisplayName
                : "<p class='systemText'>No completed assessments</p>");
        }
    }
}