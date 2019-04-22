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
            this.TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Delineation(int delineationID, DbGeometry delineationGeometry, int delineationTypeID, bool isVerified, DateTime? dateLastVerified, int? verifiedByPersonID) : this()
        {
            this.DelineationID = delineationID;
            this.DelineationGeometry = delineationGeometry;
            this.DelineationTypeID = delineationTypeID;
            this.IsVerified = isVerified;
            this.DateLastVerified = dateLastVerified;
            this.VerifiedByPersonID = verifiedByPersonID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Delineation(DbGeometry delineationGeometry, int delineationTypeID, bool isVerified) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.DelineationGeometry = delineationGeometry;
            this.DelineationTypeID = delineationTypeID;
            this.IsVerified = isVerified;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public Delineation(DbGeometry delineationGeometry, DelineationType delineationType, bool isVerified) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.DelineationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.DelineationGeometry = delineationGeometry;
            this.DelineationTypeID = delineationType.DelineationTypeID;
            this.IsVerified = isVerified;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Delineation CreateNewBlank(DelineationType delineationType)
        {
            return new Delineation(default(DbGeometry), delineationType, default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return (TreatmentBMP != null);
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Delineation).Name, typeof(TreatmentBMP).Name};


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

            foreach(var x in TreatmentBMPs.ToList())
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
        [NotMapped]
        public int PrimaryKey { get { return DelineationID; } set { DelineationID = value; } }

        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
        [NotMapped]
        public TreatmentBMP TreatmentBMP { get { return TreatmentBMPs.SingleOrDefault(); } set { TreatmentBMPs = new List<TreatmentBMP>{value};} }
        public DelineationType DelineationType { get { return DelineationType.AllLookupDictionary[DelineationTypeID]; } }
        public virtual Person VerifiedByPerson { get; set; }

        public static class FieldLengths
        {

        }
    }
}