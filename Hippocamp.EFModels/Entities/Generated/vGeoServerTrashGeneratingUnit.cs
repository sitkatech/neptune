using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vGeoServerTrashGeneratingUnit
    {
        public int TrashGeneratingUnit4326ID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? OrganizationID { get; set; }
        [StringLength(200)]
        public string OrganizationName { get; set; }
        [Required]
        [StringLength(11)]
        public string TrashCaptureStatus { get; set; }
        [StringLength(11)]
        public string AssessmentScore { get; set; }
        public int IsPriorityLandUse { get; set; }
        public int NoDataProvided { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry TrashGeneratingUnitGeometry { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public int? TreatmentBMPID { get; set; }
        [StringLength(200)]
        public string TreatmentBMPName { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        [StringLength(100)]
        public string WaterQualityManagementPlanName { get; set; }
        [StringLength(100)]
        public string LandUseType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastCalculatedDate { get; set; }
    }
}
