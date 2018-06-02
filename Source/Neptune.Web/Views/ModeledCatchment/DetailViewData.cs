/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency">
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

namespace Neptune.Web.Views.ModeledCatchment
{
    public class DetailViewData : NeptuneViewData
    {
        public readonly Models.ModeledCatchment ModeledCatchment;
        public readonly MapInitJson MapInitJson;
        public readonly bool CurrentPersonCanManage;

        public DetailViewData(Person currentPerson, Models.ModeledCatchment modeledCatchment, MapInitJson mapInitJson) : base(currentPerson, StormwaterBreadCrumbEntity.ModeledCatchment)
        {
            ModeledCatchment = modeledCatchment;
            PageTitle = modeledCatchment.ModeledCatchmentName;
            EntityName = $"{Models.FieldDefinition.Jurisdiction.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<ModeledCatchmentController>.BuildUrlFromExpression(x => x.Index());
            MapInitJson = mapInitJson;
            CurrentPersonCanManage = CurrentPerson.IsAssignedToStormwaterJurisdiction(ModeledCatchment.StormwaterJurisdiction);
        }
    }
}
