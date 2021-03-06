﻿using System.Collections.Generic;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.FieldVisit
{
    public class FieldVisitSectionViewData : NeptuneViewData
    {
        public Models.FieldVisit FieldVisit { get; }
        public string SectionName { get; }
        public string SubsectionName { get; set; }
        public bool CanManageStormwaterJurisdiction { get; }
        public string VerifiedUnverifiedFieldVisitUrl { get; }
        public string SectionHeader { get; set; }
        public List<string> ValidationWarnings { get; set; }

        public string WrapupUrl { get; }
        public string EditDateAndTypeUrl { get; }
        public bool UserCanDeleteMaintenanceRecord { get; }
        public Models.MaintenanceRecord MaintenanceRecord { get; }
        public Models.TreatmentBMPAssessment InitialAssessment { get; }
        public Models.TreatmentBMPAssessment PostMaintenanceAssessment { get; }


        public FieldVisitSectionViewData(Person currentPerson, Models.FieldVisit fieldVisit, Models.FieldVisitSection fieldVisitSection)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            FieldVisit = fieldVisit;
            SectionName = fieldVisitSection.FieldVisitSectionName;

            EntityName = "Treatment BMP Field Visits";
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = fieldVisit.TreatmentBMP.TreatmentBMPName ?? "Preview Treatment BMP Field Visit";
            SubEntityUrl = fieldVisit.TreatmentBMP?.GetDetailUrl() ?? "#";
            PageTitle = fieldVisit.VisitDate.ToStringDate();

            EditDateAndTypeUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.EditDateAndType(fieldVisit.PrimaryKey));

            CanManageStormwaterJurisdiction = currentPerson.CanManageStormwaterJurisdiction(fieldVisit.TreatmentBMP.StormwaterJurisdictionID);
            VerifiedUnverifiedFieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.VerifyFieldVisit(FieldVisit.PrimaryKey));

            SectionHeader = fieldVisitSection.SectionHeader;
            ValidationWarnings = new List<string>();

            WrapupUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.VisitSummary(fieldVisit));


            MaintenanceRecord = fieldVisit.MaintenanceRecord;
            InitialAssessment = fieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.Initial);
            PostMaintenanceAssessment = fieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.PostMaintenance);
            UserCanDeleteMaintenanceRecord = MaintenanceRecord != null &&
                                             new MaintenanceRecordManageFeature()
                                                 .HasPermission(CurrentPerson, MaintenanceRecord)
                                                 .HasPermission;
        }

        public bool UserCanDeleteAssessment(Models.TreatmentBMPAssessment assessment)
        {
            return assessment != null &&
                   new TreatmentBMPAssessmentManageFeature().HasPermission(CurrentPerson, assessment).HasPermission;
        }
    }
}
