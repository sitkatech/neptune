using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("MenuItem")]
    [Index(nameof(MenuItemDisplayName), Name = "AK_MenuItem_MenuItemDisplayName", IsUnique = true)]
    [Index(nameof(MenuItemName), Name = "AK_MenuItem_MenuItemName", IsUnique = true)]
    public partial class MenuItem
    {
        public MenuItem()
        {
            CustomPages = new HashSet<CustomPage>();
        }

        [Key]
        public int MenuItemID { get; set; }
        [Required]
        [StringLength(100)]
        public string MenuItemName { get; set; }
        [Required]
        [StringLength(100)]
        public string MenuItemDisplayName { get; set; }

        [InverseProperty(nameof(CustomPage.MenuItem))]
        public virtual ICollection<CustomPage> CustomPages { get; set; }
    }
}
