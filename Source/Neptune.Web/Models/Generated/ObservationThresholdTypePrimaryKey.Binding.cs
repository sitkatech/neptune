//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ObservationThresholdType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ObservationThresholdTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ObservationThresholdType>
    {
        public ObservationThresholdTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ObservationThresholdTypePrimaryKey(ObservationThresholdType observationThresholdType) : base(observationThresholdType){}

        public static implicit operator ObservationThresholdTypePrimaryKey(int primaryKeyValue)
        {
            return new ObservationThresholdTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ObservationThresholdTypePrimaryKey(ObservationThresholdType observationThresholdType)
        {
            return new ObservationThresholdTypePrimaryKey(observationThresholdType);
        }
    }
}