using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class OrganizationExtensionMethods
{
    public static OrganizationSimpleDto AsSimpleDto(this Organization organization)
    {
        var dto = new OrganizationSimpleDto()
        {
            OrganizationID = organization.OrganizationID,
            OrganizationGuid = organization.OrganizationGuid,
            OrganizationName = organization.OrganizationName,
            OrganizationShortName = organization.OrganizationShortName,
            PrimaryContactPersonID = organization.PrimaryContactPersonID,
            IsActive = organization.IsActive,
            OrganizationUrl = organization.OrganizationUrl,
            LogoFileResourceID = organization.LogoFileResourceID,
            OrganizationTypeID = organization.OrganizationTypeID
        };
        return dto;
    }

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