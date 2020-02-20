using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentExport
{
    public class ExportAssessmentGeospatialDataViewData : TrashModuleViewData
    {
        public List<StormwaterJurisdiction> StormwaterJurisdictions { get; }

        public ExportAssessmentGeospatialDataViewData(Person currentPerson) : base(currentPerson, NeptunePage.GetNeptunePageByPageType(NeptunePageType.ExportAssessmentGeospatialData))
        {
            EntityName = "On-land Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index());
            PageTitle = "Export Geospatial Data";
            StormwaterJurisdictions = currentPerson.GetStormwaterJurisdictionsPersonCanView().ToList().OrderBy(x=>x.GetOrganizationDisplayName()).ToList();
            
        }
    }
}
