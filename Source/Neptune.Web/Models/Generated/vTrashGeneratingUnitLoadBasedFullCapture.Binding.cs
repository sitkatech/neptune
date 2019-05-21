//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vTrashGeneratingUnitLoadBasedFullCapture]
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
    public partial class vTrashGeneratingUnitLoadBasedFullCapture
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vTrashGeneratingUnitLoadBasedFullCapture()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vTrashGeneratingUnitLoadBasedFullCapture(int primaryKey, int trashGeneratingUnitID, double? area, decimal? baselineLoadingRate) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TrashGeneratingUnitID = trashGeneratingUnitID;
            this.Area = area;
            this.BaselineLoadingRate = baselineLoadingRate;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vTrashGeneratingUnitLoadBasedFullCapture(vTrashGeneratingUnitLoadBasedFullCapture vTrashGeneratingUnitLoadBasedFullCapture) : this()
        {
            this.PrimaryKey = vTrashGeneratingUnitLoadBasedFullCapture.PrimaryKey;
            this.TrashGeneratingUnitID = vTrashGeneratingUnitLoadBasedFullCapture.TrashGeneratingUnitID;
            this.Area = vTrashGeneratingUnitLoadBasedFullCapture.Area;
            this.BaselineLoadingRate = vTrashGeneratingUnitLoadBasedFullCapture.BaselineLoadingRate;
            CallAfterConstructor(vTrashGeneratingUnitLoadBasedFullCapture);
        }

        partial void CallAfterConstructor(vTrashGeneratingUnitLoadBasedFullCapture vTrashGeneratingUnitLoadBasedFullCapture);

        public int PrimaryKey { get; set; }
        public int TrashGeneratingUnitID { get; set; }
        public double? Area { get; set; }
        public decimal? BaselineLoadingRate { get; set; }
    }
}