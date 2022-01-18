using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("MeasurementUnitType")]
    [Index(nameof(MeasurementUnitTypeDisplayName), Name = "AK_MeasurementUnitType_MeasurementUnitTypeDisplayName", IsUnique = true)]
    [Index(nameof(MeasurementUnitTypeName), Name = "AK_MeasurementUnitType_MeasurementUnitTypeName", IsUnique = true)]
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
        public string MeasurementUnitTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string MeasurementUnitTypeDisplayName { get; set; }
        [StringLength(50)]
        public string LegendDisplayName { get; set; }
        [StringLength(50)]
        public string SingularDisplayName { get; set; }
        public int NumberOfSignificantDigits { get; set; }
        public bool IncludeSpaceBeforeLegendLabel { get; set; }

        [InverseProperty(nameof(CustomAttributeType.MeasurementUnitType))]
        public virtual ICollection<CustomAttributeType> CustomAttributeTypes { get; set; }
    }
}
