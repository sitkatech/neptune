//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ObservationValueType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ObservationValueTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ObservationValueType>
    {
        public ObservationValueTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ObservationValueTypePrimaryKey(ObservationValueType observationValueType) : base(observationValueType){}

        public static implicit operator ObservationValueTypePrimaryKey(int primaryKeyValue)
        {
            return new ObservationValueTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ObservationValueTypePrimaryKey(ObservationValueType observationValueType)
        {
            return new ObservationValueTypePrimaryKey(observationValueType);
        }
    }
}