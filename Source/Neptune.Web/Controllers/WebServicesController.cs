using System;
using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.WebServices;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Security;

namespace Neptune.Web.Controllers
{
    public class WebServicesController : NeptuneBaseController
    {

        [HttpGet]
        [JurisdictionEditFeature]
        [WebServicesViewFeature]
        public ViewResult Index()
        {
            var allMethods = FindAttributedMethods(typeof(PowerBIController), typeof(WebServiceNameAndParametersAttribute));
            var serviceDocumentationList = allMethods.Select(c => new WebServiceDocumentation(c)).OrderBy(c => c.Name).ToList();
            var webServiceAccessToken = new WebServiceToken(CurrentPerson.WebServiceAccessToken.ToString());
            var viewData = new IndexViewData(CurrentPerson, webServiceAccessToken, serviceDocumentationList);
            return RazorView<Index, IndexViewData>(viewData);
        }
    }
}