//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEventFundingSource]
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
    [Table("[dbo].[FundingEventFundingSource]")]
    public partial class FundingEventFundingSource : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FundingEventFundingSource()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundingEventFundingSource(int fundingEventFundingSourceID, int fundingSourceID, int fundingEventID, decimal? amount) : this()
        {
            this.FundingEventFundingSourceID = fundingEventFundingSourceID;
            this.FundingSourceID = fundingSourceID;
            this.FundingEventID = fundingEventID;
            this.Amount = amount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundingEventFundingSource(int fundingSourceID, int fundingEventID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FundingEventFundingSourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FundingSourceID = fundingSourceID;
            this.FundingEventID = fundingEventID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FundingEventFundingSource(FundingSource fundingSource, FundingEvent fundingEvent) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FundingEventFundingSourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FundingSourceID = fundingSource.FundingSourceID;
            this.FundingSource = fundingSource;
            fundingSource.FundingEventFundingSources.Add(this);
            this.FundingEventID = fundingEvent.FundingEventID;
            this.FundingEvent = fundingEvent;
            fundingEvent.FundingEventFundingSources.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FundingEventFundingSource CreateNewBlank(FundingSource fundingSource, FundingEvent fundingEvent)
        {
            return new FundingEventFundingSource(fundingSource, fundingEvent);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FundingEventFundingSource).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.FundingEventFundingSources.Remove(this);
        }

        [Key]
        public int FundingEventFundingSourceID { get; set; }
        public int FundingSourceID { get; set; }
        public int FundingEventID { get; set; }
        public decimal? Amount { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return FundingEventFundingSourceID; } set { FundingEventFundingSourceID = value; } }

        public virtual FundingSource FundingSource { get; set; }
        public virtual FundingEvent FundingEvent { get; set; }

        public static class FieldLengths
        {

        }
    }
}