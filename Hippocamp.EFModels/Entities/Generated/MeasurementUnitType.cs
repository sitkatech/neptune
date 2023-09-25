using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("MeasurementUnitType")]
    [Index("MeasurementUnitTypeDisplayName", Name = "AK_MeasurementUnitType_MeasurementUnitTypeDisplayName", IsUnique = true)]
    [Index("MeasurementUnitTypeName", Name = "AK_MeasurementUnitType_MeasurementUnitTypeName", IsUnique = true)]
    public partial class MeasurementUnitType
    {
        public MeasurementUnitType()
        {
            CustomAttributeTypes = new HashSet<CustomAttributeType>();
        }

        [Key]
        public int MeasurementUnitTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string MeasurementUnitTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string MeasurementUnitTypeDisplayName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LegendDisplayName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SingularDisplayName { get; set; }
        public int NumberOfSignificantDigits { get; set; }
        public bool IncludeSpaceBeforeLegendLabel { get; set; }

        [InverseProperty("MeasurementUnitType")]
        public virtual ICollection<CustomAttributeType> CustomAttributeTypes { get; set; }
    }
}
