using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;
using NetTopologySuite.Geometries;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class TreatmentBMPDetailMapInitJson : StormwaterMapInitJson
    {
        public LayerGeoJson DelineationLayer { get; set; }

        public TreatmentBMPDetailMapInitJson(string mapDivID, NeptuneDbContext dbContext) : base(mapDivID, dbContext)
        {
        }

        public TreatmentBMPDetailMapInitJson(string mapDivID, Geometry locationPointIn4326, NeptuneDbContext dbContext) : base(mapDivID, locationPointIn4326, dbContext)
        {
        }

        public TreatmentBMPDetailMapInitJson(string mapDivID, int zoomLevel, List<LayerGeoJson> layers, BoundingBoxDto boundingBox) : base(mapDivID, zoomLevel, layers, boundingBox)
        {
        }
    }
}