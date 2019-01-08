﻿/*-----------------------------------------------------------------------
<copyright file="Notification.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Neptune.Web.Common;
using LtInfo.Common.Email;

namespace Neptune.Web.Models
{
    public partial class Notification
    {
        public static List<Notification> SendMessageAndLogNotification(MailMessage mailMessage, IEnumerable<string> emailsToSendTo, IEnumerable<string> emailsToReplyTo, IEnumerable<string> emailsToCc, List<Person> notificationPeople, DateTime notificationDate, NotificationType notificationType)
        {
            SendMessage(mailMessage, emailsToSendTo, emailsToReplyTo, emailsToCc);
            var notifications = new List<Notification>();
            foreach (var notificationPerson in notificationPeople)
            {
                var notification = new Notification(notificationType, notificationPerson, notificationDate);
                notifications.Add(notification);
            }
            return notifications;
        }

        public static void SendMessage(MailMessage mailMessage, IEnumerable<string> emailsToSendTo, IEnumerable<string> emailsToReplyTo, IEnumerable<string> emailsToCc)
        {
            mailMessage.From = DoNotReplyMailAddress();
            foreach (var email in emailsToSendTo)
            {
                mailMessage.To.Add(email);
            }
            foreach (var email in emailsToReplyTo)
            {
                mailMessage.ReplyToList.Add(email);
            }
            foreach (var emailToCc in emailsToCc)
            {
                mailMessage.CC.Add(emailToCc);
            }            
            SitkaSmtpClient.Send(mailMessage);
        }

        public static MailAddress DoNotReplyMailAddress()
        {
            return new MailAddress(NeptuneWebConfiguration.DoNotReplyEmail, SystemAttributeHelpers.GetToolDisplayName());
        }
    }
}
