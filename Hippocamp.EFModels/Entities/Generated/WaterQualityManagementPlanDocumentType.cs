using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanDocumentType")]
    [Index(nameof(WaterQualityManagementPlanDocumentTypeDisplayName), Name = "AK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeDisplayName", IsUnique = true)]
    [Index(nameof(WaterQualityManagementPlanDocumentTypeName), Name = "AK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeName", IsUnique = true)]
    public partial class WaterQualityManagementPlanDocumentType
    {
        public WaterQualityManagementPlanDocumentType()
        {
            WaterQualityManagementPlanDocuments = new HashSet<WaterQualityManagementPlanDocument>();
        }

        [Key]
        public int WaterQualityManagementPlanDocumentTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanDocumentTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanDocumentTypeDisplayName { get; set; }
        public bool IsRequired { get; set; }

        [InverseProperty(nameof(WaterQualityManagementPlanDocument.WaterQualityManagementPlanDocumentType))]
        public virtual ICollection<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; }
    }
}
