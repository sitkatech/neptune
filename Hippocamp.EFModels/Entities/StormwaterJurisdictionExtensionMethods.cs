using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class StormwaterJurisdictionExtensionMethods
    {
        static partial void DoCustomSimpleDtoMappings(StormwaterJurisdiction stormwaterJurisdiction, StormwaterJurisdictionSimpleDto stormwaterJurisdictionSimpleDto)
        {
            stormwaterJurisdictionSimpleDto.Organization = stormwaterJurisdiction.Organization.AsSimpleDto();
        }
    }
}