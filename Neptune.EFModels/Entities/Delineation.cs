using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class Delineation : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"Delineation ID {DelineationID}";
        }

        public bool CanDelete(Person currentPerson)
        {
            return currentPerson.IsAssignedToStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdictionID);
        }
    }
}