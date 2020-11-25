﻿using System;
using System.Linq;
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

        public HRUCharacteristic ToHRUCharacteristic()
        {
            var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(x => x.HRUCharacteristicLandUseCodeName == Attributes.LSPCLandUseDescription);
            var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(x => x.HRUCharacteristicLandUseCodeName == Attributes.BaselineLandUseDescription);

            var hruCharacteristic = new HRUCharacteristic(Attributes.HydrologicSoilGroup, Attributes.SlopePercentage,
                Attributes.ImperviousAcres, DateTime.Now, Attributes.Area / CoordinateSystemHelper.SquareFeetToAcresDivisor,
                hruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID, Attributes.QueryFeatureID)
            {
                BaselineHRUCharacteristicLandUseCodeID = baselineHruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID,
                BaselineImperviousAcres = Attributes.BaselineImperviousAcres
            };
            return hruCharacteristic;
        }
    }
}