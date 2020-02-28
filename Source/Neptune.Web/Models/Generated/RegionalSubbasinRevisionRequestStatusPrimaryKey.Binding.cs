//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.RegionalSubbasinRevisionRequestStatus
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class RegionalSubbasinRevisionRequestStatusPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<RegionalSubbasinRevisionRequestStatus>
    {
        public RegionalSubbasinRevisionRequestStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public RegionalSubbasinRevisionRequestStatusPrimaryKey(RegionalSubbasinRevisionRequestStatus regionalSubbasinRevisionRequestStatus) : base(regionalSubbasinRevisionRequestStatus){}

        public static implicit operator RegionalSubbasinRevisionRequestStatusPrimaryKey(int primaryKeyValue)
        {
            return new RegionalSubbasinRevisionRequestStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator RegionalSubbasinRevisionRequestStatusPrimaryKey(RegionalSubbasinRevisionRequestStatus regionalSubbasinRevisionRequestStatus)
        {
            return new RegionalSubbasinRevisionRequestStatusPrimaryKey(regionalSubbasinRevisionRequestStatus);
        }
    }
}