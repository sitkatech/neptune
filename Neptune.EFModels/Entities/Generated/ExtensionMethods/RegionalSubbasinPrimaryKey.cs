//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.RegionalSubbasin


namespace Neptune.EFModels.Entities
{
    public class RegionalSubbasinPrimaryKey : EntityPrimaryKey<RegionalSubbasin>
    {
        public RegionalSubbasinPrimaryKey() : base(){}
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