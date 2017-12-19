//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ObservationType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ObservationTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ObservationType>
    {
        public ObservationTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ObservationTypePrimaryKey(ObservationType observationType) : base(observationType){}

        public static implicit operator ObservationTypePrimaryKey(int primaryKeyValue)
        {
            return new ObservationTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator ObservationTypePrimaryKey(ObservationType observationType)
        {
            return new ObservationTypePrimaryKey(observationType);
        }
    }
}