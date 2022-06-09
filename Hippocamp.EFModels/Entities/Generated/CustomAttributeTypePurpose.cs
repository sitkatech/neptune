using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("CustomAttributeTypePurpose")]
    [Index("CustomAttributeTypePurposeDisplayName", Name = "AK_CustomAttributeTypePurpose_CustomAttributeTypePurposeDisplayName", IsUnique = true)]
    [Index("CustomAttributeTypePurposeName", Name = "AK_CustomAttributeTypePurpose_CustomAttributeTypePurposeName", IsUnique = true)]
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
        [Unicode(false)]
        public string CustomAttributeTypePurposeName { get; set; }
        [Required]
        [StringLength(60)]
        [Unicode(false)]
        public string CustomAttributeTypePurposeDisplayName { get; set; }

        [InverseProperty("CustomAttributeTypePurpose")]
        public virtual ICollection<CustomAttributeType> CustomAttributeTypes { get; set; }
    }
}
