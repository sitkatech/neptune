//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]
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
    // Table [dbo].[TreatmentBMPType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TreatmentBMPType]")]
    public partial class TreatmentBMPType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPType()
        {
            this.CustomAttributes = new HashSet<CustomAttribute>();
            this.MaintenanceRecords = new HashSet<MaintenanceRecord>();
            this.MaintenanceRecordObservations = new HashSet<MaintenanceRecordObservation>();
            this.QuickBMPs = new HashSet<QuickBMP>();
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
            this.TreatmentBMPAssessments = new HashSet<TreatmentBMPAssessment>();
            this.TreatmentBMPBenchmarkAndThresholds = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            this.TreatmentBMPObservations = new HashSet<TreatmentBMPObservation>();
            this.TreatmentBMPTypeAssessmentObservationTypes = new HashSet<TreatmentBMPTypeAssessmentObservationType>();
            this.TreatmentBMPTypeCustomAttributeTypes = new HashSet<TreatmentBMPTypeCustomAttributeType>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPType(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDescription, bool delineationShouldBeReconciled) : this()
        {
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPTypeName = treatmentBMPTypeName;
            this.TreatmentBMPTypeDescription = treatmentBMPTypeDescription;
            this.DelineationShouldBeReconciled = delineationShouldBeReconciled;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPType(string treatmentBMPTypeName, string treatmentBMPTypeDescription, bool delineationShouldBeReconciled) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPTypeName = treatmentBMPTypeName;
            this.TreatmentBMPTypeDescription = treatmentBMPTypeDescription;
            this.DelineationShouldBeReconciled = delineationShouldBeReconciled;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPType CreateNewBlank()
        {
            return new TreatmentBMPType(default(string), default(string), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return CustomAttributes.Any() || MaintenanceRecords.Any() || MaintenanceRecordObservations.Any() || QuickBMPs.Any() || TreatmentBMPs.Any() || TreatmentBMPAssessments.Any() || TreatmentBMPBenchmarkAndThresholds.Any() || TreatmentBMPObservations.Any() || TreatmentBMPTypeAssessmentObservationTypes.Any() || TreatmentBMPTypeCustomAttributeTypes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPType).Name, typeof(CustomAttribute).Name, typeof(MaintenanceRecord).Name, typeof(MaintenanceRecordObservation).Name, typeof(QuickBMP).Name, typeof(TreatmentBMP).Name, typeof(TreatmentBMPAssessment).Name, typeof(TreatmentBMPBenchmarkAndThreshold).Name, typeof(TreatmentBMPObservation).Name, typeof(TreatmentBMPTypeAssessmentObservationType).Name, typeof(TreatmentBMPTypeCustomAttributeType).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TreatmentBMPTypes.Remove(this);
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

            foreach(var x in MaintenanceRecords.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in MaintenanceRecordObservations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in QuickBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPAssessments.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPBenchmarkAndThresholds.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPObservations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPTypeAssessmentObservationTypes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPTypeCustomAttributeTypes.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public string TreatmentBMPTypeDescription { get; set; }
        public bool DelineationShouldBeReconciled { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPTypeID; } set { TreatmentBMPTypeID = value; } }

        public virtual ICollection<CustomAttribute> CustomAttributes { get; set; }
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        public virtual ICollection<QuickBMP> QuickBMPs { get; set; }
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        public virtual ICollection<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes { get; set; }
        public virtual ICollection<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; set; }

        public static class FieldLengths
        {
            public const int TreatmentBMPTypeName = 100;
            public const int TreatmentBMPTypeDescription = 1000;
        }
    }
}