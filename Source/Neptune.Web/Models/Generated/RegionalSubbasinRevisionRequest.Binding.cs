//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequest]
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
    // Table [dbo].[RegionalSubbasinRevisionRequest] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[RegionalSubbasinRevisionRequest]")]
    public partial class RegionalSubbasinRevisionRequest : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected RegionalSubbasinRevisionRequest()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public RegionalSubbasinRevisionRequest(int regionalSubbasinRevisionRequestID, int requestPersonID, int regionalSubbasinRevisionRequestStatusID, DateTime requestDate, int closedByPersonID, DateTime? closedDate, string notes) : this()
        {
            this.RegionalSubbasinRevisionRequestID = regionalSubbasinRevisionRequestID;
            this.RequestPersonID = requestPersonID;
            this.RegionalSubbasinRevisionRequestStatusID = regionalSubbasinRevisionRequestStatusID;
            this.RequestDate = requestDate;
            this.ClosedByPersonID = closedByPersonID;
            this.ClosedDate = closedDate;
            this.Notes = notes;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public RegionalSubbasinRevisionRequest(int requestPersonID, int regionalSubbasinRevisionRequestStatusID, DateTime requestDate, int closedByPersonID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.RegionalSubbasinRevisionRequestID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.RequestPersonID = requestPersonID;
            this.RegionalSubbasinRevisionRequestStatusID = regionalSubbasinRevisionRequestStatusID;
            this.RequestDate = requestDate;
            this.ClosedByPersonID = closedByPersonID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public RegionalSubbasinRevisionRequest(Person requestPerson, RegionalSubbasinRevisionRequestStatus regionalSubbasinRevisionRequestStatus, DateTime requestDate, Person closedByPerson) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.RegionalSubbasinRevisionRequestID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.RequestPersonID = requestPerson.PersonID;
            this.RequestPerson = requestPerson;
            requestPerson.RegionalSubbasinRevisionRequestsWhereYouAreTheRequestPerson.Add(this);
            this.RegionalSubbasinRevisionRequestStatusID = regionalSubbasinRevisionRequestStatus.RegionalSubbasinRevisionRequestStatusID;
            this.RequestDate = requestDate;
            this.ClosedByPersonID = closedByPerson.PersonID;
            this.ClosedByPerson = closedByPerson;
            closedByPerson.RegionalSubbasinRevisionRequestsWhereYouAreTheClosedByPerson.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static RegionalSubbasinRevisionRequest CreateNewBlank(Person requestPerson, RegionalSubbasinRevisionRequestStatus regionalSubbasinRevisionRequestStatus, Person closedByPerson)
        {
            return new RegionalSubbasinRevisionRequest(requestPerson, regionalSubbasinRevisionRequestStatus, default(DateTime), closedByPerson);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(RegionalSubbasinRevisionRequest).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.RegionalSubbasinRevisionRequests.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int RegionalSubbasinRevisionRequestID { get; set; }
        public int RequestPersonID { get; set; }
        public int RegionalSubbasinRevisionRequestStatusID { get; set; }
        public DateTime RequestDate { get; set; }
        public int ClosedByPersonID { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string Notes { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return RegionalSubbasinRevisionRequestID; } set { RegionalSubbasinRevisionRequestID = value; } }

        public virtual Person ClosedByPerson { get; set; }
        public virtual Person RequestPerson { get; set; }
        public RegionalSubbasinRevisionRequestStatus RegionalSubbasinRevisionRequestStatus { get { return RegionalSubbasinRevisionRequestStatus.AllLookupDictionary[RegionalSubbasinRevisionRequestStatusID]; } }

        public static class FieldLengths
        {

        }
    }
}