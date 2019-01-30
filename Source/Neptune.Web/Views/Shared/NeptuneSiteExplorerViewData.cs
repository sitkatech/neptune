/*-----------------------------------------------------------------------
<copyright file="NeptuneSiteExplorerViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System.Collections.Generic;

namespace Neptune.Web.Views.Shared
{
    public class NeptuneSiteExplorerViewData
    {
        public readonly string HomeUrl;
        public readonly NeptuneArea NeptuneArea;
        public readonly bool ShowLinkToArea;

        public readonly Person CurrentPerson;
        public readonly string RequestSupportUrl;

        public List<LtInfoMenuItem> TopLevelNeptuneMenus;

        public NeptuneSiteExplorerViewData(Person currentPerson, NeptuneArea neptuneArea)
        {
            HomeUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.Index());
            NeptuneArea = neptuneArea;
            ShowLinkToArea = neptuneArea != NeptuneArea.OCStormwaterTools;

            CurrentPerson = currentPerson;
            RequestSupportUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(c => c.Support());

            //UserCanViewMyProjectsAndProposals = new ProjectUpdateViewAndCanProposeProjectFeature().HasPermissionByPerson(CurrentPerson);
        }

    }
}
