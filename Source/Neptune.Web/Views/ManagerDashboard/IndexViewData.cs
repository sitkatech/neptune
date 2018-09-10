using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;


namespace Neptune.Web.Views.ManagerDashboard
{
    public class IndexViewData : NeptuneViewData
    {
        public ProvisionalFieldVisitGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public int FieldVisitCount { get; }
        public string FieldVisitsIndexUrl { get; }
        public string ProvisionalFieldVisitGridCheckAllUrl { get; }
        public string ProvisionalFieldVisitGridUncheckAllUrl { get; }
        public ProvisionalTreatmentBMPGridSpec ProvisionalTreatmentBMPGridSpec { get; }
        public string ProvisionalTreatmentBMPGridName { get; }
        public string ProvisionalTreatmentBMPGridDataUrl { get; }
        public string ProvisionalTreatmentBMPGridCheckAllUrl { get; }
        public string ProvisionalTreatmentBMPGridUncheckAllUrl { get; }
        public int TreatmentBMPsCount { get; }
        public string TreatmentBMPIndexUrl { get; }

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage, int fieldVisitCount, int treatmentBMPsCount)
            : base(currentPerson, StormwaterBreadCrumbEntity.FieldRecords, neptunePage)
        {
            PageTitle = "Manager Dashboard";
            EntityName = "Stormwater Tools";
            GridSpec = new ProvisionalFieldVisitGridSpec(currentPerson)
            {
                ObjectNameSingular = "Field Visit",
                ObjectNamePlural = "Field Visits",
                SaveFiltersInCookie = true
            };
            GridName = "ProvisionalFieldVisitGrid";
            GridDataUrl = SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(j => j.AllFieldVisitsGridJsonData());
            ProvisionalFieldVisitGridCheckAllUrl = $"Sitka.{GridName}.grid.checkAll()";
            ProvisionalFieldVisitGridUncheckAllUrl = $"Sitka.{GridName}.grid.uncheckAll()";
            FieldVisitCount = fieldVisitCount;
            FieldVisitsIndexUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c => c.Index());

            ProvisionalTreatmentBMPGridSpec = new ProvisionalTreatmentBMPGridSpec(currentPerson)
            {
                ObjectNameSingular = "Assessment",
                ObjectNamePlural = "Assessments",
                SaveFiltersInCookie = true
            };
            ProvisionalTreatmentBMPGridName = "assessmentsGrid";
            ProvisionalTreatmentBMPGridDataUrl =
                SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(x => x.ProvisionalTreatmentBMPGridJsonData());
            ProvisionalTreatmentBMPGridCheckAllUrl = $"Sitka.{ProvisionalTreatmentBMPGridName}.grid.checkAll()";
            ProvisionalTreatmentBMPGridUncheckAllUrl = $"Sitka.{ProvisionalTreatmentBMPGridName}.grid.uncheckAll()";
            TreatmentBMPsCount = treatmentBMPsCount;
            TreatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.Index());
        }
    }
}