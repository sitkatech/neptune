//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NetworkCatchmentStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class NetworkCatchmentStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<NetworkCatchmentStaging>
    {
        public NetworkCatchmentStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NetworkCatchmentStagingPrimaryKey(NetworkCatchmentStaging networkCatchmentStaging) : base(networkCatchmentStaging){}

        public static implicit operator NetworkCatchmentStagingPrimaryKey(int primaryKeyValue)
        {
            return new NetworkCatchmentStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator NetworkCatchmentStagingPrimaryKey(NetworkCatchmentStaging networkCatchmentStaging)
        {
            return new NetworkCatchmentStagingPrimaryKey(networkCatchmentStaging);
        }
    }
}