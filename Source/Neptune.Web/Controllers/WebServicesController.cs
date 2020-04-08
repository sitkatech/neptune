using System;
using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.WebServices;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Security;
using Neptune.Web.Service;

namespace Neptune.Web.Controllers
{
    public class WebServicesController : NeptuneBaseController
    {

        [HttpGet]
        [AllowAnonymous]
        [WebServicesViewFeature]
        public ViewResult Index()
        {
            var allMethods = FindAttributedMethods(typeof(IWebServices), typeof(WebServiceDocumentationAttribute));
            var serviceDocumentationList = allMethods.Select(c => new WebServiceDocumentation(c)).ToList();
            var webServiceAccessToken = new WebServiceToken(CurrentPerson.WebServiceAccessToken.Value.ToString());
            var viewData = new IndexViewData(CurrentPerson, webServiceAccessToken, serviceDocumentationList);
            return RazorView<Index, IndexViewData>(viewData);
        }

    }
}