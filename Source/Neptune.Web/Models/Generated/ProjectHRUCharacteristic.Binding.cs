//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectHRUCharacteristic]
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
    // Table [dbo].[ProjectHRUCharacteristic] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectHRUCharacteristic]")]
    public partial class ProjectHRUCharacteristic : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectHRUCharacteristic()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectHRUCharacteristic(int projectHRUCharacteristicID, int projectID, string hydrologicSoilGroup, int slopePercentage, double imperviousAcres, DateTime lastUpdated, double area, int hRUCharacteristicLandUseCodeID, int projectLoadGeneratingUnitID, double baselineImperviousAcres, int baselineHRUCharacteristicLandUseCodeID) : this()
        {
            this.ProjectHRUCharacteristicID = projectHRUCharacteristicID;
            this.ProjectID = projectID;
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.ImperviousAcres = imperviousAcres;
            this.LastUpdated = lastUpdated;
            this.Area = area;
            this.HRUCharacteristicLandUseCodeID = hRUCharacteristicLandUseCodeID;
            this.ProjectLoadGeneratingUnitID = projectLoadGeneratingUnitID;
            this.BaselineImperviousAcres = baselineImperviousAcres;
            this.BaselineHRUCharacteristicLandUseCodeID = baselineHRUCharacteristicLandUseCodeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectHRUCharacteristic(int projectID, string hydrologicSoilGroup, int slopePercentage, double imperviousAcres, DateTime lastUpdated, double area, int hRUCharacteristicLandUseCodeID, int projectLoadGeneratingUnitID, double baselineImperviousAcres, int baselineHRUCharacteristicLandUseCodeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectHRUCharacteristicID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.ImperviousAcres = imperviousAcres;
            this.LastUpdated = lastUpdated;
            this.Area = area;
            this.HRUCharacteristicLandUseCodeID = hRUCharacteristicLandUseCodeID;
            this.ProjectLoadGeneratingUnitID = projectLoadGeneratingUnitID;
            this.BaselineImperviousAcres = baselineImperviousAcres;
            this.BaselineHRUCharacteristicLandUseCodeID = baselineHRUCharacteristicLandUseCodeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectHRUCharacteristic(Project project, string hydrologicSoilGroup, int slopePercentage, double imperviousAcres, DateTime lastUpdated, double area, HRUCharacteristicLandUseCode hRUCharacteristicLandUseCode, ProjectLoadGeneratingUnit projectLoadGeneratingUnit, double baselineImperviousAcres, HRUCharacteristicLandUseCode baselineHRUCharacteristicLandUseCode) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectHRUCharacteristicID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectHRUCharacteristics.Add(this);
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.ImperviousAcres = imperviousAcres;
            this.LastUpdated = lastUpdated;
            this.Area = area;
            this.HRUCharacteristicLandUseCodeID = hRUCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID;
            this.ProjectLoadGeneratingUnitID = projectLoadGeneratingUnit.ProjectLoadGeneratingUnitID;
            this.ProjectLoadGeneratingUnit = projectLoadGeneratingUnit;
            projectLoadGeneratingUnit.ProjectHRUCharacteristics.Add(this);
            this.BaselineImperviousAcres = baselineImperviousAcres;
            this.BaselineHRUCharacteristicLandUseCodeID = baselineHRUCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectHRUCharacteristic CreateNewBlank(Project project, HRUCharacteristicLandUseCode hRUCharacteristicLandUseCode, ProjectLoadGeneratingUnit projectLoadGeneratingUnit, HRUCharacteristicLandUseCode baselineHRUCharacteristicLandUseCode)
        {
            return new ProjectHRUCharacteristic(project, default(string), default(int), default(double), default(DateTime), default(double), hRUCharacteristicLandUseCode, projectLoadGeneratingUnit, default(double), baselineHRUCharacteristicLandUseCode);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return false;
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectHRUCharacteristic).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectHRUCharacteristics.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectHRUCharacteristicID { get; set; }
        public int ProjectID { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        public DateTime LastUpdated { get; set; }
        public double Area { get; set; }
        public int HRUCharacteristicLandUseCodeID { get; set; }
        public int ProjectLoadGeneratingUnitID { get; set; }
        public double BaselineImperviousAcres { get; set; }
        public int BaselineHRUCharacteristicLandUseCodeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectHRUCharacteristicID; } set { ProjectHRUCharacteristicID = value; } }

        public virtual Project Project { get; set; }
        public HRUCharacteristicLandUseCode BaselineHRUCharacteristicLandUseCode { get { return HRUCharacteristicLandUseCode.AllLookupDictionary[BaselineHRUCharacteristicLandUseCodeID]; } }
        public HRUCharacteristicLandUseCode HRUCharacteristicLandUseCode { get { return HRUCharacteristicLandUseCode.AllLookupDictionary[HRUCharacteristicLandUseCodeID]; } }
        public virtual ProjectLoadGeneratingUnit ProjectLoadGeneratingUnit { get; set; }

        public static class FieldLengths
        {
            public const int HydrologicSoilGroup = 5;
        }
    }
}