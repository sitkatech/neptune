//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdiction]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    // Table [dbo].[StormwaterJurisdiction] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[StormwaterJurisdiction]")]
    public partial class StormwaterJurisdiction : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected StormwaterJurisdiction()
        {
            this.DelineationStagings = new HashSet<DelineationStaging>();
            this.LandUseBlocks = new HashSet<LandUseBlock>();
            this.OnlandVisualTrashAssessments = new HashSet<OnlandVisualTrashAssessment>();
            this.OnlandVisualTrashAssessmentAreas = new HashSet<OnlandVisualTrashAssessmentArea>();
            this.StormwaterJurisdictionGeometries = new HashSet<StormwaterJurisdictionGeometry>();
            this.StormwaterJurisdictionPeople = new HashSet<StormwaterJurisdictionPerson>();
            this.TrashGeneratingUnits = new HashSet<TrashGeneratingUnit>();
            this.TrashGeneratingUnit4326s = new HashSet<TrashGeneratingUnit4326>();
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
            this.WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public StormwaterJurisdiction(int stormwaterJurisdictionID, int organizationID, int stateProvinceID) : this()
        {
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OrganizationID = organizationID;
            this.StateProvinceID = stateProvinceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public StormwaterJurisdiction(int organizationID, int stateProvinceID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.StormwaterJurisdictionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OrganizationID = organizationID;
            this.StateProvinceID = stateProvinceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public StormwaterJurisdiction(Organization organization, StateProvince stateProvince) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.StormwaterJurisdictionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.OrganizationID = organization.OrganizationID;
            this.Organization = organization;
            this.StateProvinceID = stateProvince.StateProvinceID;
            this.StateProvince = stateProvince;
            stateProvince.StormwaterJurisdictions.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static StormwaterJurisdiction CreateNewBlank(Organization organization, StateProvince stateProvince)
        {
            return new StormwaterJurisdiction(organization, stateProvince);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return DelineationStagings.Any() || LandUseBlocks.Any() || OnlandVisualTrashAssessments.Any() || OnlandVisualTrashAssessmentAreas.Any() || (StormwaterJurisdictionGeometry != null) || StormwaterJurisdictionPeople.Any() || TrashGeneratingUnits.Any() || TrashGeneratingUnit4326s.Any() || TreatmentBMPs.Any() || WaterQualityManagementPlans.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(DelineationStagings.Any())
            {
                dependentObjects.Add(typeof(DelineationStaging).Name);
            }

            if(LandUseBlocks.Any())
            {
                dependentObjects.Add(typeof(LandUseBlock).Name);
            }

            if(OnlandVisualTrashAssessments.Any())
            {
                dependentObjects.Add(typeof(OnlandVisualTrashAssessment).Name);
            }

            if(OnlandVisualTrashAssessmentAreas.Any())
            {
                dependentObjects.Add(typeof(OnlandVisualTrashAssessmentArea).Name);
            }

            if((StormwaterJurisdictionGeometry != null))
            {
                dependentObjects.Add(typeof(StormwaterJurisdictionGeometry).Name);
            }

            if(StormwaterJurisdictionPeople.Any())
            {
                dependentObjects.Add(typeof(StormwaterJurisdictionPerson).Name);
            }

            if(TrashGeneratingUnits.Any())
            {
                dependentObjects.Add(typeof(TrashGeneratingUnit).Name);
            }

            if(TrashGeneratingUnit4326s.Any())
            {
                dependentObjects.Add(typeof(TrashGeneratingUnit4326).Name);
            }

            if(TreatmentBMPs.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMP).Name);
            }

            if(WaterQualityManagementPlans.Any())
            {
                dependentObjects.Add(typeof(WaterQualityManagementPlan).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(StormwaterJurisdiction).Name, typeof(DelineationStaging).Name, typeof(LandUseBlock).Name, typeof(OnlandVisualTrashAssessment).Name, typeof(OnlandVisualTrashAssessmentArea).Name, typeof(StormwaterJurisdictionGeometry).Name, typeof(StormwaterJurisdictionPerson).Name, typeof(TrashGeneratingUnit).Name, typeof(TrashGeneratingUnit4326).Name, typeof(TreatmentBMP).Name, typeof(WaterQualityManagementPlan).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.StormwaterJurisdictions.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in DelineationStagings.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in LandUseBlocks.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OnlandVisualTrashAssessments.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in OnlandVisualTrashAssessmentAreas.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in StormwaterJurisdictionGeometries.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in StormwaterJurisdictionPeople.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TrashGeneratingUnits.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TrashGeneratingUnit4326s.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlans.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int StormwaterJurisdictionID { get; set; }
        public int OrganizationID { get; set; }
        public int StateProvinceID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return StormwaterJurisdictionID; } set { StormwaterJurisdictionID = value; } }

        public virtual ICollection<DelineationStaging> DelineationStagings { get; set; }
        public virtual ICollection<LandUseBlock> LandUseBlocks { get; set; }
        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
        public virtual ICollection<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreas { get; set; }
        public virtual ICollection<StormwaterJurisdictionGeometry> StormwaterJurisdictionGeometries { get; set; }
        [NotMapped]
        public StormwaterJurisdictionGeometry StormwaterJurisdictionGeometry { get { return StormwaterJurisdictionGeometries.SingleOrDefault(); } set { StormwaterJurisdictionGeometries = new List<StormwaterJurisdictionGeometry>{value};} }
        public virtual ICollection<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; }
        public virtual ICollection<TrashGeneratingUnit> TrashGeneratingUnits { get; set; }
        public virtual ICollection<TrashGeneratingUnit4326> TrashGeneratingUnit4326s { get; set; }
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual StateProvince StateProvince { get; set; }

        public static class FieldLengths
        {

        }
    }
}