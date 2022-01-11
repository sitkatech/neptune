using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("CustomPage")]
    [Index(nameof(CustomPageDisplayName), Name = "AK_CustomPage_CustomPageDisplayName", IsUnique = true)]
    [Index(nameof(CustomPageVanityUrl), Name = "AK_CustomPage_CustomPageVanityUrl", IsUnique = true)]
    public partial class CustomPage
    {
        public CustomPage()
        {
            CustomPageRoles = new HashSet<CustomPageRole>();
        }

        [Key]
        public int CustomPageID { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomPageDisplayName { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomPageVanityUrl { get; set; }
        public string CustomPageContent { get; set; }
        public int MenuItemID { get; set; }
        public int? SortOrder { get; set; }

        [ForeignKey(nameof(MenuItemID))]
        [InverseProperty("CustomPages")]
        public virtual MenuItem MenuItem { get; set; }
        [InverseProperty(nameof(CustomPageRole.CustomPage))]
        public virtual ICollection<CustomPageRole> CustomPageRoles { get; set; }
    }
}
