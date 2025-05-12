﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vPyQgisOnlandVisualTrashAssessmentAreaDated
{
    public int OVTAID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry OnlandVisualTrashAssessmentAreaGeometry { get; set; } = null!;

    public DateOnly? AssessDate { get; set; }
}
