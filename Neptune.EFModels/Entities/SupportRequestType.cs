/*-----------------------------------------------------------------------
<copyright file="SupportRequestType.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Net.Mail;

namespace Neptune.EFModels.Entities
{
    public partial class SupportRequestType
    {
        public virtual void SetEmailRecipientsOfSupportRequest(NeptuneDbContext dbContext, MailMessage mailMessage)
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

    public partial class SupportRequestTypeQuestionAboutPolicies
    {
    }

    public partial class SupportRequestTypeReportBug
    {
    }

    public partial class SupportRequestTypeHelpWithProjectUpdate
    {
    }

    public partial class SupportRequestTypeForgotLoginInfo
    {
    }

    public partial class SupportRequestTypeNewOrganizationOrFundingSource
    {
    }

    public partial class SupportRequestTypeOther
    {
    }

    public partial class SupportRequestTypeRequestToBeAddedToFtipList
    {
    }

    public partial class SupportRequestTypeProvideFeedback
    {
    }

    public partial class SupportRequestTypeRequestOrganizationNameChange
    {
        public override void SetEmailRecipientsOfSupportRequest(NeptuneDbContext dbContext, MailMessage mailMessage)
        {
            mailMessage.To.Add("support@sitkatech.com"/*todo: NeptuneWebConfiguration.SitkaSupportEmail*/);
        }
    }
    public partial class SupportRequestTypeRequestToChangeUserAccountPrivileges
    {
    }
}
