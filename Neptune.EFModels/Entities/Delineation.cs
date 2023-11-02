using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteFull(NeptuneDbContext dbContext)
        {
            await dbContext.DelineationOverlaps.Where(x => x.DelineationID == DelineationID).ExecuteDeleteAsync();
            await dbContext.DirtyModelNodes.Where(x => x.DelineationID == DelineationID).ExecuteDeleteAsync();
            foreach (var loadGeneratingUnit in dbContext.LoadGeneratingUnits.Where(x => x.DelineationID == DelineationID).ToList())
            {
                await loadGeneratingUnit.DeleteFull(dbContext);
            }
            await dbContext.NereidResults.Where(x => x.DelineationID == DelineationID).ExecuteDeleteAsync();
            await dbContext.ProjectLoadGeneratingUnits.Where(x => x.DelineationID == DelineationID)
                .ExecuteDeleteAsync();
            await dbContext.ProjectNereidResults.Where(x => x.DelineationID == DelineationID).ExecuteDeleteAsync();
            await dbContext.Delineations.Where(x => x.DelineationID == DelineationID).ExecuteDeleteAsync();
        }
    }
}