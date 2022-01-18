using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanDocument")]
    [Index(nameof(DisplayName), nameof(WaterQualityManagementPlanID), Name = "AK_WaterQualityManagementPlanDocument_DisplayName_WaterQualityManagementPlanID", IsUnique = true)]
    public partial class WaterQualityManagementPlanDocument
    {
        [Key]
        public int WaterQualityManagementPlanDocumentID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int FileResourceID { get; set; }
        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UploadDate { get; set; }
        public int WaterQualityManagementPlanDocumentTypeID { get; set; }

        [ForeignKey(nameof(FileResourceID))]
        [InverseProperty("WaterQualityManagementPlanDocuments")]
        public virtual FileResource FileResource { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanID))]
        [InverseProperty("WaterQualityManagementPlanDocuments")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanDocumentTypeID))]
        [InverseProperty("WaterQualityManagementPlanDocuments")]
        public virtual WaterQualityManagementPlanDocumentType WaterQualityManagementPlanDocumentType { get; set; }
    }
}
