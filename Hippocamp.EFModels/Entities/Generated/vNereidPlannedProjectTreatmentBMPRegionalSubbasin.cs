using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vNereidPlannedProjectTreatmentBMPRegionalSubbasin
    {
        public long? PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        public int? ProjectID { get; set; }
        public int RegionalSubbasinID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
    }
}
