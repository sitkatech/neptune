﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vPyQgisModelBasinLGUInput
{
    public int ModelID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry ModelBasinGeometry { get; set; } = null!;
}
