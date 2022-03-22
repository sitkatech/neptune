using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("ProjectNetworkSolveHistoryStatusType")]
    [Index(nameof(ProjectNetworkSolveHistoryStatusTypeDisplayName), Name = "AK_ProjectNetworkSolveHistoryStatusTypeProjectNetworkSolveHistoryStatusTypeDisplayName", IsUnique = true)]
    [Index(nameof(ProjectNetworkSolveHistoryStatusTypeName), Name = "AK_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeName", IsUnique = true)]
    public partial class ProjectNetworkSolveHistoryStatusType
    {
        public ProjectNetworkSolveHistoryStatusType()
        {
            ProjectNetworkSolveHistories = new HashSet<ProjectNetworkSolveHistory>();
        }

        [Key]
        public int ProjectNetworkSolveHistoryStatusTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string ProjectNetworkSolveHistoryStatusTypeName { get; set; }
        [Required]
        [StringLength(50)]
        public string ProjectNetworkSolveHistoryStatusTypeDisplayName { get; set; }

        [InverseProperty(nameof(ProjectNetworkSolveHistory.ProjectNetworkSolveHistoryStatusType))]
        public virtual ICollection<ProjectNetworkSolveHistory> ProjectNetworkSolveHistories { get; set; }
    }
}
