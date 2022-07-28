using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vRegionalSubbasinUpstream
    {
        public int? PrimaryKey { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? OCSurveyCatchmentID { get; set; }
        public int? OCSurveyDownstreamCatchmentID { get; set; }
    }
}
