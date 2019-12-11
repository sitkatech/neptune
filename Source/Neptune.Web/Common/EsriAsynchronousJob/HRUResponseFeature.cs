using System;
using LtInfo.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class HRUResponseFeature
    {
        [JsonProperty("attributes")]
        public HRUResponseFeatureAttributes Attributes { get; set; }
        [JsonProperty("geometry")]
        public EsriPolygonGeometry Geometry { get; set; }

        // todo: overload
        public HRUCharacteristic ToHRUCharacteristic(IHaveHRUCharacteristics iHaveHRUCharacteristics)
        {
            if (iHaveHRUCharacteristics is TreatmentBMP)
            {
                return new HRUCharacteristic(Attributes.LSPCLandUseDescription,
                        Attributes.HydrologicSoilGroup, Attributes.SlopePercentage, Attributes.ImperviousAcres, DateTime.Now, Attributes.Area / CoordinateSystemHelper.SquareFeetToAcresDivisor)
                    {TreatmentBMPID = iHaveHRUCharacteristics.PrimaryKey};
            } else if (iHaveHRUCharacteristics is WaterQualityManagementPlan)

            {
                return new HRUCharacteristic(Attributes.LSPCLandUseDescription,
                        Attributes.HydrologicSoilGroup, Attributes.SlopePercentage, Attributes.ImperviousAcres, DateTime.Now, Attributes.Area / CoordinateSystemHelper.SquareFeetToAcresDivisor)
                { WaterQualityManagementPlanID = iHaveHRUCharacteristics.PrimaryKey };
            }
            else if (iHaveHRUCharacteristics is NetworkCatchment)
            {
                return new HRUCharacteristic(Attributes.LSPCLandUseDescription,
                        Attributes.HydrologicSoilGroup, Attributes.SlopePercentage, Attributes.ImperviousAcres, DateTime.Now, Attributes.Area / CoordinateSystemHelper.SquareFeetToAcresDivisor)
                { NetworkCatchmentID = iHaveHRUCharacteristics.PrimaryKey };
            }
            {
                throw new NotImplementedException(
                    "Tried to add HRU characteristics to an entity that hasn't fully implemented them yet.");
            }
        }
    }
}