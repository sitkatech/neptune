using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("FieldDefinition")]
    public partial class FieldDefinition
    {
        [Key]
        public int FieldDefinitionID { get; set; }
        public int FieldDefinitionTypeID { get; set; }
        public string FieldDefinitionValue { get; set; }

        [ForeignKey(nameof(FieldDefinitionTypeID))]
        [InverseProperty("FieldDefinitions")]
        public virtual FieldDefinitionType FieldDefinitionType { get; set; }
    }
}
