using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("Project")]
    [Index(nameof(ProjectName), Name = "AK_Project_ProjectName", IsUnique = true)]
    public partial class Project
    {
        public Project()
        {
            ProjectDocuments = new HashSet<ProjectDocument>();
            TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        [Key]
        public int ProjectID { get; set; }
        [Required]
        [StringLength(200)]
        public string ProjectName { get; set; }
        public int OrganizationID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int ProjectStatusID { get; set; }
        public int PrimaryContactPersonID { get; set; }
        public int CreatePersonID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        [StringLength(500)]
        public string ProjectDescription { get; set; }
        [StringLength(500)]
        public string AdditionalContactInformation { get; set; }

        [ForeignKey(nameof(CreatePersonID))]
        [InverseProperty(nameof(Person.ProjectCreatePeople))]
        public virtual Person CreatePerson { get; set; }
        [ForeignKey(nameof(OrganizationID))]
        [InverseProperty("Projects")]
        public virtual Organization Organization { get; set; }
        [ForeignKey(nameof(PrimaryContactPersonID))]
        [InverseProperty(nameof(Person.ProjectPrimaryContactPeople))]
        public virtual Person PrimaryContactPerson { get; set; }
        [ForeignKey(nameof(ProjectStatusID))]
        [InverseProperty("Projects")]
        public virtual ProjectStatus ProjectStatus { get; set; }
        [ForeignKey(nameof(StormwaterJurisdictionID))]
        [InverseProperty("Projects")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [InverseProperty(nameof(ProjectDocument.Project))]
        public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; }
        [InverseProperty(nameof(TreatmentBMP.Project))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
