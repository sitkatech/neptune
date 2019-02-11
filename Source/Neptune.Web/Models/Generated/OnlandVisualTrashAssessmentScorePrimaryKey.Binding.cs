//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentScore
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentScorePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<OnlandVisualTrashAssessmentScore>
    {
        public OnlandVisualTrashAssessmentScorePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentScorePrimaryKey(OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore) : base(onlandVisualTrashAssessmentScore){}

        public static implicit operator OnlandVisualTrashAssessmentScorePrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentScorePrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentScorePrimaryKey(OnlandVisualTrashAssessmentScore onlandVisualTrashAssessmentScore)
        {
            return new OnlandVisualTrashAssessmentScorePrimaryKey(onlandVisualTrashAssessmentScore);
        }
    }
}