using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("ProjectNetworkSolveHistory")]
    public partial class ProjectNetworkSolveHistory
    {
        [Key]
        public int ProjectNetworkSolveHistoryID { get; set; }
        public int ProjectID { get; set; }
        public int RequestedByPersonID { get; set; }
        public int ProjectNetworkSolveHistoryStatusTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; }
        [Unicode(false)]
        public string ErrorMessage { get; set; }

        [ForeignKey("ProjectID")]
        [InverseProperty("ProjectNetworkSolveHistories")]
        public virtual Project Project { get; set; }
        [ForeignKey("ProjectNetworkSolveHistoryStatusTypeID")]
        [InverseProperty("ProjectNetworkSolveHistories")]
        public virtual ProjectNetworkSolveHistoryStatusType ProjectNetworkSolveHistoryStatusType { get; set; }
        [ForeignKey("RequestedByPersonID")]
        [InverseProperty("ProjectNetworkSolveHistories")]
        public virtual Person RequestedByPerson { get; set; }
    }
}
