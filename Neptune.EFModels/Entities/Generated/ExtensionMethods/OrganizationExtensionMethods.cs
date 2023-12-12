//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Organization]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OrganizationExtensionMethods
    {

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