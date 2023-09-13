namespace Neptune.EFModels.Entities
{
    public partial class Delineation
    {
        public bool CanDelete(Person currentPerson)
        {
            return currentPerson.IsAssignedToStormwaterJurisdiction(TreatmentBMP.StormwaterJurisdictionID);
        }
    }
}