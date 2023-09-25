using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vGeoServerTrashGeneratingUnit
    {
        public int TrashGeneratingUnit4326ID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? OrganizationID { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string OrganizationName { get; set; }
        [Required]
        [StringLength(11)]
        [Unicode(false)]
        public string TrashCaptureStatus { get; set; }
        [StringLength(11)]
        [Unicode(false)]
        public string AssessmentScore { get; set; }
        public int IsPriorityLandUse { get; set; }
        public int NoDataProvided { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry TrashGeneratingUnitGeometry { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public int? TreatmentBMPID { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string TreatmentBMPName { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string WaterQualityManagementPlanName { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string LandUseType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastCalculatedDate { get; set; }
    }
}
