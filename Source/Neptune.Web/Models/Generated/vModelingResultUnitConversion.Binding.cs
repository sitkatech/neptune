//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vModelingResultUnitConversion]
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class vModelingResultUnitConversion
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vModelingResultUnitConversion()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vModelingResultUnitConversion(int primaryKey, decimal poundsToKilogramsFactor, decimal poundsToGramsFactor, double billionsFactor) : this()
        {
            this.PrimaryKey = primaryKey;
            this.PoundsToKilogramsFactor = poundsToKilogramsFactor;
            this.PoundsToGramsFactor = poundsToGramsFactor;
            this.BillionsFactor = billionsFactor;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vModelingResultUnitConversion(vModelingResultUnitConversion vModelingResultUnitConversion) : this()
        {
            this.PrimaryKey = vModelingResultUnitConversion.PrimaryKey;
            this.PoundsToKilogramsFactor = vModelingResultUnitConversion.PoundsToKilogramsFactor;
            this.PoundsToGramsFactor = vModelingResultUnitConversion.PoundsToGramsFactor;
            this.BillionsFactor = vModelingResultUnitConversion.BillionsFactor;
            CallAfterConstructor(vModelingResultUnitConversion);
        }

        partial void CallAfterConstructor(vModelingResultUnitConversion vModelingResultUnitConversion);

        public int PrimaryKey { get; set; }
        public decimal PoundsToKilogramsFactor { get; set; }
        public decimal PoundsToGramsFactor { get; set; }
        public double BillionsFactor { get; set; }
    }
}