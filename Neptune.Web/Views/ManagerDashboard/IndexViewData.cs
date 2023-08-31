using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;


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

        public ProvisionalBMPDelineationsGridSpec ProvisionalBMPDelineationsGridSpec { get; }
        public string ProvisionalBMPDelineationGridName { get; }
        public string ProvisionalBMPDelineationGridDataUrl { get; }
        public string ProvisionalBMPDelineationGridCheckAllUrl { get; }
        public string ProvisionalBMPDelineationGridUncheckAllUrl { get; }
        public int BMPDelineationsCount { get; }
        public string DelineationIndexUrl { get; }
        public bool UserCanViewBMPDelineations { get; }

        public IndexViewData(Person currentPerson, NeptunePage neptunePage, LinkGenerator linkGenerator, HttpContext httpContext, int fieldVisitCount, int treatmentBMPsCount, int bmpDelineationsCount)
            : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            PageTitle = "Manager Dashboard";
            EntityName = "Stormwater Tools";
            GridName = "ProvisionalFieldVisitGrid";
            GridSpec = new ProvisionalFieldVisitGridSpec(currentPerson, GridName, linkGenerator)
            {
                ObjectNameSingular = "Field Visit",
                ObjectNamePlural = "Field Visits",
                SaveFiltersInCookie = true
            };
            GridDataUrl = SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(linkGenerator, j => j.AllFieldVisitsGridJsonData(GridName));
            ProvisionalFieldVisitGridCheckAllUrl = $"Sitka.{GridName}.grid.checkAll()";
            ProvisionalFieldVisitGridUncheckAllUrl = $"Sitka.{GridName}.grid.uncheckAll()";
            FieldVisitCount = fieldVisitCount;
            FieldVisitsIndexUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, c => c.Index());


            ProvisionalTreatmentBMPGridName = "assessmentsGrid";
            ProvisionalTreatmentBMPGridSpec = new ProvisionalTreatmentBMPGridSpec(currentPerson, ProvisionalTreatmentBMPGridName)
            {
                ObjectNameSingular = "Provisional BMP Record",
                ObjectNamePlural = "Provisional BMP Records",
                SaveFiltersInCookie = true
            };
            ProvisionalTreatmentBMPGridDataUrl =
                SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(linkGenerator, x => x.ProvisionalTreatmentBMPGridJsonData(ProvisionalTreatmentBMPGridName));
            ProvisionalTreatmentBMPGridCheckAllUrl = $"Sitka.{ProvisionalTreatmentBMPGridName}.grid.checkAll()";
            ProvisionalTreatmentBMPGridUncheckAllUrl = $"Sitka.{ProvisionalTreatmentBMPGridName}.grid.uncheckAll()";
            TreatmentBMPsCount = treatmentBMPsCount;
            //TreatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, c => c.Index());


            ProvisionalBMPDelineationGridName = "ProvisionalBMPDelineationsGrid";
            ProvisionalBMPDelineationsGridSpec = new ProvisionalBMPDelineationsGridSpec(currentPerson, ProvisionalBMPDelineationGridName)
            {
                ObjectNameSingular = "Provisional BMP Delineation Record",
                ObjectNamePlural = "Provisional BMP Delineation Records",
                SaveFiltersInCookie = true
            };

            ProvisionalBMPDelineationGridDataUrl = SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(linkGenerator, x =>
                x.ProvisionalBMPDelineationsGridJson(ProvisionalBMPDelineationGridName));

            ProvisionalBMPDelineationGridCheckAllUrl = $"Sitka.{ProvisionalBMPDelineationGridName}.grid.checkAll()";
            ProvisionalBMPDelineationGridUncheckAllUrl = $"Sitka.{ProvisionalBMPDelineationGridName}.gird.uncheckAll()";
            BMPDelineationsCount = bmpDelineationsCount;
            UserCanViewBMPDelineations = new JurisdictionManageFeature().HasPermissionByPerson(CurrentPerson);
            //DelineationIndexUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(x => x.Index)
        }
    }
}