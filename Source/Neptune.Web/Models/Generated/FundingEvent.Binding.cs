//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEvent]
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
    [Table("[dbo].[FundingEvent]")]
    public partial class FundingEvent : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FundingEvent()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundingEvent(int fundingEventID, int fundingSourceID, int treatmentBMPID, decimal? amount) : this()
        {
            this.FundingEventID = fundingEventID;
            this.FundingSourceID = fundingSourceID;
            this.TreatmentBMPID = treatmentBMPID;
            this.Amount = amount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundingEvent(int fundingSourceID, int treatmentBMPID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FundingEventID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FundingSourceID = fundingSourceID;
            this.TreatmentBMPID = treatmentBMPID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FundingEvent(FundingSource fundingSource, TreatmentBMP treatmentBMP) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FundingEventID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FundingSourceID = fundingSource.FundingSourceID;
            this.FundingSource = fundingSource;
            fundingSource.FundingEvents.Add(this);
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.FundingEvents.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FundingEvent CreateNewBlank(FundingSource fundingSource, TreatmentBMP treatmentBMP)
        {
            return new FundingEvent(fundingSource, treatmentBMP);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FundingEvent).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllFundingEvents.Remove(this);                
        }

        [Key]
        public int FundingEventID { get; set; }
        public int TenantID { get; private set; }
        public int FundingSourceID { get; set; }
        public int TreatmentBMPID { get; set; }
        public decimal? Amount { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return FundingEventID; } set { FundingEventID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual FundingSource FundingSource { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }

        public static class FieldLengths
        {

        }
    }
}