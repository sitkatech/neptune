using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("StormwaterJurisdiction")]
    [Index(nameof(OrganizationID), Name = "AK_StormwaterJurisdiction_OrganizationID", IsUnique = true)]
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

        [ForeignKey(nameof(OrganizationID))]
        [InverseProperty("StormwaterJurisdiction")]
        public virtual Organization Organization { get; set; }
        [ForeignKey(nameof(StateProvinceID))]
        [InverseProperty("StormwaterJurisdictions")]
        public virtual StateProvince StateProvince { get; set; }
        [ForeignKey(nameof(StormwaterJurisdictionPublicBMPVisibilityTypeID))]
        [InverseProperty("StormwaterJurisdictions")]
        public virtual StormwaterJurisdictionPublicBMPVisibilityType StormwaterJurisdictionPublicBMPVisibilityType { get; set; }
        [ForeignKey(nameof(StormwaterJurisdictionPublicWQMPVisibilityTypeID))]
        [InverseProperty("StormwaterJurisdictions")]
        public virtual StormwaterJurisdictionPublicWQMPVisibilityType StormwaterJurisdictionPublicWQMPVisibilityType { get; set; }
        [InverseProperty("StormwaterJurisdiction")]
        public virtual StormwaterJurisdictionGeometry StormwaterJurisdictionGeometry { get; set; }
        [InverseProperty(nameof(DelineationStaging.StormwaterJurisdiction))]
        public virtual ICollection<DelineationStaging> DelineationStagings { get; set; }
        [InverseProperty(nameof(LandUseBlock.StormwaterJurisdiction))]
        public virtual ICollection<LandUseBlock> LandUseBlocks { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessmentArea.StormwaterJurisdiction))]
        public virtual ICollection<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreas { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessment.StormwaterJurisdiction))]
        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
        [InverseProperty(nameof(Project.StormwaterJurisdiction))]
        public virtual ICollection<Project> Projects { get; set; }
        [InverseProperty(nameof(StormwaterJurisdictionPerson.StormwaterJurisdiction))]
        public virtual ICollection<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; }
        [InverseProperty(nameof(TrashGeneratingUnit4326.StormwaterJurisdiction))]
        public virtual ICollection<TrashGeneratingUnit4326> TrashGeneratingUnit4326s { get; set; }
        [InverseProperty(nameof(TrashGeneratingUnit.StormwaterJurisdiction))]
        public virtual ICollection<TrashGeneratingUnit> TrashGeneratingUnits { get; set; }
        [InverseProperty(nameof(TreatmentBMP.StormwaterJurisdiction))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlan.StormwaterJurisdiction))]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
