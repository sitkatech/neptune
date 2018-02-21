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
            this.MaintenanceActivities = new HashSet<MaintenanceActivity>();
            this.TreatmentBMPAssessments = new HashSet<TreatmentBMPAssessment>();
            this.TreatmentBMPAttributes = new HashSet<TreatmentBMPAttribute>();
            this.TreatmentBMPBenchmarkAndThresholds = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            this.TreatmentBMPDocuments = new HashSet<TreatmentBMPDocument>();
            this.TreatmentBMPImages = new HashSet<TreatmentBMPImage>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMP(int treatmentBMPID, string treatmentBMPName, int treatmentBMPTypeID, DbGeometry locationPoint, int stormwaterJurisdictionID, int? modeledCatchmentID, string notes, string systemOfRecordID, int? yearBuilt, int ownerOrganizationID) : this()
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
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMP(string treatmentBMPName, int treatmentBMPTypeID, int stormwaterJurisdictionID, int ownerOrganizationID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OwnerOrganizationID = ownerOrganizationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMP(string treatmentBMPName, TreatmentBMPType treatmentBMPType, StormwaterJurisdiction stormwaterJurisdiction, Organization ownerOrganization) : this()
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
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMP CreateNewBlank(TreatmentBMPType treatmentBMPType, StormwaterJurisdiction stormwaterJurisdiction, Organization ownerOrganization)
        {
            return new TreatmentBMP(default(string), treatmentBMPType, stormwaterJurisdiction, ownerOrganization);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return MaintenanceActivities.Any() || TreatmentBMPAssessments.Any() || TreatmentBMPAttributes.Any() || TreatmentBMPBenchmarkAndThresholds.Any() || TreatmentBMPDocuments.Any() || TreatmentBMPImages.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMP).Name, typeof(MaintenanceActivity).Name, typeof(TreatmentBMPAssessment).Name, typeof(TreatmentBMPAttribute).Name, typeof(TreatmentBMPBenchmarkAndThreshold).Name, typeof(TreatmentBMPDocument).Name, typeof(TreatmentBMPImage).Name};

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
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPID; } set { TreatmentBMPID = value; } }

        public virtual ICollection<MaintenanceActivity> MaintenanceActivities { get; set; }
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
        public virtual ICollection<TreatmentBMPAttribute> TreatmentBMPAttributes { get; set; }
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual ICollection<TreatmentBMPDocument> TreatmentBMPDocuments { get; set; }
        public virtual ICollection<TreatmentBMPImage> TreatmentBMPImages { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        public virtual ModeledCatchment ModeledCatchment { get; set; }
        public virtual Organization OwnerOrganization { get; set; }

        public static class FieldLengths
        {
            public const int TreatmentBMPName = 30;
            public const int Notes = 200;
            public const int SystemOfRecordID = 100;
        }
    }
}