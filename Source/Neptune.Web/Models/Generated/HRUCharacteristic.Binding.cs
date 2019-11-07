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
        public HRUCharacteristic(int hRUCharacteristicID, string lSPCLandUseDescription, string hydrologicSoilGroup, int slopePercentage, double imperviousAcres, DateTime lastUpdated, int? treatmentBMPID) : this()
        {
            this.HRUCharacteristicID = hRUCharacteristicID;
            this.LSPCLandUseDescription = lSPCLandUseDescription;
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.ImperviousAcres = imperviousAcres;
            this.LastUpdated = lastUpdated;
            this.TreatmentBMPID = treatmentBMPID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public HRUCharacteristic(string lSPCLandUseDescription, string hydrologicSoilGroup, int slopePercentage, double imperviousAcres, DateTime lastUpdated) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.HRUCharacteristicID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.LSPCLandUseDescription = lSPCLandUseDescription;
            this.HydrologicSoilGroup = hydrologicSoilGroup;
            this.SlopePercentage = slopePercentage;
            this.ImperviousAcres = imperviousAcres;
            this.LastUpdated = lastUpdated;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static HRUCharacteristic CreateNewBlank()
        {
            return new HRUCharacteristic(default(string), default(string), default(int), default(double), default(DateTime));
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
        public string LSPCLandUseDescription { get; set; }
        public string HydrologicSoilGroup { get; set; }
        public int SlopePercentage { get; set; }
        public double ImperviousAcres { get; set; }
        public DateTime LastUpdated { get; set; }
        public int? TreatmentBMPID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return HRUCharacteristicID; } set { HRUCharacteristicID = value; } }

        public virtual TreatmentBMP TreatmentBMP { get; set; }

        public static class FieldLengths
        {
            public const int LSPCLandUseDescription = 100;
            public const int HydrologicSoilGroup = 5;
        }
    }
}