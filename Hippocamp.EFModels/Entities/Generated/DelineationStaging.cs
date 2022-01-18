using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("DelineationStaging")]
    [Index(nameof(TreatmentBMPName), nameof(StormwaterJurisdictionID), nameof(UploadedByPersonID), Name = "AK_DelineationStaging_TreatmentBMPName_StormwaterJurisdictionID", IsUnique = true)]
    public partial class DelineationStaging
    {
        [Key]
        public int DelineationStagingID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry DelineationStagingGeometry { get; set; }
        public int UploadedByPersonID { get; set; }
        [Required]
        [StringLength(200)]
        public string TreatmentBMPName { get; set; }
        public int StormwaterJurisdictionID { get; set; }

        [ForeignKey(nameof(StormwaterJurisdictionID))]
        [InverseProperty("DelineationStagings")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [ForeignKey(nameof(UploadedByPersonID))]
        [InverseProperty(nameof(Person.DelineationStagings))]
        public virtual Person UploadedByPerson { get; set; }
    }
}
