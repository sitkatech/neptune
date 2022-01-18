using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("MonthsOfOperation")]
    [Index(nameof(MonthsOfOperationDisplayName), Name = "AK_MonthsOfOperation_MonthsOfOperationDisplayName", IsUnique = true)]
    [Index(nameof(MonthsOfOperationName), Name = "AK_MonthsOfOperation_MonthsOfOperationName", IsUnique = true)]
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
        public string MonthsOfOperationName { get; set; }
        [Required]
        [StringLength(6)]
        public string MonthsOfOperationDisplayName { get; set; }
        [Required]
        [StringLength(6)]
        public string MonthsOfOperationNereidAlias { get; set; }

        [InverseProperty(nameof(TreatmentBMPModelingAttribute.MonthsOfOperation))]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
    }
}
