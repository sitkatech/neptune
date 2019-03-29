using System.Collections.Generic;
using System.Data.Entity.Spatial;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class TreatmentBMPDetailMapInitJson : StormwaterMapInitJson
    {
        public LayerGeoJson DelineationLayer { get; set; }

        public TreatmentBMPDetailMapInitJson(string mapDivID) : base(mapDivID)
        {
        }

        public TreatmentBMPDetailMapInitJson(string mapDivID, DbGeometry locationPoint) : base(mapDivID, locationPoint)
        {
        }

        public TreatmentBMPDetailMapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBox boundingBox) : base(mapDivID, zoomLevel, layers, boundingBox)
        {
        }
    }
}