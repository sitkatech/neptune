//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Organization]
using System;


namespace Neptune.Models.DataTransferObjects
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
        public System.Int32? PrimaryContactPersonID { get; set; }
        public bool IsActive { get; set; }
        public string OrganizationUrl { get; set; }
        public System.Int32? LogoFileResourceID { get; set; }
        public System.Int32 OrganizationTypeID { get; set; }
    }

}