//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.HydromodificationAppliesType


namespace Neptune.EFModels.Entities
{
    public class HydromodificationAppliesTypePrimaryKey : EntityPrimaryKey<HydromodificationAppliesType>
    {
        public HydromodificationAppliesTypePrimaryKey() : base(){}
        public HydromodificationAppliesTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public HydromodificationAppliesTypePrimaryKey(HydromodificationAppliesType hydromodificationAppliesType) : base(hydromodificationAppliesType){}

        public static implicit operator HydromodificationAppliesTypePrimaryKey(int primaryKeyValue)
        {
            return new HydromodificationAppliesTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator HydromodificationAppliesTypePrimaryKey(HydromodificationAppliesType hydromodificationAppliesType)
        {
            return new HydromodificationAppliesTypePrimaryKey(hydromodificationAppliesType);
        }
    }
}