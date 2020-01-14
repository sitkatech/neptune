using System;

namespace Neptune.Web.Models
{
    public partial class Delineation : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"Delineation ID {DelineationID}";
        }

        public void MarkAsVerified(Person currentPerson)
        {
            IsVerified = true;
            DateLastVerified = DateTime.Now;
            VerifiedByPersonID = currentPerson.PersonID;
        }

        public bool CanDelete(Person currentPerson)
        {
            return currentPerson.IsAssignedToStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdictionID);
        }
    }
}