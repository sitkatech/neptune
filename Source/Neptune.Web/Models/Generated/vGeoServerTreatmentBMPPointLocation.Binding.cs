//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vGeoServerTreatmentBMPPointLocation]
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
    public partial class vGeoServerTreatmentBMPPointLocation
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vGeoServerTreatmentBMPPointLocation()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vGeoServerTreatmentBMPPointLocation(int primaryKey, string treatmentBMPName) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPName = treatmentBMPName;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vGeoServerTreatmentBMPPointLocation(vGeoServerTreatmentBMPPointLocation vGeoServerTreatmentBMPPointLocation) : this()
        {
            this.PrimaryKey = vGeoServerTreatmentBMPPointLocation.PrimaryKey;
            this.TreatmentBMPName = vGeoServerTreatmentBMPPointLocation.TreatmentBMPName;
            CallAfterConstructor(vGeoServerTreatmentBMPPointLocation);
        }

        partial void CallAfterConstructor(vGeoServerTreatmentBMPPointLocation vGeoServerTreatmentBMPPointLocation);

        public int PrimaryKey { get; set; }
        public string TreatmentBMPName { get; set; }
    }
}