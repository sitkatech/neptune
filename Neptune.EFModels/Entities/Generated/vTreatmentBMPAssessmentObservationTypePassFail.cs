using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vTreatmentBMPAssessmentObservationTypePassFail
{
    public int TreatmentBMPAssessmentObservationTypeID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TreatmentBMPAssessmentObservationTypeName { get; set; }

    [Unicode(false)]
    public string? TreatmentBMPAssessmentObservationTypeSchema { get; set; }

    public int ObservationTypeSpecificationID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ObservationTypeSpecificationName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ObservationTypeSpecificationDisplayName { get; set; }

    public int ObservationTypeCollectionMethodID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ObservationTypeCollectionMethodName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ObservationTypeCollectionMethodDisplayName { get; set; }

    [Unicode(false)]
    public string? ObservationTypeCollectionMethodDescription { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? AssessmentDescription { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? PassingScoreLabel { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? FailingScoreLabel { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? PropertiesToObserve { get; set; }
}
