//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ObservationTypeCollectionMethod
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ObservationTypeCollectionMethodPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ObservationTypeCollectionMethod>
    {
        public ObservationTypeCollectionMethodPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ObservationTypeCollectionMethodPrimaryKey(ObservationTypeCollectionMethod observationTypeCollectionMethod) : base(observationTypeCollectionMethod){}

        public static implicit operator ObservationTypeCollectionMethodPrimaryKey(int primaryKeyValue)
        {
            return new ObservationTypeCollectionMethodPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ObservationTypeCollectionMethodPrimaryKey(ObservationTypeCollectionMethod observationTypeCollectionMethod)
        {
            return new ObservationTypeCollectionMethodPrimaryKey(observationTypeCollectionMethod);
        }
    }
}