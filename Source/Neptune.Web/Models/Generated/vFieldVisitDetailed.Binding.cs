//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vFieldVisitDetailed]
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
    public partial class vFieldVisitDetailed
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vFieldVisitDetailed()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vFieldVisitDetailed(int primaryKey, int treatmentBMPID, string treatmentBMPName, int treatmentBMPTypeID, string treatmentBMPTypeName, int stormwaterJurisdictionID, string organizationName, int fieldVisitID, DateTime visitDate, int fieldVisitTypeID, string fieldVisitTypeDisplayName, int performedByPersonID, string performedByPersonName, int fieldVisitStatusID, string fieldVisitStatusDisplayName, bool isFieldVisitVerified, bool inventoryUpdated, int numberOfRequiredAttributes, int numberRequiredAttributesEntered, int? treatmentBMPAssessmentIDInitial, bool isAssessmentCompleteInitial, double? assessmentScoreInitial, int? treatmentBMPAssessmentIDPM, bool isAssessmentCompletePM, double? assessmentScorePM, int? maintenanceRecordID) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPTypeName = treatmentBMPTypeName;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OrganizationName = organizationName;
            this.FieldVisitID = fieldVisitID;
            this.VisitDate = visitDate;
            this.FieldVisitTypeID = fieldVisitTypeID;
            this.FieldVisitTypeDisplayName = fieldVisitTypeDisplayName;
            this.PerformedByPersonID = performedByPersonID;
            this.PerformedByPersonName = performedByPersonName;
            this.FieldVisitStatusID = fieldVisitStatusID;
            this.FieldVisitStatusDisplayName = fieldVisitStatusDisplayName;
            this.IsFieldVisitVerified = isFieldVisitVerified;
            this.InventoryUpdated = inventoryUpdated;
            this.NumberOfRequiredAttributes = numberOfRequiredAttributes;
            this.NumberRequiredAttributesEntered = numberRequiredAttributesEntered;
            this.TreatmentBMPAssessmentIDInitial = treatmentBMPAssessmentIDInitial;
            this.IsAssessmentCompleteInitial = isAssessmentCompleteInitial;
            this.AssessmentScoreInitial = assessmentScoreInitial;
            this.TreatmentBMPAssessmentIDPM = treatmentBMPAssessmentIDPM;
            this.IsAssessmentCompletePM = isAssessmentCompletePM;
            this.AssessmentScorePM = assessmentScorePM;
            this.MaintenanceRecordID = maintenanceRecordID;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vFieldVisitDetailed(vFieldVisitDetailed vFieldVisitDetailed) : this()
        {
            this.PrimaryKey = vFieldVisitDetailed.PrimaryKey;
            this.TreatmentBMPID = vFieldVisitDetailed.TreatmentBMPID;
            this.TreatmentBMPName = vFieldVisitDetailed.TreatmentBMPName;
            this.TreatmentBMPTypeID = vFieldVisitDetailed.TreatmentBMPTypeID;
            this.TreatmentBMPTypeName = vFieldVisitDetailed.TreatmentBMPTypeName;
            this.StormwaterJurisdictionID = vFieldVisitDetailed.StormwaterJurisdictionID;
            this.OrganizationName = vFieldVisitDetailed.OrganizationName;
            this.FieldVisitID = vFieldVisitDetailed.FieldVisitID;
            this.VisitDate = vFieldVisitDetailed.VisitDate;
            this.FieldVisitTypeID = vFieldVisitDetailed.FieldVisitTypeID;
            this.FieldVisitTypeDisplayName = vFieldVisitDetailed.FieldVisitTypeDisplayName;
            this.PerformedByPersonID = vFieldVisitDetailed.PerformedByPersonID;
            this.PerformedByPersonName = vFieldVisitDetailed.PerformedByPersonName;
            this.FieldVisitStatusID = vFieldVisitDetailed.FieldVisitStatusID;
            this.FieldVisitStatusDisplayName = vFieldVisitDetailed.FieldVisitStatusDisplayName;
            this.IsFieldVisitVerified = vFieldVisitDetailed.IsFieldVisitVerified;
            this.InventoryUpdated = vFieldVisitDetailed.InventoryUpdated;
            this.NumberOfRequiredAttributes = vFieldVisitDetailed.NumberOfRequiredAttributes;
            this.NumberRequiredAttributesEntered = vFieldVisitDetailed.NumberRequiredAttributesEntered;
            this.TreatmentBMPAssessmentIDInitial = vFieldVisitDetailed.TreatmentBMPAssessmentIDInitial;
            this.IsAssessmentCompleteInitial = vFieldVisitDetailed.IsAssessmentCompleteInitial;
            this.AssessmentScoreInitial = vFieldVisitDetailed.AssessmentScoreInitial;
            this.TreatmentBMPAssessmentIDPM = vFieldVisitDetailed.TreatmentBMPAssessmentIDPM;
            this.IsAssessmentCompletePM = vFieldVisitDetailed.IsAssessmentCompletePM;
            this.AssessmentScorePM = vFieldVisitDetailed.AssessmentScorePM;
            this.MaintenanceRecordID = vFieldVisitDetailed.MaintenanceRecordID;
            CallAfterConstructor(vFieldVisitDetailed);
        }

        partial void CallAfterConstructor(vFieldVisitDetailed vFieldVisitDetailed);

        public int PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public string OrganizationName { get; set; }
        public int FieldVisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public int FieldVisitTypeID { get; set; }
        public string FieldVisitTypeDisplayName { get; set; }
        public int PerformedByPersonID { get; set; }
        public string PerformedByPersonName { get; set; }
        public int FieldVisitStatusID { get; set; }
        public string FieldVisitStatusDisplayName { get; set; }
        public bool IsFieldVisitVerified { get; set; }
        public bool InventoryUpdated { get; set; }
        public int NumberOfRequiredAttributes { get; set; }
        public int NumberRequiredAttributesEntered { get; set; }
        public int? TreatmentBMPAssessmentIDInitial { get; set; }
        public bool IsAssessmentCompleteInitial { get; set; }
        public double? AssessmentScoreInitial { get; set; }
        public int? TreatmentBMPAssessmentIDPM { get; set; }
        public bool IsAssessmentCompletePM { get; set; }
        public double? AssessmentScorePM { get; set; }
        public int? MaintenanceRecordID { get; set; }
    }
}