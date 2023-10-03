using Neptune.Common.GeoSpatial;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public partial class Delineation
    {
        public string GetGeometry4326GeoJson()
        {
            var attributesTable = new AttributesTable
            {
                { "DelineationID", DelineationID },
                { "TreatmentBMPID", TreatmentBMPID }
            };

            var feature = new Feature(DelineationGeometry4326, attributesTable);
            return GeoJsonSerializer.Serialize(feature);
        }

        public bool CanDelete(Person currentPerson)
        {
            return currentPerson.IsAssignedToStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdictionID);
        }

        public void MarkAsVerified(Person currentPerson)
        {
            IsVerified = true;
            DateLastVerified = DateTime.Now;
            VerifiedByPersonID = currentPerson.PersonID;
        }
    }
}