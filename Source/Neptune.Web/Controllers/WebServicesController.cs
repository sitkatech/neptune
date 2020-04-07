using System;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.WebServices;
using System.Linq;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Security;

namespace Neptune.Web.Controllers
{
    public class WebServicesController : NeptuneBaseController
    {

        [HttpGet]
        [AllowAnonymous]
        [WebServicesViewFeature]
        public ViewResult Index()
        {
            var webServicesListUrl = SitkaRoute<WebServicesController>.BuildUrlFromExpression(x => x.List());
            var getWebServiceAccessTokenUrl = SitkaRoute<WebServicesController>.BuildUrlFromExpression(x => x.GetWebServiceAccessToken(CurrentPerson));
            var viewData = new IndexViewData(CurrentPerson, CurrentPerson.WebServiceAccessToken, webServicesListUrl,
                getWebServiceAccessTokenUrl);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [WebServicesViewFeature]
        public PartialViewResult GetWebServiceAccessToken(PersonPrimaryKey personPrimaryKey)
        {
            Check.Require(personPrimaryKey.PrimaryKeyValue == CurrentPerson.PersonID, "The person ID passed in to GetWebServiceAccessToken must match the logged in user!");
            var person = personPrimaryKey.EntityObject;
            if (!person.WebServiceAccessToken.HasValue)
            {
                person.WebServiceAccessToken = Guid.NewGuid();
                HttpRequestStorage.DatabaseEntities.SaveChanges(CurrentPerson);
            }
            var viewData = new ViewAccessTokenViewData(person.WebServiceAccessToken.Value, SitkaRoute<WebServicesController>.BuildUrlFromExpression(c => c.List()));
            return RazorPartialView<ViewAccessToken, ViewAccessTokenViewData>(viewData);
        }

        [HttpGet]
        [AllowAnonymous]
        [WebServicesViewFeature]
        public ViewResult List()
        {
            var viewData = new ListViewData(CurrentPerson);
            return RazorView<Neptune.Web.Views.WebServices.List, ListViewData>(viewData);
        }

    }
}