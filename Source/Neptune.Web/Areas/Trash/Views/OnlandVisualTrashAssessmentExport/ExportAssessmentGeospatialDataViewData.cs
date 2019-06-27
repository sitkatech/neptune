using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentExport
{
    public class ExportAssessmentGeospatialDataViewData : TrashModuleViewData
    {

        public ExportAssessmentGeospatialDataViewData(Person currentPerson) : base(currentPerson, NeptunePage.GetNeptunePageByPageType(NeptunePageType.ExportAssessmentGeospatialData))
        {
            EntityName = "On-land Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index());
            StormwaterJurisdictions = currentPerson.GetStormwaterJurisdictionsPersonCanEdit().ToList();
        }

        public List<StormwaterJurisdiction> StormwaterJurisdictions { get; set; }
    }
}