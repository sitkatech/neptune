
using System.Web.Mvc;
using Neptune.Web.Areas.Modeling.Views.Home;
using Neptune.Web.Controllers;
using Neptune.Web.Security.Shared;

namespace Neptune.Web.Areas.Modeling.Controllers
{
    public class HomeController : NeptuneBaseController
    {
        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public ViewResult Index()
        {
            IndexViewData viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }   
    }
}
