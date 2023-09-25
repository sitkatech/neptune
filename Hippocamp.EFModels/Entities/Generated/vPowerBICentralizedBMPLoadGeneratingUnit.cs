using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vPowerBICentralizedBMPLoadGeneratingUnit
    {
        public long? PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        public int LoadGeneratingUnitID { get; set; }
    }
}
