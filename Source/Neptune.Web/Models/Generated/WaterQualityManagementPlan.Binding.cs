//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlan]
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
    [Table("[dbo].[WaterQualityManagementPlan]")]
    public partial class WaterQualityManagementPlan : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlan()
        {
            this.QuickBMPs = new HashSet<QuickBMP>();
            this.SourceControlBMPs = new HashSet<SourceControlBMP>();
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
            this.WaterQualityManagementPlanDocuments = new HashSet<WaterQualityManagementPlanDocument>();
            this.WaterQualityManagementPlanParcels = new HashSet<WaterQualityManagementPlanParcel>();
            this.WaterQualityManagementPlanVerifies = new HashSet<WaterQualityManagementPlanVerify>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlan(int waterQualityManagementPlanID, int stormwaterJurisdictionID, int? waterQualityManagementPlanLandUseID, int? waterQualityManagementPlanPriorityID, int? waterQualityManagementPlanStatusID, int? waterQualityManagementPlanDevelopmentTypeID, string waterQualityManagementPlanName, DateTime? approvalDate, string maintenanceContactName, string maintenanceContactOrganization, string maintenanceContactPhone, string maintenanceContactAddress1, string maintenanceContactAddress2, string maintenanceContactCity, string maintenanceContactState, string maintenanceContactZip, int? waterQualityManagementPlanPermitTermID, int? hydromodificationAppliesID, DateTime? dateOfContruction, int? hydrologicSubareaID, string recordNumber, decimal? recordedWQMPAreaInAcres, int trashCaptureStatusTypeID) : this()
        {
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.WaterQualityManagementPlanLandUseID = waterQualityManagementPlanLandUseID;
            this.WaterQualityManagementPlanPriorityID = waterQualityManagementPlanPriorityID;
            this.WaterQualityManagementPlanStatusID = waterQualityManagementPlanStatusID;
            this.WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlanDevelopmentTypeID;
            this.WaterQualityManagementPlanName = waterQualityManagementPlanName;
            this.ApprovalDate = approvalDate;
            this.MaintenanceContactName = maintenanceContactName;
            this.MaintenanceContactOrganization = maintenanceContactOrganization;
            this.MaintenanceContactPhone = maintenanceContactPhone;
            this.MaintenanceContactAddress1 = maintenanceContactAddress1;
            this.MaintenanceContactAddress2 = maintenanceContactAddress2;
            this.MaintenanceContactCity = maintenanceContactCity;
            this.MaintenanceContactState = maintenanceContactState;
            this.MaintenanceContactZip = maintenanceContactZip;
            this.WaterQualityManagementPlanPermitTermID = waterQualityManagementPlanPermitTermID;
            this.HydromodificationAppliesID = hydromodificationAppliesID;
            this.DateOfContruction = dateOfContruction;
            this.HydrologicSubareaID = hydrologicSubareaID;
            this.RecordNumber = recordNumber;
            this.RecordedWQMPAreaInAcres = recordedWQMPAreaInAcres;
            this.TrashCaptureStatusTypeID = trashCaptureStatusTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlan(int stormwaterJurisdictionID, string waterQualityManagementPlanName, int trashCaptureStatusTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.WaterQualityManagementPlanName = waterQualityManagementPlanName;
            this.TrashCaptureStatusTypeID = trashCaptureStatusTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public WaterQualityManagementPlan(StormwaterJurisdiction stormwaterJurisdiction, string waterQualityManagementPlanName, TrashCaptureStatusType trashCaptureStatusType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.StormwaterJurisdictionID = stormwaterJurisdiction.StormwaterJurisdictionID;
            this.StormwaterJurisdiction = stormwaterJurisdiction;
            stormwaterJurisdiction.WaterQualityManagementPlans.Add(this);
            this.WaterQualityManagementPlanName = waterQualityManagementPlanName;
            this.TrashCaptureStatusTypeID = trashCaptureStatusType.TrashCaptureStatusTypeID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlan CreateNewBlank(StormwaterJurisdiction stormwaterJurisdiction, TrashCaptureStatusType trashCaptureStatusType)
        {
            return new WaterQualityManagementPlan(stormwaterJurisdiction, default(string), trashCaptureStatusType);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return QuickBMPs.Any() || SourceControlBMPs.Any() || TreatmentBMPs.Any() || WaterQualityManagementPlanDocuments.Any() || WaterQualityManagementPlanParcels.Any() || WaterQualityManagementPlanVerifies.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlan).Name, typeof(QuickBMP).Name, typeof(SourceControlBMP).Name, typeof(TreatmentBMP).Name, typeof(WaterQualityManagementPlanDocument).Name, typeof(WaterQualityManagementPlanParcel).Name, typeof(WaterQualityManagementPlanVerify).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.WaterQualityManagementPlans.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in QuickBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in SourceControlBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TreatmentBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanDocuments.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanParcels.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanVerifies.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int WaterQualityManagementPlanID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? WaterQualityManagementPlanLandUseID { get; set; }
        public int? WaterQualityManagementPlanPriorityID { get; set; }
        public int? WaterQualityManagementPlanStatusID { get; set; }
        public int? WaterQualityManagementPlanDevelopmentTypeID { get; set; }
        public string WaterQualityManagementPlanName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string MaintenanceContactName { get; set; }
        public string MaintenanceContactOrganization { get; set; }
        public string MaintenanceContactPhone { get; set; }
        public string MaintenanceContactAddress1 { get; set; }
        public string MaintenanceContactAddress2 { get; set; }
        public string MaintenanceContactCity { get; set; }
        public string MaintenanceContactState { get; set; }
        public string MaintenanceContactZip { get; set; }
        public int? WaterQualityManagementPlanPermitTermID { get; set; }
        public int? HydromodificationAppliesID { get; set; }
        public DateTime? DateOfContruction { get; set; }
        public int? HydrologicSubareaID { get; set; }
        public string RecordNumber { get; set; }
        public decimal? RecordedWQMPAreaInAcres { get; set; }
        public int TrashCaptureStatusTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanID; } set { WaterQualityManagementPlanID = value; } }

        public virtual ICollection<QuickBMP> QuickBMPs { get; set; }
        public virtual ICollection<SourceControlBMP> SourceControlBMPs { get; set; }
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        public virtual ICollection<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; }
        public virtual ICollection<WaterQualityManagementPlanParcel> WaterQualityManagementPlanParcels { get; set; }
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public WaterQualityManagementPlanLandUse WaterQualityManagementPlanLandUse { get { return WaterQualityManagementPlanLandUseID.HasValue ? WaterQualityManagementPlanLandUse.AllLookupDictionary[WaterQualityManagementPlanLandUseID.Value] : null; } }
        public WaterQualityManagementPlanPriority WaterQualityManagementPlanPriority { get { return WaterQualityManagementPlanPriorityID.HasValue ? WaterQualityManagementPlanPriority.AllLookupDictionary[WaterQualityManagementPlanPriorityID.Value] : null; } }
        public WaterQualityManagementPlanStatus WaterQualityManagementPlanStatus { get { return WaterQualityManagementPlanStatusID.HasValue ? WaterQualityManagementPlanStatus.AllLookupDictionary[WaterQualityManagementPlanStatusID.Value] : null; } }
        public WaterQualityManagementPlanDevelopmentType WaterQualityManagementPlanDevelopmentType { get { return WaterQualityManagementPlanDevelopmentTypeID.HasValue ? WaterQualityManagementPlanDevelopmentType.AllLookupDictionary[WaterQualityManagementPlanDevelopmentTypeID.Value] : null; } }
        public WaterQualityManagementPlanPermitTerm WaterQualityManagementPlanPermitTerm { get { return WaterQualityManagementPlanPermitTermID.HasValue ? WaterQualityManagementPlanPermitTerm.AllLookupDictionary[WaterQualityManagementPlanPermitTermID.Value] : null; } }
        public HydromodificationApplies HydromodificationApplies { get { return HydromodificationAppliesID.HasValue ? HydromodificationApplies.AllLookupDictionary[HydromodificationAppliesID.Value] : null; } }
        public virtual HydrologicSubarea HydrologicSubarea { get; set; }
        public TrashCaptureStatusType TrashCaptureStatusType { get { return TrashCaptureStatusType.AllLookupDictionary[TrashCaptureStatusTypeID]; } }

        public static class FieldLengths
        {
            public const int WaterQualityManagementPlanName = 100;
            public const int MaintenanceContactName = 100;
            public const int MaintenanceContactOrganization = 100;
            public const int MaintenanceContactPhone = 100;
            public const int MaintenanceContactAddress1 = 100;
            public const int MaintenanceContactAddress2 = 100;
            public const int MaintenanceContactCity = 100;
            public const int MaintenanceContactState = 100;
            public const int MaintenanceContactZip = 100;
            public const int RecordNumber = 500;
        }
    }
}