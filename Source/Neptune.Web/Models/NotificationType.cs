/*-----------------------------------------------------------------------
<copyright file="NotificationType.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Web;

namespace Neptune.Web.Models
{
    public partial class NotificationType
    {
        public abstract HtmlString GetEntityDetailsAsHref(Notification notification);
        public abstract int GetEntityCount(Notification notification);
        public abstract HtmlString GetFullDescriptionFromUserPerspective(Notification notification);
        public abstract string GetFullDescriptionFromProjectPerspective();
        public abstract string GetFullDescriptionFromRegistrationPerspective();
    }

    public partial class NotificationTypeCustom
    {
        public override HtmlString GetEntityDetailsAsHref(Notification notification)
        {
            return new HtmlString(string.Empty);
        }

        public override int GetEntityCount(Notification notification)
        {
            return 0;
        }

        public override HtmlString GetFullDescriptionFromUserPerspective(Notification notification)
        {
            return new HtmlString("A customized notification was sent.");
        }

        public override string GetFullDescriptionFromProjectPerspective()
        {
            return string.Empty;
        }

        public override string GetFullDescriptionFromRegistrationPerspective()
        {
            return string.Empty;
        }
    }
}
