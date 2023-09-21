//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Organization]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class OrganizationDto
    {
        public int OrganizationID { get; set; }
        public Guid? OrganizationGuid { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationShortName { get; set; }
        public PersonDto PrimaryContactPerson { get; set; }
        public bool IsActive { get; set; }
        public string OrganizationUrl { get; set; }
        public FileResourceDto LogoFileResource { get; set; }
        public OrganizationTypeDto OrganizationType { get; set; }
    }

    public partial class OrganizationSimpleDto
    {
        public int OrganizationID { get; set; }
        public Guid? OrganizationGuid { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationShortName { get; set; }
        public int? PrimaryContactPersonID { get; set; }
        public bool IsActive { get; set; }
        public string OrganizationUrl { get; set; }
        public int? LogoFileResourceID { get; set; }
        public int OrganizationTypeID { get; set; }
    }

}