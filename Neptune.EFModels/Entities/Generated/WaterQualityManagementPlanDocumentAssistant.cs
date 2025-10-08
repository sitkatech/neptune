using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("WaterQualityManagementPlanDocumentAssistant")]
[Index("WaterQualityManagementPlanDocumentID", Name = "UQ_WaterQualityManagementPlanDocumentAssistant_WaterQualityManagementPlanDocumentID", IsUnique = true)]
public partial class WaterQualityManagementPlanDocumentAssistant
{
    [Key]
    public int WaterQualityManagementPlanDocumentAssistantID { get; set; }

    public int WaterQualityManagementPlanDocumentID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AssistantID { get; set; } = null!;

    [ForeignKey("WaterQualityManagementPlanDocumentID")]
    [InverseProperty("WaterQualityManagementPlanDocumentAssistant")]
    public virtual WaterQualityManagementPlanDocument WaterQualityManagementPlanDocument { get; set; } = null!;
}
