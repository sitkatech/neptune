//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ParcelGeometry


namespace Neptune.EFModels.Entities
{
    public class ParcelGeometryPrimaryKey : EntityPrimaryKey<ParcelGeometry>
    {
        public ParcelGeometryPrimaryKey() : base(){}
        public ParcelGeometryPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ParcelGeometryPrimaryKey(ParcelGeometry parcelGeometry) : base(parcelGeometry){}

        public static implicit operator ParcelGeometryPrimaryKey(int primaryKeyValue)
        {
            return new ParcelGeometryPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ParcelGeometryPrimaryKey(ParcelGeometry parcelGeometry)
        {
            return new ParcelGeometryPrimaryKey(parcelGeometry);
        }
    }
}