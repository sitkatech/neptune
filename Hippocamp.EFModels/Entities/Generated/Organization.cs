using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("Organization")]
    public partial class Organization
    {
        public Organization()
        {
            FundingSources = new HashSet<FundingSource>();
            People = new HashSet<Person>();
            TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        [Key]
        public int OrganizationID { get; set; }
        public Guid? OrganizationGuid { get; set; }
        [Required]
        [StringLength(200)]
        public string OrganizationName { get; set; }
        [StringLength(50)]
        public string OrganizationShortName { get; set; }
        public int? PrimaryContactPersonID { get; set; }
        public bool IsActive { get; set; }
        [StringLength(200)]
        public string OrganizationUrl { get; set; }
        public int? LogoFileResourceID { get; set; }
        public int OrganizationTypeID { get; set; }

        [ForeignKey(nameof(LogoFileResourceID))]
        [InverseProperty(nameof(FileResource.Organizations))]
        public virtual FileResource LogoFileResource { get; set; }
        [ForeignKey(nameof(OrganizationTypeID))]
        [InverseProperty("Organizations")]
        public virtual OrganizationType OrganizationType { get; set; }
        [ForeignKey(nameof(PrimaryContactPersonID))]
        [InverseProperty(nameof(Person.Organizations))]
        public virtual Person PrimaryContactPerson { get; set; }
        [InverseProperty("Organization")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [InverseProperty(nameof(FundingSource.Organization))]
        public virtual ICollection<FundingSource> FundingSources { get; set; }
        [InverseProperty(nameof(Person.Organization))]
        public virtual ICollection<Person> People { get; set; }
        [InverseProperty(nameof(TreatmentBMP.OwnerOrganization))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
