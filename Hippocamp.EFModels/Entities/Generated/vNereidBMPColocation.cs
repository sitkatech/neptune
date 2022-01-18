﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vNereidBMPColocation
    {
        public int PrimaryKey { get; set; }
        public int DownstreamBMPID { get; set; }
        public int UpstreamBMPID { get; set; }
    }
}
