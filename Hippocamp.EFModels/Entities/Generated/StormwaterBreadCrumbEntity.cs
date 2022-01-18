using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("StormwaterBreadCrumbEntity")]
    public partial class StormwaterBreadCrumbEntity
    {
        [Key]
        public int StormwaterBreadCrumbEntityID { get; set; }
        [Required]
        [StringLength(100)]
        public string StormwaterBreadCrumbEntityName { get; set; }
        [Required]
        [StringLength(100)]
        public string StormwaterBreadCrumbEntityDisplayName { get; set; }
        [Required]
        [StringLength(100)]
        public string GlyphIconClass { get; set; }
        [Required]
        [StringLength(100)]
        public string ColorClass { get; set; }
    }
}
