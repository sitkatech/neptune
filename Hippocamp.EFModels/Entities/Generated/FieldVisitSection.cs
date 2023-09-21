using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("FieldVisitSection")]
    [Index("FieldVisitSectionName", Name = "AK_FieldVisitSection_FieldVisitSectionName", IsUnique = true)]
    public partial class FieldVisitSection
    {
        [Key]
        public int FieldVisitSectionID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string FieldVisitSectionName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string FieldVisitSectionDisplayName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string SectionHeader { get; set; }
        public int SortOrder { get; set; }
    }
}
