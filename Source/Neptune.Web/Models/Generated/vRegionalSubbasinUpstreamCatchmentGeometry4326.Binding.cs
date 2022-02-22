//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vRegionalSubbasinUpstreamCatchmentGeometry4326]
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
    public partial class vRegionalSubbasinUpstreamCatchmentGeometry4326
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vRegionalSubbasinUpstreamCatchmentGeometry4326()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vRegionalSubbasinUpstreamCatchmentGeometry4326(int? primaryKey) : this()
        {
            this.PrimaryKey = primaryKey;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vRegionalSubbasinUpstreamCatchmentGeometry4326(vRegionalSubbasinUpstreamCatchmentGeometry4326 vRegionalSubbasinUpstreamCatchmentGeometry4326) : this()
        {
            this.PrimaryKey = vRegionalSubbasinUpstreamCatchmentGeometry4326.PrimaryKey;
            CallAfterConstructor(vRegionalSubbasinUpstreamCatchmentGeometry4326);
        }

        partial void CallAfterConstructor(vRegionalSubbasinUpstreamCatchmentGeometry4326 vRegionalSubbasinUpstreamCatchmentGeometry4326);

        public int? PrimaryKey { get; set; }
    }
}