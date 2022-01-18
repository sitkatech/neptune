//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OrganizationType]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class OrganizationTypeDto
    {
        public int OrganizationTypeID { get; set; }
        public string OrganizationTypeName { get; set; }
        public string OrganizationTypeAbbreviation { get; set; }
        public string LegendColor { get; set; }
        public bool IsDefaultOrganizationType { get; set; }
    }

    public partial class OrganizationTypeSimpleDto
    {
        public int OrganizationTypeID { get; set; }
        public string OrganizationTypeName { get; set; }
        public string OrganizationTypeAbbreviation { get; set; }
        public string LegendColor { get; set; }
        public bool IsDefaultOrganizationType { get; set; }
    }

}