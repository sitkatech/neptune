using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vProjectWQLRIScore
    {
        public int PrimaryKey { get; set; }
        public int ProjectID { get; set; }
        public double? PercentReducedVolume { get; set; }
        public double? WeightedReductionVolume { get; set; }
        public double? PercentReducedTSS { get; set; }
        public double? WeightedReductionTSS { get; set; }
        public double? PercentReducedFC { get; set; }
        public double? WeightedReductionBacteria { get; set; }
        public double? PercentReducedTN { get; set; }
        public double? PercentReducedTP { get; set; }
        public double? PercentReducedNutrients { get; set; }
        public double? WeightedReductionNutrients { get; set; }
        public double? PercentReducedTPb { get; set; }
        public double? PercentReducedTCu { get; set; }
        public double? PercentReducedTZn { get; set; }
        public double? PercentReducedMetals { get; set; }
        public double? WeightedReductionMetals { get; set; }
    }
}
