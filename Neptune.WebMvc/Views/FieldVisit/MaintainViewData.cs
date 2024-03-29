﻿using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class MaintainViewData : FieldVisitSectionViewData
    {
        public bool IsNew { get; }
        public string PostMaintenanceAssessmentUrl { get; }
        public string EditMaintenanceRecordUrl { get; }

        public MaintainViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.FieldVisit fieldVisit,
            List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments,
            EFModels.Entities.MaintenanceRecord? maintenanceRecord) : base(httpContext, linkGenerator, webConfiguration, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.Maintenance, fieldVisit.TreatmentBMP.TreatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
            PostMaintenanceAssessmentUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.PostMaintenanceAssessment(fieldVisit));
            EditMaintenanceRecordUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.EditMaintenanceRecord(fieldVisit));
            IsNew = maintenanceRecord == null;
        }
    }
}
