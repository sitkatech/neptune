using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("ProjectNetworkSolveHistoryStatusType")]
    [Index("ProjectNetworkSolveHistoryStatusTypeDisplayName", Name = "AK_ProjectNetworkSolveHistoryStatusTypeProjectNetworkSolveHistoryStatusTypeDisplayName", IsUnique = true)]
    [Index("ProjectNetworkSolveHistoryStatusTypeName", Name = "AK_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string ProjectNetworkSolveHistoryStatusTypeName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string ProjectNetworkSolveHistoryStatusTypeDisplayName { get; set; }

        [InverseProperty("ProjectNetworkSolveHistoryStatusType")]
        public virtual ICollection<ProjectNetworkSolveHistory> ProjectNetworkSolveHistories { get; set; }
    }
}
