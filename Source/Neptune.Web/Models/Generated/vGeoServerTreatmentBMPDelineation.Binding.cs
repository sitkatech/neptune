//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerTreatmentBMPDelineation]
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
    public partial class vGeoServerTreatmentBMPDelineation
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerTreatmentBMPDelineation()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerTreatmentBMPDelineation(int primaryKey, int treatmentBMPID, string treatmentBMPName) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPName = treatmentBMPName;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerTreatmentBMPDelineation(vGeoServerTreatmentBMPDelineation vGeoServerTreatmentBMPDelineation) : this()
        {
            this.PrimaryKey = vGeoServerTreatmentBMPDelineation.PrimaryKey;
            this.TreatmentBMPID = vGeoServerTreatmentBMPDelineation.TreatmentBMPID;
            this.TreatmentBMPName = vGeoServerTreatmentBMPDelineation.TreatmentBMPName;
            CallAfterConstructor(vGeoServerTreatmentBMPDelineation);
        }

        partial void CallAfterConstructor(vGeoServerTreatmentBMPDelineation vGeoServerTreatmentBMPDelineation);

        public int PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
    }
}