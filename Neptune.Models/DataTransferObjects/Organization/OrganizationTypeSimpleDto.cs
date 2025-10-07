//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OrganizationType]

namespace Neptune.Models.DataTransferObjects
{
    public partial class OrganizationTypeSimpleDto
    {
        public int OrganizationTypeID { get; set; }
        public string OrganizationTypeName { get; set; }
        public string OrganizationTypeAbbreviation { get; set; }
        public string LegendColor { get; set; }
        public bool IsDefaultOrganizationType { get; set; }
    }
}