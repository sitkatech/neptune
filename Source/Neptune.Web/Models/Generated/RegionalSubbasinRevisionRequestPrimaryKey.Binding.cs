//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.RegionalSubbasinRevisionRequest
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class RegionalSubbasinRevisionRequestPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<RegionalSubbasinRevisionRequest>
    {
        public RegionalSubbasinRevisionRequestPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public RegionalSubbasinRevisionRequestPrimaryKey(RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest) : base(regionalSubbasinRevisionRequest){}

        public static implicit operator RegionalSubbasinRevisionRequestPrimaryKey(int primaryKeyValue)
        {
            return new RegionalSubbasinRevisionRequestPrimaryKey(primaryKeyValue);
        }

        public static implicit operator RegionalSubbasinRevisionRequestPrimaryKey(RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest)
        {
            return new RegionalSubbasinRevisionRequestPrimaryKey(regionalSubbasinRevisionRequest);
        }
    }
}