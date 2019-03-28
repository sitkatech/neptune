//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ObservationTargetType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ObservationTargetTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ObservationTargetType>
    {
        public ObservationTargetTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ObservationTargetTypePrimaryKey(ObservationTargetType observationTargetType) : base(observationTargetType){}

        public static implicit operator ObservationTargetTypePrimaryKey(int primaryKeyValue)
        {
            return new ObservationTargetTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ObservationTargetTypePrimaryKey(ObservationTargetType observationTargetType)
        {
            return new ObservationTargetTypePrimaryKey(observationTargetType);
        }
    }
}