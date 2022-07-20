//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ParcelStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class ParcelStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<ParcelStaging>
    {
        public ParcelStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ParcelStagingPrimaryKey(ParcelStaging parcelStaging) : base(parcelStaging){}

        public static implicit operator ParcelStagingPrimaryKey(int primaryKeyValue)
        {
            return new ParcelStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ParcelStagingPrimaryKey(ParcelStaging parcelStaging)
        {
            return new ParcelStagingPrimaryKey(parcelStaging);
        }
    }
}