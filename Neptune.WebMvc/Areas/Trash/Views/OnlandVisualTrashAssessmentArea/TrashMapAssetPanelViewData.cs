using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Areas.Trash.Controllers;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessmentArea
{
    public class TrashMapAssetPanelViewData
    {
        public EFModels.Entities.OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea { get; }
        public bool UserHasViewDetailsPermission { get; }
        public HtmlString ScoreHtmlString { get; set; }
        public string DetailUrl { get; }
        public DateTime? MostRecentAssessmentDate { get; }
        public string BeginOVTAUrl { get; }

        public TrashMapAssetPanelViewData(LinkGenerator linkGenerator, Person currentPerson,
            EFModels.Entities.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea,
            DateTime? mostRecentAssessmentCompletedDate)
        {
            OnlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentArea;
            UserHasViewDetailsPermission = new OnlandVisualTrashAssessmentAreaViewFeature().HasPermission(currentPerson, onlandVisualTrashAssessmentArea).HasPermission;
            ScoreHtmlString = new HtmlString(OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore != null
                ? OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore
                    .OnlandVisualTrashAssessmentScoreDisplayName
                : "<p class='systemText'>No completed assessments</p>");
            DetailUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator,
                x => x.Detail(onlandVisualTrashAssessmentArea));
            MostRecentAssessmentDate = mostRecentAssessmentCompletedDate;
            BeginOVTAUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator,
                        x => x.NewAssessment(onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID));
        }
    }
}