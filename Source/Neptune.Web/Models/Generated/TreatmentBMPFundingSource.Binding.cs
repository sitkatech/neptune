//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPFundingSource]
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
    [Table("[dbo].[TreatmentBMPFundingSource]")]
    public partial class TreatmentBMPFundingSource : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPFundingSource()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPFundingSource(int treatmentBMPFundingSourceID, int fundingSourceID, int treatmentBMPID, decimal amount) : this()
        {
            this.TreatmentBMPFundingSourceID = treatmentBMPFundingSourceID;
            this.FundingSourceID = fundingSourceID;
            this.TreatmentBMPID = treatmentBMPID;
            this.Amount = amount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPFundingSource(int fundingSourceID, int treatmentBMPID, decimal amount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPFundingSourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FundingSourceID = fundingSourceID;
            this.TreatmentBMPID = treatmentBMPID;
            this.Amount = amount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPFundingSource(FundingSource fundingSource, TreatmentBMP treatmentBMP, decimal amount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPFundingSourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FundingSourceID = fundingSource.FundingSourceID;
            this.FundingSource = fundingSource;
            fundingSource.TreatmentBMPFundingSources.Add(this);
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.TreatmentBMPFundingSources.Add(this);
            this.Amount = amount;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPFundingSource CreateNewBlank(FundingSource fundingSource, TreatmentBMP treatmentBMP)
        {
            return new TreatmentBMPFundingSource(fundingSource, treatmentBMP, default(decimal));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPFundingSource).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPFundingSources.Remove(this);                
        }

        [Key]
        public int TreatmentBMPFundingSourceID { get; set; }
        public int TenantID { get; private set; }
        public int FundingSourceID { get; set; }
        public int TreatmentBMPID { get; set; }
        public decimal Amount { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPFundingSourceID; } set { TreatmentBMPFundingSourceID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual FundingSource FundingSource { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }

        public static class FieldLengths
        {

        }
    }
}