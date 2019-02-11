//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeCustomAttributeType]
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
    // Table [dbo].[TreatmentBMPTypeCustomAttributeType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TreatmentBMPTypeCustomAttributeType]")]
    public partial class TreatmentBMPTypeCustomAttributeType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPTypeCustomAttributeType()
        {
            this.CustomAttributes = new HashSet<CustomAttribute>();
            this.MaintenanceRecordObservations = new HashSet<MaintenanceRecordObservation>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPTypeCustomAttributeType(int treatmentBMPTypeCustomAttributeTypeID, int treatmentBMPTypeID, int customAttributeTypeID, int? sortOrder) : this()
        {
            this.TreatmentBMPTypeCustomAttributeTypeID = treatmentBMPTypeCustomAttributeTypeID;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.CustomAttributeTypeID = customAttributeTypeID;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPTypeCustomAttributeType(int treatmentBMPTypeID, int customAttributeTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeCustomAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.CustomAttributeTypeID = customAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPTypeCustomAttributeType(TreatmentBMPType treatmentBMPType, CustomAttributeType customAttributeType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeCustomAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Add(this);
            this.CustomAttributeTypeID = customAttributeType.CustomAttributeTypeID;
            this.CustomAttributeType = customAttributeType;
            customAttributeType.TreatmentBMPTypeCustomAttributeTypes.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPTypeCustomAttributeType CreateNewBlank(TreatmentBMPType treatmentBMPType, CustomAttributeType customAttributeType)
        {
            return new TreatmentBMPTypeCustomAttributeType(treatmentBMPType, customAttributeType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return CustomAttributes.Any() || MaintenanceRecordObservations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPTypeCustomAttributeType).Name, typeof(CustomAttribute).Name, typeof(MaintenanceRecordObservation).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TreatmentBMPTypeCustomAttributeTypes.Remove(this);
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

            foreach(var x in CustomAttributes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in MaintenanceRecordObservations.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int TreatmentBMPTypeCustomAttributeTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int CustomAttributeTypeID { get; set; }
        public int? SortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPTypeCustomAttributeTypeID; } set { TreatmentBMPTypeCustomAttributeTypeID = value; } }

        public virtual ICollection<CustomAttribute> CustomAttributes { get; set; }
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual CustomAttributeType CustomAttributeType { get; set; }

        public static class FieldLengths
        {

        }
    }
}