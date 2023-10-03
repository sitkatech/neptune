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
using Microsoft.Extensions.Options;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Security;
using Neptune.Web.Views.Help;
using System.Net.Mail;
using Neptune.Common;
using Neptune.Common.Email;

namespace Neptune.Web.Controllers
{
    public class HelpController : NeptuneBaseController<HelpController>
    {
        private readonly SitkaSmtpClientService _sitkaSmtpClientService;

        public HelpController(NeptuneDbContext dbContext, ILogger<HelpController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, SitkaSmtpClientService sitkaSmtpClientService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _sitkaSmtpClientService = sitkaSmtpClientService;
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
            var viewData = new SupportFormViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, successMessage, CurrentPerson.IsAnonymousUser(), supportRequestTypes, allSupportRequestTypes.Select(x => x.AsSimpleDto()).ToList(), cancelUrl);
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
            await SendMessage(supportRequestLog, HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Request.Headers.UserAgent, viewModel.CurrentPageUrl, supportRequestLog.SupportRequestType);
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
            var viewData = new SupportFormViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, string.Empty, isAnonymousUser, supportRequestTypes, allSupportRequestTypes.Select(x => x.AsSimpleDto()).ToList(), cancelUrl);
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
            await SendMessage(supportRequestLog, HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Request.Headers.UserAgent, viewModel.CurrentPageUrl, supportRequestLog.SupportRequestType);               
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
                new BulkUploadRequestViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.BulkUploadRequest)));
        }

        private async Task SendMessage(SupportRequestLog supportRequestLog, string ipAddress, string userAgent, string currentUrl, SupportRequestType supportRequestType)
        {
            var subject = $"Support Request for Neptune - {DateTime.Now.ToStringDateTime()}";
            var message = string.Format(@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>{0}</strong><br />
    <br />
    <strong>From:</strong> {1} - {2}<br />
    <strong>Email:</strong> {3}<br />
    <strong>Phone:</strong> {4}<br />
    <br />
    <strong>Subject:</strong> {5}<br />
    <br />
    <strong>Description:</strong><br />
    {6}
    <br />
    <br />
    <br />
    <div style='font-size: 10px; color: gray'>
    OTHER DETAILS:<br />
    LOGIN: {7}<br />
    IP ADDRESS: {8}<br />
    USERAGENT: {9}<br />
    URL FROM: {10}<br />
    <br />
    </div>
    <div>You received this email because you are set up as a point of contact for support - if that's not correct, let us know: {11}</div>.
</div>
",
                subject,
                supportRequestLog.RequestPersonName,
                supportRequestLog.RequestPersonOrganization ?? "(not provided)",
                supportRequestLog.RequestPersonEmail,
                supportRequestLog.RequestPersonPhone ?? "(not provided)",
                supportRequestType.SupportRequestTypeDisplayName,
                supportRequestLog.RequestDescription.HtmlEncodeWithBreaks(),
                supportRequestLog.RequestPerson != null ? $"{supportRequestLog.RequestPerson.GetFullNameFirstLast()} (UserID {supportRequestLog.RequestPerson.PersonID})" : "(anonymous user)",
                ipAddress,
                userAgent,
                currentUrl,
                "support@sitkatech.com");
            // Create Notification
            var mailMessage = new MailMessage { From = new MailAddress("DoNotReplyEmail"), Subject = subject, Body = message, IsBodyHtml = true };

            // Reply-To Header
            mailMessage.ReplyToList.Add(supportRequestLog.RequestPersonEmail);

            // TO field
            SetEmailRecipientsOfSupportRequest(_dbContext, mailMessage);

            await _sitkaSmtpClientService.Send(mailMessage);
        }


        private static void SetEmailRecipientsOfSupportRequest(NeptuneDbContext dbContext, MailMessage mailMessage)
        {
            var supportPersonEmails = People.GetEmailAddressesForAdminsThatReceiveSupportEmails(dbContext).ToList();

            if (!supportPersonEmails.Any())
            {
                var defaultSupportPerson = People.GetByID(dbContext, 2);
                supportPersonEmails.Add(defaultSupportPerson.Email);
                mailMessage.Body = $"<p style=\"font-weight:bold\">Note: No users are currently configured to receive support emails. Defaulting to User: {defaultSupportPerson}</p>{mailMessage.Body}";
            }
            foreach (var supportPerson in supportPersonEmails)
            {
                mailMessage.To.Add(supportPerson);
            }
        }
    }
}
