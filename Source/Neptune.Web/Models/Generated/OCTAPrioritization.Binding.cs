//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OCTAPrioritization]
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
    // Table [dbo].[OCTAPrioritization] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[OCTAPrioritization]")]
    public partial class OCTAPrioritization : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected OCTAPrioritization()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public OCTAPrioritization(int oCTAPrioritizationID, int oCTAPrioritizationKey, DbGeometry oCTAPrioritizationGeometry, DateTime lastUpdate, string watershed, string catchIDN, double tPI, double wQNLU, double wQNMON, double iMPAIR, double mON, double sEA, string sEA_PCTL, double pC_VOL_PCT, double pC_NUT_PCT, double pC_BAC_PCT, double pC_MET_PCT, double pC_TSS_PCT) : this()
        {
            this.OCTAPrioritizationID = oCTAPrioritizationID;
            this.OCTAPrioritizationKey = oCTAPrioritizationKey;
            this.OCTAPrioritizationGeometry = oCTAPrioritizationGeometry;
            this.LastUpdate = lastUpdate;
            this.Watershed = watershed;
            this.CatchIDN = catchIDN;
            this.TPI = tPI;
            this.WQNLU = wQNLU;
            this.WQNMON = wQNMON;
            this.IMPAIR = iMPAIR;
            this.MON = mON;
            this.SEA = sEA;
            this.SEA_PCTL = sEA_PCTL;
            this.PC_VOL_PCT = pC_VOL_PCT;
            this.PC_NUT_PCT = pC_NUT_PCT;
            this.PC_BAC_PCT = pC_BAC_PCT;
            this.PC_MET_PCT = pC_MET_PCT;
            this.PC_TSS_PCT = pC_TSS_PCT;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public OCTAPrioritization(int oCTAPrioritizationKey, DbGeometry oCTAPrioritizationGeometry, DateTime lastUpdate, string watershed, string catchIDN, double tPI, double wQNLU, double wQNMON, double iMPAIR, double mON, double sEA, string sEA_PCTL, double pC_VOL_PCT, double pC_NUT_PCT, double pC_BAC_PCT, double pC_MET_PCT, double pC_TSS_PCT) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.OCTAPrioritizationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OCTAPrioritizationKey = oCTAPrioritizationKey;
            this.OCTAPrioritizationGeometry = oCTAPrioritizationGeometry;
            this.LastUpdate = lastUpdate;
            this.Watershed = watershed;
            this.CatchIDN = catchIDN;
            this.TPI = tPI;
            this.WQNLU = wQNLU;
            this.WQNMON = wQNMON;
            this.IMPAIR = iMPAIR;
            this.MON = mON;
            this.SEA = sEA;
            this.SEA_PCTL = sEA_PCTL;
            this.PC_VOL_PCT = pC_VOL_PCT;
            this.PC_NUT_PCT = pC_NUT_PCT;
            this.PC_BAC_PCT = pC_BAC_PCT;
            this.PC_MET_PCT = pC_MET_PCT;
            this.PC_TSS_PCT = pC_TSS_PCT;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static OCTAPrioritization CreateNewBlank()
        {
            return new OCTAPrioritization(default(int), default(DbGeometry), default(DateTime), default(string), default(string), default(double), default(double), default(double), default(double), default(double), default(double), default(string), default(double), default(double), default(double), default(double), default(double));
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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(OCTAPrioritization).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.OCTAPrioritizations.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int OCTAPrioritizationID { get; set; }
        public int OCTAPrioritizationKey { get; set; }
        public DbGeometry OCTAPrioritizationGeometry { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Watershed { get; set; }
        public string CatchIDN { get; set; }
        public double TPI { get; set; }
        public double WQNLU { get; set; }
        public double WQNMON { get; set; }
        public double IMPAIR { get; set; }
        public double MON { get; set; }
        public double SEA { get; set; }
        public string SEA_PCTL { get; set; }
        public double PC_VOL_PCT { get; set; }
        public double PC_NUT_PCT { get; set; }
        public double PC_BAC_PCT { get; set; }
        public double PC_MET_PCT { get; set; }
        public double PC_TSS_PCT { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return OCTAPrioritizationID; } set { OCTAPrioritizationID = value; } }



        public static class FieldLengths
        {
            public const int Watershed = 80;
            public const int CatchIDN = 80;
            public const int SEA_PCTL = 80;
        }
    }
}