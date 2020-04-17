//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnit]
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
    // Table [dbo].[LoadGeneratingUnit] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[LoadGeneratingUnit]")]
    public partial class LoadGeneratingUnit : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected LoadGeneratingUnit()
        {
            this.HRUCharacteristics = new HashSet<HRUCharacteristic>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public LoadGeneratingUnit(int loadGeneratingUnitID, DbGeometry loadGeneratingUnitGeometry, int? lSPCBasinID, int? regionalSubbasinID, int? delineationID, int? waterQualityManagementPlanID, bool? isEmptyResponseFromHRUService) : this()
        {
            this.LoadGeneratingUnitID = loadGeneratingUnitID;
            this.LoadGeneratingUnitGeometry = loadGeneratingUnitGeometry;
            this.LSPCBasinID = lSPCBasinID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.DelineationID = delineationID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.IsEmptyResponseFromHRUService = isEmptyResponseFromHRUService;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public LoadGeneratingUnit(DbGeometry loadGeneratingUnitGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LoadGeneratingUnitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.LoadGeneratingUnitGeometry = loadGeneratingUnitGeometry;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static LoadGeneratingUnit CreateNewBlank()
        {
            return new LoadGeneratingUnit(default(DbGeometry));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return HRUCharacteristics.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(LoadGeneratingUnit).Name, typeof(HRUCharacteristic).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.LoadGeneratingUnits.Remove(this);
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

            foreach(var x in HRUCharacteristics.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int LoadGeneratingUnitID { get; set; }
        public DbGeometry LoadGeneratingUnitGeometry { get; set; }
        public int? LSPCBasinID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return LoadGeneratingUnitID; } set { LoadGeneratingUnitID = value; } }

        public virtual ICollection<HRUCharacteristic> HRUCharacteristics { get; set; }
        public virtual LSPCBasin LSPCBasin { get; set; }
        public virtual RegionalSubbasin RegionalSubbasin { get; set; }
        public virtual Delineation Delineation { get; set; }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }

        public static class FieldLengths
        {

        }
    }
}