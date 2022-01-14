//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinition]
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
    // Table [dbo].[FieldDefinition] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[FieldDefinition]")]
    public partial class FieldDefinition : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FieldDefinition()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FieldDefinition(int fieldDefinitionID, int fieldDefinitionTypeID, string fieldDefinitionValue) : this()
        {
            this.FieldDefinitionID = fieldDefinitionID;
            this.FieldDefinitionTypeID = fieldDefinitionTypeID;
            this.FieldDefinitionValue = fieldDefinitionValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FieldDefinition(int fieldDefinitionTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FieldDefinitionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FieldDefinitionTypeID = fieldDefinitionTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FieldDefinition(FieldDefinitionType fieldDefinitionType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FieldDefinitionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FieldDefinitionTypeID = fieldDefinitionType.FieldDefinitionTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FieldDefinition CreateNewBlank(FieldDefinitionType fieldDefinitionType)
        {
            return new FieldDefinition(fieldDefinitionType);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FieldDefinition).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.FieldDefinitions.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int FieldDefinitionID { get; set; }
        public int FieldDefinitionTypeID { get; set; }
        public string FieldDefinitionValue { get; set; }
        [NotMapped]
        public HtmlString FieldDefinitionValueHtmlString
        { 
            get { return FieldDefinitionValue == null ? null : new HtmlString(FieldDefinitionValue); }
            set { FieldDefinitionValue = value?.ToString(); }
        }
        [NotMapped]
        public int PrimaryKey { get { return FieldDefinitionID; } set { FieldDefinitionID = value; } }

        public FieldDefinitionType FieldDefinitionType { get { return FieldDefinitionType.AllLookupDictionary[FieldDefinitionTypeID]; } }

        public static class FieldLengths
        {

        }
    }
}