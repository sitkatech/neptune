using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("TrashGeneratingUnitAdjustment")]
    [Index("DeletedGeometry", Name = "SPATIAL_TrashGeneratingUnitAdjustment_DeletedGeometry")]
    public partial class TrashGeneratingUnitAdjustment
    {
        [Key]
        public int TrashGeneratingUnitAdjustmentID { get; set; }
        public int? AdjustedDelineationID { get; set; }
        public int? AdjustedOnlandVisualTrashAssessmentAreaID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry DeletedGeometry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AdjustmentDate { get; set; }
        public int AdjustedByPersonID { get; set; }
        public bool IsProcessed { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ProcessedDate { get; set; }

        [ForeignKey("AdjustedByPersonID")]
        [InverseProperty("TrashGeneratingUnitAdjustments")]
        public virtual Person AdjustedByPerson { get; set; }
    }
}
