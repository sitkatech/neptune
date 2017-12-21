//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterRole]
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
    [Table("[dbo].[StormwaterRole]")]
    public partial class StormwaterRole : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected StormwaterRole()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public StormwaterRole(int stormwaterRoleID, string stormwaterRoleName, string stormwaterRoleDisplayName, string stormwaterRoleDescription) : this()
        {
            this.StormwaterRoleID = stormwaterRoleID;
            this.StormwaterRoleName = stormwaterRoleName;
            this.StormwaterRoleDisplayName = stormwaterRoleDisplayName;
            this.StormwaterRoleDescription = stormwaterRoleDescription;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public StormwaterRole(string stormwaterRoleName, string stormwaterRoleDisplayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.StormwaterRoleID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.StormwaterRoleName = stormwaterRoleName;
            this.StormwaterRoleDisplayName = stormwaterRoleDisplayName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static StormwaterRole CreateNewBlank()
        {
            return new StormwaterRole(default(string), default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(StormwaterRole).Name};

        [Key]
        public int StormwaterRoleID { get; set; }
        public int TenantID { get; private set; }
        public string StormwaterRoleName { get; set; }
        public string StormwaterRoleDisplayName { get; set; }
        public string StormwaterRoleDescription { get; set; }
        public int PrimaryKey { get { return StormwaterRoleID; } set { StormwaterRoleID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }

        public static class FieldLengths
        {
            public const int StormwaterRoleName = 100;
            public const int StormwaterRoleDisplayName = 100;
            public const int StormwaterRoleDescription = 255;
        }
    }
}