//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.ParcelStaging


namespace Neptune.EFModels.Entities
{
    public class ParcelStagingPrimaryKey : EntityPrimaryKey<ParcelStaging>
    {
        public ParcelStagingPrimaryKey() : base(){}
        public ParcelStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public ParcelStagingPrimaryKey(ParcelStaging parcelStaging) : base(parcelStaging){}

        public static implicit operator ParcelStagingPrimaryKey(int primaryKeyValue)
        {
            return new ParcelStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator ParcelStagingPrimaryKey(ParcelStaging parcelStaging)
        {
            return new ParcelStagingPrimaryKey(parcelStaging);
        }
    }
}