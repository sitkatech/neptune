using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;

namespace Neptune.Web.Views.FieldVisit
{
    public class IndexViewData : NeptuneViewData
    {
        //public FieldVisitGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        //public TreatmentBMPAssessmentGridSpec TreatmentBMPAssessmentGridSpec { get; }
        public string TreatmentBMPAssessmentGridName { get; }
        public string TreatmentBMPAssessmentGridDataUrl { get; }
        //public MaintenanceRecordGridSpec MaintenanceRecordGridSpec { get; }
        public string MaintenanceRecordGridName { get; }
        public string MaintenanceRecordGridDataUrl { get; }
        public bool HasManagePermissions { get; }
        public string BulkUploadTrashScreenVisitUrl { get; set; }

        public IndexViewData(Person currentPerson, EFModels.Entities.NeptunePage neptunePage,
            IEnumerable<CustomAttributeType> maintenanceAttributeTypes, IQueryable<TreatmentBMPAssessmentObservationType> allObservationTypes,
            LinkGenerator linkGenerator, HttpContext httpContext)
            : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            PageTitle = "All Field Records";
            EntityName = "Field Records";
            //GridSpec = new FieldVisitGridSpec(currentPerson, false)
            //{
            //    ObjectNameSingular = "Field Visit",
            //    ObjectNamePlural = "Field Visits",
            //    SaveFiltersInCookie = true
            //};
            GridName = "fieldVisitsGrid";
            //GridDataUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, j => j.AllFieldVisitsGridJsonData());

            //TreatmentBMPAssessmentGridSpec = new TreatmentBMPAssessmentGridSpec(currentPerson, allObservationTypes)
            //{
            //    ObjectNameSingular = "Assessment",
            //    ObjectNamePlural = "Assessments",
            //    SaveFiltersInCookie = true
            //};
            TreatmentBMPAssessmentGridName = "assessmentsGrid";
            //TreatmentBMPAssessmentGridDataUrl =
            //    SitkaRoute<AssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.TreatmentBMPAssessmentsGridJsonData());

            //MaintenanceRecordGridSpec = new MaintenanceRecordGridSpec(currentPerson, maintenanceAttributeTypes)
            //{
            //    ObjectNameSingular = "Maintenance Record",
            //    ObjectNamePlural = "Maintenance Records",
            //    SaveFiltersInCookie = true
            //};
            MaintenanceRecordGridName = "maintenanceRecordsGrid";
            //MaintenanceRecordGridDataUrl =
            //    SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, x => x.AllMaintenanceRecordsGridJsonData());

            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            //BulkUploadTrashScreenVisitUrl =
            //    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.BulkUploadTrashScreenVisit());
        }
    }
}