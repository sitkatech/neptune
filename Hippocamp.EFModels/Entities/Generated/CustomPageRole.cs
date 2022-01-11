using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("CustomPageRole")]
    public partial class CustomPageRole
    {
        [Key]
        public int CustomPageRoleID { get; set; }
        public int CustomPageID { get; set; }
        public int RoleID { get; set; }

        [ForeignKey(nameof(CustomPageID))]
        [InverseProperty("CustomPageRoles")]
        public virtual CustomPage CustomPage { get; set; }
        [ForeignKey(nameof(RoleID))]
        [InverseProperty("CustomPageRoles")]
        public virtual Role Role { get; set; }
    }
}
