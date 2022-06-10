//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vProjectDryWeatherWQLRIScore]
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
    public partial class vProjectDryWeatherWQLRIScore
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vProjectDryWeatherWQLRIScore()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vProjectDryWeatherWQLRIScore(int primaryKey, int projectID, double? percentReducedVolume, double? percentReducedTSS, double? percentReducedBacteria, double? percentReducedTN, double? percentReducedTP, double? percentReducedNutrients, double? percentReducedTPb, double? percentReducedTCu, double? percentReducedTZn, double? percentReducedMetals) : this()
        {
            this.PrimaryKey = primaryKey;
            this.ProjectID = projectID;
            this.PercentReducedVolume = percentReducedVolume;
            this.PercentReducedTSS = percentReducedTSS;
            this.PercentReducedBacteria = percentReducedBacteria;
            this.PercentReducedTN = percentReducedTN;
            this.PercentReducedTP = percentReducedTP;
            this.PercentReducedNutrients = percentReducedNutrients;
            this.PercentReducedTPb = percentReducedTPb;
            this.PercentReducedTCu = percentReducedTCu;
            this.PercentReducedTZn = percentReducedTZn;
            this.PercentReducedMetals = percentReducedMetals;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vProjectDryWeatherWQLRIScore(vProjectDryWeatherWQLRIScore vProjectDryWeatherWQLRIScore) : this()
        {
            this.PrimaryKey = vProjectDryWeatherWQLRIScore.PrimaryKey;
            this.ProjectID = vProjectDryWeatherWQLRIScore.ProjectID;
            this.PercentReducedVolume = vProjectDryWeatherWQLRIScore.PercentReducedVolume;
            this.PercentReducedTSS = vProjectDryWeatherWQLRIScore.PercentReducedTSS;
            this.PercentReducedBacteria = vProjectDryWeatherWQLRIScore.PercentReducedBacteria;
            this.PercentReducedTN = vProjectDryWeatherWQLRIScore.PercentReducedTN;
            this.PercentReducedTP = vProjectDryWeatherWQLRIScore.PercentReducedTP;
            this.PercentReducedNutrients = vProjectDryWeatherWQLRIScore.PercentReducedNutrients;
            this.PercentReducedTPb = vProjectDryWeatherWQLRIScore.PercentReducedTPb;
            this.PercentReducedTCu = vProjectDryWeatherWQLRIScore.PercentReducedTCu;
            this.PercentReducedTZn = vProjectDryWeatherWQLRIScore.PercentReducedTZn;
            this.PercentReducedMetals = vProjectDryWeatherWQLRIScore.PercentReducedMetals;
            CallAfterConstructor(vProjectDryWeatherWQLRIScore);
        }

        partial void CallAfterConstructor(vProjectDryWeatherWQLRIScore vProjectDryWeatherWQLRIScore);

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