//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LSPCBasin]
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
    // Table [dbo].[LSPCBasin] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[LSPCBasin]")]
    public partial class LSPCBasin : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected LSPCBasin()
        {
            this.LoadGeneratingUnits = new HashSet<LoadGeneratingUnit>();
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public LSPCBasin(int lSPCBasinID, int lSPCBasinKey, string lSPCBasinName, DbGeometry lSPCBasinGeometry, DateTime lastUpdate) : this()
        {
            this.LSPCBasinID = lSPCBasinID;
            this.LSPCBasinKey = lSPCBasinKey;
            this.LSPCBasinName = lSPCBasinName;
            this.LSPCBasinGeometry = lSPCBasinGeometry;
            this.LastUpdate = lastUpdate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public LSPCBasin(int lSPCBasinKey, string lSPCBasinName, DbGeometry lSPCBasinGeometry, DateTime lastUpdate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LSPCBasinID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.LSPCBasinKey = lSPCBasinKey;
            this.LSPCBasinName = lSPCBasinName;
            this.LSPCBasinGeometry = lSPCBasinGeometry;
            this.LastUpdate = lastUpdate;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static LSPCBasin CreateNewBlank()
        {
            return new LSPCBasin(default(int), default(string), default(DbGeometry), default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return LoadGeneratingUnits.Any() || TreatmentBMPs.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(LoadGeneratingUnits.Any())
            {
                dependentObjects.Add(typeof(LoadGeneratingUnit).Name);
            }

            if(TreatmentBMPs.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMP).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(LSPCBasin).Name, typeof(LoadGeneratingUnit).Name, typeof(TreatmentBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.LSPCBasins.Remove(this);
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

            foreach(var x in LoadGeneratingUnits.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int LSPCBasinID { get; set; }
        public int LSPCBasinKey { get; set; }
        public string LSPCBasinName { get; set; }
        public DbGeometry LSPCBasinGeometry { get; set; }
        public DateTime LastUpdate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return LSPCBasinID; } set { LSPCBasinID = value; } }

        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }

        public static class FieldLengths
        {
            public const int LSPCBasinName = 100;
        }
    }
}