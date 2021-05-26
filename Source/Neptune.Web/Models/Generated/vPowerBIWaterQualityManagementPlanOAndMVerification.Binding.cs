//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vPowerBIWaterQualityManagementPlanOAndMVerification]
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
    public partial class vPowerBIWaterQualityManagementPlanOAndMVerification
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vPowerBIWaterQualityManagementPlanOAndMVerification()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vPowerBIWaterQualityManagementPlanOAndMVerification(int primaryKey, string wQMPName, string jurisdiction, DateTime verificationDate, DateTime lastEditedDate, string lastEditedBy, string typeOfVerification, string visitStatus, string verificationStatus, string sourceControlCondition, string enforcementOrFollowupActions, string draftOrFinalized) : this()
        {
            this.PrimaryKey = primaryKey;
            this.WQMPName = wQMPName;
            this.Jurisdiction = jurisdiction;
            this.VerificationDate = verificationDate;
            this.LastEditedDate = lastEditedDate;
            this.LastEditedBy = lastEditedBy;
            this.TypeOfVerification = typeOfVerification;
            this.VisitStatus = visitStatus;
            this.VerificationStatus = verificationStatus;
            this.SourceControlCondition = sourceControlCondition;
            this.EnforcementOrFollowupActions = enforcementOrFollowupActions;
            this.DraftOrFinalized = draftOrFinalized;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vPowerBIWaterQualityManagementPlanOAndMVerification(vPowerBIWaterQualityManagementPlanOAndMVerification vPowerBIWaterQualityManagementPlanOAndMVerification) : this()
        {
            this.PrimaryKey = vPowerBIWaterQualityManagementPlanOAndMVerification.PrimaryKey;
            this.WQMPName = vPowerBIWaterQualityManagementPlanOAndMVerification.WQMPName;
            this.Jurisdiction = vPowerBIWaterQualityManagementPlanOAndMVerification.Jurisdiction;
            this.VerificationDate = vPowerBIWaterQualityManagementPlanOAndMVerification.VerificationDate;
            this.LastEditedDate = vPowerBIWaterQualityManagementPlanOAndMVerification.LastEditedDate;
            this.LastEditedBy = vPowerBIWaterQualityManagementPlanOAndMVerification.LastEditedBy;
            this.TypeOfVerification = vPowerBIWaterQualityManagementPlanOAndMVerification.TypeOfVerification;
            this.VisitStatus = vPowerBIWaterQualityManagementPlanOAndMVerification.VisitStatus;
            this.VerificationStatus = vPowerBIWaterQualityManagementPlanOAndMVerification.VerificationStatus;
            this.SourceControlCondition = vPowerBIWaterQualityManagementPlanOAndMVerification.SourceControlCondition;
            this.EnforcementOrFollowupActions = vPowerBIWaterQualityManagementPlanOAndMVerification.EnforcementOrFollowupActions;
            this.DraftOrFinalized = vPowerBIWaterQualityManagementPlanOAndMVerification.DraftOrFinalized;
            CallAfterConstructor(vPowerBIWaterQualityManagementPlanOAndMVerification);
        }

        partial void CallAfterConstructor(vPowerBIWaterQualityManagementPlanOAndMVerification vPowerBIWaterQualityManagementPlanOAndMVerification);

        public int PrimaryKey { get; set; }
        public string WQMPName { get; set; }
        public string Jurisdiction { get; set; }
        public DateTime VerificationDate { get; set; }
        public DateTime LastEditedDate { get; set; }
        public string LastEditedBy { get; set; }
        public string TypeOfVerification { get; set; }
        public string VisitStatus { get; set; }
        public string VerificationStatus { get; set; }
        public string SourceControlCondition { get; set; }
        public string EnforcementOrFollowupActions { get; set; }
        public string DraftOrFinalized { get; set; }
    }
}