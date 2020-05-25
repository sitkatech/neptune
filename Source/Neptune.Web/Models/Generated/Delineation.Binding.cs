//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Delineation]
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
    // Table [dbo].[Delineation] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Delineation]")]
    public partial class Delineation : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Delineation()
        {
            this.DelineationOverlaps = new HashSet<DelineationOverlap>();
            this.DelineationOverlapsWhereYouAreTheOverlappingDelineation = new HashSet<DelineationOverlap>();
            this.LoadGeneratingUnits = new HashSet<LoadGeneratingUnit>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Delineation(int delineationID, DbGeometry delineationGeometry, int delineationTypeID, bool isVerified, DateTime? dateLastVerified, int? verifiedByPersonID, int treatmentBMPID, DateTime dateLastModified, DbGeometry delineationGeometry4326, bool hasDiscrepancies) : this()
        {
            this.DelineationID = delineationID;
            this.DelineationGeometry = delineationGeometry;
            this.DelineationTypeID = delineationTypeID;
            this.IsVerified = isVerified;
            this.DateLastVerified = dateLastVerified;
            this.VerifiedByPersonID = verifiedByPersonID;
            this.TreatmentBMPID = treatmentBMPID;
            this.DateLastModified = dateLastModified;
            this.DelineationGeometry4326 = delineationGeometry4326;
            this.HasDiscrepancies = hasDiscrepancies;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Delineation(DbGeometry delineationGeometry, int delineationTypeID, bool isVerified, int treatmentBMPID, DateTime dateLastModified, bool hasDiscrepancies) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.DelineationGeometry = delineationGeometry;
            this.DelineationTypeID = delineationTypeID;
            this.IsVerified = isVerified;
            this.TreatmentBMPID = treatmentBMPID;
            this.DateLastModified = dateLastModified;
            this.HasDiscrepancies = hasDiscrepancies;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Delineation(DbGeometry delineationGeometry, DelineationType delineationType, bool isVerified, TreatmentBMP treatmentBMP, DateTime dateLastModified, bool hasDiscrepancies) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.DelineationGeometry = delineationGeometry;
            this.DelineationTypeID = delineationType.DelineationTypeID;
            this.IsVerified = isVerified;
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            this.DateLastModified = dateLastModified;
            this.HasDiscrepancies = hasDiscrepancies;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Delineation CreateNewBlank(DelineationType delineationType, TreatmentBMP treatmentBMP)
        {
            return new Delineation(default(DbGeometry), delineationType, default(bool), treatmentBMP, default(DateTime), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return DelineationOverlaps.Any() || DelineationOverlapsWhereYouAreTheOverlappingDelineation.Any() || LoadGeneratingUnits.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(DelineationOverlaps.Any())
            {
                dependentObjects.Add(typeof(DelineationOverlap).Name);
            }

            if(DelineationOverlapsWhereYouAreTheOverlappingDelineation.Any())
            {
                dependentObjects.Add(typeof(DelineationOverlap).Name);
            }

            if(LoadGeneratingUnits.Any())
            {
                dependentObjects.Add(typeof(LoadGeneratingUnit).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Delineation).Name, typeof(DelineationOverlap).Name, typeof(LoadGeneratingUnit).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.Delineations.Remove(this);
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

            foreach(var x in DelineationOverlaps.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in DelineationOverlapsWhereYouAreTheOverlappingDelineation.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in LoadGeneratingUnits.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int DelineationID { get; set; }
        public DbGeometry DelineationGeometry { get; set; }
        public int DelineationTypeID { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? DateLastVerified { get; set; }
        public int? VerifiedByPersonID { get; set; }
        public int TreatmentBMPID { get; set; }
        public DateTime DateLastModified { get; set; }
        public DbGeometry DelineationGeometry4326 { get; set; }
        public bool HasDiscrepancies { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return DelineationID; } set { DelineationID = value; } }

        public virtual ICollection<DelineationOverlap> DelineationOverlaps { get; set; }
        public virtual ICollection<DelineationOverlap> DelineationOverlapsWhereYouAreTheOverlappingDelineation { get; set; }
        public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        public DelineationType DelineationType { get { return DelineationType.AllLookupDictionary[DelineationTypeID]; } }
        public virtual Person VerifiedByPerson { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }

        public static class FieldLengths
        {

        }
    }
}