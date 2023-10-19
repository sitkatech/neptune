﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vPyQgisDelineationTGUInput
{
    public int DelinID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry DelineationGeometry { get; set; } = null!;

    public int SJID { get; set; }

    public int TCEffect { get; set; }
}
