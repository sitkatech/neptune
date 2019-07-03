using Neptune.Web.Areas.DroolTool.Security;
using Neptune.Web.Areas.DroolTool.Views.Home;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Neptune.Web.Areas.DroolTool.Controllers
{
    public class HomeController : NeptuneBaseController
    {
        [HttpGet]
        [DroolToolViewFeature]
        public ViewResult Index()
        {
            var visitedCookie = Request.Cookies["visitedDroolTool"];
            var firstTimeVisit = visitedCookie == null;

            if (firstTimeVisit)
            {
                HttpCookie myCookie = new HttpCookie("visitedDroolTool");
                myCookie.Values.Add("firstVisitDate", DateTime.Now.ToShortDateString());
                myCookie.Expires = DateTime.Now.AddMonths(12);
                Response.Cookies.Add(myCookie);
            }

            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.DroolToolHomePage);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, firstTimeVisit);
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
