using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("OVTASection")]
    [Index("OVTASectionName", Name = "AK_OVTASection_OVTASectionName", IsUnique = true)]
    public partial class OVTASection
    {
        [Key]
        public int OVTASectionID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string OVTASectionName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string OVTASectionDisplayName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string SectionHeader { get; set; }
        public int SortOrder { get; set; }
        public bool HasCompletionStatus { get; set; }
    }
}
