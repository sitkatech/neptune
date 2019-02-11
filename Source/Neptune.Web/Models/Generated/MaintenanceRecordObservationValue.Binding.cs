//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservationValue]
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
    // Table [dbo].[MaintenanceRecordObservationValue] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[MaintenanceRecordObservationValue]")]
    public partial class MaintenanceRecordObservationValue : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected MaintenanceRecordObservationValue()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public MaintenanceRecordObservationValue(int maintenanceRecordObservationValueID, int maintenanceRecordObservationID, string observationValue) : this()
        {
            this.MaintenanceRecordObservationValueID = maintenanceRecordObservationValueID;
            this.MaintenanceRecordObservationID = maintenanceRecordObservationID;
            this.ObservationValue = observationValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public MaintenanceRecordObservationValue(int maintenanceRecordObservationID, string observationValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.MaintenanceRecordObservationValueID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.MaintenanceRecordObservationID = maintenanceRecordObservationID;
            this.ObservationValue = observationValue;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public MaintenanceRecordObservationValue(MaintenanceRecordObservation maintenanceRecordObservation, string observationValue) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.MaintenanceRecordObservationValueID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.MaintenanceRecordObservationID = maintenanceRecordObservation.MaintenanceRecordObservationID;
            this.MaintenanceRecordObservation = maintenanceRecordObservation;
            maintenanceRecordObservation.MaintenanceRecordObservationValues.Add(this);
            this.ObservationValue = observationValue;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static MaintenanceRecordObservationValue CreateNewBlank(MaintenanceRecordObservation maintenanceRecordObservation)
        {
            return new MaintenanceRecordObservationValue(maintenanceRecordObservation, default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(MaintenanceRecordObservationValue).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.MaintenanceRecordObservationValues.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int MaintenanceRecordObservationValueID { get; set; }
        public int MaintenanceRecordObservationID { get; set; }
        public string ObservationValue { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return MaintenanceRecordObservationValueID; } set { MaintenanceRecordObservationValueID = value; } }

        public virtual MaintenanceRecordObservation MaintenanceRecordObservation { get; set; }

        public static class FieldLengths
        {
            public const int ObservationValue = 1000;
        }
    }
}