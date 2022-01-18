using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("Watershed")]
    public partial class Watershed
    {
        public Watershed()
        {
            TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        [Key]
        public int WatershedID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry WatershedGeometry { get; set; }
        [StringLength(50)]
        public string WatershedName { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry WatershedGeometry4326 { get; set; }

        [InverseProperty(nameof(TreatmentBMP.Watershed))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
