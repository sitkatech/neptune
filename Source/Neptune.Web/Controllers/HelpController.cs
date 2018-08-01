/*-----------------------------------------------------------------------
<copyright file="HelpController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security.Shared;
using Neptune.Web.Views.Shared;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using Neptune.Web.Security;

namespace Neptune.Web.Controllers
{
    public class HelpController : NeptuneBaseController
    {
        [AnonymousUnclassifiedFeature]
        [HttpGet]
        public ViewResult Support()
        {
            return ViewSupport(null, "");
        }
        
        private ViewResult ViewSupport(SupportRequestTypeEnum? supportRequestTypeEnum, string optionalPrepopulatedDescription)
        {
            var currentPageUrl = string.Empty;
            if (Request.UrlReferrer != null)
            {
                currentPageUrl = Request.UrlReferrer.ToString();
            }
            else if (Request.Url != null)
            {
                currentPageUrl = Request.Url.ToString();
            }

            var viewModel = new SupportFormViewModel(currentPageUrl, supportRequestTypeEnum);
            if (!IsCurrentUserAnonymous())
            {
                viewModel.RequestPersonName = CurrentPerson.GetFullNameFirstLast();
                viewModel.RequestPersonEmail = CurrentPerson.Email;
                if (CurrentPerson.Organization != null)
                {
                    viewModel.RequestPersonOrganization = CurrentPerson.Organization.OrganizationName;
                }
                viewModel.RequestPersonPhone = CurrentPerson.Phone;
            }
            viewModel.RequestDescription = optionalPrepopulatedDescription;
            return ViewSupportImpl(viewModel, string.Empty);
        }

        private ViewResult ViewSupportImpl(SupportFormViewModel viewModel, string successMessage)
        {
            var allSupportRequestTypes = SupportRequestType.All.OrderBy(x => x.SupportRequestTypeSortOrder).ToList();

            var supportRequestTypes =
                allSupportRequestTypes.OrderBy(x => x.SupportRequestTypeSortOrder)
                    .ToSelectListWithEmptyFirstRow(x => x.SupportRequestTypeID.ToString(CultureInfo.InvariantCulture), x => x.SupportRequestTypeDisplayName);
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.RequestSupport);
            var cancelUrl = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Index());
            viewModel.ReturnUrl = cancelUrl;
            var viewData = new SupportFormViewData(CurrentPerson, neptunePage, successMessage, IsCurrentUserAnonymous(), supportRequestTypes, allSupportRequestTypes.Select(x => new SupportRequestTypeSimple(x)).ToList(), cancelUrl);
            return RazorView<SupportForm, SupportFormViewData, SupportFormViewModel>(viewData, viewModel);
        }

        [AnonymousUnclassifiedFeature]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Support(SupportFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewSupportImpl(viewModel, string.Empty);
            }
            var supportRequestLog = SupportRequestLog.Create(CurrentPerson);
            viewModel.UpdateModel(supportRequestLog, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.AllSupportRequestLogs.Add(supportRequestLog);
            supportRequestLog.SendMessage(Request.UserHostAddress, Request.UserAgent, viewModel.CurrentPageUrl, supportRequestLog.SupportRequestType);               
            SetMessageForDisplay("Support request sent.");
            var returnUrl = viewModel.ReturnUrl ?? SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Index());
            return Redirect(returnUrl);
        }

        [LoggedInUnclassifiedFeature]
        [HttpGet]
        public ViewResult RequestOrganizationNameChange()
        {
            return ViewSupport(SupportRequestTypeEnum.RequestOrganizationNameChange, string.Empty);
        }

        [AnonymousUnclassifiedFeature]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RequestOrganizationNameChange(SupportFormViewModel viewModel)
        {
            return Support(viewModel);
        }

        [LoggedInUnclassifiedFeature]
        [HttpGet]
        public ViewResult RequestToChangePrivileges()
        {
            return ViewSupport(SupportRequestTypeEnum.RequestToChangeUserAccountPrivileges, string.Empty);
        }

        [AnonymousUnclassifiedFeature]
        [CrossAreaRoute]
        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RequestToChangePrivileges(SupportFormViewModel viewModel)
        {
            return Support(viewModel);
        }
    }
}
