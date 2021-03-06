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
    // Table [dbo].[TreatmentBMP] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TreatmentBMP]")]
    public partial class TreatmentBMP : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMP()
        {
            this.CustomAttributes = new HashSet<CustomAttribute>();
            this.Delineations = new HashSet<Delineation>();
            this.FieldVisits = new HashSet<FieldVisit>();
            this.FundingEvents = new HashSet<FundingEvent>();
            this.MaintenanceRecords = new HashSet<MaintenanceRecord>();
            this.RegionalSubbasinRevisionRequests = new HashSet<RegionalSubbasinRevisionRequest>();
            this.TreatmentBMPsWhereYouAreTheUpstreamBMP = new HashSet<TreatmentBMP>();
            this.TreatmentBMPAssessments = new HashSet<TreatmentBMPAssessment>();
            this.TreatmentBMPBenchmarkAndThresholds = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            this.TreatmentBMPDocuments = new HashSet<TreatmentBMPDocument>();
            this.TreatmentBMPImages = new HashSet<TreatmentBMPImage>();
            this.TreatmentBMPModelingAttributes = new HashSet<TreatmentBMPModelingAttribute>();
            this.TreatmentBMPModelingAttributesWhereYouAreTheUpstreamTreatmentBMP = new HashSet<TreatmentBMPModelingAttribute>();
            this.WaterQualityManagementPlanVerifyTreatmentBMPs = new HashSet<WaterQualityManagementPlanVerifyTreatmentBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMP(int treatmentBMPID, string treatmentBMPName, int treatmentBMPTypeID, DbGeometry locationPoint, int stormwaterJurisdictionID, string notes, string systemOfRecordID, int? yearBuilt, int ownerOrganizationID, int? waterQualityManagementPlanID, int? treatmentBMPLifespanTypeID, DateTime? treatmentBMPLifespanEndDate, int? requiredFieldVisitsPerYear, int? requiredPostStormFieldVisitsPerYear, bool inventoryIsVerified, DateTime? dateOfLastInventoryVerification, int? inventoryVerifiedByPersonID, DateTime? inventoryLastChangedDate, int trashCaptureStatusTypeID, int sizingBasisTypeID, int? trashCaptureEffectiveness, DbGeometry locationPoint4326, int? watershedID, int? lSPCBasinID, int? precipitationZoneID, int? upstreamBMPID, int? regionalSubbasinID) : this()
        {
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.LocationPoint = locationPoint;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
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
            this.InventoryVerifiedByPersonID = inventoryVerifiedByPersonID;
            this.InventoryLastChangedDate = inventoryLastChangedDate;
            this.TrashCaptureStatusTypeID = trashCaptureStatusTypeID;
            this.SizingBasisTypeID = sizingBasisTypeID;
            this.TrashCaptureEffectiveness = trashCaptureEffectiveness;
            this.LocationPoint4326 = locationPoint4326;
            this.WatershedID = watershedID;
            this.LSPCBasinID = lSPCBasinID;
            this.PrecipitationZoneID = precipitationZoneID;
            this.UpstreamBMPID = upstreamBMPID;
            this.RegionalSubbasinID = regionalSubbasinID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMP(string treatmentBMPName, int treatmentBMPTypeID, int stormwaterJurisdictionID, int ownerOrganizationID, bool inventoryIsVerified, int trashCaptureStatusTypeID, int sizingBasisTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OwnerOrganizationID = ownerOrganizationID;
            this.InventoryIsVerified = inventoryIsVerified;
            this.TrashCaptureStatusTypeID = trashCaptureStatusTypeID;
            this.SizingBasisTypeID = sizingBasisTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMP(string treatmentBMPName, TreatmentBMPType treatmentBMPType, StormwaterJurisdiction stormwaterJurisdiction, Organization ownerOrganization, bool inventoryIsVerified, TrashCaptureStatusType trashCaptureStatusType, SizingBasisType sizingBasisType) : this()
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
            this.TrashCaptureStatusTypeID = trashCaptureStatusType.TrashCaptureStatusTypeID;
            this.SizingBasisTypeID = sizingBasisType.SizingBasisTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMP CreateNewBlank(TreatmentBMPType treatmentBMPType, StormwaterJurisdiction stormwaterJurisdiction, Organization ownerOrganization, TrashCaptureStatusType trashCaptureStatusType, SizingBasisType sizingBasisType)
        {
            return new TreatmentBMP(default(string), treatmentBMPType, stormwaterJurisdiction, ownerOrganization, default(bool), trashCaptureStatusType, sizingBasisType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return CustomAttributes.Any() || (Delineation != null) || FieldVisits.Any() || FundingEvents.Any() || MaintenanceRecords.Any() || RegionalSubbasinRevisionRequests.Any() || TreatmentBMPsWhereYouAreTheUpstreamBMP.Any() || TreatmentBMPAssessments.Any() || TreatmentBMPBenchmarkAndThresholds.Any() || TreatmentBMPDocuments.Any() || TreatmentBMPImages.Any() || (TreatmentBMPModelingAttribute != null) || TreatmentBMPModelingAttributesWhereYouAreTheUpstreamTreatmentBMP.Any() || WaterQualityManagementPlanVerifyTreatmentBMPs.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(CustomAttributes.Any())
            {
                dependentObjects.Add(typeof(CustomAttribute).Name);
            }

            if((Delineation != null))
            {
                dependentObjects.Add(typeof(Delineation).Name);
            }

            if(FieldVisits.Any())
            {
                dependentObjects.Add(typeof(FieldVisit).Name);
            }

            if(FundingEvents.Any())
            {
                dependentObjects.Add(typeof(FundingEvent).Name);
            }

            if(MaintenanceRecords.Any())
            {
                dependentObjects.Add(typeof(MaintenanceRecord).Name);
            }

            if(RegionalSubbasinRevisionRequests.Any())
            {
                dependentObjects.Add(typeof(RegionalSubbasinRevisionRequest).Name);
            }

            if(TreatmentBMPsWhereYouAreTheUpstreamBMP.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMP).Name);
            }

            if(TreatmentBMPAssessments.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMPAssessment).Name);
            }

            if(TreatmentBMPBenchmarkAndThresholds.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMPBenchmarkAndThreshold).Name);
            }

            if(TreatmentBMPDocuments.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMPDocument).Name);
            }

            if(TreatmentBMPImages.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMPImage).Name);
            }

            if((TreatmentBMPModelingAttribute != null))
            {
                dependentObjects.Add(typeof(TreatmentBMPModelingAttribute).Name);
            }

            if(TreatmentBMPModelingAttributesWhereYouAreTheUpstreamTreatmentBMP.Any())
            {
                dependentObjects.Add(typeof(TreatmentBMPModelingAttribute).Name);
            }

            if(WaterQualityManagementPlanVerifyTreatmentBMPs.Any())
            {
                dependentObjects.Add(typeof(WaterQualityManagementPlanVerifyTreatmentBMP).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMP).Name, typeof(CustomAttribute).Name, typeof(Delineation).Name, typeof(FieldVisit).Name, typeof(FundingEvent).Name, typeof(MaintenanceRecord).Name, typeof(RegionalSubbasinRevisionRequest).Name, typeof(TreatmentBMPAssessment).Name, typeof(TreatmentBMPBenchmarkAndThreshold).Name, typeof(TreatmentBMPDocument).Name, typeof(TreatmentBMPImage).Name, typeof(TreatmentBMPModelingAttribute).Name, typeof(WaterQualityManagementPlanVerifyTreatmentBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TreatmentBMPs.Remove(this);
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

            foreach(var x in Delineations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in FieldVisits.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in FundingEvents.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in MaintenanceRecords.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in RegionalSubbasinRevisionRequests.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPsWhereYouAreTheUpstreamBMP.ToList())
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

            foreach(var x in TreatmentBMPDocuments.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPImages.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPModelingAttributes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPModelingAttributesWhereYouAreTheUpstreamTreatmentBMP.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanVerifyTreatmentBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public DbGeometry LocationPoint { get; set; }
        public int StormwaterJurisdictionID { get; set; }
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
        public int? InventoryVerifiedByPersonID { get; set; }
        public DateTime? InventoryLastChangedDate { get; set; }
        public int TrashCaptureStatusTypeID { get; set; }
        public int SizingBasisTypeID { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        public DbGeometry LocationPoint4326 { get; set; }
        public int? WatershedID { get; set; }
        public int? LSPCBasinID { get; set; }
        public int? PrecipitationZoneID { get; set; }
        public int? UpstreamBMPID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPID; } set { TreatmentBMPID = value; } }

        public virtual ICollection<CustomAttribute> CustomAttributes { get; set; }
        public virtual ICollection<Delineation> Delineations { get; set; }
        [NotMapped]
        public Delineation Delineation { get { return Delineations.SingleOrDefault(); } set { Delineations = new List<Delineation>{value};} }
        public virtual ICollection<FieldVisit> FieldVisits { get; set; }
        public virtual ICollection<FundingEvent> FundingEvents { get; set; }
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
        public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequests { get; set; }
        public virtual ICollection<TreatmentBMP> TreatmentBMPsWhereYouAreTheUpstreamBMP { get; set; }
        public virtual TreatmentBMP UpstreamBMP { get; set; }
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual ICollection<TreatmentBMPDocument> TreatmentBMPDocuments { get; set; }
        public virtual ICollection<TreatmentBMPImage> TreatmentBMPImages { get; set; }
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
        [NotMapped]
        public TreatmentBMPModelingAttribute TreatmentBMPModelingAttribute { get { return TreatmentBMPModelingAttributes.SingleOrDefault(); } set { TreatmentBMPModelingAttributes = new List<TreatmentBMPModelingAttribute>{value};} }
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributesWhereYouAreTheUpstreamTreatmentBMP { get; set; }
        public virtual ICollection<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; set; }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public virtual Organization OwnerOrganization { get; set; }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        public TreatmentBMPLifespanType TreatmentBMPLifespanType { get { return TreatmentBMPLifespanTypeID.HasValue ? TreatmentBMPLifespanType.AllLookupDictionary[TreatmentBMPLifespanTypeID.Value] : null; } }
        public virtual Person InventoryVerifiedByPerson { get; set; }
        public TrashCaptureStatusType TrashCaptureStatusType { get { return TrashCaptureStatusType.AllLookupDictionary[TrashCaptureStatusTypeID]; } }
        public SizingBasisType SizingBasisType { get { return SizingBasisType.AllLookupDictionary[SizingBasisTypeID]; } }
        public virtual Watershed Watershed { get; set; }
        public virtual LSPCBasin LSPCBasin { get; set; }
        public virtual PrecipitationZone PrecipitationZone { get; set; }

        public static class FieldLengths
        {
            public const int TreatmentBMPName = 200;
            public const int Notes = 1000;
            public const int SystemOfRecordID = 100;
        }
    }
}