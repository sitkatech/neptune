//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Organization]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
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
                OrganizationType = organization.OrganizationType.AsDto()
            };
            DoCustomMappings(organization, organizationDto);
            return organizationDto;
        }

        static partial void DoCustomMappings(Organization organization, OrganizationDto organizationDto);

        public static OrganizationSimpleDto AsSimpleDto(this Organization organization)
        {
            var organizationSimpleDto = new OrganizationSimpleDto()
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
            DoCustomSimpleDtoMappings(organization, organizationSimpleDto);
            return organizationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(Organization organization, OrganizationSimpleDto organizationSimpleDto);
    }
}