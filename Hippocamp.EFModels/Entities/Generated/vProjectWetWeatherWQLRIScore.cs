using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vProjectWetWeatherWQLRIScore
    {
        public int PrimaryKey { get; set; }
        public int ProjectID { get; set; }
        public double? PercentReducedVolume { get; set; }
        public double? PercentReducedTSS { get; set; }
        public double? PercentReducedBacteria { get; set; }
        public double? PercentReducedTN { get; set; }
        public double? PercentReducedTP { get; set; }
        public double? PercentReducedNutrients { get; set; }
        public double? PercentReducedTPb { get; set; }
        public double? PercentReducedTCu { get; set; }
        public double? PercentReducedTZn { get; set; }
        public double? PercentReducedMetals { get; set; }
    }
}
