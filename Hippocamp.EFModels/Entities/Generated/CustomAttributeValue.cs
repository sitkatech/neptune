using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("CustomAttributeValue")]
    public partial class CustomAttributeValue
    {
        [Key]
        public int CustomAttributeValueID { get; set; }
        public int CustomAttributeID { get; set; }
        [Required]
        [StringLength(1000)]
        public string AttributeValue { get; set; }

        [ForeignKey(nameof(CustomAttributeID))]
        [InverseProperty("CustomAttributeValues")]
        public virtual CustomAttribute CustomAttribute { get; set; }
    }
}
