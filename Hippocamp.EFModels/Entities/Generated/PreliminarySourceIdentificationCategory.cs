using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("PreliminarySourceIdentificationCategory")]
    [Index(nameof(PreliminarySourceIdentificationCategoryDisplayName), Name = "AK_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryDisplayName", IsUnique = true)]
    [Index(nameof(PreliminarySourceIdentificationCategoryName), Name = "AK_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryName", IsUnique = true)]
    public partial class PreliminarySourceIdentificationCategory
    {
        public PreliminarySourceIdentificationCategory()
        {
            PreliminarySourceIdentificationTypes = new HashSet<PreliminarySourceIdentificationType>();
        }

        [Key]
        public int PreliminarySourceIdentificationCategoryID { get; set; }
        [Required]
        [StringLength(100)]
        public string PreliminarySourceIdentificationCategoryName { get; set; }
        [Required]
        [StringLength(500)]
        public string PreliminarySourceIdentificationCategoryDisplayName { get; set; }

        [InverseProperty(nameof(PreliminarySourceIdentificationType.PreliminarySourceIdentificationCategory))]
        public virtual ICollection<PreliminarySourceIdentificationType> PreliminarySourceIdentificationTypes { get; set; }
    }
}
