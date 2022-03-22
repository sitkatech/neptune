using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("Delineation")]
    [Index(nameof(TreatmentBMPID), Name = "AK_Delineation_TreatmentBMPID", IsUnique = true)]
    [Index(nameof(DelineationGeometry), Name = "SPATIAL_Delineation_DelineationGeometry")]
    public partial class Delineation
    {
        public Delineation()
        {
            DelineationOverlapDelineations = new HashSet<DelineationOverlap>();
            DelineationOverlapOverlappingDelineations = new HashSet<DelineationOverlap>();
            LoadGeneratingUnits = new HashSet<LoadGeneratingUnit>();
            ProjectLoadGeneratingUnits = new HashSet<ProjectLoadGeneratingUnit>();
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

        [ForeignKey(nameof(DelineationTypeID))]
        [InverseProperty("Delineations")]
        public virtual DelineationType DelineationType { get; set; }
        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("Delineation")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        [ForeignKey(nameof(VerifiedByPersonID))]
        [InverseProperty(nameof(Person.Delineations))]
        public virtual Person VerifiedByPerson { get; set; }
        [InverseProperty(nameof(DelineationOverlap.Delineation))]
        public virtual ICollection<DelineationOverlap> DelineationOverlapDelineations { get; set; }
        [InverseProperty(nameof(DelineationOverlap.OverlappingDelineation))]
        public virtual ICollection<DelineationOverlap> DelineationOverlapOverlappingDelineations { get; set; }
        [InverseProperty(nameof(LoadGeneratingUnit.Delineation))]
        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        [InverseProperty(nameof(ProjectLoadGeneratingUnit.Delineation))]
        public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; }
    }
}
