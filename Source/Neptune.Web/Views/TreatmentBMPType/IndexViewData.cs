/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency">
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

using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly Dictionary<TreatmentBMPTypeEnum, List<ObservationType>> ObservationTypeByTreatmentBMPTypeDictionary;
        public readonly List<Models.TreatmentBMP> TreatmentBMPs;
        public readonly string TreatmentBMPIndexUrl;
      
        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP, neptunePage)
        {
            PageTitle = "Treatment BMP Types";
            var treatmentBMPTypeObservationTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeObservationTypes;
            ObservationTypeByTreatmentBMPTypeDictionary = Models.TreatmentBMPType.All.OrderBy(x => x.SortOrder).ToDictionary(x => x.ToEnum, x => treatmentBMPTypeObservationTypes.GetObservationTypesForTreatmentType(x).OrderBy(y => y.SortOrder).ToList());
            TreatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList();
            TreatmentBMPIndexUrl = SitkaRoute<Neptune.Web.Controllers.TreatmentBMPController>.BuildUrlFromExpression(x => x.Index());

        }
    }
}
