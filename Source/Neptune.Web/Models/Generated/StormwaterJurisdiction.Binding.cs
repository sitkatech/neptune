//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdiction]
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
    [Table("[dbo].[StormwaterJurisdiction]")]
    public partial class StormwaterJurisdiction : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected StormwaterJurisdiction()
        {
            this.ModeledCatchments = new HashSet<ModeledCatchment>();
            this.StormwaterJurisdictionPeople = new HashSet<StormwaterJurisdictionPerson>();
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public StormwaterJurisdiction(int stormwaterJurisdictionID, int organizationID, DbGeometry stormwaterJurisdictionGeometry, DbGeometry roadNetworkGeometry, DbGeometry roadAreaOfInterestGeometry, int? stateProvinceID, bool isTransportationJurisdiction) : this()
        {
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OrganizationID = organizationID;
            this.StormwaterJurisdictionGeometry = stormwaterJurisdictionGeometry;
            this.RoadNetworkGeometry = roadNetworkGeometry;
            this.RoadAreaOfInterestGeometry = roadAreaOfInterestGeometry;
            this.StateProvinceID = stateProvinceID;
            this.IsTransportationJurisdiction = isTransportationJurisdiction;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public StormwaterJurisdiction(int organizationID, bool isTransportationJurisdiction) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.StormwaterJurisdictionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OrganizationID = organizationID;
            this.IsTransportationJurisdiction = isTransportationJurisdiction;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public StormwaterJurisdiction(Organization organization, bool isTransportationJurisdiction) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.StormwaterJurisdictionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.OrganizationID = organization.OrganizationID;
            this.Organization = organization;
            this.IsTransportationJurisdiction = isTransportationJurisdiction;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static StormwaterJurisdiction CreateNewBlank(Organization organization)
        {
            return new StormwaterJurisdiction(organization, default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ModeledCatchments.Any() || StormwaterJurisdictionPeople.Any() || TreatmentBMPs.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(StormwaterJurisdiction).Name, typeof(ModeledCatchment).Name, typeof(StormwaterJurisdictionPerson).Name, typeof(TreatmentBMP).Name};

        [Key]
        public int StormwaterJurisdictionID { get; set; }
        public int TenantID { get; private set; }
        public int OrganizationID { get; set; }
        public DbGeometry StormwaterJurisdictionGeometry { get; set; }
        public DbGeometry RoadNetworkGeometry { get; set; }
        public DbGeometry RoadAreaOfInterestGeometry { get; set; }
        public int? StateProvinceID { get; set; }
        public bool IsTransportationJurisdiction { get; set; }
        public int PrimaryKey { get { return StormwaterJurisdictionID; } set { StormwaterJurisdictionID = value; } }

        public virtual ICollection<ModeledCatchment> ModeledCatchments { get; set; }
        public virtual ICollection<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; }
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual Organization Organization { get; set; }
        public virtual StateProvince StateProvince { get; set; }

        public static class FieldLengths
        {

        }
    }
}