//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vProjectGrantScores]
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
    public partial class vProjectGrantScores
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vProjectGrantScores()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vProjectGrantScores(int primaryKey, int projectID, double? projectArea, string watersheds, double? pollutantVolume, double? pollutantMetals, double? pollutantBacteria, double? pollutantNutrients, double? pollutantTSS, double? tPI, double? sEA, double? dryWeatherWQLRI, double? wetWeatherWQLRI) : this()
        {
            this.PrimaryKey = primaryKey;
            this.ProjectID = projectID;
            this.ProjectArea = projectArea;
            this.Watersheds = watersheds;
            this.PollutantVolume = pollutantVolume;
            this.PollutantMetals = pollutantMetals;
            this.PollutantBacteria = pollutantBacteria;
            this.PollutantNutrients = pollutantNutrients;
            this.PollutantTSS = pollutantTSS;
            this.TPI = tPI;
            this.SEA = sEA;
            this.DryWeatherWQLRI = dryWeatherWQLRI;
            this.WetWeatherWQLRI = wetWeatherWQLRI;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vProjectGrantScores(vProjectGrantScores vProjectGrantScores) : this()
        {
            this.PrimaryKey = vProjectGrantScores.PrimaryKey;
            this.ProjectID = vProjectGrantScores.ProjectID;
            this.ProjectArea = vProjectGrantScores.ProjectArea;
            this.Watersheds = vProjectGrantScores.Watersheds;
            this.PollutantVolume = vProjectGrantScores.PollutantVolume;
            this.PollutantMetals = vProjectGrantScores.PollutantMetals;
            this.PollutantBacteria = vProjectGrantScores.PollutantBacteria;
            this.PollutantNutrients = vProjectGrantScores.PollutantNutrients;
            this.PollutantTSS = vProjectGrantScores.PollutantTSS;
            this.TPI = vProjectGrantScores.TPI;
            this.SEA = vProjectGrantScores.SEA;
            this.DryWeatherWQLRI = vProjectGrantScores.DryWeatherWQLRI;
            this.WetWeatherWQLRI = vProjectGrantScores.WetWeatherWQLRI;
            CallAfterConstructor(vProjectGrantScores);
        }

        partial void CallAfterConstructor(vProjectGrantScores vProjectGrantScores);

        public int PrimaryKey { get; set; }
        public int ProjectID { get; set; }
        public double? ProjectArea { get; set; }
        public string Watersheds { get; set; }
        public double? PollutantVolume { get; set; }
        public double? PollutantMetals { get; set; }
        public double? PollutantBacteria { get; set; }
        public double? PollutantNutrients { get; set; }
        public double? PollutantTSS { get; set; }
        public double? TPI { get; set; }
        public double? SEA { get; set; }
        public double? DryWeatherWQLRI { get; set; }
        public double? WetWeatherWQLRI { get; set; }
    }
}