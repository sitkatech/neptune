using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("StormwaterJurisdiction")]
    [Index("OrganizationID", Name = "AK_StormwaterJurisdiction_OrganizationID", IsUnique = true)]
    public partial class StormwaterJurisdiction
    {
        public StormwaterJurisdiction()
        {
            DelineationStagings = new HashSet<DelineationStaging>();
            LandUseBlocks = new HashSet<LandUseBlock>();
            OnlandVisualTrashAssessmentAreas = new HashSet<OnlandVisualTrashAssessmentArea>();
            OnlandVisualTrashAssessments = new HashSet<OnlandVisualTrashAssessment>();
            Projects = new HashSet<Project>();
            StormwaterJurisdictionPeople = new HashSet<StormwaterJurisdictionPerson>();
            TrashGeneratingUnit4326s = new HashSet<TrashGeneratingUnit4326>();
            TrashGeneratingUnits = new HashSet<TrashGeneratingUnit>();
            TreatmentBMPs = new HashSet<TreatmentBMP>();
            WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        [Key]
        public int StormwaterJurisdictionID { get; set; }
        public int OrganizationID { get; set; }
        public int StateProvinceID { get; set; }
        public int StormwaterJurisdictionPublicBMPVisibilityTypeID { get; set; }
        public int StormwaterJurisdictionPublicWQMPVisibilityTypeID { get; set; }

        [ForeignKey("OrganizationID")]
        [InverseProperty("StormwaterJurisdiction")]
        public virtual Organization Organization { get; set; }
        [ForeignKey("StateProvinceID")]
        [InverseProperty("StormwaterJurisdictions")]
        public virtual StateProvince StateProvince { get; set; }
        [ForeignKey("StormwaterJurisdictionPublicBMPVisibilityTypeID")]
        [InverseProperty("StormwaterJurisdictions")]
        public virtual StormwaterJurisdictionPublicBMPVisibilityType StormwaterJurisdictionPublicBMPVisibilityType { get; set; }
        [ForeignKey("StormwaterJurisdictionPublicWQMPVisibilityTypeID")]
        [InverseProperty("StormwaterJurisdictions")]
        public virtual StormwaterJurisdictionPublicWQMPVisibilityType StormwaterJurisdictionPublicWQMPVisibilityType { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual StormwaterJurisdictionGeometry StormwaterJurisdictionGeometry { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual ICollection<DelineationStaging> DelineationStagings { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual ICollection<LandUseBlock> LandUseBlocks { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual ICollection<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreas { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual ICollection<Project> Projects { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual ICollection<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual ICollection<TrashGeneratingUnit4326> TrashGeneratingUnit4326s { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual ICollection<TrashGeneratingUnit> TrashGeneratingUnits { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
