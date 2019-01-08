using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Neptune.Web.Controllers;
using Neptune.Web.Security.Shared;
using Neptune.Web.Areas.Trash.Views.Home;

namespace Neptune.Web.Areas.Trash.Controllers
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
