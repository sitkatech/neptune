//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vTreatmentBMPDetailed]
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
    public partial class vTreatmentBMPDetailed
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vTreatmentBMPDetailed()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vTreatmentBMPDetailed(int primaryKey, int treatmentBMPID, string treatmentBMPName, int treatmentBMPTypeID, string treatmentBMPTypeName, int stormwaterJurisdictionID, string organizationName, int? requiredFieldVisitsPerYear, int? requiredPostStormFieldVisitsPerYear, DateTime? treatmentBMPLifespanEndDate, int? yearBuilt, string notes, int ownerOrganizationID, string ownerOrganizationName, int? treatmentBMPLifespanTypeID, string treatmentBMPLifespanTypeDisplayName, int trashCaptureStatusTypeID, string trashCaptureStatusTypeDisplayName, int sizingBasisTypeID, string sizingBasisTypeDisplayName, int? delineationTypeID, string delineationTypeDisplayName, long numberOfAssessments, int? latestTreatmentBMPAssessmentID, DateTime? latestAssessmentDate, double? latestAssessmentScore, long numberOfMaintenanceRecords, int? latestMaintenanceRecordID, DateTime? latestMaintenanceDate, int? numberOfBenchmarkAndThresholds, int numberOfBenchmarkAndThresholdsEntered) : this()
        {
            this.PrimaryKey = primaryKey;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPName = treatmentBMPName;
            this.TreatmentBMPTypeID = treatmentBMPTypeID;
            this.TreatmentBMPTypeName = treatmentBMPTypeName;
            this.StormwaterJurisdictionID = stormwaterJurisdictionID;
            this.OrganizationName = organizationName;
            this.RequiredFieldVisitsPerYear = requiredFieldVisitsPerYear;
            this.RequiredPostStormFieldVisitsPerYear = requiredPostStormFieldVisitsPerYear;
            this.TreatmentBMPLifespanEndDate = treatmentBMPLifespanEndDate;
            this.YearBuilt = yearBuilt;
            this.Notes = notes;
            this.OwnerOrganizationID = ownerOrganizationID;
            this.OwnerOrganizationName = ownerOrganizationName;
            this.TreatmentBMPLifespanTypeID = treatmentBMPLifespanTypeID;
            this.TreatmentBMPLifespanTypeDisplayName = treatmentBMPLifespanTypeDisplayName;
            this.TrashCaptureStatusTypeID = trashCaptureStatusTypeID;
            this.TrashCaptureStatusTypeDisplayName = trashCaptureStatusTypeDisplayName;
            this.SizingBasisTypeID = sizingBasisTypeID;
            this.SizingBasisTypeDisplayName = sizingBasisTypeDisplayName;
            this.DelineationTypeID = delineationTypeID;
            this.DelineationTypeDisplayName = delineationTypeDisplayName;
            this.NumberOfAssessments = numberOfAssessments;
            this.LatestTreatmentBMPAssessmentID = latestTreatmentBMPAssessmentID;
            this.LatestAssessmentDate = latestAssessmentDate;
            this.LatestAssessmentScore = latestAssessmentScore;
            this.NumberOfMaintenanceRecords = numberOfMaintenanceRecords;
            this.LatestMaintenanceRecordID = latestMaintenanceRecordID;
            this.LatestMaintenanceDate = latestMaintenanceDate;
            this.NumberOfBenchmarkAndThresholds = numberOfBenchmarkAndThresholds;
            this.NumberOfBenchmarkAndThresholdsEntered = numberOfBenchmarkAndThresholdsEntered;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vTreatmentBMPDetailed(vTreatmentBMPDetailed vTreatmentBMPDetailed) : this()
        {
            this.PrimaryKey = vTreatmentBMPDetailed.PrimaryKey;
            this.TreatmentBMPID = vTreatmentBMPDetailed.TreatmentBMPID;
            this.TreatmentBMPName = vTreatmentBMPDetailed.TreatmentBMPName;
            this.TreatmentBMPTypeID = vTreatmentBMPDetailed.TreatmentBMPTypeID;
            this.TreatmentBMPTypeName = vTreatmentBMPDetailed.TreatmentBMPTypeName;
            this.StormwaterJurisdictionID = vTreatmentBMPDetailed.StormwaterJurisdictionID;
            this.OrganizationName = vTreatmentBMPDetailed.OrganizationName;
            this.RequiredFieldVisitsPerYear = vTreatmentBMPDetailed.RequiredFieldVisitsPerYear;
            this.RequiredPostStormFieldVisitsPerYear = vTreatmentBMPDetailed.RequiredPostStormFieldVisitsPerYear;
            this.TreatmentBMPLifespanEndDate = vTreatmentBMPDetailed.TreatmentBMPLifespanEndDate;
            this.YearBuilt = vTreatmentBMPDetailed.YearBuilt;
            this.Notes = vTreatmentBMPDetailed.Notes;
            this.OwnerOrganizationID = vTreatmentBMPDetailed.OwnerOrganizationID;
            this.OwnerOrganizationName = vTreatmentBMPDetailed.OwnerOrganizationName;
            this.TreatmentBMPLifespanTypeID = vTreatmentBMPDetailed.TreatmentBMPLifespanTypeID;
            this.TreatmentBMPLifespanTypeDisplayName = vTreatmentBMPDetailed.TreatmentBMPLifespanTypeDisplayName;
            this.TrashCaptureStatusTypeID = vTreatmentBMPDetailed.TrashCaptureStatusTypeID;
            this.TrashCaptureStatusTypeDisplayName = vTreatmentBMPDetailed.TrashCaptureStatusTypeDisplayName;
            this.SizingBasisTypeID = vTreatmentBMPDetailed.SizingBasisTypeID;
            this.SizingBasisTypeDisplayName = vTreatmentBMPDetailed.SizingBasisTypeDisplayName;
            this.DelineationTypeID = vTreatmentBMPDetailed.DelineationTypeID;
            this.DelineationTypeDisplayName = vTreatmentBMPDetailed.DelineationTypeDisplayName;
            this.NumberOfAssessments = vTreatmentBMPDetailed.NumberOfAssessments;
            this.LatestTreatmentBMPAssessmentID = vTreatmentBMPDetailed.LatestTreatmentBMPAssessmentID;
            this.LatestAssessmentDate = vTreatmentBMPDetailed.LatestAssessmentDate;
            this.LatestAssessmentScore = vTreatmentBMPDetailed.LatestAssessmentScore;
            this.NumberOfMaintenanceRecords = vTreatmentBMPDetailed.NumberOfMaintenanceRecords;
            this.LatestMaintenanceRecordID = vTreatmentBMPDetailed.LatestMaintenanceRecordID;
            this.LatestMaintenanceDate = vTreatmentBMPDetailed.LatestMaintenanceDate;
            this.NumberOfBenchmarkAndThresholds = vTreatmentBMPDetailed.NumberOfBenchmarkAndThresholds;
            this.NumberOfBenchmarkAndThresholdsEntered = vTreatmentBMPDetailed.NumberOfBenchmarkAndThresholdsEntered;
            CallAfterConstructor(vTreatmentBMPDetailed);
        }

        partial void CallAfterConstructor(vTreatmentBMPDetailed vTreatmentBMPDetailed);

        public int PrimaryKey { get; set; }
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public string OrganizationName { get; set; }
        public int? RequiredFieldVisitsPerYear { get; set; }
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }
        public int? YearBuilt { get; set; }
        public string Notes { get; set; }
        public int OwnerOrganizationID { get; set; }
        public string OwnerOrganizationName { get; set; }
        public int? TreatmentBMPLifespanTypeID { get; set; }
        public string TreatmentBMPLifespanTypeDisplayName { get; set; }
        public int TrashCaptureStatusTypeID { get; set; }
        public string TrashCaptureStatusTypeDisplayName { get; set; }
        public int SizingBasisTypeID { get; set; }
        public string SizingBasisTypeDisplayName { get; set; }
        public int? DelineationTypeID { get; set; }
        public string DelineationTypeDisplayName { get; set; }
        public long NumberOfAssessments { get; set; }
        public int? LatestTreatmentBMPAssessmentID { get; set; }
        public DateTime? LatestAssessmentDate { get; set; }
        public double? LatestAssessmentScore { get; set; }
        public long NumberOfMaintenanceRecords { get; set; }
        public int? LatestMaintenanceRecordID { get; set; }
        public DateTime? LatestMaintenanceDate { get; set; }
        public int? NumberOfBenchmarkAndThresholds { get; set; }
        public int NumberOfBenchmarkAndThresholdsEntered { get; set; }
    }
}