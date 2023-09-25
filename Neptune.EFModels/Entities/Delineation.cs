using Neptune.Common.GeoSpatial;

namespace Neptune.EFModels.Entities
{
    public partial class Delineation
    {
        public string Geometry4326GeoJson => GeoJsonSerializer.SerializeGeometryToGeoJsonString(DelineationGeometry4326);
        public bool CanDelete(Person currentPerson)
        {
            return currentPerson.IsAssignedToStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdictionID);
        }
    }
}