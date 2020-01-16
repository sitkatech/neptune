//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.PrecipitationZone
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class PrecipitationZonePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<PrecipitationZone>
    {
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