//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PrecipitationZoneStaging


namespace Neptune.EFModels.Entities
{
    public class PrecipitationZoneStagingPrimaryKey : EntityPrimaryKey<PrecipitationZoneStaging>
    {
        public PrecipitationZoneStagingPrimaryKey() : base(){}
        public PrecipitationZoneStagingPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PrecipitationZoneStagingPrimaryKey(PrecipitationZoneStaging precipitationZoneStaging) : base(precipitationZoneStaging){}

        public static implicit operator PrecipitationZoneStagingPrimaryKey(int primaryKeyValue)
        {
            return new PrecipitationZoneStagingPrimaryKey(primaryKeyValue);
        }

        public static implicit operator PrecipitationZoneStagingPrimaryKey(PrecipitationZoneStaging precipitationZoneStaging)
        {
            return new PrecipitationZoneStagingPrimaryKey(precipitationZoneStaging);
        }
    }
}