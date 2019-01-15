using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using MoreLinq;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMP;
using Index = Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment.Index;
using IndexViewData = Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment.IndexViewData;
using TreatmentBMPController = Neptune.Web.Controllers.TreatmentBMPController;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson, NeptunePage.GetNeptunePageByPageType(NeptunePageType.OVTAIndex));
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<OnlandVisualTrashAssessment> OVTAGridJsonData()
        {
            var treatmentBMPs = GetOVTAsAndGridSpec(out var gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<OnlandVisualTrashAssessment>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<OnlandVisualTrashAssessment> GetOVTAsAndGridSpec(out OVTAIndexGridSpec gridSpec, Person currentPerson)
        {
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            gridSpec = new OVTAIndexGridSpec(currentPerson, showDelete, showEdit);
            return HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessments.ToList();
        }
    }
}

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public abstract class Index : TypedWebViewPage<IndexViewData>
    {


    }

    public class IndexViewData : OVTASectionViewData
    {
        public OVTAIndexGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public string NewUrl { get; }
        public bool HasManagePermissions { get; }
        

        public IndexViewData(Person currentPerson, NeptunePage neptunePage)
            : base(currentPerson, neptunePage)
        {
            PageTitle = "All OVTAs";
            EntityName = $"{FieldDefinition.OnlandVisualTrashAssessment.GetFieldDefinitionLabelPluralized()}";
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new OVTAIndexGridSpec(currentPerson, showDelete, showEdit) { ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true };
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(j => j.OVTAGridJsonData());
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.New());
            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
           
        }
    }
}