using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vNereidPlannedProjectRegionalSubbasinCentralizedBMP
    {
        public int PrimaryKey { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int? ProjectID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int? UpstreamBMPID { get; set; }
        public long? RowNumber { get; set; }
    }
}
