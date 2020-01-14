//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPOperationMonth]
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
    // Table [dbo].[TreatmentBMPOperationMonth] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TreatmentBMPOperationMonth]")]
    public partial class TreatmentBMPOperationMonth : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPOperationMonth()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPOperationMonth(int treatmentBMPOperationMonthID, int treatmentBMPID, int operationMonth) : this()
        {
            this.TreatmentBMPOperationMonthID = treatmentBMPOperationMonthID;
            this.TreatmentBMPID = treatmentBMPID;
            this.OperationMonth = operationMonth;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPOperationMonth(int treatmentBMPID, int operationMonth) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPOperationMonthID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
            this.OperationMonth = operationMonth;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPOperationMonth(TreatmentBMP treatmentBMP, int operationMonth) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPOperationMonthID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
            treatmentBMP.TreatmentBMPOperationMonths.Add(this);
            this.OperationMonth = operationMonth;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPOperationMonth CreateNewBlank(TreatmentBMP treatmentBMP)
        {
            return new TreatmentBMPOperationMonth(treatmentBMP, default(int));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPOperationMonth).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TreatmentBMPOperationMonths.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int TreatmentBMPOperationMonthID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int OperationMonth { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPOperationMonthID; } set { TreatmentBMPOperationMonthID = value; } }

        public virtual TreatmentBMP TreatmentBMP { get; set; }

        public static class FieldLengths
        {

        }
    }
}