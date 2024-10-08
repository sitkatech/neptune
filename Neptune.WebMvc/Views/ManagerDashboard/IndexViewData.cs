﻿using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;


namespace Neptune.WebMvc.Views.ManagerDashboard
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
        public bool UserCanViewBMPDelineations { get; }
        public string BulkRowFieldVisitsUrl { get; }
        public string BulkRowTreatmentBMPsUrl { get; }
        public string BulkRowDelineationsUrl { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.NeptunePage neptunePage, int fieldVisitCount, int treatmentBMPsCount, int bmpDelineationsCount)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
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
            GridDataUrl = SitkaRoute<ManagerDashboardController>.BuildUrlFromExpression(linkGenerator, x => x.AllFieldVisitsGridJsonData(GridName));
            ProvisionalFieldVisitGridCheckAllUrl = $"Sitka.{GridName}.grid.checkAll()";
            ProvisionalFieldVisitGridUncheckAllUrl = $"Sitka.{GridName}.grid.uncheckAll()";
            FieldVisitCount = fieldVisitCount;
            FieldVisitsIndexUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Index());


            ProvisionalTreatmentBMPGridName = "assessmentsGrid";
            ProvisionalTreatmentBMPGridSpec = new ProvisionalTreatmentBMPGridSpec(linkGenerator, currentPerson, ProvisionalTreatmentBMPGridName)
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
            TreatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Index());


            ProvisionalBMPDelineationGridName = "ProvisionalBMPDelineationsGrid";
            ProvisionalBMPDelineationsGridSpec = new ProvisionalBMPDelineationsGridSpec(LinkGenerator, currentPerson, ProvisionalBMPDelineationGridName)
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

            BulkRowFieldVisitsUrl = SitkaRoute<BulkRowController>.BuildUrlFromExpression(linkGenerator, x => x.BulkRowFieldVisits(null));
            BulkRowTreatmentBMPsUrl = SitkaRoute<BulkRowController>.BuildUrlFromExpression(linkGenerator, x => x.BulkRowTreatmentBMPs(null));
            BulkRowDelineationsUrl = SitkaRoute<BulkRowController>.BuildUrlFromExpression(linkGenerator, x => x.BulkRowBMPDelineation(null));
        }
    }
}