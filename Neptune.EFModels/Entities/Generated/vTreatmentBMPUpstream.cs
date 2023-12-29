using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vTreatmentBMPUpstream
{
    public int? TreatmentBMPID { get; set; }

    public int? UpstreamBMPID { get; set; }

    public int? Depth { get; set; }
}
