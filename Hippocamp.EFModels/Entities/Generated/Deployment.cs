using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("Deployment")]
    public partial class Deployment
    {
        [Key]
        public int DeploymentID { get; set; }
        [Required]
        [StringLength(15)]
        public string Version { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(100)]
        public string DeployedBy { get; set; }
        [Required]
        [StringLength(100)]
        public string DeployedFrom { get; set; }
        [Required]
        [StringLength(1000)]
        public string Source { get; set; }
        [Required]
        [StringLength(1000)]
        public string Script { get; set; }
    }
}
