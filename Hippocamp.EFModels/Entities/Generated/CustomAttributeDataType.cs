using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("CustomAttributeDataType")]
    [Index(nameof(CustomAttributeDataTypeDisplayName), Name = "AK_CustomAttributeDataType_CustomAttributeDataTypeDisplayName", IsUnique = true)]
    [Index(nameof(CustomAttributeDataTypeName), Name = "AK_CustomAttributeDataType_CustomAttributeDataTypeName", IsUnique = true)]
    public partial class CustomAttributeDataType
    {
        public CustomAttributeDataType()
        {
            CustomAttributeTypes = new HashSet<CustomAttributeType>();
        }

        [Key]
        public int CustomAttributeDataTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomAttributeDataTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string CustomAttributeDataTypeDisplayName { get; set; }

        [InverseProperty(nameof(CustomAttributeType.CustomAttributeDataType))]
        public virtual ICollection<CustomAttributeType> CustomAttributeTypes { get; set; }
    }
}
