using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("RegionalSubbasin")]
    [Index(nameof(OCSurveyCatchmentID), Name = "AK_RegionalSubbasin_OCSurveyCatchmentID", IsUnique = true)]
    [Index(nameof(OCSurveyDownstreamCatchmentID), Name = "IX_RegionalSubbasin_OCSurveyDownstreamCatchmentID")]
    [Index(nameof(CatchmentGeometry), Name = "SPATIAL_RegionalSubbasin_CatchmentGeometry")]
    public partial class RegionalSubbasin
    {
        public RegionalSubbasin()
        {
            InverseOCSurveyDownstreamCatchment = new HashSet<RegionalSubbasin>();
            LoadGeneratingUnits = new HashSet<LoadGeneratingUnit>();
            ProjectLoadGeneratingUnits = new HashSet<ProjectLoadGeneratingUnit>();
        }

        [Key]
        public int RegionalSubbasinID { get; set; }
        [StringLength(10)]
        public string DrainID { get; set; }
        [StringLength(100)]
        public string Watershed { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry CatchmentGeometry { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int? OCSurveyDownstreamCatchmentID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry CatchmentGeometry4326 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        public bool? IsWaitingForLGURefresh { get; set; }
        public bool? IsInModelBasin { get; set; }
        public int? ModelBasinID { get; set; }

        [ForeignKey(nameof(ModelBasinID))]
        [InverseProperty("RegionalSubbasins")]
        public virtual ModelBasin ModelBasin { get; set; }
        public virtual RegionalSubbasin OCSurveyDownstreamCatchment { get; set; }
        public virtual ICollection<RegionalSubbasin> InverseOCSurveyDownstreamCatchment { get; set; }
        [InverseProperty(nameof(LoadGeneratingUnit.RegionalSubbasin))]
        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        [InverseProperty(nameof(ProjectLoadGeneratingUnit.RegionalSubbasin))]
        public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; }
    }
}
