using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("FieldVisitSection")]
    [Index(nameof(FieldVisitSectionName), Name = "AK_FieldVisitSection_FieldVisitSectionName", IsUnique = true)]
    public partial class FieldVisitSection
    {
        [Key]
        public int FieldVisitSectionID { get; set; }
        [Required]
        [StringLength(50)]
        public string FieldVisitSectionName { get; set; }
        [Required]
        [StringLength(50)]
        public string FieldVisitSectionDisplayName { get; set; }
        [Required]
        [StringLength(100)]
        public string SectionHeader { get; set; }
        public int SortOrder { get; set; }
    }
}
