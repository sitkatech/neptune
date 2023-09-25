using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("Organization")]
    public partial class Organization
    {
        public Organization()
        {
            FundingSources = new HashSet<FundingSource>();
            People = new HashSet<Person>();
            Projects = new HashSet<Project>();
            TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        [Key]
        public int OrganizationID { get; set; }
        public Guid? OrganizationGuid { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string OrganizationName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OrganizationShortName { get; set; }
        public int? PrimaryContactPersonID { get; set; }
        public bool IsActive { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string OrganizationUrl { get; set; }
        public int? LogoFileResourceID { get; set; }
        public int OrganizationTypeID { get; set; }

        [ForeignKey("LogoFileResourceID")]
        [InverseProperty("Organizations")]
        public virtual FileResource LogoFileResource { get; set; }
        [ForeignKey("OrganizationTypeID")]
        [InverseProperty("Organizations")]
        public virtual OrganizationType OrganizationType { get; set; }
        [ForeignKey("PrimaryContactPersonID")]
        [InverseProperty("Organizations")]
        public virtual Person PrimaryContactPerson { get; set; }
        [InverseProperty("Organization")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<FundingSource> FundingSources { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<Person> People { get; set; }
        [InverseProperty("Organization")]
        public virtual ICollection<Project> Projects { get; set; }
        [InverseProperty("OwnerOrganization")]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
