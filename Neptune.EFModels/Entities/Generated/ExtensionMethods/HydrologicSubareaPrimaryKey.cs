//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.HydrologicSubarea


namespace Neptune.EFModels.Entities
{
    public class HydrologicSubareaPrimaryKey : EntityPrimaryKey<HydrologicSubarea>
    {
        public HydrologicSubareaPrimaryKey() : base(){}
        public HydrologicSubareaPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public HydrologicSubareaPrimaryKey(HydrologicSubarea hydrologicSubarea) : base(hydrologicSubarea){}

        public static implicit operator HydrologicSubareaPrimaryKey(int primaryKeyValue)
        {
            return new HydrologicSubareaPrimaryKey(primaryKeyValue);
        }

        public static implicit operator HydrologicSubareaPrimaryKey(HydrologicSubarea hydrologicSubarea)
        {
            return new HydrologicSubareaPrimaryKey(hydrologicSubarea);
        }
    }
}