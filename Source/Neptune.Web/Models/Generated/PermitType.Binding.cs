//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PermitType]
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
    // Table [dbo].[PermitType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[PermitType]")]
    public partial class PermitType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected PermitType()
        {
            this.LandUseBlocks = new HashSet<LandUseBlock>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PermitType(int permitTypeID, string permitTypeName, string permitTypeDisplayName) : this()
        {
            this.PermitTypeID = permitTypeID;
            this.PermitTypeName = permitTypeName;
            this.PermitTypeDisplayName = permitTypeDisplayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public PermitType(string permitTypeName, string permitTypeDisplayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PermitTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PermitTypeName = permitTypeName;
            this.PermitTypeDisplayName = permitTypeDisplayName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static PermitType CreateNewBlank()
        {
            return new PermitType(default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return LandUseBlocks.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(PermitType).Name, typeof(LandUseBlock).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.PermitTypes.Remove(this);
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

            foreach(var x in LandUseBlocks.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int PermitTypeID { get; set; }
        public string PermitTypeName { get; set; }
        public string PermitTypeDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PermitTypeID; } set { PermitTypeID = value; } }

        public virtual ICollection<LandUseBlock> LandUseBlocks { get; set; }

        public static class FieldLengths
        {
            public const int PermitTypeName = 80;
            public const int PermitTypeDisplayName = 80;
        }
    }
}