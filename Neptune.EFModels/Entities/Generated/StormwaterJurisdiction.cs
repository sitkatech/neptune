using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("StormwaterJurisdiction")]
[Index("OrganizationID", Name = "AK_StormwaterJurisdiction_OrganizationID", IsUnique = true)]
public partial class StormwaterJurisdiction
{
    [Key]
    public int StormwaterJurisdictionID { get; set; }

    public int OrganizationID { get; set; }

    public int StateProvinceID { get; set; }

    public int StormwaterJurisdictionPublicBMPVisibilityTypeID { get; set; }

    public int StormwaterJurisdictionPublicWQMPVisibilityTypeID { get; set; }

    [InverseProperty("StormwaterJurisdiction")]
    public virtual ICollection<DelineationStaging> DelineationStagings { get; set; } = new List<DelineationStaging>();

    [InverseProperty("StormwaterJurisdiction")]
    public virtual ICollection<LandUseBlock> LandUseBlocks { get; set; } = new List<LandUseBlock>();

    [InverseProperty("StormwaterJurisdiction")]
    public virtual ICollection<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreas { get; set; } = new List<OnlandVisualTrashAssessmentArea>();

    [InverseProperty("StormwaterJurisdiction")]
    public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; } = new List<OnlandVisualTrashAssessment>();

    [ForeignKey("OrganizationID")]
    [InverseProperty("StormwaterJurisdiction")]
    public virtual Organization Organization { get; set; } = null!;

    [InverseProperty("StormwaterJurisdiction")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [ForeignKey("StateProvinceID")]
    [InverseProperty("StormwaterJurisdictions")]
    public virtual StateProvince StateProvince { get; set; } = null!;

    [InverseProperty("StormwaterJurisdiction")]
    public virtual StormwaterJurisdictionGeometry? StormwaterJurisdictionGeometry { get; set; }

    [InverseProperty("StormwaterJurisdiction")]
    public virtual ICollection<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; } = new List<StormwaterJurisdictionPerson>();

    [InverseProperty("StormwaterJurisdiction")]
    public virtual ICollection<TrashGeneratingUnit4326> TrashGeneratingUnit4326s { get; set; } = new List<TrashGeneratingUnit4326>();

    [InverseProperty("StormwaterJurisdiction")]
    public virtual ICollection<TrashGeneratingUnit> TrashGeneratingUnits { get; set; } = new List<TrashGeneratingUnit>();

    [InverseProperty("StormwaterJurisdiction")]
    public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; } = new List<TreatmentBMP>();

    [InverseProperty("StormwaterJurisdiction")]
    public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; } = new List<WaterQualityManagementPlan>();
}
