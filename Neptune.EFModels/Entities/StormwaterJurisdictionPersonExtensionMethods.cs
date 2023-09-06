using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class StormwaterJurisdictionPersonExtensionMethods
{
    public static StormwaterJurisdictionPersonUpsertDto AsUpsertDto(this StormwaterJurisdictionPerson stormwaterJurisdictionPerson, Person currentPerson)
    {
        return new StormwaterJurisdictionPersonUpsertDto()
        {
            StormwaterJurisdictionPersonID = stormwaterJurisdictionPerson.StormwaterJurisdictionPersonID,
            PersonID = stormwaterJurisdictionPerson.PersonID,
            StormwaterJurisdictionID = stormwaterJurisdictionPerson.StormwaterJurisdictionID,
            CurrentPersonCanRemove =
                currentPerson.IsAssignedToStormwaterJurisdiction(stormwaterJurisdictionPerson.StormwaterJurisdictionID)
        };
    }
}