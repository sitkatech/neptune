using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("Role")]
    [Index("RoleDisplayName", Name = "AK_Role_RoleDisplayName", IsUnique = true)]
    [Index("RoleName", Name = "AK_Role_RoleName", IsUnique = true)]
    public partial class Role
    {
        public Role()
        {
            People = new HashSet<Person>();
        }

        [Key]
        public int RoleID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string RoleName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string RoleDisplayName { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string RoleDescription { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<Person> People { get; set; }
    }
}
