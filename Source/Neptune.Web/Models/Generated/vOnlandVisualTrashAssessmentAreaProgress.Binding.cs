//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vOnlandVisualTrashAssessmentAreaProgress]
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
    public partial class vOnlandVisualTrashAssessmentAreaProgress
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vOnlandVisualTrashAssessmentAreaProgress()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vOnlandVisualTrashAssessmentAreaProgress(int? primaryKey, int? onlandVisualTrashAssessmentAreaID, string onlandVisualTrashAssessmentScoreDisplayName, int onlandVisualTrashAssessmentScoreID) : this()
        {
            this.PrimaryKey = primaryKey;
            this.OnlandVisualTrashAssessmentAreaID = onlandVisualTrashAssessmentAreaID;
            this.OnlandVisualTrashAssessmentScoreDisplayName = onlandVisualTrashAssessmentScoreDisplayName;
            this.OnlandVisualTrashAssessmentScoreID = onlandVisualTrashAssessmentScoreID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vOnlandVisualTrashAssessmentAreaProgress(vOnlandVisualTrashAssessmentAreaProgress vOnlandVisualTrashAssessmentAreaProgress) : this()
        {
            this.PrimaryKey = vOnlandVisualTrashAssessmentAreaProgress.PrimaryKey;
            this.OnlandVisualTrashAssessmentAreaID = vOnlandVisualTrashAssessmentAreaProgress.OnlandVisualTrashAssessmentAreaID;
            this.OnlandVisualTrashAssessmentScoreDisplayName = vOnlandVisualTrashAssessmentAreaProgress.OnlandVisualTrashAssessmentScoreDisplayName;
            this.OnlandVisualTrashAssessmentScoreID = vOnlandVisualTrashAssessmentAreaProgress.OnlandVisualTrashAssessmentScoreID;
            CallAfterConstructor(vOnlandVisualTrashAssessmentAreaProgress);
        }

        partial void CallAfterConstructor(vOnlandVisualTrashAssessmentAreaProgress vOnlandVisualTrashAssessmentAreaProgress);

        public int? PrimaryKey { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public string OnlandVisualTrashAssessmentScoreDisplayName { get; set; }
        public int OnlandVisualTrashAssessmentScoreID { get; set; }
    }
}