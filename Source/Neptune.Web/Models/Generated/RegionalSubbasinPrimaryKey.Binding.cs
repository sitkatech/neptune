//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.RegionalSubbasin
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class RegionalSubbasinPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<RegionalSubbasin>
    {
        public RegionalSubbasinPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public RegionalSubbasinPrimaryKey(RegionalSubbasin regionalSubbasin) : base(regionalSubbasin){}

        public static implicit operator RegionalSubbasinPrimaryKey(int primaryKeyValue)
        {
            return new RegionalSubbasinPrimaryKey(primaryKeyValue);
        }

        public static implicit operator RegionalSubbasinPrimaryKey(RegionalSubbasin regionalSubbasin)
        {
            return new RegionalSubbasinPrimaryKey(regionalSubbasin);
        }
    }
}