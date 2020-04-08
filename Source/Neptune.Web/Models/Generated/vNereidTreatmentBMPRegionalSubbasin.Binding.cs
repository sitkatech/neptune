//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vNereidTreatmentBMPRegionalSubbasin]
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
    public partial class vNereidTreatmentBMPRegionalSubbasin
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vNereidTreatmentBMPRegionalSubbasin()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vNereidTreatmentBMPRegionalSubbasin(long? primaryKey, int treatmentBMPID, int regionalSubbasinID) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPID = treatmentBMPID;
            this.RegionalSubbasinID = regionalSubbasinID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vNereidTreatmentBMPRegionalSubbasin(vNereidTreatmentBMPRegionalSubbasin vNereidTreatmentBMPRegionalSubbasin) : this()
        {
            this.PrimaryKey = vNereidTreatmentBMPRegionalSubbasin.PrimaryKey;
            this.TreatmentBMPID = vNereidTreatmentBMPRegionalSubbasin.TreatmentBMPID;
            this.RegionalSubbasinID = vNereidTreatmentBMPRegionalSubbasin.RegionalSubbasinID;
            CallAfterConstructor(vNereidTreatmentBMPRegionalSubbasin);
        }

        partial void CallAfterConstructor(vNereidTreatmentBMPRegionalSubbasin vNereidTreatmentBMPRegionalSubbasin);

        public long? PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        public int RegionalSubbasinID { get; set; }
    }
}