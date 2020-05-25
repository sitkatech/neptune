//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DroolToolWatershed]
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
    // Table [dbo].[DroolToolWatershed] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[DroolToolWatershed]")]
    public partial class DroolToolWatershed : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected DroolToolWatershed()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public DroolToolWatershed(int droolToolWatershedID, DbGeometry droolToolWatershedGeometry, string droolToolWatershedName, DbGeometry droolToolWatershedGeometry4326) : this()
        {
            this.DroolToolWatershedID = droolToolWatershedID;
            this.DroolToolWatershedGeometry = droolToolWatershedGeometry;
            this.DroolToolWatershedName = droolToolWatershedName;
            this.DroolToolWatershedGeometry4326 = droolToolWatershedGeometry4326;
        }



        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static DroolToolWatershed CreateNewBlank()
        {
            return new DroolToolWatershed();
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(DroolToolWatershed).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.DroolToolWatersheds.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int DroolToolWatershedID { get; set; }
        public DbGeometry DroolToolWatershedGeometry { get; set; }
        public string DroolToolWatershedName { get; set; }
        public DbGeometry DroolToolWatershedGeometry4326 { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return DroolToolWatershedID; } set { DroolToolWatershedID = value; } }



        public static class FieldLengths
        {
            public const int DroolToolWatershedName = 50;
        }
    }
}