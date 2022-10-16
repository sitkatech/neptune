//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vProjectGrantScore]
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class vProjectGrantScore
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vProjectGrantScore()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vProjectGrantScore(int primaryKey, int projectID, double? projectArea, double? totalProjectArea, double? imperviousAreaTreatedAcres, string watersheds, double? pollutantVolume, double? pollutantMetals, double? pollutantBacteria, double? pollutantNutrients, double? pollutantTSS, double? tPI, double? sEA, double? dryWeatherWeightedReductionVolume, double? dryWeatherWeightedReductionMetals, double? dryWeatherWeightedReductionBacteria, double? dryWeatherWeightedReductionNutrients, double? dryWeatherWeightedReductionTSS, double? dryWeatherWQLRI, double? wetWeatherWeightedReductionVolume, double? wetWeatherWeightedReductionMetals, double? wetWeatherWeightedReductionBacteria, double? wetWeatherWeightedReductionNutrients, double? wetWeatherWeightedReductionTSS, double? wetWeatherWQLRI) : this()
        {
            this.PrimaryKey = primaryKey;
            this.ProjectID = projectID;
            this.ProjectArea = projectArea;
            this.TotalProjectArea = totalProjectArea;
            this.ImperviousAreaTreatedAcres = imperviousAreaTreatedAcres;
            this.Watersheds = watersheds;
            this.PollutantVolume = pollutantVolume;
            this.PollutantMetals = pollutantMetals;
            this.PollutantBacteria = pollutantBacteria;
            this.PollutantNutrients = pollutantNutrients;
            this.PollutantTSS = pollutantTSS;
            this.TPI = tPI;
            this.SEA = sEA;
            this.DryWeatherWeightedReductionVolume = dryWeatherWeightedReductionVolume;
            this.DryWeatherWeightedReductionMetals = dryWeatherWeightedReductionMetals;
            this.DryWeatherWeightedReductionBacteria = dryWeatherWeightedReductionBacteria;
            this.DryWeatherWeightedReductionNutrients = dryWeatherWeightedReductionNutrients;
            this.DryWeatherWeightedReductionTSS = dryWeatherWeightedReductionTSS;
            this.DryWeatherWQLRI = dryWeatherWQLRI;
            this.WetWeatherWeightedReductionVolume = wetWeatherWeightedReductionVolume;
            this.WetWeatherWeightedReductionMetals = wetWeatherWeightedReductionMetals;
            this.WetWeatherWeightedReductionBacteria = wetWeatherWeightedReductionBacteria;
            this.WetWeatherWeightedReductionNutrients = wetWeatherWeightedReductionNutrients;
            this.WetWeatherWeightedReductionTSS = wetWeatherWeightedReductionTSS;
            this.WetWeatherWQLRI = wetWeatherWQLRI;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vProjectGrantScore(vProjectGrantScore vProjectGrantScore) : this()
        {
            this.PrimaryKey = vProjectGrantScore.PrimaryKey;
            this.ProjectID = vProjectGrantScore.ProjectID;
            this.ProjectArea = vProjectGrantScore.ProjectArea;
            this.TotalProjectArea = vProjectGrantScore.TotalProjectArea;
            this.ImperviousAreaTreatedAcres = vProjectGrantScore.ImperviousAreaTreatedAcres;
            this.Watersheds = vProjectGrantScore.Watersheds;
            this.PollutantVolume = vProjectGrantScore.PollutantVolume;
            this.PollutantMetals = vProjectGrantScore.PollutantMetals;
            this.PollutantBacteria = vProjectGrantScore.PollutantBacteria;
            this.PollutantNutrients = vProjectGrantScore.PollutantNutrients;
            this.PollutantTSS = vProjectGrantScore.PollutantTSS;
            this.TPI = vProjectGrantScore.TPI;
            this.SEA = vProjectGrantScore.SEA;
            this.DryWeatherWeightedReductionVolume = vProjectGrantScore.DryWeatherWeightedReductionVolume;
            this.DryWeatherWeightedReductionMetals = vProjectGrantScore.DryWeatherWeightedReductionMetals;
            this.DryWeatherWeightedReductionBacteria = vProjectGrantScore.DryWeatherWeightedReductionBacteria;
            this.DryWeatherWeightedReductionNutrients = vProjectGrantScore.DryWeatherWeightedReductionNutrients;
            this.DryWeatherWeightedReductionTSS = vProjectGrantScore.DryWeatherWeightedReductionTSS;
            this.DryWeatherWQLRI = vProjectGrantScore.DryWeatherWQLRI;
            this.WetWeatherWeightedReductionVolume = vProjectGrantScore.WetWeatherWeightedReductionVolume;
            this.WetWeatherWeightedReductionMetals = vProjectGrantScore.WetWeatherWeightedReductionMetals;
            this.WetWeatherWeightedReductionBacteria = vProjectGrantScore.WetWeatherWeightedReductionBacteria;
            this.WetWeatherWeightedReductionNutrients = vProjectGrantScore.WetWeatherWeightedReductionNutrients;
            this.WetWeatherWeightedReductionTSS = vProjectGrantScore.WetWeatherWeightedReductionTSS;
            this.WetWeatherWQLRI = vProjectGrantScore.WetWeatherWQLRI;
            CallAfterConstructor(vProjectGrantScore);
        }

        partial void CallAfterConstructor(vProjectGrantScore vProjectGrantScore);

        public int PrimaryKey { get; set; }
        public int ProjectID { get; set; }
        public double? ProjectArea { get; set; }
        public double? TotalProjectArea { get; set; }
        public double? ImperviousAreaTreatedAcres { get; set; }
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