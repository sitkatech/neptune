using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("Role")]
    [Index(nameof(RoleDisplayName), Name = "AK_Role_RoleDisplayName", IsUnique = true)]
    [Index(nameof(RoleName), Name = "AK_Role_RoleName", IsUnique = true)]
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
        public string RoleName { get; set; }
        [Required]
        [StringLength(100)]
        public string RoleDisplayName { get; set; }
        [StringLength(255)]
        public string RoleDescription { get; set; }

        [InverseProperty(nameof(Person.Role))]
        public virtual ICollection<Person> People { get; set; }
    }
}
