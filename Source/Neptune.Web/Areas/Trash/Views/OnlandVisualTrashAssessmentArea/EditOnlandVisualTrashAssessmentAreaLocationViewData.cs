/*-----------------------------------------------------------------------
<copyright file="EditVitalSignLocationViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Models;
using Neptune.Web.Common;
using Neptune.Web.Areas.Trash.Controllers;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentArea
{
    public class EditOnlandVisualTrashAssessmentAreaLocationViewData : TrashModuleViewData
    {


        public EditOnlandVisualTrashAssessmentAreaLocationViewData(Person currentPerson, Models.OnlandVisualTrashAssessmentArea ovtaArea) : base(currentPerson)
        {
            EntityName = "OVTA Areas";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index());

            SubEntityName = ovtaArea.OnlandVisualTrashAssessmentAreaName;
            SubEntityUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x => x.Detail(ovtaArea));

            PageTitle = "Edit Location";
        }
    }
}
