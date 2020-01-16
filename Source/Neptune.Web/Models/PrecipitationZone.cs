using LtInfo.Common;

namespace Neptune.Web.Models
{
    public partial class PrecipitationZone : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"{PrecipitationZoneKey}: {DesignStormwaterDepthInInches.ToGroupedNumeric()}";
        }
    }
}