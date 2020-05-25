//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Watershed]
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
    // Table [dbo].[Watershed] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Watershed]")]
    public partial class Watershed : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Watershed()
        {
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Watershed(int watershedID, DbGeometry watershedGeometry, string watershedName, DbGeometry watershedGeometry4326) : this()
        {
            this.WatershedID = watershedID;
            this.WatershedGeometry = watershedGeometry;
            this.WatershedName = watershedName;
            this.WatershedGeometry4326 = watershedGeometry4326;
        }



        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Watershed CreateNewBlank()
        {
            return new Watershed();
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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(TreatmentBMPs.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMP).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Watershed).Name, typeof(TreatmentBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.Watersheds.Remove(this);
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
        public int WatershedID { get; set; }
        public DbGeometry WatershedGeometry { get; set; }
        public string WatershedName { get; set; }
        public DbGeometry WatershedGeometry4326 { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WatershedID; } set { WatershedID = value; } }

        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }

        public static class FieldLengths
        {
            public const int WatershedName = 50;
        }
    }
}