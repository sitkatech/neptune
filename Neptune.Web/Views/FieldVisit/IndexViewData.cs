using Neptune.EFModels.Entities;
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

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, NeptunePage neptunePage,
            IEnumerable<CustomAttributeType> maintenanceAttributeTypes, IQueryable<EFModels.Entities.TreatmentBMPAssessmentObservationType> allObservationTypes)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
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
            //GridDataUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, linkGenerator, x => x.AllFieldVisitsGridJsonData());

            //TreatmentBMPAssessmentGridSpec = new TreatmentBMPAssessmentGridSpec(currentPerson, allObservationTypes)
            //{
            //    ObjectNameSingular = "Assessment",
            //    ObjectNamePlural = "Assessments",
            //    SaveFiltersInCookie = true
            //};
            TreatmentBMPAssessmentGridName = "assessmentsGrid";
            //TreatmentBMPAssessmentGridDataUrl =
            //    SitkaRoute<AssessmentController>.BuildUrlFromExpression(linkGenerator, linkGenerator, x => x.TreatmentBMPAssessmentsGridJsonData());

            //MaintenanceRecordGridSpec = new MaintenanceRecordGridSpec(currentPerson, maintenanceAttributeTypes)
            //{
            //    ObjectNameSingular = "Maintenance Record",
            //    ObjectNamePlural = "Maintenance Records",
            //    SaveFiltersInCookie = true
            //};
            MaintenanceRecordGridName = "maintenanceRecordsGrid";
            //MaintenanceRecordGridDataUrl =
            //    SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, linkGenerator, x => x.AllMaintenanceRecordsGridJsonData());

            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            //BulkUploadTrashScreenVisitUrl =
            //    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, linkGenerator, x => x.BulkUploadTrashScreenVisit());
        }
    }
}