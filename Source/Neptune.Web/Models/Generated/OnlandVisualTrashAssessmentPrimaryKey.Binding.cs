//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessment
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<OnlandVisualTrashAssessment>
    {
        public OnlandVisualTrashAssessmentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentPrimaryKey(OnlandVisualTrashAssessment onlandVisualTrashAssessment) : base(onlandVisualTrashAssessment){}

        public static implicit operator OnlandVisualTrashAssessmentPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentPrimaryKey(OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            return new OnlandVisualTrashAssessmentPrimaryKey(onlandVisualTrashAssessment);
        }
    }
}