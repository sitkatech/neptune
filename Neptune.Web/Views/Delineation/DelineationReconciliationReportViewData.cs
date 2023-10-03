using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
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

        public DelineationReconciliationReportViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.NeptunePage neptunePage, DateTime? regionalSubbasinsLastUpdated) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            RegionalSubbasinsLastUpdated = regionalSubbasinsLastUpdated.HasValue ? regionalSubbasinsLastUpdated.ToStringDateTime() : "n/a";
            EntityName = FieldDefinitionType.Delineation.FieldDefinitionTypeDisplayName;
            PageTitle = "Delineation Reconciliation";

            HasManagePermission = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);

            GridSpec = new MisalignedDelineationGridSpec(linkGenerator) { ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true };
            GridName = "misalignedTreatmentBMPsGrid";
            GridDataUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationsMisalignedWithRegionalSubbasinsGridJsonData());
            OverlappingTreatmentBMPsGridSpec = new DelineationOverlapsDelineationGridSpec(linkGenerator) { ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true };
            OverlappingTreatmentBMPsGridName = "overlappingTreatmentBMPsGrid";
            OverlappingTreatmentBMPsGridDataUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationsOverlappingEachOtherGridJsonData());
            CheckForDiscrepanciesUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.CheckForDiscrepancies());
        }

        public bool HasManagePermission { get; }
        public string RegionalSubbasinsLastUpdated { get; }
        public string CheckForDiscrepanciesUrl { get; }
    }
}