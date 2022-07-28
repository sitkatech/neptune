//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vRegionalSubbasinUpstream]
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
    public partial class vRegionalSubbasinUpstream
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vRegionalSubbasinUpstream()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vRegionalSubbasinUpstream(int? primaryKey, int? regionalSubbasinID, int? oCSurveyCatchmentID, int? oCSurveyDownstreamCatchmentID) : this()
        {
            this.PrimaryKey = primaryKey;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.OCSurveyDownstreamCatchmentID = oCSurveyDownstreamCatchmentID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vRegionalSubbasinUpstream(vRegionalSubbasinUpstream vRegionalSubbasinUpstream) : this()
        {
            this.PrimaryKey = vRegionalSubbasinUpstream.PrimaryKey;
            this.RegionalSubbasinID = vRegionalSubbasinUpstream.RegionalSubbasinID;
            this.OCSurveyCatchmentID = vRegionalSubbasinUpstream.OCSurveyCatchmentID;
            this.OCSurveyDownstreamCatchmentID = vRegionalSubbasinUpstream.OCSurveyDownstreamCatchmentID;
            CallAfterConstructor(vRegionalSubbasinUpstream);
        }

        partial void CallAfterConstructor(vRegionalSubbasinUpstream vRegionalSubbasinUpstream);

        public int? PrimaryKey { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? OCSurveyCatchmentID { get; set; }
        public int? OCSurveyDownstreamCatchmentID { get; set; }
    }
}