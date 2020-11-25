//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRUCharacteristic]
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
    // Table [dbo].[HRUCharacteristic] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[HRUCharacteristic]")]
    public partial class HRUCharacteristic : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected HRUCharacteristic()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public HRUCharacteristic(int hRUCharacteristicID, string hydrologicSoilGroup, int slopePercentage, double imperviousAcres, DateTime lastUpdated, double area, int hRUCharacteristicLandUseCodeID, int loadGeneratingUnitID, double? baselineImperviousAcres, int? baselineHRUCharacteristicLandUseCodeID) : this()
        {
            this.HRUCharacteristicID = hRUCharacteristicID;
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.ImperviousAcres = imperviousAcres;
            this.LastUpdated = lastUpdated;
            this.Area = area;
            this.HRUCharacteristicLandUseCodeID = hRUCharacteristicLandUseCodeID;
            this.LoadGeneratingUnitID = loadGeneratingUnitID;
            this.BaselineImperviousAcres = baselineImperviousAcres;
            this.BaselineHRUCharacteristicLandUseCodeID = baselineHRUCharacteristicLandUseCodeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public HRUCharacteristic(string hydrologicSoilGroup, int slopePercentage, double imperviousAcres, DateTime lastUpdated, double area, int hRUCharacteristicLandUseCodeID, int loadGeneratingUnitID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.HRUCharacteristicID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.ImperviousAcres = imperviousAcres;
            this.LastUpdated = lastUpdated;
            this.Area = area;
            this.HRUCharacteristicLandUseCodeID = hRUCharacteristicLandUseCodeID;
            this.LoadGeneratingUnitID = loadGeneratingUnitID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public HRUCharacteristic(string hydrologicSoilGroup, int slopePercentage, double imperviousAcres, DateTime lastUpdated, double area, HRUCharacteristicLandUseCode hRUCharacteristicLandUseCode, LoadGeneratingUnit loadGeneratingUnit) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.HRUCharacteristicID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.ImperviousAcres = imperviousAcres;
            this.LastUpdated = lastUpdated;
            this.Area = area;
            this.HRUCharacteristicLandUseCodeID = hRUCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID;
            this.LoadGeneratingUnitID = loadGeneratingUnit.LoadGeneratingUnitID;
            this.LoadGeneratingUnit = loadGeneratingUnit;
            loadGeneratingUnit.HRUCharacteristics.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static HRUCharacteristic CreateNewBlank(HRUCharacteristicLandUseCode hRUCharacteristicLandUseCode, LoadGeneratingUnit loadGeneratingUnit)
        {
            return new HRUCharacteristic(default(string), default(int), default(double), default(DateTime), default(double), hRUCharacteristicLandUseCode, loadGeneratingUnit);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(HRUCharacteristic).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.HRUCharacteristics.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int HRUCharacteristicID { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        public DateTime LastUpdated { get; set; }
        public double Area { get; set; }
        public int HRUCharacteristicLandUseCodeID { get; set; }
        public int LoadGeneratingUnitID { get; set; }
        public double? BaselineImperviousAcres { get; set; }
        public int? BaselineHRUCharacteristicLandUseCodeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return HRUCharacteristicID; } set { HRUCharacteristicID = value; } }

        public HRUCharacteristicLandUseCode BaselineHRUCharacteristicLandUseCode { get { return BaselineHRUCharacteristicLandUseCodeID.HasValue ? HRUCharacteristicLandUseCode.AllLookupDictionary[BaselineHRUCharacteristicLandUseCodeID.Value] : null; } }
        public HRUCharacteristicLandUseCode HRUCharacteristicLandUseCode { get { return HRUCharacteristicLandUseCode.AllLookupDictionary[HRUCharacteristicLandUseCodeID]; } }
        public virtual LoadGeneratingUnit LoadGeneratingUnit { get; set; }

        public static class FieldLengths
        {
            public const int HydrologicSoilGroup = 5;
        }
    }
}