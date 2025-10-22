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
            PrimaryContactPerson = organization.PrimaryContactPerson?.AsSimpleDto(),
            IsActive = organization.IsActive,
            OrganizationUrl = organization.OrganizationUrl,
            LogoFileResource = organization.LogoFileResource?.AsSimpleDto(),
            OrganizationType = organization.OrganizationType.AsSimpleDto()
        };
        return organizationDto;
    }

    public static Organization AsEntity(this OrganizationUpsertDto dto, int? organizationID = null)
    {
        return new Organization
        {
            OrganizationID = organizationID ?? 0, // 0 for new, or set for update
            OrganizationGuid = dto.OrganizationGuid,
            OrganizationName = dto.OrganizationName,
            OrganizationShortName = dto.OrganizationShortName,
            PrimaryContactPersonID = dto.PrimaryContactPersonID,
            IsActive = dto.IsActive,
            OrganizationUrl = dto.OrganizationUrl,
            LogoFileResourceID = dto.LogoFileResourceID,
            OrganizationTypeID = dto.OrganizationTypeID
        };
    }

    public static void UpdateFromUpsertDto(this Organization entity, OrganizationUpsertDto dto)
    {
        entity.OrganizationGuid = dto.OrganizationGuid;
        entity.OrganizationName = dto.OrganizationName;
        entity.OrganizationShortName = dto.OrganizationShortName;
        entity.PrimaryContactPersonID = dto.PrimaryContactPersonID;
        entity.IsActive = dto.IsActive;
        entity.OrganizationUrl = dto.OrganizationUrl;
        entity.LogoFileResourceID = dto.LogoFileResourceID;
        entity.OrganizationTypeID = dto.OrganizationTypeID;
    }
}