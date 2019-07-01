//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlockStaging]
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
    // Table [dbo].[LandUseBlockStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[LandUseBlockStaging]")]
    public partial class LandUseBlockStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected LandUseBlockStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public LandUseBlockStaging(int landUseBlockStagingID, string priorityLandUseType, string landUseDescription, DbGeometry landUseBlockStagingGeometry, decimal? trashGenerationRate, string landUseForTGR, decimal? medianHouseholdIncome, string stormwaterJurisdiction, string permitType, int uploadedByPersonID) : this()
        {
            this.LandUseBlockStagingID = landUseBlockStagingID;
            this.PriorityLandUseType = priorityLandUseType;
            this.LandUseDescription = landUseDescription;
            this.LandUseBlockStagingGeometry = landUseBlockStagingGeometry;
            this.TrashGenerationRate = trashGenerationRate;
            this.LandUseForTGR = landUseForTGR;
            this.MedianHouseholdIncome = medianHouseholdIncome;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            this.PermitType = permitType;
            this.UploadedByPersonID = uploadedByPersonID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public LandUseBlockStaging(DbGeometry landUseBlockStagingGeometry, string stormwaterJurisdiction, int uploadedByPersonID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LandUseBlockStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.LandUseBlockStagingGeometry = landUseBlockStagingGeometry;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            this.UploadedByPersonID = uploadedByPersonID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public LandUseBlockStaging(DbGeometry landUseBlockStagingGeometry, string stormwaterJurisdiction, Person uploadedByPerson) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LandUseBlockStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.LandUseBlockStagingGeometry = landUseBlockStagingGeometry;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            this.UploadedByPersonID = uploadedByPerson.PersonID;
            this.UploadedByPerson = uploadedByPerson;
            uploadedByPerson.LandUseBlockStagingsWhereYouAreTheUploadedByPerson.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static LandUseBlockStaging CreateNewBlank(Person uploadedByPerson)
        {
            return new LandUseBlockStaging(default(DbGeometry), default(string), uploadedByPerson);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(LandUseBlockStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.LandUseBlockStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int LandUseBlockStagingID { get; set; }
        public string PriorityLandUseType { get; set; }
        public string LandUseDescription { get; set; }
        public DbGeometry LandUseBlockStagingGeometry { get; set; }
        public decimal? TrashGenerationRate { get; set; }
        public string LandUseForTGR { get; set; }
        public decimal? MedianHouseholdIncome { get; set; }
        public string StormwaterJurisdiction { get; set; }
        public string PermitType { get; set; }
        public int UploadedByPersonID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return LandUseBlockStagingID; } set { LandUseBlockStagingID = value; } }

        public virtual Person UploadedByPerson { get; set; }

        public static class FieldLengths
        {
            public const int PriorityLandUseType = 255;
            public const int LandUseDescription = 500;
            public const int LandUseForTGR = 80;
            public const int StormwaterJurisdiction = 255;
            public const int PermitType = 255;
        }
    }
}