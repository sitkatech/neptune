using System.Web.Mvc;
using Neptune.Web.Areas.Trash.Views.TreatmentBMP;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class TreatmentBMPController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public PartialViewResult TrashMapAssetPanel(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewData = new TrashMapAssetPanelViewData(CurrentPerson, treatmentBMP);
            return RazorPartialView<TrashMapAssetPanel, TrashMapAssetPanelViewData>(viewData);
        }   
    }
}
