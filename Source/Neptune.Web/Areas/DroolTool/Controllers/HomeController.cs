using System.Web.Mvc;
using Neptune.Web.Areas.DroolTool.Security;
using Neptune.Web.Areas.DroolTool.Views.Home;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Areas.DroolTool.Controllers
{
    public class HomeController : NeptuneBaseController
    {
        [HttpGet]
        [DroolToolViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.DroolToolHomePage);
            var viewData = new IndexViewData(CurrentPerson, neptunePage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [DroolToolViewFeature]
        public ViewResult About()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.DroolToolAboutPage);
            var viewData = new AboutViewData(CurrentPerson, neptunePage);
            return RazorView<About, AboutViewData>(viewData);
        }
    }
}
