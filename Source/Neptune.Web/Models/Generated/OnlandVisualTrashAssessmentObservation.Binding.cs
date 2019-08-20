//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservation]
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
    // Table [dbo].[OnlandVisualTrashAssessmentObservation] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[OnlandVisualTrashAssessmentObservation]")]
    public partial class OnlandVisualTrashAssessmentObservation : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected OnlandVisualTrashAssessmentObservation()
        {
            this.OnlandVisualTrashAssessmentObservationPhotos = new HashSet<OnlandVisualTrashAssessmentObservationPhoto>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessmentObservation(int onlandVisualTrashAssessmentObservationID, int onlandVisualTrashAssessmentID, DbGeometry locationPoint, string note, DateTime observationDatetime, DbGeometry locationPoint4326) : this()
        {
            this.OnlandVisualTrashAssessmentObservationID = onlandVisualTrashAssessmentObservationID;
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID;
            this.LocationPoint = locationPoint;
            this.Note = note;
            this.ObservationDatetime = observationDatetime;
            this.LocationPoint4326 = locationPoint4326;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public OnlandVisualTrashAssessmentObservation(int onlandVisualTrashAssessmentID, DbGeometry locationPoint, DateTime observationDatetime) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentObservationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessmentID;
            this.LocationPoint = locationPoint;
            this.ObservationDatetime = observationDatetime;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public OnlandVisualTrashAssessmentObservation(OnlandVisualTrashAssessment onlandVisualTrashAssessment, DbGeometry locationPoint, DateTime observationDatetime) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OnlandVisualTrashAssessmentObservationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.OnlandVisualTrashAssessmentID = onlandVisualTrashAssessment.OnlandVisualTrashAssessmentID;
            this.OnlandVisualTrashAssessment = onlandVisualTrashAssessment;
            onlandVisualTrashAssessment.OnlandVisualTrashAssessmentObservations.Add(this);
            this.LocationPoint = locationPoint;
            this.ObservationDatetime = observationDatetime;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static OnlandVisualTrashAssessmentObservation CreateNewBlank(OnlandVisualTrashAssessment onlandVisualTrashAssessment)
        {
            return new OnlandVisualTrashAssessmentObservation(onlandVisualTrashAssessment, default(DbGeometry), default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return OnlandVisualTrashAssessmentObservationPhotos.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(OnlandVisualTrashAssessmentObservation).Name, typeof(OnlandVisualTrashAssessmentObservationPhoto).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.OnlandVisualTrashAssessmentObservations.Remove(this);
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

            foreach(var x in OnlandVisualTrashAssessmentObservationPhotos.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int OnlandVisualTrashAssessmentObservationID { get; set; }
        public int OnlandVisualTrashAssessmentID { get; set; }
        public DbGeometry LocationPoint { get; set; }
        public string Note { get; set; }
        public DateTime ObservationDatetime { get; set; }
        public DbGeometry LocationPoint4326 { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OnlandVisualTrashAssessmentObservationID; } set { OnlandVisualTrashAssessmentObservationID = value; } }

        public virtual ICollection<OnlandVisualTrashAssessmentObservationPhoto> OnlandVisualTrashAssessmentObservationPhotos { get; set; }
        public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; set; }

        public static class FieldLengths
        {
            public const int Note = 500;
        }
    }
}