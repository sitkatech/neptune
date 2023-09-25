using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("CustomAttributeDataType")]
    [Index("CustomAttributeDataTypeDisplayName", Name = "AK_CustomAttributeDataType_CustomAttributeDataTypeDisplayName", IsUnique = true)]
    [Index("CustomAttributeDataTypeName", Name = "AK_CustomAttributeDataType_CustomAttributeDataTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string CustomAttributeDataTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CustomAttributeDataTypeDisplayName { get; set; }

        [InverseProperty("CustomAttributeDataType")]
        public virtual ICollection<CustomAttributeType> CustomAttributeTypes { get; set; }
    }
}
