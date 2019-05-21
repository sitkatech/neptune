//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vTrashGeneratingUnitLoadBasedPartialCapture]
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
    public partial class vTrashGeneratingUnitLoadBasedPartialCapture
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vTrashGeneratingUnitLoadBasedPartialCapture()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vTrashGeneratingUnitLoadBasedPartialCapture(int primaryKey, int trashGeneratingUnitID, int stormwaterJurisdictionID, double area, decimal? baselineLoadingRate, decimal? actualLoadingAfterTrashCapture) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TrashGeneratingUnitID = trashGeneratingUnitID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.Area = area;
            this.BaselineLoadingRate = baselineLoadingRate;
            this.ActualLoadingAfterTrashCapture = actualLoadingAfterTrashCapture;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vTrashGeneratingUnitLoadBasedPartialCapture(vTrashGeneratingUnitLoadBasedPartialCapture vTrashGeneratingUnitLoadBasedPartialCapture) : this()
        {
            this.PrimaryKey = vTrashGeneratingUnitLoadBasedPartialCapture.PrimaryKey;
            this.TrashGeneratingUnitID = vTrashGeneratingUnitLoadBasedPartialCapture.TrashGeneratingUnitID;
            this.StormwaterJurisdictionID = vTrashGeneratingUnitLoadBasedPartialCapture.StormwaterJurisdictionID;
            this.Area = vTrashGeneratingUnitLoadBasedPartialCapture.Area;
            this.BaselineLoadingRate = vTrashGeneratingUnitLoadBasedPartialCapture.BaselineLoadingRate;
            this.ActualLoadingAfterTrashCapture = vTrashGeneratingUnitLoadBasedPartialCapture.ActualLoadingAfterTrashCapture;
            CallAfterConstructor(vTrashGeneratingUnitLoadBasedPartialCapture);
        }

        partial void CallAfterConstructor(vTrashGeneratingUnitLoadBasedPartialCapture vTrashGeneratingUnitLoadBasedPartialCapture);

        public int PrimaryKey { get; set; }
        public int TrashGeneratingUnitID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public double Area { get; set; }
        public decimal? BaselineLoadingRate { get; set; }
        public decimal? ActualLoadingAfterTrashCapture { get; set; }
    }
}