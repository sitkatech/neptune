//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterBreadCrumbEntity
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class StormwaterBreadCrumbEntityPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<StormwaterBreadCrumbEntity>
    {
        public StormwaterBreadCrumbEntityPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public StormwaterBreadCrumbEntityPrimaryKey(StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity) : base(stormwaterBreadCrumbEntity){}

        public static implicit operator StormwaterBreadCrumbEntityPrimaryKey(int primaryKeyValue)
        {
            return new StormwaterBreadCrumbEntityPrimaryKey(primaryKeyValue);
        }

        public static implicit operator StormwaterBreadCrumbEntityPrimaryKey(StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity)
        {
            return new StormwaterBreadCrumbEntityPrimaryKey(stormwaterBreadCrumbEntity);
        }
    }
}