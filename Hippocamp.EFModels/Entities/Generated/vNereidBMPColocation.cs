using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vNereidBMPColocation
    {
        public int PrimaryKey { get; set; }
        public int DownstreamBMPID { get; set; }
        public int? DownstreamRSBID { get; set; }
        public int UpstreamBMPID { get; set; }
        public int? UpstreamRSBID { get; set; }
    }
}
