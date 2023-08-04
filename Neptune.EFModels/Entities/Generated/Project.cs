using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("Project")]
[Index("ProjectName", Name = "AK_Project_ProjectName", IsUnique = true)]
public partial class Project
{
    [Key]
    public int ProjectID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? ProjectName { get; set; }

    public int OrganizationID { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    public int ProjectStatusID { get; set; }

    public int PrimaryContactPersonID { get; set; }

    public int CreatePersonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ProjectDescription { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? AdditionalContactInformation { get; set; }

    public bool DoesNotIncludeTreatmentBMPs { get; set; }

    public bool CalculateOCTAM2Tier2Scores { get; set; }

    public bool ShareOCTAM2Tier2Scores { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OCTAM2Tier2ScoresLastSharedDate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? OCTAWatersheds { get; set; }

    public double? PollutantVolume { get; set; }

    public double? PollutantMetals { get; set; }

    public double? PollutantBacteria { get; set; }

    public double? PollutantNutrients { get; set; }

    public double? PollutantTSS { get; set; }

    public double? TPI { get; set; }

    public double? SEA { get; set; }

    public double? DryWeatherWQLRI { get; set; }

    public double? WetWeatherWQLRI { get; set; }

    public double? AreaTreatedAcres { get; set; }

    public double? ImperviousAreaTreatedAcres { get; set; }

    public int? UpdatePersonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateUpdated { get; set; }

    [ForeignKey("CreatePersonID")]
    [InverseProperty("ProjectCreatePeople")]
    public virtual Person CreatePerson { get; set; } = null!;

    [ForeignKey("OrganizationID")]
    [InverseProperty("Projects")]
    public virtual Organization Organization { get; set; } = null!;

    [ForeignKey("PrimaryContactPersonID")]
    [InverseProperty("ProjectPrimaryContactPeople")]
    public virtual Person PrimaryContactPerson { get; set; } = null!;

    [InverseProperty("Project")]
    public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; } = new List<ProjectDocument>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectHRUCharacteristic> ProjectHRUCharacteristics { get; set; } = new List<ProjectHRUCharacteristic>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; } = new List<ProjectLoadGeneratingUnit>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectNereidResult> ProjectNereidResults { get; set; } = new List<ProjectNereidResult>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectNetworkSolveHistory> ProjectNetworkSolveHistories { get; set; } = new List<ProjectNetworkSolveHistory>();

    [ForeignKey("StormwaterJurisdictionID")]
    [InverseProperty("Projects")]
    public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; } = null!;

    [InverseProperty("Project")]
    public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; } = new List<TreatmentBMP>();

    [ForeignKey("UpdatePersonID")]
    [InverseProperty("ProjectUpdatePeople")]
    public virtual Person? UpdatePerson { get; set; }
}
