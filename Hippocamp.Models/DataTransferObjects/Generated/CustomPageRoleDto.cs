//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomPageRole]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class CustomPageRoleDto
    {
        public int CustomPageRoleID { get; set; }
        public CustomPageDto CustomPage { get; set; }
        public RoleDto Role { get; set; }
    }

    public partial class CustomPageRoleSimpleDto
    {
        public int CustomPageRoleID { get; set; }
        public int CustomPageID { get; set; }
        public int RoleID { get; set; }
    }

}