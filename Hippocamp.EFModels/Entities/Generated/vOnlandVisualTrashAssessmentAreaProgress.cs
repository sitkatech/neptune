using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vOnlandVisualTrashAssessmentAreaProgress
    {
        public int? PrimaryKey { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        [StringLength(1)]
        public string OnlandVisualTrashAssessmentScoreDisplayName { get; set; }
        public int OnlandVisualTrashAssessmentScoreID { get; set; }
    }
}
