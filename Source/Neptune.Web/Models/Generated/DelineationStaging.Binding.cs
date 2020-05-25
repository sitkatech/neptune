//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationStaging]
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
    // Table [dbo].[DelineationStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[DelineationStaging]")]
    public partial class DelineationStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected DelineationStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public DelineationStaging(int delineationStagingID, DbGeometry delineationStagingGeometry, int uploadedByPersonID, string treatmentBMPName, int stormwaterJurisdictionID) : this()
        {
            this.DelineationStagingID = delineationStagingID;
            this.DelineationStagingGeometry = delineationStagingGeometry;
            this.UploadedByPersonID = uploadedByPersonID;
            this.TreatmentBMPName = treatmentBMPName;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public DelineationStaging(DbGeometry delineationStagingGeometry, int uploadedByPersonID, string treatmentBMPName, int stormwaterJurisdictionID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.DelineationStagingGeometry = delineationStagingGeometry;
            this.UploadedByPersonID = uploadedByPersonID;
            this.TreatmentBMPName = treatmentBMPName;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public DelineationStaging(DbGeometry delineationStagingGeometry, Person uploadedByPerson, string treatmentBMPName, StormwaterJurisdiction stormwaterJurisdiction) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.DelineationStagingGeometry = delineationStagingGeometry;
            this.UploadedByPersonID = uploadedByPerson.PersonID;
            this.UploadedByPerson = uploadedByPerson;
            uploadedByPerson.DelineationStagingsWhereYouAreTheUploadedByPerson.Add(this);
            this.TreatmentBMPName = treatmentBMPName;
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.DelineationStagings.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static DelineationStaging CreateNewBlank(Person uploadedByPerson, StormwaterJurisdiction stormwaterJurisdiction)
        {
            return new DelineationStaging(default(DbGeometry), uploadedByPerson, default(string), stormwaterJurisdiction);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(DelineationStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.DelineationStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int DelineationStagingID { get; set; }
        public DbGeometry DelineationStagingGeometry { get; set; }
        public int UploadedByPersonID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return DelineationStagingID; } set { DelineationStagingID = value; } }

        public virtual Person UploadedByPerson { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }

        public static class FieldLengths
        {
            public const int TreatmentBMPName = 200;
        }
    }
}