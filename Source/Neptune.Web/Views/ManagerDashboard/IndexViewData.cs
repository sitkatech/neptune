using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Assessment;
using Neptune.Web.Views.MaintenanceRecord;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.ManagerDashboard
{
    public class IndexViewData : NeptuneViewData
    {
        public ProvisionalFieldVisitGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public ProvisionalTreatmentBMPGridSpec ProvisionalTreatmentBMPGridSpec { get; }
        public string ProvisionalTreatmentBMPGridName { get; }
        public string ProvisionalTreatmentBMPGridDataUrl { get; }

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage, IQueryable<Models.TreatmentBMPAssessmentObservationType> allObservationTypes)
            : base(currentPerson, StormwaterBreadCrumbEntity.FieldRecords, neptunePage)
        {
            PageTitle = "Manager Dashboard";
            EntityName = "Stormwater Tools";
            GridSpec = new ProvisionalFieldVisitGridSpec(currentPerson, false)
            {
                ObjectNameSingular = "Field Visit",
                ObjectNamePlural = "Field Visits",
                SaveFiltersInCookie = true,
                GridInstructionsWhenEmpty = "All Assessment and Maintenance Records Added during a Field Visit have been reviewed and verified"
            };
            GridName = "ProvisionalFieldVisitGrid";
            GridDataUrl = SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(j => j.AllFieldVisitsGridJsonData());

            ProvisionalTreatmentBMPGridSpec = new ProvisionalTreatmentBMPGridSpec(currentPerson, allObservationTypes)
            {
                ObjectNameSingular = "Assessment",
                ObjectNamePlural = "Assessments",
                SaveFiltersInCookie = true,
                GridInstructionsWhenEmpty = "All provisional BMP Records have been reviewed and verified"
            };
            ProvisionalTreatmentBMPGridName = "assessmentsGrid";
            ProvisionalTreatmentBMPGridDataUrl =
                SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(x => x.ProvisionalTreatmentBMPGridJsonData());
        }
    }
}