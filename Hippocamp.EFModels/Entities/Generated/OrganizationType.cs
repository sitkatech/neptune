using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
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
        [Unicode(false)]
        public string OrganizationTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string OrganizationTypeAbbreviation { get; set; }
        [Required]
        [StringLength(10)]
        [Unicode(false)]
        public string LegendColor { get; set; }
        public bool IsDefaultOrganizationType { get; set; }

        [InverseProperty("OrganizationType")]
        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
