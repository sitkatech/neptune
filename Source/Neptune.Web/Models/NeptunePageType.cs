/*-----------------------------------------------------------------------
<copyright file="NeptunePageType.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Controllers;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class NeptunePageType
    {
        public abstract string GetViewUrl();
    }

    public partial class NeptunePageTypeHomePage
    {
        public override string GetViewUrl()
        {
            return SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Index());
        }
    }

    public partial class NeptunePageTypeAbout
    {
        public override string GetViewUrl()
        {
            return SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.About());
        }
    }

    public partial class NeptunePageTypeHomeAdditionalInfo
    {
        public override string GetViewUrl()
        {
            return SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Index());
        }
    }

    public partial class NeptunePageTypeHomeMapInfo
    {
        public override string GetViewUrl()
        {
            return SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Index());
        }
    }

    public partial class NeptunePageTypeOrganizationsList
    {
        public override string GetViewUrl()
        {
            return SitkaRoute<OrganizationController>.BuildUrlFromExpression(x => x.Index());
        }
    }
}
