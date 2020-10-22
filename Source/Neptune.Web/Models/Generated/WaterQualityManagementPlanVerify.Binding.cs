//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerify]
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
    // Table [dbo].[WaterQualityManagementPlanVerify] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[WaterQualityManagementPlanVerify]")]
    public partial class WaterQualityManagementPlanVerify : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WaterQualityManagementPlanVerify()
        {
            this.WaterQualityManagementPlanVerifyPhotos = new HashSet<WaterQualityManagementPlanVerifyPhoto>();
            this.WaterQualityManagementPlanVerifyQuickBMPs = new HashSet<WaterQualityManagementPlanVerifyQuickBMP>();
            this.WaterQualityManagementPlanVerifySourceControlBMPs = new HashSet<WaterQualityManagementPlanVerifySourceControlBMP>();
            this.WaterQualityManagementPlanVerifyTreatmentBMPs = new HashSet<WaterQualityManagementPlanVerifyTreatmentBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVerify(int waterQualityManagementPlanVerifyID, int waterQualityManagementPlanID, int waterQualityManagementPlanVerifyTypeID, int waterQualityManagementPlanVisitStatusID, int? fileResourceID, int? waterQualityManagementPlanVerifyStatusID, int lastEditedByPersonID, string sourceControlCondition, string enforcementOrFollowupActions, DateTime lastEditedDate, bool isDraft, DateTime verificationDate) : this()
        {
            this.WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.WaterQualityManagementPlanVerifyTypeID = waterQualityManagementPlanVerifyTypeID;
            this.WaterQualityManagementPlanVisitStatusID = waterQualityManagementPlanVisitStatusID;
            this.FileResourceID = fileResourceID;
            this.WaterQualityManagementPlanVerifyStatusID = waterQualityManagementPlanVerifyStatusID;
            this.LastEditedByPersonID = lastEditedByPersonID;
            this.SourceControlCondition = sourceControlCondition;
            this.EnforcementOrFollowupActions = enforcementOrFollowupActions;
            this.LastEditedDate = lastEditedDate;
            this.IsDraft = isDraft;
            this.VerificationDate = verificationDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WaterQualityManagementPlanVerify(int waterQualityManagementPlanID, int waterQualityManagementPlanVerifyTypeID, int waterQualityManagementPlanVisitStatusID, int lastEditedByPersonID, DateTime lastEditedDate, bool isDraft, DateTime verificationDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVerifyID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.WaterQualityManagementPlanVerifyTypeID = waterQualityManagementPlanVerifyTypeID;
            this.WaterQualityManagementPlanVisitStatusID = waterQualityManagementPlanVisitStatusID;
            this.LastEditedByPersonID = lastEditedByPersonID;
            this.LastEditedDate = lastEditedDate;
            this.IsDraft = isDraft;
            this.VerificationDate = verificationDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public WaterQualityManagementPlanVerify(WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType, WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatus, Person lastEditedByPerson, DateTime lastEditedDate, bool isDraft, DateTime verificationDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WaterQualityManagementPlanVerifyID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            this.WaterQualityManagementPlan = waterQualityManagementPlan;
            waterQualityManagementPlan.WaterQualityManagementPlanVerifies.Add(this);
            this.WaterQualityManagementPlanVerifyTypeID = waterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifyTypeID;
            this.WaterQualityManagementPlanVerifyType = waterQualityManagementPlanVerifyType;
            waterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifies.Add(this);
            this.WaterQualityManagementPlanVisitStatusID = waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusID;
            this.WaterQualityManagementPlanVisitStatus = waterQualityManagementPlanVisitStatus;
            waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVerifies.Add(this);
            this.LastEditedByPersonID = lastEditedByPerson.PersonID;
            this.LastEditedByPerson = lastEditedByPerson;
            lastEditedByPerson.WaterQualityManagementPlanVerifiesWhereYouAreTheLastEditedByPerson.Add(this);
            this.LastEditedDate = lastEditedDate;
            this.IsDraft = isDraft;
            this.VerificationDate = verificationDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WaterQualityManagementPlanVerify CreateNewBlank(WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType, WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatus, Person lastEditedByPerson)
        {
            return new WaterQualityManagementPlanVerify(waterQualityManagementPlan, waterQualityManagementPlanVerifyType, waterQualityManagementPlanVisitStatus, lastEditedByPerson, default(DateTime), default(bool), default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return WaterQualityManagementPlanVerifyPhotos.Any() || WaterQualityManagementPlanVerifyQuickBMPs.Any() || WaterQualityManagementPlanVerifySourceControlBMPs.Any() || WaterQualityManagementPlanVerifyTreatmentBMPs.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(WaterQualityManagementPlanVerifyPhotos.Any())
            {
                dependentObjects.Add(typeof(WaterQualityManagementPlanVerifyPhoto).Name);
            }

            if(WaterQualityManagementPlanVerifyQuickBMPs.Any())
            {
                dependentObjects.Add(typeof(WaterQualityManagementPlanVerifyQuickBMP).Name);
            }

            if(WaterQualityManagementPlanVerifySourceControlBMPs.Any())
            {
                dependentObjects.Add(typeof(WaterQualityManagementPlanVerifySourceControlBMP).Name);
            }

            if(WaterQualityManagementPlanVerifyTreatmentBMPs.Any())
            {
                dependentObjects.Add(typeof(WaterQualityManagementPlanVerifyTreatmentBMP).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WaterQualityManagementPlanVerify).Name, typeof(WaterQualityManagementPlanVerifyPhoto).Name, typeof(WaterQualityManagementPlanVerifyQuickBMP).Name, typeof(WaterQualityManagementPlanVerifySourceControlBMP).Name, typeof(WaterQualityManagementPlanVerifyTreatmentBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.WaterQualityManagementPlanVerifies.Remove(this);
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

            foreach(var x in WaterQualityManagementPlanVerifyPhotos.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanVerifyQuickBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanVerifySourceControlBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in WaterQualityManagementPlanVerifyTreatmentBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int WaterQualityManagementPlanVerifyTypeID { get; set; }
        public int WaterQualityManagementPlanVisitStatusID { get; set; }
        public int? FileResourceID { get; set; }
        public int? WaterQualityManagementPlanVerifyStatusID { get; set; }
        public int LastEditedByPersonID { get; set; }
        public string SourceControlCondition { get; set; }
        public string EnforcementOrFollowupActions { get; set; }
        public DateTime LastEditedDate { get; set; }
        public bool IsDraft { get; set; }
        public DateTime VerificationDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanVerifyID; } set { WaterQualityManagementPlanVerifyID = value; } }

        public virtual ICollection<WaterQualityManagementPlanVerifyPhoto> WaterQualityManagementPlanVerifyPhotos { get; set; }
        public virtual ICollection<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; set; }
        public virtual ICollection<WaterQualityManagementPlanVerifySourceControlBMP> WaterQualityManagementPlanVerifySourceControlBMPs { get; set; }
        public virtual ICollection<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; set; }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        public virtual WaterQualityManagementPlanVerifyType WaterQualityManagementPlanVerifyType { get; set; }
        public virtual WaterQualityManagementPlanVisitStatus WaterQualityManagementPlanVisitStatus { get; set; }
        public virtual FileResource FileResource { get; set; }
        public virtual WaterQualityManagementPlanVerifyStatus WaterQualityManagementPlanVerifyStatus { get; set; }
        public virtual Person LastEditedByPerson { get; set; }

        public static class FieldLengths
        {
            public const int SourceControlCondition = 1000;
            public const int EnforcementOrFollowupActions = 1000;
        }
    }
}