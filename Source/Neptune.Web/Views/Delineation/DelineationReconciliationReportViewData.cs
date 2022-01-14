using System;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.Delineation
{
    public class DelineationReconciliationReportViewData : NeptuneViewData
    {
        public string GridDataUrl { get; }
        public string GridName { get; }
        public MisalignedDelineationGridSpec GridSpec { get; }

        public string OverlappingTreatmentBMPsGridDataUrl { get; }
        public string OverlappingTreatmentBMPsGridName { get; }
        public DelineationOverlapsDelineationGridSpec OverlappingTreatmentBMPsGridSpec { get; }

        public DelineationReconciliationReportViewData(Person currentPerson, Models.NeptunePage neptunePage, DateTime? regionalSubbasinsLastUpdated) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            RegionalSubbasinsLastUpdated = regionalSubbasinsLastUpdated.HasValue ? regionalSubbasinsLastUpdated.ToStringDateTime() : "n/a";
            EntityName = FieldDefinitionType.Delineation.FieldDefinitionTypeDisplayName;
            PageTitle = "Delineation Reconciliation";

            HasManagePermission = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);

            GridSpec = new MisalignedDelineationGridSpec() { ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true };
            GridName = "misalignedTreatmentBMPsGrid";
            GridDataUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(j => j.DelineationsMisalignedWithRegionalSubbasinsGridJsonData());
            OverlappingTreatmentBMPsGridSpec = new DelineationOverlapsDelineationGridSpec() { ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true };
            OverlappingTreatmentBMPsGridName = "overlappingTreatmentBMPsGrid";
            OverlappingTreatmentBMPsGridDataUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(j => j.DelineationsOverlappingEachOtherGridJsonData());
            CheckForDiscrepanciesUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(j => j.CheckForDiscrepancies());
        }

        public bool HasManagePermission { get; }
        public string RegionalSubbasinsLastUpdated { get; }
        public string CheckForDiscrepanciesUrl { get; }
    }
}