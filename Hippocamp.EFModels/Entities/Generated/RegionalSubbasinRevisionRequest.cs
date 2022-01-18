﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("RegionalSubbasinRevisionRequest")]
    public partial class RegionalSubbasinRevisionRequest
    {
        [Key]
        public int RegionalSubbasinRevisionRequestID { get; set; }
        public int TreatmentBMPID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry RegionalSubbasinRevisionRequestGeometry { get; set; }
        public int RequestPersonID { get; set; }
        public int RegionalSubbasinRevisionRequestStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RequestDate { get; set; }
        public int? ClosedByPersonID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ClosedDate { get; set; }
        public string Notes { get; set; }
        public string CloseNotes { get; set; }

        [ForeignKey(nameof(ClosedByPersonID))]
        [InverseProperty(nameof(Person.RegionalSubbasinRevisionRequestClosedByPeople))]
        public virtual Person ClosedByPerson { get; set; }
        [ForeignKey(nameof(RegionalSubbasinRevisionRequestStatusID))]
        [InverseProperty("RegionalSubbasinRevisionRequests")]
        public virtual RegionalSubbasinRevisionRequestStatus RegionalSubbasinRevisionRequestStatus { get; set; }
        [ForeignKey(nameof(RequestPersonID))]
        [InverseProperty(nameof(Person.RegionalSubbasinRevisionRequestRequestPeople))]
        public virtual Person RequestPerson { get; set; }
        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("RegionalSubbasinRevisionRequests")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
    }
}
