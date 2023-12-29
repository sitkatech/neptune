//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PrecipitationZone


namespace Neptune.EFModels.Entities
{
    public class PrecipitationZonePrimaryKey : EntityPrimaryKey<PrecipitationZone>
    {
        public PrecipitationZonePrimaryKey() : base(){}
        public PrecipitationZonePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public PrecipitationZonePrimaryKey(PrecipitationZone precipitationZone) : base(precipitationZone){}

        public static implicit operator PrecipitationZonePrimaryKey(int primaryKeyValue)
        {
            return new PrecipitationZonePrimaryKey(primaryKeyValue);
        }

        public static implicit operator PrecipitationZonePrimaryKey(PrecipitationZone precipitationZone)
        {
            return new PrecipitationZonePrimaryKey(precipitationZone);
        }
    }
}