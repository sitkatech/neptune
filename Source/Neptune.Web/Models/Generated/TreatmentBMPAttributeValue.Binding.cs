//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAttributeValue]
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
    [Table("[dbo].[TreatmentBMPAttributeValue]")]
    public partial class TreatmentBMPAttributeValue : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPAttributeValue()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAttributeValue(int treatmentBMPAttributeValueID, int treatmentBMPAttributeID, string attributeValue) : this()
        {
            this.TreatmentBMPAttributeValueID = treatmentBMPAttributeValueID;
            this.TreatmentBMPAttributeID = treatmentBMPAttributeID;
            this.AttributeValue = attributeValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPAttributeValue(int treatmentBMPAttributeID, string attributeValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAttributeValueID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPAttributeID = treatmentBMPAttributeID;
            this.AttributeValue = attributeValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPAttributeValue(TreatmentBMPAttribute treatmentBMPAttribute, string attributeValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPAttributeValueID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPAttributeID = treatmentBMPAttribute.TreatmentBMPAttributeID;
            this.TreatmentBMPAttribute = treatmentBMPAttribute;
            treatmentBMPAttribute.TreatmentBMPAttributeValues.Add(this);
            this.AttributeValue = attributeValue;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPAttributeValue CreateNewBlank(TreatmentBMPAttribute treatmentBMPAttribute)
        {
            return new TreatmentBMPAttributeValue(treatmentBMPAttribute, default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPAttributeValue).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributeValues.Remove(this);                
        }

        [Key]
        public int TreatmentBMPAttributeValueID { get; set; }
        public int TenantID { get; private set; }
        public int TreatmentBMPAttributeID { get; set; }
        public string AttributeValue { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPAttributeValueID; } set { TreatmentBMPAttributeValueID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMPAttribute TreatmentBMPAttribute { get; set; }

        public static class FieldLengths
        {
            public const int AttributeValue = 1000;
        }
    }
}