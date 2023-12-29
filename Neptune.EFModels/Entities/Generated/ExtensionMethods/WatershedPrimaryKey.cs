//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Watershed


namespace Neptune.EFModels.Entities
{
    public class WatershedPrimaryKey : EntityPrimaryKey<Watershed>
    {
        public WatershedPrimaryKey() : base(){}
        public WatershedPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WatershedPrimaryKey(Watershed watershed) : base(watershed){}

        public static implicit operator WatershedPrimaryKey(int primaryKeyValue)
        {
            return new WatershedPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WatershedPrimaryKey(Watershed watershed)
        {
            return new WatershedPrimaryKey(watershed);
        }
    }
}