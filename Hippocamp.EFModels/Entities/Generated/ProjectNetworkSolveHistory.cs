using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("ProjectNetworkSolveHistory")]
    public partial class ProjectNetworkSolveHistory
    {
        [Key]
        public int ProjectNetworkSolveHistoryID { get; set; }
        public int ProjectID { get; set; }
        public int RequestedByPersonID { get; set; }
        public int ProjectNetworkSolveHistoryStatusTypeID { get; set; }
        public string ErrorMessage { get; set; }

        [ForeignKey(nameof(ProjectID))]
        [InverseProperty("ProjectNetworkSolveHistories")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(ProjectNetworkSolveHistoryStatusTypeID))]
        [InverseProperty("ProjectNetworkSolveHistories")]
        public virtual ProjectNetworkSolveHistoryStatusType ProjectNetworkSolveHistoryStatusType { get; set; }
        [ForeignKey(nameof(RequestedByPersonID))]
        [InverseProperty(nameof(Person.ProjectNetworkSolveHistories))]
        public virtual Person RequestedByPerson { get; set; }
    }
}
