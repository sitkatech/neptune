//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterAssessmentType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class StormwaterAssessmentTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<StormwaterAssessmentType>
    {
        public StormwaterAssessmentTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public StormwaterAssessmentTypePrimaryKey(StormwaterAssessmentType stormwaterAssessmentType) : base(stormwaterAssessmentType){}

        public static implicit operator StormwaterAssessmentTypePrimaryKey(int primaryKeyValue)
        {
            return new StormwaterAssessmentTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator StormwaterAssessmentTypePrimaryKey(StormwaterAssessmentType stormwaterAssessmentType)
        {
            return new StormwaterAssessmentTypePrimaryKey(stormwaterAssessmentType);
        }
    }
}