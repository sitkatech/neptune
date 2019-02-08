//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentStatus
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentStatusPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<OnlandVisualTrashAssessmentStatus>
    {
        public OnlandVisualTrashAssessmentStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentStatusPrimaryKey(OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus) : base(onlandVisualTrashAssessmentStatus){}

        public static implicit operator OnlandVisualTrashAssessmentStatusPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentStatusPrimaryKey(OnlandVisualTrashAssessmentStatus onlandVisualTrashAssessmentStatus)
        {
            return new OnlandVisualTrashAssessmentStatusPrimaryKey(onlandVisualTrashAssessmentStatus);
        }
    }
}