//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMP]
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
    // Table [dbo].[SourceControlBMP] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[SourceControlBMP]")]
    public partial class SourceControlBMP : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected SourceControlBMP()
        {
            this.WaterQualityManagementPlanVerifySourceControlBMPs = new HashSet<WaterQualityManagementPlanVerifySourceControlBMP>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public SourceControlBMP(int sourceControlBMPID, int waterQualityManagementPlanID, int sourceControlBMPAttributeID, bool? isPresent, string sourceControlBMPNote) : this()
        {
            this.SourceControlBMPID = sourceControlBMPID;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.SourceControlBMPAttributeID = sourceControlBMPAttributeID;
            this.IsPresent = isPresent;
            this.SourceControlBMPNote = sourceControlBMPNote;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public SourceControlBMP(int waterQualityManagementPlanID, int sourceControlBMPAttributeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.SourceControlBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.SourceControlBMPAttributeID = sourceControlBMPAttributeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public SourceControlBMP(WaterQualityManagementPlan waterQualityManagementPlan, SourceControlBMPAttribute sourceControlBMPAttribute) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.SourceControlBMPID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;
            this.WaterQualityManagementPlan = waterQualityManagementPlan;
            waterQualityManagementPlan.SourceControlBMPs.Add(this);
            this.SourceControlBMPAttributeID = sourceControlBMPAttribute.SourceControlBMPAttributeID;
            this.SourceControlBMPAttribute = sourceControlBMPAttribute;
            sourceControlBMPAttribute.SourceControlBMPs.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static SourceControlBMP CreateNewBlank(WaterQualityManagementPlan waterQualityManagementPlan, SourceControlBMPAttribute sourceControlBMPAttribute)
        {
            return new SourceControlBMP(waterQualityManagementPlan, sourceControlBMPAttribute);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return WaterQualityManagementPlanVerifySourceControlBMPs.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(WaterQualityManagementPlanVerifySourceControlBMPs.Any())
            {
                dependentObjects.Add(typeof(WaterQualityManagementPlanVerifySourceControlBMP).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(SourceControlBMP).Name, typeof(WaterQualityManagementPlanVerifySourceControlBMP).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.SourceControlBMPs.Remove(this);
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

            foreach(var x in WaterQualityManagementPlanVerifySourceControlBMPs.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int SourceControlBMPID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int SourceControlBMPAttributeID { get; set; }
        public bool? IsPresent { get; set; }
        public string SourceControlBMPNote { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return SourceControlBMPID; } set { SourceControlBMPID = value; } }

        public virtual ICollection<WaterQualityManagementPlanVerifySourceControlBMP> WaterQualityManagementPlanVerifySourceControlBMPs { get; set; }
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        public virtual SourceControlBMPAttribute SourceControlBMPAttribute { get; set; }

        public static class FieldLengths
        {
            public const int SourceControlBMPNote = 200;
        }
    }
}