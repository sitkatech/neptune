//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModelBasinStaging]
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
    // Table [dbo].[ModelBasinStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ModelBasinStaging]")]
    public partial class ModelBasinStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ModelBasinStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ModelBasinStaging(int modelBasinStagingID, int modelBasinKey, DbGeometry modelBasinGeometry, string modelBasinState, string modelBasinRegion) : this()
        {
            this.ModelBasinStagingID = modelBasinStagingID;
            this.ModelBasinKey = modelBasinKey;
            this.ModelBasinGeometry = modelBasinGeometry;
            this.ModelBasinState = modelBasinState;
            this.ModelBasinRegion = modelBasinRegion;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ModelBasinStaging(int modelBasinKey, DbGeometry modelBasinGeometry, string modelBasinState, string modelBasinRegion) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ModelBasinStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ModelBasinKey = modelBasinKey;
            this.ModelBasinGeometry = modelBasinGeometry;
            this.ModelBasinState = modelBasinState;
            this.ModelBasinRegion = modelBasinRegion;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ModelBasinStaging CreateNewBlank()
        {
            return new ModelBasinStaging(default(int), default(DbGeometry), default(string), default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ModelBasinStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ModelBasinStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ModelBasinStagingID { get; set; }
        public int ModelBasinKey { get; set; }
        public DbGeometry ModelBasinGeometry { get; set; }
        public string ModelBasinState { get; set; }
        public string ModelBasinRegion { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ModelBasinStagingID; } set { ModelBasinStagingID = value; } }



        public static class FieldLengths
        {
            public const int ModelBasinState = 5;
            public const int ModelBasinRegion = 10;
        }
    }
}