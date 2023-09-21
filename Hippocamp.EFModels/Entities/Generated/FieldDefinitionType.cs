using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("FieldDefinitionType")]
    [Index("FieldDefinitionTypeDisplayName", Name = "AK_FieldDefinitionType_FieldDefinitionTypeDisplayName", IsUnique = true)]
    [Index("FieldDefinitionTypeName", Name = "AK_FieldDefinitionType_FieldDefinitionTypeName", IsUnique = true)]
    public partial class FieldDefinitionType
    {
        public FieldDefinitionType()
        {
            FieldDefinitions = new HashSet<FieldDefinition>();
        }

        [Key]
        public int FieldDefinitionTypeID { get; set; }
        [Required]
        [StringLength(300)]
        [Unicode(false)]
        public string FieldDefinitionTypeName { get; set; }
        [Required]
        [StringLength(300)]
        [Unicode(false)]
        public string FieldDefinitionTypeDisplayName { get; set; }

        [InverseProperty("FieldDefinitionType")]
        public virtual ICollection<FieldDefinition> FieldDefinitions { get; set; }
    }
}
