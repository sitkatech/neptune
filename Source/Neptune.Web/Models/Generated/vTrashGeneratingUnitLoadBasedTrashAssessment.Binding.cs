//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vTrashGeneratingUnitLoadBasedTrashAssessment]
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
    public partial class vTrashGeneratingUnitLoadBasedTrashAssessment
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vTrashGeneratingUnitLoadBasedTrashAssessment()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vTrashGeneratingUnitLoadBasedTrashAssessment(int primaryKey, int trashGeneratingUnitID, double? area, decimal? progressLoadingRate, decimal? baselineLoadingRate) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TrashGeneratingUnitID = trashGeneratingUnitID;
            this.Area = area;
            this.ProgressLoadingRate = progressLoadingRate;
            this.BaselineLoadingRate = baselineLoadingRate;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vTrashGeneratingUnitLoadBasedTrashAssessment(vTrashGeneratingUnitLoadBasedTrashAssessment vTrashGeneratingUnitLoadBasedTrashAssessment) : this()
        {
            this.PrimaryKey = vTrashGeneratingUnitLoadBasedTrashAssessment.PrimaryKey;
            this.TrashGeneratingUnitID = vTrashGeneratingUnitLoadBasedTrashAssessment.TrashGeneratingUnitID;
            this.Area = vTrashGeneratingUnitLoadBasedTrashAssessment.Area;
            this.ProgressLoadingRate = vTrashGeneratingUnitLoadBasedTrashAssessment.ProgressLoadingRate;
            this.BaselineLoadingRate = vTrashGeneratingUnitLoadBasedTrashAssessment.BaselineLoadingRate;
            CallAfterConstructor(vTrashGeneratingUnitLoadBasedTrashAssessment);
        }

        partial void CallAfterConstructor(vTrashGeneratingUnitLoadBasedTrashAssessment vTrashGeneratingUnitLoadBasedTrashAssessment);

        public int PrimaryKey { get; set; }
        public int TrashGeneratingUnitID { get; set; }
        public double? Area { get; set; }
        public decimal? ProgressLoadingRate { get; set; }
        public decimal? BaselineLoadingRate { get; set; }
    }
}