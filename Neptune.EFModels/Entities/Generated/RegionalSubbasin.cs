using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("RegionalSubbasin")]
[Index("OCSurveyCatchmentID", Name = "AK_RegionalSubbasin_OCSurveyCatchmentID", IsUnique = true)]
[Index("OCSurveyDownstreamCatchmentID", Name = "IX_RegionalSubbasin_OCSurveyDownstreamCatchmentID")]
[Index("CatchmentGeometry", Name = "SPATIAL_RegionalSubbasin_CatchmentGeometry")]
public partial class RegionalSubbasin
{
    [Key]
    public int RegionalSubbasinID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? DrainID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Watershed { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry CatchmentGeometry { get; set; } = null!;

    public int OCSurveyCatchmentID { get; set; }

    public int? OCSurveyDownstreamCatchmentID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? CatchmentGeometry4326 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdate { get; set; }

    public bool? IsWaitingForLGURefresh { get; set; }

    public bool? IsInModelBasin { get; set; }

    public int? ModelBasinID { get; set; }

    [InverseProperty("RegionalSubbasin")]
    public virtual ICollection<DirtyModelNode> DirtyModelNodes { get; set; } = new List<DirtyModelNode>();

    public virtual ICollection<RegionalSubbasin> InverseOCSurveyDownstreamCatchment { get; set; } = new List<RegionalSubbasin>();

    [InverseProperty("RegionalSubbasin")]
    public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; } = new List<LoadGeneratingUnit>();

    [ForeignKey("ModelBasinID")]
    [InverseProperty("RegionalSubbasins")]
    public virtual ModelBasin? ModelBasin { get; set; }

    [InverseProperty("RegionalSubbasin")]
    public virtual ICollection<NereidResult> NereidResults { get; set; } = new List<NereidResult>();

    public virtual RegionalSubbasin? OCSurveyDownstreamCatchment { get; set; }

    [InverseProperty("RegionalSubbasin")]
    public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; } = new List<ProjectLoadGeneratingUnit>();

    [InverseProperty("RegionalSubbasin")]
    public virtual ICollection<ProjectNereidResult> ProjectNereidResults { get; set; } = new List<ProjectNereidResult>();
}
