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
            this.TrashGeneratingUnits = new HashSet<TrashGeneratingUnit>();
            this.TrashGeneratingUnit4326s = new HashSet<TrashGeneratingUnit4326>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Delineation(int delineationID, DbGeometry delineationGeometry, int delineationTypeID, bool isVerified, DateTime? dateLastVerified, int? verifiedByPersonID, int treatmentBMPID, DateTime dateLastModified, DbGeometry delineationGeometry4326) : this()
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
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Delineation(DbGeometry delineationGeometry, int delineationTypeID, bool isVerified, int treatmentBMPID, DateTime dateLastModified) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.DelineationGeometry = delineationGeometry;
            this.DelineationTypeID = delineationTypeID;
            this.IsVerified = isVerified;
            this.TreatmentBMPID = treatmentBMPID;
            this.DateLastModified = dateLastModified;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Delineation(DbGeometry delineationGeometry, DelineationType delineationType, bool isVerified, TreatmentBMP treatmentBMP, DateTime dateLastModified) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.DelineationGeometry = delineationGeometry;
            this.DelineationTypeID = delineationType.DelineationTypeID;
            this.IsVerified = isVerified;
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            this.DateLastModified = dateLastModified;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Delineation CreateNewBlank(DelineationType delineationType, TreatmentBMP treatmentBMP)
        {
            return new Delineation(default(DbGeometry), delineationType, default(bool), treatmentBMP, default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TrashGeneratingUnits.Any() || TrashGeneratingUnit4326s.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Delineation).Name, typeof(TrashGeneratingUnit).Name, typeof(TrashGeneratingUnit4326).Name};


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

            foreach(var x in TrashGeneratingUnits.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in TrashGeneratingUnit4326s.ToList())
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
        [NotMapped]
        public int PrimaryKey { get { return DelineationID; } set { DelineationID = value; } }

        public virtual ICollection<TrashGeneratingUnit> TrashGeneratingUnits { get; set; }
        public virtual ICollection<TrashGeneratingUnit4326> TrashGeneratingUnit4326s { get; set; }
        public DelineationType DelineationType { get { return DelineationType.AllLookupDictionary[DelineationTypeID]; } }
        public virtual Person VerifiedByPerson { get; set; }
        public virtual TreatmentBMP TreatmentBMP { get; set; }

        public static class FieldLengths
        {

        }
    }
}