using System;
using System.Linq;
using System.Web;

namespace Neptune.Web.Models
{
    public partial class OnlandVisualTrashAssessmentArea : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"OVTA Area {OnlandVisualTrashAssessmentAreaID}";
        }

        // todo: this is the old calculation, needs to be updated to take baseline vs progress into account
        public OnlandVisualTrashAssessmentScore CalculateScoreFromBackingData(bool calculateProgressScore)
        {
            if (OnlandVisualTrashAssessments.All(x => x.OnlandVisualTrashAssessmentStatusID != OnlandVisualTrashAssessmentStatus.Complete.OnlandVisualTrashAssessmentStatusID && x.IsProgressAssessment == calculateProgressScore))
            {
                return null;
            }

            var average = OnlandVisualTrashAssessments
                .Where(x => x.OnlandVisualTrashAssessmentStatusID ==
                            OnlandVisualTrashAssessmentStatus.Complete.OnlandVisualTrashAssessmentStatusID)
                .Average(x => x.OnlandVisualTrashAssessmentScore.NumericValue);

            var round = (int) Math.Round(average);

            return OnlandVisualTrashAssessmentScore.All.SingleOrDefault(x => x.NumericValue == round);
        }

        public HtmlString GetMostRecentAssessmentDateAsHtmlString()
        {
            var shortDateString = OnlandVisualTrashAssessments.Where(x => x.OnlandVisualTrashAssessmentStatusID ==
                                                                          OnlandVisualTrashAssessmentStatus.Complete
                                                                              .OnlandVisualTrashAssessmentStatusID).Max(x => x.CompletedDate)
                ?.ToShortDateString();
            return shortDateString != null ? new HtmlString(shortDateString) : new HtmlString("<p class='systemText'>No assessment completed</p>");
        }
    }
}