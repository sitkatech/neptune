using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("WaterQualityManagementPlanDocumentVectorStore")]
[Index("WaterQualityManagementPlanDocumentID", Name = "UQ_WaterQualityManagementPlanDocumentVectorStore_WaterQualityManagementPlanDocumentID", IsUnique = true)]
public partial class WaterQualityManagementPlanDocumentVectorStore
{
    [Key]
    public int WaterQualityManagementPlanDocumentVectorStoreID { get; set; }

    public int WaterQualityManagementPlanDocumentID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string OpenAIVectorStoreID { get; set; } = null!;

    [ForeignKey("WaterQualityManagementPlanDocumentID")]
    [InverseProperty("WaterQualityManagementPlanDocumentVectorStore")]
    public virtual WaterQualityManagementPlanDocument WaterQualityManagementPlanDocument { get; set; } = null!;
}
