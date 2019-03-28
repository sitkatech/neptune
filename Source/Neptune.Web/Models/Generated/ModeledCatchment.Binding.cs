//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModeledCatchment]
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
    // Table [dbo].[ModeledCatchment] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ModeledCatchment]")]
    public partial class ModeledCatchment : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ModeledCatchment()
        {
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ModeledCatchment(int modeledCatchmentID, string modeledCatchmentName, int stormwaterJurisdictionID, string notes, DbGeometry modeledCatchmentGeometry) : this()
        {
            this.ModeledCatchmentID = modeledCatchmentID;
            this.ModeledCatchmentName = modeledCatchmentName;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.Notes = notes;
            this.ModeledCatchmentGeometry = modeledCatchmentGeometry;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ModeledCatchment(string modeledCatchmentName, int stormwaterJurisdictionID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ModeledCatchmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ModeledCatchmentName = modeledCatchmentName;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ModeledCatchment(string modeledCatchmentName, StormwaterJurisdiction stormwaterJurisdiction) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ModeledCatchmentID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ModeledCatchmentName = modeledCatchmentName;
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.ModeledCatchments.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ModeledCatchment CreateNewBlank(StormwaterJurisdiction stormwaterJurisdiction)
        {
            return new ModeledCatchment(default(string), stormwaterJurisdiction);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TreatmentBMPs.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ModeledCatchment).Name, typeof(TreatmentBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ModeledCatchments.Remove(this);
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

            foreach(var x in TreatmentBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ModeledCatchmentID { get; set; }
        public string ModeledCatchmentName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public string Notes { get; set; }
        public DbGeometry ModeledCatchmentGeometry { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ModeledCatchmentID; } set { ModeledCatchmentID = value; } }

        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }

        public static class FieldLengths
        {
            public const int ModeledCatchmentName = 100;
            public const int Notes = 1000;
        }
    }
}