using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("TrainingVideo")]
    public partial class TrainingVideo
    {
        [Key]
        public int TrainingVideoID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string VideoName { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string VideoDescription { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string VideoURL { get; set; }
    }
}
