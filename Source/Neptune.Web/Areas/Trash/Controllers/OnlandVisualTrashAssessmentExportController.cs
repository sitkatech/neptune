using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentExport;
using Neptune.Web.Controllers;
using System.Web.Mvc;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class OnlandVisualTrashAssessmentExportController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public ViewResult ExportAssessmentGeospatialData()
        {

            return RazorView<ExportAssessmentGeospatialData, ExportAssessmentGeospatialDataViewData>(
                new ExportAssessmentGeospatialDataViewData(CurrentPerson));
        }

    }
}