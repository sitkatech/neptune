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
using Neptune.Web.Common;
using Neptune.Web.Views.Shared;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Help;

namespace Neptune.Web.Controllers
{
    public class HelpController : NeptuneBaseController<HelpController>
    {
        public HelpController(NeptuneDbContext dbContext, ILogger<HelpController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        [HttpGet]
        public ViewResult Support()
        {
            return ViewSupport(null, "");
        }
        
        private ViewResult ViewSupport(SupportRequestTypeEnum? supportRequestTypeEnum, string optionalPrepopulatedDescription)
        {
            var currentPageUrl = string.Empty;
            var referrer = HttpContext.Request.GetReferrer();
            if (referrer != null)
            {
                currentPageUrl = referrer;
            }
            else
            {
                var displayUrl = HttpContext.Request.GetDisplayUrl();
                if (displayUrl != null)
                {
                    currentPageUrl = displayUrl;
                }
            }

            var viewModel = new SupportFormViewModel(currentPageUrl, supportRequestTypeEnum);
            var isAnonymousUser = CurrentPerson.IsAnonymousUser();
            if (!isAnonymousUser)
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
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.RequestSupport);
            var referrer = HttpContext.Request.GetReferrer();
            var cancelUrl = referrer ?? SitkaRoute<HomeController>.BuildUrlFromExpression(_linkGenerator, x => x.Index());
            viewModel.ReturnUrl = cancelUrl;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            var viewData = new SupportFormViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage, successMessage, CurrentPerson.IsAnonymousUser(), supportRequestTypes, allSupportRequestTypes.Select(x => x.AsSimpleDto()).ToList(), cancelUrl);
            return RazorView<SupportForm, SupportFormViewData, SupportFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Support(SupportFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewSupportImpl(viewModel, string.Empty);
            }
            var supportRequestLog = SupportRequestLogs.Create(CurrentPerson);
            viewModel.UpdateModel(supportRequestLog, CurrentPerson);
            await _dbContext.SupportRequestLogs.AddAsync(supportRequestLog);
            await _dbContext.SaveChangesAsync();
            //supportRequestLog.SendMessage(Request.UserHostAddress, Request.UserAgent, viewModel.CurrentPageUrl, supportRequestLog.SupportRequestType);               
            SetMessageForDisplay("Support request sent.");
            return Redirect(viewModel.ReturnUrl);
        }

        [HttpGet]
        [LoggedInUnclassifiedFeature]
        public ViewResult RequestOrganizationNameChange()
        {
            var currentPageUrl = string.Empty;
            var referrer = HttpContext.Request.GetReferrer();
            if (referrer != null)
            {
                currentPageUrl = referrer;
            }
            else
            {
                var displayUrl = HttpContext.Request.GetDisplayUrl();
                if (displayUrl != null)
                {
                    currentPageUrl = displayUrl;
                }
            }

            var viewModel = new SupportFormViewModel(currentPageUrl, SupportRequestTypeEnum.RequestOrganizationNameChange);
            var isAnonymousUser = CurrentPerson.IsAnonymousUser();
            if (!isAnonymousUser)
            {
                viewModel.RequestPersonName = CurrentPerson.GetFullNameFirstLast();
                viewModel.RequestPersonEmail = CurrentPerson.Email;
                if (CurrentPerson.Organization != null)
                {
                    viewModel.RequestPersonOrganization = CurrentPerson.Organization.OrganizationName;
                }
                viewModel.RequestPersonPhone = CurrentPerson.Phone;
            }
            viewModel.RequestDescription = string.Empty;
            var allSupportRequestTypes = SupportRequestType.All.OrderBy(x => x.SupportRequestTypeSortOrder).ToList();

            var supportRequestTypes =
                allSupportRequestTypes.OrderBy(x => x.SupportRequestTypeSortOrder)
                    .ToSelectListWithEmptyFirstRow(x => x.SupportRequestTypeID.ToString(CultureInfo.InvariantCulture), x => x.SupportRequestTypeDisplayName);
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.RequestSupport);
            var cancelUrl = referrer ?? SitkaRoute<HomeController>.BuildUrlFromExpression(_linkGenerator, x => x.Index());
            viewModel.ReturnUrl = cancelUrl;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            var viewData = new SupportFormViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage, string.Empty, isAnonymousUser, supportRequestTypes, allSupportRequestTypes.Select(x => x.AsSimpleDto()).ToList(), cancelUrl);
            return RazorView<Views.Help.RequestOrganizationNameChange, SupportFormViewData, SupportFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RequestOrganizationNameChange(SupportFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewSupportImpl(viewModel, string.Empty);
            }
            var supportRequestLog = SupportRequestLogs.Create(CurrentPerson);
            viewModel.UpdateModel(supportRequestLog, CurrentPerson);
            await _dbContext.SupportRequestLogs.AddAsync(supportRequestLog);
            await _dbContext.SaveChangesAsync();
            supportRequestLog.SendMessage(HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Request.Headers.UserAgent, viewModel.CurrentPageUrl, supportRequestLog.SupportRequestType);               
            SetMessageForDisplay("Support request sent.");
            return Redirect(SitkaRoute<OrganizationController>.BuildUrlFromExpression(_linkGenerator, x => x.Index()));
        }

        [HttpGet]
        [LoggedInUnclassifiedFeature]
        public ViewResult RequestToChangePrivileges()
        {
            return ViewSupport(SupportRequestTypeEnum.RequestToChangeUserAccountPrivileges, string.Empty);
        }

        [HttpPost]
        public async Task<IActionResult> RequestToChangePrivileges(SupportFormViewModel viewModel)
        {
            return await Support(viewModel);
        }

        [HttpGet]
        [LoggedInUnclassifiedFeature]
        public ViewResult BulkUploadRequest()
        {
            return RazorView<BulkUploadRequest, BulkUploadRequestViewData>(
                new BulkUploadRequestViewData(HttpContext, _linkGenerator, CurrentPerson, NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.BulkUploadRequest)));
        }
    }
}
