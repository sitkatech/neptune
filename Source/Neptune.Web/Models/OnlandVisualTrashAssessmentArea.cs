using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;

namespace Neptune.Web.Models
{
    public partial class OnlandVisualTrashAssessmentArea : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"OVTA Area {OnlandVisualTrashAssessmentAreaID}";
        }

        public OnlandVisualTrashAssessmentScore CalculateScoreFromBackingData(bool calculateProgressScore)
        {
            var onlandVisualTrashAssessments = OnlandVisualTrashAssessments
                .Where(x => x.OnlandVisualTrashAssessmentStatusID ==
                            OnlandVisualTrashAssessmentStatus.Complete.OnlandVisualTrashAssessmentStatusID && x.IsProgressAssessment == calculateProgressScore).ToList();

            if (!onlandVisualTrashAssessments.Any())
            {
                return null;
            }
            
            var average = onlandVisualTrashAssessments
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