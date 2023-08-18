//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.RegionalSubbasinStaging


namespace Neptune.EFModels.Entities
{
    public class RegionalSubbasinStagingPrimaryKey : EntityPrimaryKey<RegionalSubbasinStaging>
    {
        public RegionalSubbasinStagingPrimaryKey() : base(){}
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