using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("ProjectStatus")]
    [Index(nameof(ProjectStatusDisplayName), Name = "AK_ProjectStatus_ProjectStatusDisplayName", IsUnique = true)]
    [Index(nameof(ProjectStatusName), Name = "AK_ProjectStatus_ProjectStatusName", IsUnique = true)]
    public partial class ProjectStatus
    {
        public ProjectStatus()
        {
            Projects = new HashSet<Project>();
        }

        [Key]
        public int ProjectStatusID { get; set; }
        [Required]
        [StringLength(50)]
        public string ProjectStatusName { get; set; }
        [Required]
        [StringLength(50)]
        public string ProjectStatusDisplayName { get; set; }
        public int ProjectStatusSortOrder { get; set; }

        [InverseProperty(nameof(Project.ProjectStatus))]
        public virtual ICollection<Project> Projects { get; set; }
    }
}
