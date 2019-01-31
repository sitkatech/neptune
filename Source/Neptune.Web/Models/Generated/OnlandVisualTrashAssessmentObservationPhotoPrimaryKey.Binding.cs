//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentObservationPhoto
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentObservationPhotoPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<OnlandVisualTrashAssessmentObservationPhoto>
    {
        public OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(OnlandVisualTrashAssessmentObservationPhoto onlandVisualTrashAssessmentObservationPhoto) : base(onlandVisualTrashAssessmentObservationPhoto){}

        public static implicit operator OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(OnlandVisualTrashAssessmentObservationPhoto onlandVisualTrashAssessmentObservationPhoto)
        {
            return new OnlandVisualTrashAssessmentObservationPhotoPrimaryKey(onlandVisualTrashAssessmentObservationPhoto);
        }
    }
}