//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeValue]
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
    [Table("[dbo].[CustomAttributeValue]")]
    public partial class CustomAttributeValue : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected CustomAttributeValue()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public CustomAttributeValue(int customAttributeValueID, int customAttributeID, string attributeValue) : this()
        {
            this.CustomAttributeValueID = customAttributeValueID;
            this.CustomAttributeID = customAttributeID;
            this.AttributeValue = attributeValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public CustomAttributeValue(int customAttributeID, string attributeValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.CustomAttributeValueID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.CustomAttributeID = customAttributeID;
            this.AttributeValue = attributeValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public CustomAttributeValue(CustomAttribute customAttribute, string attributeValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.CustomAttributeValueID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.CustomAttributeID = customAttribute.CustomAttributeID;
            this.CustomAttribute = customAttribute;
            customAttribute.CustomAttributeValues.Add(this);
            this.AttributeValue = attributeValue;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static CustomAttributeValue CreateNewBlank(CustomAttribute customAttribute)
        {
            return new CustomAttributeValue(customAttribute, default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(CustomAttributeValue).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            DeleteFull(HttpRequestStorage.DatabaseEntities);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            dbContext.AllCustomAttributeValues.Remove(this);
        }

        [Key]
        public int CustomAttributeValueID { get; set; }
        public int TenantID { get; private set; }
        public int CustomAttributeID { get; set; }
        public string AttributeValue { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return CustomAttributeValueID; } set { CustomAttributeValueID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual CustomAttribute CustomAttribute { get; set; }

        public static class FieldLengths
        {
            public const int AttributeValue = 1000;
        }
    }
}