using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("MonthsOfOperation")]
    [Index("MonthsOfOperationDisplayName", Name = "AK_MonthsOfOperation_MonthsOfOperationDisplayName", IsUnique = true)]
    [Index("MonthsOfOperationName", Name = "AK_MonthsOfOperation_MonthsOfOperationName", IsUnique = true)]
    public partial class MonthsOfOperation
    {
        public MonthsOfOperation()
        {
            TreatmentBMPModelingAttributes = new HashSet<TreatmentBMPModelingAttribute>();
        }

        [Key]
        public int MonthsOfOperationID { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string MonthsOfOperationName { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string MonthsOfOperationDisplayName { get; set; }
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string MonthsOfOperationNereidAlias { get; set; }

        [InverseProperty("MonthsOfOperation")]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
    }
}
