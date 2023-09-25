using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vModelingResultUnitConversion
    {
        public int PrimaryKey { get; set; }
        [Column(TypeName = "numeric(6, 6)")]
        public decimal PoundsToKilogramsFactor { get; set; }
        [Column(TypeName = "numeric(5, 2)")]
        public decimal PoundsToGramsFactor { get; set; }
        public double BillionsFactor { get; set; }
    }
}
