using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vTreatmentBMPObservation
{
    public int TreatmentBMPObservationID { get; set; }

    public int TreatmentBMPAssessmentID { get; set; }

    public int TreatmentBMPAssessmentObservationTypeID { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? ObservationValue { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? Notes { get; set; }
}
