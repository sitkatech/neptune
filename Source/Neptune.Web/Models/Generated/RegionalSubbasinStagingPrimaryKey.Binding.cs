//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.RegionalSubbasinStaging
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class RegionalSubbasinStagingPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<RegionalSubbasinStaging>
    {
        public RegionalSubbasinStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public RegionalSubbasinStagingPrimaryKey(RegionalSubbasinStaging regionalSubbasinStaging) : base(regionalSubbasinStaging){}

        public static implicit operator RegionalSubbasinStagingPrimaryKey(int primaryKeyValue)
        {
            return new RegionalSubbasinStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator RegionalSubbasinStagingPrimaryKey(RegionalSubbasinStaging regionalSubbasinStaging)
        {
            return new RegionalSubbasinStagingPrimaryKey(regionalSubbasinStaging);
        }
    }
}