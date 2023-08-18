//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.StormwaterBreadCrumbEntity


namespace Neptune.EFModels.Entities
{
    public class StormwaterBreadCrumbEntityPrimaryKey : EntityPrimaryKey<StormwaterBreadCrumbEntity>
    {
        public StormwaterBreadCrumbEntityPrimaryKey() : base(){}
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