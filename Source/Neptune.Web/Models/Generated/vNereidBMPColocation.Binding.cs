//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vNereidBMPColocation]
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
    public partial class vNereidBMPColocation
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vNereidBMPColocation()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vNereidBMPColocation(int primaryKey, int downstreamBMPID, int? downstreamRSBID, int upstreamBMPID, int? upstreamRSBID) : this()
        {
            this.PrimaryKey = primaryKey;
            this.DownstreamBMPID = downstreamBMPID;
            this.DownstreamRSBID = downstreamRSBID;
            this.UpstreamBMPID = upstreamBMPID;
            this.UpstreamRSBID = upstreamRSBID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vNereidBMPColocation(vNereidBMPColocation vNereidBMPColocation) : this()
        {
            this.PrimaryKey = vNereidBMPColocation.PrimaryKey;
            this.DownstreamBMPID = vNereidBMPColocation.DownstreamBMPID;
            this.DownstreamRSBID = vNereidBMPColocation.DownstreamRSBID;
            this.UpstreamBMPID = vNereidBMPColocation.UpstreamBMPID;
            this.UpstreamRSBID = vNereidBMPColocation.UpstreamRSBID;
            CallAfterConstructor(vNereidBMPColocation);
        }

        partial void CallAfterConstructor(vNereidBMPColocation vNereidBMPColocation);

        public int PrimaryKey { get; set; }
        public int DownstreamBMPID { get; set; }
        public int? DownstreamRSBID { get; set; }
        public int UpstreamBMPID { get; set; }
        public int? UpstreamRSBID { get; set; }
    }
}