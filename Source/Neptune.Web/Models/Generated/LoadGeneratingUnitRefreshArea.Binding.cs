//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnitRefreshArea]
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
    // Table [dbo].[LoadGeneratingUnitRefreshArea] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[LoadGeneratingUnitRefreshArea]")]
    public partial class LoadGeneratingUnitRefreshArea : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected LoadGeneratingUnitRefreshArea()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public LoadGeneratingUnitRefreshArea(int loadGeneratingUnitRefreshAreaID, DbGeometry loadGeneratingUnitRefreshAreaGeometry, DateTime? processDate) : this()
        {
            this.LoadGeneratingUnitRefreshAreaID = loadGeneratingUnitRefreshAreaID;
            this.LoadGeneratingUnitRefreshAreaGeometry = loadGeneratingUnitRefreshAreaGeometry;
            this.ProcessDate = processDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public LoadGeneratingUnitRefreshArea(DbGeometry loadGeneratingUnitRefreshAreaGeometry) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.LoadGeneratingUnitRefreshAreaID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.LoadGeneratingUnitRefreshAreaGeometry = loadGeneratingUnitRefreshAreaGeometry;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static LoadGeneratingUnitRefreshArea CreateNewBlank()
        {
            return new LoadGeneratingUnitRefreshArea(default(DbGeometry));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(LoadGeneratingUnitRefreshArea).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.LoadGeneratingUnitRefreshAreas.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int LoadGeneratingUnitRefreshAreaID { get; set; }
        public DbGeometry LoadGeneratingUnitRefreshAreaGeometry { get; set; }
        public DateTime? ProcessDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return LoadGeneratingUnitRefreshAreaID; } set { LoadGeneratingUnitRefreshAreaID = value; } }



        public static class FieldLengths
        {

        }
    }
}