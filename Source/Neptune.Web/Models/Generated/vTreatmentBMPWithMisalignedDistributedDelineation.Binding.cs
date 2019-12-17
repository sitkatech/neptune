//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vTreatmentBMPWithMisalignedDistributedDelineation]
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
    public partial class vTreatmentBMPWithMisalignedDistributedDelineation
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vTreatmentBMPWithMisalignedDistributedDelineation()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vTreatmentBMPWithMisalignedDistributedDelineation(int primaryKey, int treatmentBMPID, string treatmentBMPName, int treatmentBMPTypeID, string treatmentBMPTypeName, int delineationID, int delineationTypeID, string delineationTypeDisplayName, double? delineationArea, DateTime dateLastModified, DateTime? dateLastVerified) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPTypeName = treatmentBMPTypeName;
            this.DelineationID = delineationID;
            this.DelineationTypeID = delineationTypeID;
            this.DelineationTypeDisplayName = delineationTypeDisplayName;
            this.DelineationArea = delineationArea;
            this.DateLastModified = dateLastModified;
            this.DateLastVerified = dateLastVerified;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vTreatmentBMPWithMisalignedDistributedDelineation(vTreatmentBMPWithMisalignedDistributedDelineation vTreatmentBMPWithMisalignedDistributedDelineation) : this()
        {
            this.PrimaryKey = vTreatmentBMPWithMisalignedDistributedDelineation.PrimaryKey;
            this.TreatmentBMPID = vTreatmentBMPWithMisalignedDistributedDelineation.TreatmentBMPID;
            this.TreatmentBMPName = vTreatmentBMPWithMisalignedDistributedDelineation.TreatmentBMPName;
            this.TreatmentBMPTypeID = vTreatmentBMPWithMisalignedDistributedDelineation.TreatmentBMPTypeID;
            this.TreatmentBMPTypeName = vTreatmentBMPWithMisalignedDistributedDelineation.TreatmentBMPTypeName;
            this.DelineationID = vTreatmentBMPWithMisalignedDistributedDelineation.DelineationID;
            this.DelineationTypeID = vTreatmentBMPWithMisalignedDistributedDelineation.DelineationTypeID;
            this.DelineationTypeDisplayName = vTreatmentBMPWithMisalignedDistributedDelineation.DelineationTypeDisplayName;
            this.DelineationArea = vTreatmentBMPWithMisalignedDistributedDelineation.DelineationArea;
            this.DateLastModified = vTreatmentBMPWithMisalignedDistributedDelineation.DateLastModified;
            this.DateLastVerified = vTreatmentBMPWithMisalignedDistributedDelineation.DateLastVerified;
            CallAfterConstructor(vTreatmentBMPWithMisalignedDistributedDelineation);
        }

        partial void CallAfterConstructor(vTreatmentBMPWithMisalignedDistributedDelineation vTreatmentBMPWithMisalignedDistributedDelineation);

        public int PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public int DelineationID { get; set; }
        public int DelineationTypeID { get; set; }
        public string DelineationTypeDisplayName { get; set; }
        public double? DelineationArea { get; set; }
        public DateTime DateLastModified { get; set; }
        public DateTime? DateLastVerified { get; set; }
    }
}