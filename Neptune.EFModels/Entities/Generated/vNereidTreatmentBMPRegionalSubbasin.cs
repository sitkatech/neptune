using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vNereidTreatmentBMPRegionalSubbasin
{
    public long? PrimaryKey { get; set; }

    public int TreatmentBMPID { get; set; }

    public int RegionalSubbasinID { get; set; }

    public int OCSurveyCatchmentID { get; set; }
}
