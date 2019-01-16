//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentObservation
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentObservationPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<OnlandVisualTrashAssessmentObservation>
    {
        public OnlandVisualTrashAssessmentObservationPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentObservationPrimaryKey(OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation) : base(onlandVisualTrashAssessmentObservation){}

        public static implicit operator OnlandVisualTrashAssessmentObservationPrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentObservationPrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentObservationPrimaryKey(OnlandVisualTrashAssessmentObservation onlandVisualTrashAssessmentObservation)
        {
            return new OnlandVisualTrashAssessmentObservationPrimaryKey(onlandVisualTrashAssessmentObservation);
        }
    }
}