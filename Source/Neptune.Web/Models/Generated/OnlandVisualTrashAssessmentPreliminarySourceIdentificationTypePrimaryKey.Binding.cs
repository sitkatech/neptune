//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.OnlandVisualTrashAssessmentPreliminarySourceIdentificationType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType>
    {
        public OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType) : base(onlandVisualTrashAssessmentPreliminarySourceIdentificationType){}

        public static implicit operator OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(int primaryKeyValue)
        {
            return new OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType onlandVisualTrashAssessmentPreliminarySourceIdentificationType)
        {
            return new OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypePrimaryKey(onlandVisualTrashAssessmentPreliminarySourceIdentificationType);
        }
    }
}