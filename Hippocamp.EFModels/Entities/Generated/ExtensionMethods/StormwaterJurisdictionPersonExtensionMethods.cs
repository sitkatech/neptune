//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPerson]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class StormwaterJurisdictionPersonExtensionMethods
    {
        public static StormwaterJurisdictionPersonDto AsDto(this StormwaterJurisdictionPerson stormwaterJurisdictionPerson)
        {
            var stormwaterJurisdictionPersonDto = new StormwaterJurisdictionPersonDto()
            {
                StormwaterJurisdictionPersonID = stormwaterJurisdictionPerson.StormwaterJurisdictionPersonID,
                StormwaterJurisdiction = stormwaterJurisdictionPerson.StormwaterJurisdiction.AsDto(),
                Person = stormwaterJurisdictionPerson.Person.AsDto()
            };
            DoCustomMappings(stormwaterJurisdictionPerson, stormwaterJurisdictionPersonDto);
            return stormwaterJurisdictionPersonDto;
        }

        static partial void DoCustomMappings(StormwaterJurisdictionPerson stormwaterJurisdictionPerson, StormwaterJurisdictionPersonDto stormwaterJurisdictionPersonDto);

        public static StormwaterJurisdictionPersonSimpleDto AsSimpleDto(this StormwaterJurisdictionPerson stormwaterJurisdictionPerson)
        {
            var stormwaterJurisdictionPersonSimpleDto = new StormwaterJurisdictionPersonSimpleDto()
            {
                StormwaterJurisdictionPersonID = stormwaterJurisdictionPerson.StormwaterJurisdictionPersonID,
                StormwaterJurisdictionID = stormwaterJurisdictionPerson.StormwaterJurisdictionID,
                PersonID = stormwaterJurisdictionPerson.PersonID
            };
            DoCustomSimpleDtoMappings(stormwaterJurisdictionPerson, stormwaterJurisdictionPersonSimpleDto);
            return stormwaterJurisdictionPersonSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(StormwaterJurisdictionPerson stormwaterJurisdictionPerson, StormwaterJurisdictionPersonSimpleDto stormwaterJurisdictionPersonSimpleDto);
    }
}