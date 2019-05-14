//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlock]
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
    // Table [dbo].[LandUseBlock] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[LandUseBlock]")]
    public partial class LandUseBlock : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected LandUseBlock()
        {
            this.TrashGeneratingUnits = new HashSet<TrashGeneratingUnit>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public LandUseBlock(int landUseBlockID, int? priorityLandUseTypeID, string landUseDescription, DbGeometry landUseBlockGeometry, decimal trashGenerationRate, string landUseForTGR, decimal medianHouseholdIncomeResidential, decimal medianHouseholdIncomeRetail, int stormwaterJurisdictionID, int permitTypeID) : this()
        {
            this.LandUseBlockID = landUseBlockID;
            this.PriorityLandUseTypeID = priorityLandUseTypeID;
            this.LandUseDescription = landUseDescription;
            this.LandUseBlockGeometry = landUseBlockGeometry;
            this.TrashGenerationRate = trashGenerationRate;
            this.LandUseForTGR = landUseForTGR;
            this.MedianHouseholdIncomeResidential = medianHouseholdIncomeResidential;
            this.MedianHouseholdIncomeRetail = medianHouseholdIncomeRetail;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.PermitTypeID = permitTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public LandUseBlock(DbGeometry landUseBlockGeometry, decimal trashGenerationRate, decimal medianHouseholdIncomeResidential, decimal medianHouseholdIncomeRetail, int stormwaterJurisdictionID, int permitTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LandUseBlockID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.LandUseBlockGeometry = landUseBlockGeometry;
            this.TrashGenerationRate = trashGenerationRate;
            this.MedianHouseholdIncomeResidential = medianHouseholdIncomeResidential;
            this.MedianHouseholdIncomeRetail = medianHouseholdIncomeRetail;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.PermitTypeID = permitTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public LandUseBlock(DbGeometry landUseBlockGeometry, decimal trashGenerationRate, decimal medianHouseholdIncomeResidential, decimal medianHouseholdIncomeRetail, StormwaterJurisdiction stormwaterJurisdiction, PermitType permitType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LandUseBlockID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.LandUseBlockGeometry = landUseBlockGeometry;
            this.TrashGenerationRate = trashGenerationRate;
            this.MedianHouseholdIncomeResidential = medianHouseholdIncomeResidential;
            this.MedianHouseholdIncomeRetail = medianHouseholdIncomeRetail;
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.LandUseBlocks.Add(this);
            this.PermitTypeID = permitType.PermitTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static LandUseBlock CreateNewBlank(StormwaterJurisdiction stormwaterJurisdiction, PermitType permitType)
        {
            return new LandUseBlock(default(DbGeometry), default(decimal), default(decimal), default(decimal), stormwaterJurisdiction, permitType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TrashGeneratingUnits.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(LandUseBlock).Name, typeof(TrashGeneratingUnit).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.LandUseBlocks.Remove(this);
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

            foreach(var x in TrashGeneratingUnits.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int LandUseBlockID { get; set; }
        public int? PriorityLandUseTypeID { get; set; }
        public string LandUseDescription { get; set; }
        public DbGeometry LandUseBlockGeometry { get; set; }
        public decimal TrashGenerationRate { get; set; }
        public string LandUseForTGR { get; set; }
        public decimal MedianHouseholdIncomeResidential { get; set; }
        public decimal MedianHouseholdIncomeRetail { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int PermitTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return LandUseBlockID; } set { LandUseBlockID = value; } }

        public virtual ICollection<TrashGeneratingUnit> TrashGeneratingUnits { get; set; }
        public PriorityLandUseType PriorityLandUseType { get { return PriorityLandUseTypeID.HasValue ? PriorityLandUseType.AllLookupDictionary[PriorityLandUseTypeID.Value] : null; } }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public PermitType PermitType { get { return PermitType.AllLookupDictionary[PermitTypeID]; } }

        public static class FieldLengths
        {
            public const int LandUseDescription = 500;
            public const int LandUseForTGR = 80;
        }
    }
}