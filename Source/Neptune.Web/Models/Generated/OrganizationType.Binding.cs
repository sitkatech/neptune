//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OrganizationType]
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
    [Table("[dbo].[OrganizationType]")]
    public partial class OrganizationType : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected OrganizationType()
        {
            this.Organizations = new HashSet<Organization>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public OrganizationType(int organizationTypeID, string organizationTypeName, string organizationTypeAbbreviation, string legendColor, bool isDefaultOrganizationType) : this()
        {
            this.OrganizationTypeID = organizationTypeID;
            this.OrganizationTypeName = organizationTypeName;
            this.OrganizationTypeAbbreviation = organizationTypeAbbreviation;
            this.LegendColor = legendColor;
            this.IsDefaultOrganizationType = isDefaultOrganizationType;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public OrganizationType(string organizationTypeName, string organizationTypeAbbreviation, string legendColor, bool isDefaultOrganizationType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OrganizationTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OrganizationTypeName = organizationTypeName;
            this.OrganizationTypeAbbreviation = organizationTypeAbbreviation;
            this.LegendColor = legendColor;
            this.IsDefaultOrganizationType = isDefaultOrganizationType;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static OrganizationType CreateNewBlank()
        {
            return new OrganizationType(default(string), default(string), default(string), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return Organizations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(OrganizationType).Name, typeof(Organization).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(HttpRequestStorage.DatabaseEntities);
            dbContext.AllOrganizationTypes.Remove(this);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in Organizations.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int OrganizationTypeID { get; set; }
        public int TenantID { get; private set; }
        public string OrganizationTypeName { get; set; }
        public string OrganizationTypeAbbreviation { get; set; }
        public string LegendColor { get; set; }
        public bool IsDefaultOrganizationType { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OrganizationTypeID; } set { OrganizationTypeID = value; } }

        public virtual ICollection<Organization> Organizations { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }

        public static class FieldLengths
        {
            public const int OrganizationTypeName = 200;
            public const int OrganizationTypeAbbreviation = 100;
            public const int LegendColor = 10;
        }
    }
}