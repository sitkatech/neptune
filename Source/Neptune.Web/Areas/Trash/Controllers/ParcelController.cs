using System.Web.Mvc;
using Neptune.Web.Areas.Trash.Views.Parcel;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class ParcelController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneViewFeature]
        public PartialViewResult TrashMapAssetPanel(ParcelPrimaryKey parcelPrimaryKey)
        {
            var parcel = parcelPrimaryKey.EntityObject;
            var viewData = new TrashMapAssetPanelViewData(CurrentPerson, parcel);
            return RazorPartialView<TrashMapAssetPanel, TrashMapAssetPanelViewData>(viewData);
        }   
    }
}
