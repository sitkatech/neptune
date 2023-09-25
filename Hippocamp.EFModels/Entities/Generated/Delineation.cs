using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    [Table("Delineation")]
    [Index("TreatmentBMPID", Name = "AK_Delineation_TreatmentBMPID", IsUnique = true)]
    [Index("DelineationGeometry", Name = "SPATIAL_Delineation_DelineationGeometry")]
    public partial class Delineation
    {
        public Delineation()
        {
            DelineationOverlapDelineations = new HashSet<DelineationOverlap>();
            DelineationOverlapOverlappingDelineations = new HashSet<DelineationOverlap>();
            DirtyModelNodes = new HashSet<DirtyModelNode>();
            LoadGeneratingUnits = new HashSet<LoadGeneratingUnit>();
            NereidResults = new HashSet<NereidResult>();
            ProjectLoadGeneratingUnits = new HashSet<ProjectLoadGeneratingUnit>();
            ProjectNereidResults = new HashSet<ProjectNereidResult>();
        }

        [Key]
        public int DelineationID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry DelineationGeometry { get; set; }
        public int DelineationTypeID { get; set; }
        public bool IsVerified { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateLastVerified { get; set; }
        public int? VerifiedByPersonID { get; set; }
        public int TreatmentBMPID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateLastModified { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry DelineationGeometry4326 { get; set; }
        public bool HasDiscrepancies { get; set; }

        [ForeignKey("DelineationTypeID")]
        [InverseProperty("Delineations")]
        public virtual DelineationType DelineationType { get; set; }
        [ForeignKey("TreatmentBMPID")]
        [InverseProperty("Delineation")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        [ForeignKey("VerifiedByPersonID")]
        [InverseProperty("Delineations")]
        public virtual Person VerifiedByPerson { get; set; }
        [InverseProperty("Delineation")]
        public virtual ICollection<DelineationOverlap> DelineationOverlapDelineations { get; set; }
        [InverseProperty("OverlappingDelineation")]
        public virtual ICollection<DelineationOverlap> DelineationOverlapOverlappingDelineations { get; set; }
        [InverseProperty("Delineation")]
        public virtual ICollection<DirtyModelNode> DirtyModelNodes { get; set; }
        [InverseProperty("Delineation")]
        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        [InverseProperty("Delineation")]
        public virtual ICollection<NereidResult> NereidResults { get; set; }
        [InverseProperty("Delineation")]
        public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; }
        [InverseProperty("Delineation")]
        public virtual ICollection<ProjectNereidResult> ProjectNereidResults { get; set; }
    }
}
