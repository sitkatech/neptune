using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanDocumentType")]
    [Index("WaterQualityManagementPlanDocumentTypeDisplayName", Name = "AK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeDisplayName", IsUnique = true)]
    [Index("WaterQualityManagementPlanDocumentTypeName", Name = "AK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string WaterQualityManagementPlanDocumentTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanDocumentTypeDisplayName { get; set; }
        public bool IsRequired { get; set; }

        [InverseProperty("WaterQualityManagementPlanDocumentType")]
        public virtual ICollection<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; }
    }
}
