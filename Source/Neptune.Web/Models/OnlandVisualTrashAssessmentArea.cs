using System;
using System.Linq;

namespace Neptune.Web.Models
{
    public partial class OnlandVisualTrashAssessmentArea : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"OVTA Area {OnlandVisualTrashAssessmentAreaID}";
        }

        public OnlandVisualTrashAssessmentScore GetScore()
        {
            var average = OnlandVisualTrashAssessments
                .Where(x => x.OnlandVisualTrashAssessmentStatusID ==
                            OnlandVisualTrashAssessmentStatus.Complete.OnlandVisualTrashAssessmentStatusID)
                .Average(x => x.OnlandVisualTrashAssessmentScore.NumericValue);

            var round = (int) Math.Round(average);

            return OnlandVisualTrashAssessmentScore.All.Single(x => x.NumericValue == round);
        }
    }
}