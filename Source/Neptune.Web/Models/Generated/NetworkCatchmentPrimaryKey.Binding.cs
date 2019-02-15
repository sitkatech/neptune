//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.NetworkCatchment
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class NetworkCatchmentPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<NetworkCatchment>
    {
        public NetworkCatchmentPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public NetworkCatchmentPrimaryKey(NetworkCatchment networkCatchment) : base(networkCatchment){}

        public static implicit operator NetworkCatchmentPrimaryKey(int primaryKeyValue)
        {
            return new NetworkCatchmentPrimaryKey(primaryKeyValue);
        }

        public static implicit operator NetworkCatchmentPrimaryKey(NetworkCatchment networkCatchment)
        {
            return new NetworkCatchmentPrimaryKey(networkCatchment);
        }
    }
}