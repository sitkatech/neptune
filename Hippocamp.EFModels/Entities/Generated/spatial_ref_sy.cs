﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class spatial_ref_sy
    {
        [Key]
        public int srid { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string auth_name { get; set; }
        public int? auth_srid { get; set; }
        [StringLength(2048)]
        [Unicode(false)]
        public string srtext { get; set; }
        [StringLength(2048)]
        [Unicode(false)]
        public string proj4text { get; set; }
    }
}
