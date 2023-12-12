//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPerson]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StormwaterJurisdictionPersonExtensionMethods
    {
        public static StormwaterJurisdictionPersonSimpleDto AsSimpleDto(this StormwaterJurisdictionPerson stormwaterJurisdictionPerson)
        {
            var dto = new StormwaterJurisdictionPersonSimpleDto()
            {
                StormwaterJurisdictionPersonID = stormwaterJurisdictionPerson.StormwaterJurisdictionPersonID,
                StormwaterJurisdictionID = stormwaterJurisdictionPerson.StormwaterJurisdictionID,
                PersonID = stormwaterJurisdictionPerson.PersonID
            };
            return dto;
        }
    }
}