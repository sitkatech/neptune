//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerify]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanVerifyDto
    {
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
        public WaterQualityManagementPlanVerifyTypeDto WaterQualityManagementPlanVerifyType { get; set; }
        public WaterQualityManagementPlanVisitStatusDto WaterQualityManagementPlanVisitStatus { get; set; }
        public FileResourceDto FileResource { get; set; }
        public WaterQualityManagementPlanVerifyStatusDto WaterQualityManagementPlanVerifyStatus { get; set; }
        public PersonDto LastEditedByPerson { get; set; }
        public string SourceControlCondition { get; set; }
        public string EnforcementOrFollowupActions { get; set; }
        public DateTime LastEditedDate { get; set; }
        public bool IsDraft { get; set; }
        public DateTime VerificationDate { get; set; }
    }

    public partial class WaterQualityManagementPlanVerifySimpleDto
    {
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int WaterQualityManagementPlanVerifyTypeID { get; set; }
        public int WaterQualityManagementPlanVisitStatusID { get; set; }
        public int? FileResourceID { get; set; }
        public int? WaterQualityManagementPlanVerifyStatusID { get; set; }
        public int LastEditedByPersonID { get; set; }
        public string SourceControlCondition { get; set; }
        public string EnforcementOrFollowupActions { get; set; }
        public DateTime LastEditedDate { get; set; }
        public bool IsDraft { get; set; }
        public DateTime VerificationDate { get; set; }
    }

}