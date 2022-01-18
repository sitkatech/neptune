using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("OrganizationType")]
    public partial class OrganizationType
    {
        public OrganizationType()
        {
            Organizations = new HashSet<Organization>();
        }

        [Key]
        public int OrganizationTypeID { get; set; }
        [Required]
        [StringLength(200)]
        public string OrganizationTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string OrganizationTypeAbbreviation { get; set; }
        [Required]
        [StringLength(10)]
        public string LegendColor { get; set; }
        public bool IsDefaultOrganizationType { get; set; }

        [InverseProperty(nameof(Organization.OrganizationType))]
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
