using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
        [Unicode(false)]
        public string AttributeValue { get; set; }

        [ForeignKey("CustomAttributeID")]
        [InverseProperty("CustomAttributeValues")]
        public virtual CustomAttribute CustomAttribute { get; set; }
    }
}
