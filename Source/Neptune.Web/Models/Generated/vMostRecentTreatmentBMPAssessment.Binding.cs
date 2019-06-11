//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vMostRecentTreatmentBMPAssessment]
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
    public partial class vMostRecentTreatmentBMPAssessment
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vMostRecentTreatmentBMPAssessment()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vMostRecentTreatmentBMPAssessment(int primaryKey, int treatmentBMPID, string treatmentBMPName, string stormwaterJurisdictionName, int stormwaterJurisdictionID, string ownerOrganizationName, int ownerOrganizationID, int? requiredFieldVisitsPerYear, int? numberOfAssessments, DateTime? lastAssessmentDate, int lastAssessmentID, double? assessmentScore, string fieldVisitType) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPName = treatmentBMPName;
            this.StormwaterJurisdictionName = stormwaterJurisdictionName;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OwnerOrganizationName = ownerOrganizationName;
            this.OwnerOrganizationID = ownerOrganizationID;
            this.RequiredFieldVisitsPerYear = requiredFieldVisitsPerYear;
            this.NumberOfAssessments = numberOfAssessments;
            this.LastAssessmentDate = lastAssessmentDate;
            this.LastAssessmentID = lastAssessmentID;
            this.AssessmentScore = assessmentScore;
            this.FieldVisitType = fieldVisitType;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vMostRecentTreatmentBMPAssessment(vMostRecentTreatmentBMPAssessment vMostRecentTreatmentBMPAssessment) : this()
        {
            this.PrimaryKey = vMostRecentTreatmentBMPAssessment.PrimaryKey;
            this.TreatmentBMPID = vMostRecentTreatmentBMPAssessment.TreatmentBMPID;
            this.TreatmentBMPName = vMostRecentTreatmentBMPAssessment.TreatmentBMPName;
            this.StormwaterJurisdictionName = vMostRecentTreatmentBMPAssessment.StormwaterJurisdictionName;
            this.StormwaterJurisdictionID = vMostRecentTreatmentBMPAssessment.StormwaterJurisdictionID;
            this.OwnerOrganizationName = vMostRecentTreatmentBMPAssessment.OwnerOrganizationName;
            this.OwnerOrganizationID = vMostRecentTreatmentBMPAssessment.OwnerOrganizationID;
            this.RequiredFieldVisitsPerYear = vMostRecentTreatmentBMPAssessment.RequiredFieldVisitsPerYear;
            this.NumberOfAssessments = vMostRecentTreatmentBMPAssessment.NumberOfAssessments;
            this.LastAssessmentDate = vMostRecentTreatmentBMPAssessment.LastAssessmentDate;
            this.LastAssessmentID = vMostRecentTreatmentBMPAssessment.LastAssessmentID;
            this.AssessmentScore = vMostRecentTreatmentBMPAssessment.AssessmentScore;
            this.FieldVisitType = vMostRecentTreatmentBMPAssessment.FieldVisitType;
            CallAfterConstructor(vMostRecentTreatmentBMPAssessment);
        }

        partial void CallAfterConstructor(vMostRecentTreatmentBMPAssessment vMostRecentTreatmentBMPAssessment);

        public int PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public string StormwaterJurisdictionName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public string OwnerOrganizationName { get; set; }
        public int OwnerOrganizationID { get; set; }
        public int? RequiredFieldVisitsPerYear { get; set; }
        public int? NumberOfAssessments { get; set; }
        public DateTime? LastAssessmentDate { get; set; }
        public int LastAssessmentID { get; set; }
        public double? AssessmentScore { get; set; }
        public string FieldVisitType { get; set; }
    }
}