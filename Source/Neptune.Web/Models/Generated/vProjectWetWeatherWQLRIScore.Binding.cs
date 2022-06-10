//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vProjectWetWeatherWQLRIScore]
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
    public partial class vProjectWetWeatherWQLRIScore
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vProjectWetWeatherWQLRIScore()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vProjectWetWeatherWQLRIScore(int primaryKey, int projectID, double? percentReducedVolume, double? percentReducedTSS, double? percentReducedBacteria, double? percentReducedTN, double? percentReducedTP, double? percentReducedNutrients, double? percentReducedTPb, double? percentReducedTCu, double? percentReducedTZn, double? percentReducedMetals) : this()
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
        public vProjectWetWeatherWQLRIScore(vProjectWetWeatherWQLRIScore vProjectWetWeatherWQLRIScore) : this()
        {
            this.PrimaryKey = vProjectWetWeatherWQLRIScore.PrimaryKey;
            this.ProjectID = vProjectWetWeatherWQLRIScore.ProjectID;
            this.PercentReducedVolume = vProjectWetWeatherWQLRIScore.PercentReducedVolume;
            this.PercentReducedTSS = vProjectWetWeatherWQLRIScore.PercentReducedTSS;
            this.PercentReducedBacteria = vProjectWetWeatherWQLRIScore.PercentReducedBacteria;
            this.PercentReducedTN = vProjectWetWeatherWQLRIScore.PercentReducedTN;
            this.PercentReducedTP = vProjectWetWeatherWQLRIScore.PercentReducedTP;
            this.PercentReducedNutrients = vProjectWetWeatherWQLRIScore.PercentReducedNutrients;
            this.PercentReducedTPb = vProjectWetWeatherWQLRIScore.PercentReducedTPb;
            this.PercentReducedTCu = vProjectWetWeatherWQLRIScore.PercentReducedTCu;
            this.PercentReducedTZn = vProjectWetWeatherWQLRIScore.PercentReducedTZn;
            this.PercentReducedMetals = vProjectWetWeatherWQLRIScore.PercentReducedMetals;
            CallAfterConstructor(vProjectWetWeatherWQLRIScore);
        }

        partial void CallAfterConstructor(vProjectWetWeatherWQLRIScore vProjectWetWeatherWQLRIScore);

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