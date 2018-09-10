//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMP]
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
    [Table("[dbo].[TreatmentBMP]")]
    public partial class TreatmentBMP : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMP()
        {
            this.CustomAttributes = new HashSet<CustomAttribute>();
            this.FieldVisits = new HashSet<FieldVisit>();
            this.FundingEvents = new HashSet<FundingEvent>();
            this.MaintenanceRecords = new HashSet<MaintenanceRecord>();
            this.TreatmentBMPAssessments = new HashSet<TreatmentBMPAssessment>();
            this.TreatmentBMPBenchmarkAndThresholds = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            this.TreatmentBMPDocuments = new HashSet<TreatmentBMPDocument>();
            this.TreatmentBMPImages = new HashSet<TreatmentBMPImage>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMP(int treatmentBMPID, string treatmentBMPName, int treatmentBMPTypeID, DbGeometry locationPoint, int stormwaterJurisdictionID, int? modeledCatchmentID, string notes, string systemOfRecordID, int? yearBuilt, int ownerOrganizationID, int? waterQualityManagementPlanID, int? treatmentBMPLifespanTypeID, DateTime? treatmentBMPLifespanEndDate, int? requiredFieldVisitsPerYear, int? requiredPostStormFieldVisitsPerYear, bool inventoryIsVerified, DateTime? dateOfLastInventoryVerification) : this()
        {
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.LocationPoint = locationPoint;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.ModeledCatchmentID = modeledCatchmentID;
            this.Notes = notes;
            this.SystemOfRecordID = systemOfRecordID;
            this.YearBuilt = yearBuilt;
            this.OwnerOrganizationID = ownerOrganizationID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.TreatmentBMPLifespanTypeID = treatmentBMPLifespanTypeID;
            this.TreatmentBMPLifespanEndDate = treatmentBMPLifespanEndDate;
            this.RequiredFieldVisitsPerYear = requiredFieldVisitsPerYear;
            this.RequiredPostStormFieldVisitsPerYear = requiredPostStormFieldVisitsPerYear;
            this.InventoryIsVerified = inventoryIsVerified;
            this.DateOfLastInventoryVerification = dateOfLastInventoryVerification;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMP(string treatmentBMPName, int treatmentBMPTypeID, int stormwaterJurisdictionID, int ownerOrganizationID, bool inventoryIsVerified) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OwnerOrganizationID = ownerOrganizationID;
            this.InventoryIsVerified = inventoryIsVerified;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMP(string treatmentBMPName, TreatmentBMPType treatmentBMPType, StormwaterJurisdiction stormwaterJurisdiction, Organization ownerOrganization, bool inventoryIsVerified) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID;
            this.TreatmentBMPType = treatmentBMPType;
            treatmentBMPType.TreatmentBMPs.Add(this);
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.TreatmentBMPs.Add(this);
            this.OwnerOrganizationID = ownerOrganization.OrganizationID;
            this.OwnerOrganization = ownerOrganization;
            ownerOrganization.TreatmentBMPsWhereYouAreTheOwnerOrganization.Add(this);
            this.InventoryIsVerified = inventoryIsVerified;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMP CreateNewBlank(TreatmentBMPType treatmentBMPType, StormwaterJurisdiction stormwaterJurisdiction, Organization ownerOrganization)
        {
            return new TreatmentBMP(default(string), treatmentBMPType, stormwaterJurisdiction, ownerOrganization, default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return CustomAttributes.Any() || FieldVisits.Any() || FundingEvents.Any() || MaintenanceRecords.Any() || TreatmentBMPAssessments.Any() || TreatmentBMPBenchmarkAndThresholds.Any() || TreatmentBMPDocuments.Any() || TreatmentBMPImages.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMP).Name, typeof(CustomAttribute).Name, typeof(FieldVisit).Name, typeof(FundingEvent).Name, typeof(MaintenanceRecord).Name, typeof(TreatmentBMPAssessment).Name, typeof(TreatmentBMPBenchmarkAndThreshold).Name, typeof(TreatmentBMPDocument).Name, typeof(TreatmentBMPImage).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in CustomAttributes.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in FieldVisits.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in FundingEvents.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in MaintenanceRecords.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in TreatmentBMPAssessments.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in TreatmentBMPBenchmarkAndThresholds.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in TreatmentBMPDocuments.ToList())
            {
                x.DeleteFull();
            }

            foreach(var x in TreatmentBMPImages.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPs.Remove(this);                
        }

        [Key]
        public int TreatmentBMPID { get; set; }
        public int TenantID { get; private set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public DbGeometry LocationPoint { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? ModeledCatchmentID { get; set; }
        public string Notes { get; set; }
        public string SystemOfRecordID { get; set; }
        public int? YearBuilt { get; set; }
        public int OwnerOrganizationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? TreatmentBMPLifespanTypeID { get; set; }
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }
        public int? RequiredFieldVisitsPerYear { get; set; }
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }
        public bool InventoryIsVerified { get; set; }
        public DateTime? DateOfLastInventoryVerification { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPID; } set { TreatmentBMPID = value; } }

        public virtual ICollection<CustomAttribute> CustomAttributes { get; set; }
        public virtual ICollection<FieldVisit> FieldVisits { get; set; }
        public virtual ICollection<FundingEvent> FundingEvents { get; set; }
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual ICollection<TreatmentBMPDocument> TreatmentBMPDocuments { get; set; }
        public virtual ICollection<TreatmentBMPImage> TreatmentBMPImages { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public virtual ModeledCatchment ModeledCatchment { get; set; }
        public virtual Organization OwnerOrganization { get; set; }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        public TreatmentBMPLifespanType TreatmentBMPLifespanType { get { return TreatmentBMPLifespanTypeID.HasValue ? TreatmentBMPLifespanType.AllLookupDictionary[TreatmentBMPLifespanTypeID.Value] : null; } }

        public static class FieldLengths
        {
            public const int TreatmentBMPName = 200;
            public const int Notes = 1000;
            public const int SystemOfRecordID = 100;
        }
    }
}