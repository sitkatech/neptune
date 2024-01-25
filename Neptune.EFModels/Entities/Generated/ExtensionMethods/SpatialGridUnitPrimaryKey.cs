//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.SpatialGridUnit


namespace Neptune.EFModels.Entities
{
    public class SpatialGridUnitPrimaryKey : EntityPrimaryKey<SpatialGridUnit>
    {
        public SpatialGridUnitPrimaryKey() : base(){}
        public SpatialGridUnitPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public SpatialGridUnitPrimaryKey(SpatialGridUnit spatialGridUnit) : base(spatialGridUnit){}

        public static implicit operator SpatialGridUnitPrimaryKey(int primaryKeyValue)
        {
            return new SpatialGridUnitPrimaryKey(primaryKeyValue);
        }

        public static implicit operator SpatialGridUnitPrimaryKey(SpatialGridUnit spatialGridUnit)
        {
            return new SpatialGridUnitPrimaryKey(spatialGridUnit);
        }
    }
}