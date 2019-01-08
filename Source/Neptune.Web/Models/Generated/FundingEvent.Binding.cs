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
    public partial class FundingEvent : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FundingEvent()
        {
            this.FundingEventFundingSources = new HashSet<FundingEventFundingSource>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundingEvent(int fundingEventID, int treatmentBMPID, int fundingEventTypeID, int year, string description) : this()
        {
            this.FundingEventID = fundingEventID;
            this.TreatmentBMPID = treatmentBMPID;
            this.FundingEventTypeID = fundingEventTypeID;
            this.Year = year;
            this.Description = description;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundingEvent(int treatmentBMPID, int fundingEventTypeID, int year) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FundingEventID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.FundingEventTypeID = fundingEventTypeID;
            this.Year = year;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FundingEvent(TreatmentBMP treatmentBMP, FundingEventType fundingEventType, int year) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FundingEventID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.FundingEvents.Add(this);
            this.FundingEventTypeID = fundingEventType.FundingEventTypeID;
            this.Year = year;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FundingEvent CreateNewBlank(TreatmentBMP treatmentBMP, FundingEventType fundingEventType)
        {
            return new FundingEvent(treatmentBMP, fundingEventType, default(int));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return FundingEventFundingSources.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FundingEvent).Name, typeof(FundingEventFundingSource).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.FundingEvents.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in FundingEventFundingSources.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int FundingEventID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int FundingEventTypeID { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return FundingEventID; } set { FundingEventID = value; } }

        public virtual ICollection<FundingEventFundingSource> FundingEventFundingSources { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public FundingEventType FundingEventType { get { return FundingEventType.AllLookupDictionary[FundingEventTypeID]; } }

        public static class FieldLengths
        {
            public const int Description = 500;
        }
    }
}