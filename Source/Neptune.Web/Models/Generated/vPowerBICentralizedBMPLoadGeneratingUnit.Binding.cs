//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vPowerBICentralizedBMPLoadGeneratingUnit]
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
    public partial class vPowerBICentralizedBMPLoadGeneratingUnit
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vPowerBICentralizedBMPLoadGeneratingUnit()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vPowerBICentralizedBMPLoadGeneratingUnit(long? primaryKey, int treatmentBMPID, int loadGeneratingUnitID) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPID = treatmentBMPID;
            this.LoadGeneratingUnitID = loadGeneratingUnitID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vPowerBICentralizedBMPLoadGeneratingUnit(vPowerBICentralizedBMPLoadGeneratingUnit vPowerBICentralizedBMPLoadGeneratingUnit) : this()
        {
            this.PrimaryKey = vPowerBICentralizedBMPLoadGeneratingUnit.PrimaryKey;
            this.TreatmentBMPID = vPowerBICentralizedBMPLoadGeneratingUnit.TreatmentBMPID;
            this.LoadGeneratingUnitID = vPowerBICentralizedBMPLoadGeneratingUnit.LoadGeneratingUnitID;
            CallAfterConstructor(vPowerBICentralizedBMPLoadGeneratingUnit);
        }

        partial void CallAfterConstructor(vPowerBICentralizedBMPLoadGeneratingUnit vPowerBICentralizedBMPLoadGeneratingUnit);

        public long? PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        public int LoadGeneratingUnitID { get; set; }
    }
}