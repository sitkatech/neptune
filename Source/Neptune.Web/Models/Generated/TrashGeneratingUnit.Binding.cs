//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit]
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
    // Table [dbo].[TrashGeneratingUnit] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TrashGeneratingUnit]")]
    public partial class TrashGeneratingUnit : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TrashGeneratingUnit()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TrashGeneratingUnit(int trashGeneratingUnitID, int stormwaterJurisdictionID, int? treatmentBMPID, int? onlandVisualTrashAssessmentAreaID, int? landUseBlockID, DbGeometry trashGeneratingUnitGeometry, DateTime? lastUpdateDate) : this()
        {
            this.TrashGeneratingUnitID = trashGeneratingUnitID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.TreatmentBMPID = treatmentBMPID;
            this.OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID;
            this.LandUseBlockID = landUseBlockID;
            this.TrashGeneratingUnitGeometry = trashGeneratingUnitGeometry;
            this.LastUpdateDate = lastUpdateDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TrashGeneratingUnit(int stormwaterJurisdictionID, DbGeometry trashGeneratingUnitGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TrashGeneratingUnitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.TrashGeneratingUnitGeometry = trashGeneratingUnitGeometry;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TrashGeneratingUnit(StormwaterJurisdiction stormwaterJurisdiction, DbGeometry trashGeneratingUnitGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TrashGeneratingUnitID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.TrashGeneratingUnits.Add(this);
            this.TrashGeneratingUnitGeometry = trashGeneratingUnitGeometry;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TrashGeneratingUnit CreateNewBlank(StormwaterJurisdiction stormwaterJurisdiction)
        {
            return new TrashGeneratingUnit(stormwaterJurisdiction, default(DbGeometry));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TrashGeneratingUnit).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TrashGeneratingUnits.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int TrashGeneratingUnitID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public int? LandUseBlockID { get; set; }
        public DbGeometry TrashGeneratingUnitGeometry { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TrashGeneratingUnitID; } set { TrashGeneratingUnitID = value; } }

        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public virtual LandUseBlock LandUseBlock { get; set; }

        public static class FieldLengths
        {

        }
    }
}