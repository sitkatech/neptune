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

        public int GetScore()
        {
            OnlandVisualTrashAssessments.Sum(x => x.OnlandVisualTrashAssessmentScore.NumericValue);

            throw new NotImplementedException();
        }
    }
}