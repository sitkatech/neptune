using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class StormwaterJurisdictionExtensionMethods
    {
        static partial void DoCustomSimpleDtoMappings(StormwaterJurisdiction stormwaterJurisdiction, StormwaterJurisdictionSimpleDto stormwaterJurisdictionSimpleDto)
        {
            stormwaterJurisdictionSimpleDto.Organization = stormwaterJurisdiction.Organization.AsSimpleDto();
        }
    }
}