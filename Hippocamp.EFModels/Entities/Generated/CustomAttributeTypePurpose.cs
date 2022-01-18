using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("CustomAttributeTypePurpose")]
    [Index(nameof(CustomAttributeTypePurposeDisplayName), Name = "AK_CustomAttributeTypePurpose_CustomAttributeTypePurposeDisplayName", IsUnique = true)]
    [Index(nameof(CustomAttributeTypePurposeName), Name = "AK_CustomAttributeTypePurpose_CustomAttributeTypePurposeName", IsUnique = true)]
    public partial class CustomAttributeTypePurpose
    {
        public CustomAttributeTypePurpose()
        {
            CustomAttributeTypes = new HashSet<CustomAttributeType>();
        }

        [Key]
        public int CustomAttributeTypePurposeID { get; set; }
        [Required]
        [StringLength(60)]
        public string CustomAttributeTypePurposeName { get; set; }
        [Required]
        [StringLength(60)]
        public string CustomAttributeTypePurposeDisplayName { get; set; }

        [InverseProperty(nameof(CustomAttributeType.CustomAttributeTypePurpose))]
        public virtual ICollection<CustomAttributeType> CustomAttributeTypes { get; set; }
    }
}
