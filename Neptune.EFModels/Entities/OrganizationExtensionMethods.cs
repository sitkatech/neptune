using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class OrganizationExtensionMethods
{
    public static OrganizationDto AsDto(this Organization organization)
    {
        var organizationDto = new OrganizationDto()
        {
            OrganizationID = organization.OrganizationID,
            OrganizationGuid = organization.OrganizationGuid,
            OrganizationName = organization.OrganizationName,
            OrganizationShortName = organization.OrganizationShortName,
            PrimaryContactPerson = organization.PrimaryContactPerson?.AsDto(),
            IsActive = organization.IsActive,
            OrganizationUrl = organization.OrganizationUrl,
            LogoFileResource = organization.LogoFileResource?.AsDto(),
            OrganizationType = organization.OrganizationType.AsSimpleDto()
        };
        return organizationDto;
    }
}