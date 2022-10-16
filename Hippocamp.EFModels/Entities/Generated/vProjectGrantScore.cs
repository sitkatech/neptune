using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vProjectGrantScore
    {
        public int PrimaryKey { get; set; }
        public int ProjectID { get; set; }
        public double? ProjectArea { get; set; }
        public double? ImperviousAreaTreatedAcres { get; set; }
        [StringLength(8000)]
        [Unicode(false)]
        public string Watersheds { get; set; }
        public double? PollutantVolume { get; set; }
        public double? PollutantMetals { get; set; }
        public double? PollutantBacteria { get; set; }
        public double? PollutantNutrients { get; set; }
        public double? PollutantTSS { get; set; }
        public double? TPI { get; set; }
        public double? SEA { get; set; }
        public double? DryWeatherWeightedReductionVolume { get; set; }
        public double? DryWeatherWeightedReductionMetals { get; set; }
        public double? DryWeatherWeightedReductionBacteria { get; set; }
        public double? DryWeatherWeightedReductionNutrients { get; set; }
        public double? DryWeatherWeightedReductionTSS { get; set; }
        public double? DryWeatherWQLRI { get; set; }
        public double? WetWeatherWeightedReductionVolume { get; set; }
        public double? WetWeatherWeightedReductionMetals { get; set; }
        public double? WetWeatherWeightedReductionBacteria { get; set; }
        public double? WetWeatherWeightedReductionNutrients { get; set; }
        public double? WetWeatherWeightedReductionTSS { get; set; }
        public double? WetWeatherWQLRI { get; set; }
    }
}
