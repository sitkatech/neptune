using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("FieldDefinitionType")]
    [Index(nameof(FieldDefinitionTypeDisplayName), Name = "AK_FieldDefinitionType_FieldDefinitionTypeDisplayName", IsUnique = true)]
    [Index(nameof(FieldDefinitionTypeName), Name = "AK_FieldDefinitionType_FieldDefinitionTypeName", IsUnique = true)]
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
        public string FieldDefinitionTypeName { get; set; }
        [Required]
        [StringLength(300)]
        public string FieldDefinitionTypeDisplayName { get; set; }

        [InverseProperty(nameof(FieldDefinition.FieldDefinitionType))]
        public virtual ICollection<FieldDefinition> FieldDefinitions { get; set; }
    }
}
