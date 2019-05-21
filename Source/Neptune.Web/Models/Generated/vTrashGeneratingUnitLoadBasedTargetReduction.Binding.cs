//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vTrashGeneratingUnitLoadBasedTargetReduction]
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
    public partial class vTrashGeneratingUnitLoadBasedTargetReduction
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vTrashGeneratingUnitLoadBasedTargetReduction()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vTrashGeneratingUnitLoadBasedTargetReduction(int primaryKey, int trashGeneratingUnitID, int stormwaterJurisdictionID, decimal? baselineLoadingRate) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TrashGeneratingUnitID = trashGeneratingUnitID;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.BaselineLoadingRate = baselineLoadingRate;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vTrashGeneratingUnitLoadBasedTargetReduction(vTrashGeneratingUnitLoadBasedTargetReduction vTrashGeneratingUnitLoadBasedTargetReduction) : this()
        {
            this.PrimaryKey = vTrashGeneratingUnitLoadBasedTargetReduction.PrimaryKey;
            this.TrashGeneratingUnitID = vTrashGeneratingUnitLoadBasedTargetReduction.TrashGeneratingUnitID;
            this.StormwaterJurisdictionID = vTrashGeneratingUnitLoadBasedTargetReduction.StormwaterJurisdictionID;
            this.BaselineLoadingRate = vTrashGeneratingUnitLoadBasedTargetReduction.BaselineLoadingRate;
            CallAfterConstructor(vTrashGeneratingUnitLoadBasedTargetReduction);
        }

        partial void CallAfterConstructor(vTrashGeneratingUnitLoadBasedTargetReduction vTrashGeneratingUnitLoadBasedTargetReduction);

        public int PrimaryKey { get; set; }
        public int TrashGeneratingUnitID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public decimal? BaselineLoadingRate { get; set; }
    }
}