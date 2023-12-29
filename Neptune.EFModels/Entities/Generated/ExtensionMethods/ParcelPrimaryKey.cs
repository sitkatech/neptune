//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.Parcel


namespace Neptune.EFModels.Entities
{
    public class ParcelPrimaryKey : EntityPrimaryKey<Parcel>
    {
        public ParcelPrimaryKey() : base(){}
        public ParcelPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ParcelPrimaryKey(Parcel parcel) : base(parcel){}

        public static implicit operator ParcelPrimaryKey(int primaryKeyValue)
        {
            return new ParcelPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ParcelPrimaryKey(Parcel parcel)
        {
            return new ParcelPrimaryKey(parcel);
        }
    }
}