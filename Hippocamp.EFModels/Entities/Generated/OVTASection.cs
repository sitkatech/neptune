using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("OVTASection")]
    [Index(nameof(OVTASectionName), Name = "AK_OVTASection_OVTASectionName", IsUnique = true)]
    public partial class OVTASection
    {
        [Key]
        public int OVTASectionID { get; set; }
        [Required]
        [StringLength(50)]
        public string OVTASectionName { get; set; }
        [Required]
        [StringLength(50)]
        public string OVTASectionDisplayName { get; set; }
        [Required]
        [StringLength(100)]
        public string SectionHeader { get; set; }
        public int SortOrder { get; set; }
        public bool HasCompletionStatus { get; set; }
    }
}
