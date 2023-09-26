//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OrganizationType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class OrganizationTypeExtensionMethods
    {
        public static OrganizationTypeDto AsDto(this OrganizationType organizationType)
        {
            var organizationTypeDto = new OrganizationTypeDto()
            {
                OrganizationTypeID = organizationType.OrganizationTypeID,
                OrganizationTypeName = organizationType.OrganizationTypeName,
                OrganizationTypeAbbreviation = organizationType.OrganizationTypeAbbreviation,
                LegendColor = organizationType.LegendColor,
                IsDefaultOrganizationType = organizationType.IsDefaultOrganizationType
            };
            DoCustomMappings(organizationType, organizationTypeDto);
            return organizationTypeDto;
        }

        static partial void DoCustomMappings(OrganizationType organizationType, OrganizationTypeDto organizationTypeDto);

        public static OrganizationTypeSimpleDto AsSimpleDto(this OrganizationType organizationType)
        {
            var organizationTypeSimpleDto = new OrganizationTypeSimpleDto()
            {
                OrganizationTypeID = organizationType.OrganizationTypeID,
                OrganizationTypeName = organizationType.OrganizationTypeName,
                OrganizationTypeAbbreviation = organizationType.OrganizationTypeAbbreviation,
                LegendColor = organizationType.LegendColor,
                IsDefaultOrganizationType = organizationType.IsDefaultOrganizationType
            };
            DoCustomSimpleDtoMappings(organizationType, organizationTypeSimpleDto);
            return organizationTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OrganizationType organizationType, OrganizationTypeSimpleDto organizationTypeSimpleDto);
    }
}