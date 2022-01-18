using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TrainingVideo")]
    public partial class TrainingVideo
    {
        [Key]
        public int TrainingVideoID { get; set; }
        [Required]
        [StringLength(100)]
        public string VideoName { get; set; }
        [StringLength(500)]
        public string VideoDescription { get; set; }
        [Required]
        [StringLength(100)]
        public string VideoURL { get; set; }
    }
}
