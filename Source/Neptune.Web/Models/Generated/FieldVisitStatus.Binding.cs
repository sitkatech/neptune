//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisitStatus]
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
    [Table("[dbo].[FieldVisitStatus]")]
    public partial class FieldVisitStatus : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FieldVisitStatus()
        {
            this.FieldVisits = new HashSet<FieldVisit>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FieldVisitStatus(int fieldVisitStatusID, string fieldVisitStatusName, string fieldVisitStatusDisplayName) : this()
        {
            this.FieldVisitStatusID = fieldVisitStatusID;
            this.FieldVisitStatusName = fieldVisitStatusName;
            this.FieldVisitStatusDisplayName = fieldVisitStatusDisplayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FieldVisitStatus(string fieldVisitStatusName, string fieldVisitStatusDisplayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FieldVisitStatusID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FieldVisitStatusName = fieldVisitStatusName;
            this.FieldVisitStatusDisplayName = fieldVisitStatusDisplayName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FieldVisitStatus CreateNewBlank()
        {
            return new FieldVisitStatus(default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return FieldVisits.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FieldVisitStatus).Name, typeof(FieldVisit).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull()
        {

            foreach(var x in FieldVisits.ToList())
            {
                x.DeleteFull();
            }
            HttpRequestStorage.DatabaseEntities.FieldVisitStatuses.Remove(this);                
        }

        [Key]
        public int FieldVisitStatusID { get; set; }
        public string FieldVisitStatusName { get; set; }
        public string FieldVisitStatusDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return FieldVisitStatusID; } set { FieldVisitStatusID = value; } }

        public virtual ICollection<FieldVisit> FieldVisits { get; set; }

        public static class FieldLengths
        {
            public const int FieldVisitStatusName = 20;
            public const int FieldVisitStatusDisplayName = 20;
        }
    }
}