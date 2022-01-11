//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomPage]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class CustomPageDto
    {
        public int CustomPageID { get; set; }
        public string CustomPageDisplayName { get; set; }
        public string CustomPageVanityUrl { get; set; }
        public string CustomPageContent { get; set; }
        public MenuItemDto MenuItem { get; set; }
        public int? SortOrder { get; set; }
    }

    public partial class CustomPageSimpleDto
    {
        public int CustomPageID { get; set; }
        public string CustomPageDisplayName { get; set; }
        public string CustomPageVanityUrl { get; set; }
        public string CustomPageContent { get; set; }
        public int MenuItemID { get; set; }
        public int? SortOrder { get; set; }
    }

}