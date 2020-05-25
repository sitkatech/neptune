//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LSPCBasinStaging]
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
    // Table [dbo].[LSPCBasinStaging] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[LSPCBasinStaging]")]
    public partial class LSPCBasinStaging : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected LSPCBasinStaging()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public LSPCBasinStaging(int lSPCBasinStagingID, int lSPCBasinKey, string lSPCBasinName, DbGeometry lSPCBasinGeometry) : this()
        {
            this.LSPCBasinStagingID = lSPCBasinStagingID;
            this.LSPCBasinKey = lSPCBasinKey;
            this.LSPCBasinName = lSPCBasinName;
            this.LSPCBasinGeometry = lSPCBasinGeometry;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public LSPCBasinStaging(int lSPCBasinKey, string lSPCBasinName, DbGeometry lSPCBasinGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LSPCBasinStagingID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.LSPCBasinKey = lSPCBasinKey;
            this.LSPCBasinName = lSPCBasinName;
            this.LSPCBasinGeometry = lSPCBasinGeometry;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static LSPCBasinStaging CreateNewBlank()
        {
            return new LSPCBasinStaging(default(int), default(string), default(DbGeometry));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(LSPCBasinStaging).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.LSPCBasinStagings.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int LSPCBasinStagingID { get; set; }
        public int LSPCBasinKey { get; set; }
        public string LSPCBasinName { get; set; }
        public DbGeometry LSPCBasinGeometry { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return LSPCBasinStagingID; } set { LSPCBasinStagingID = value; } }



        public static class FieldLengths
        {
            public const int LSPCBasinName = 100;
        }
    }
}