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

using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly TreatmentBMPGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;
        public readonly MapInitJson MapInitJson;
        public readonly TreatmentBMPTypeLegendViewData TreatmentBMPTypeLegendViewData;
        public readonly string FindTreatmentBMPByNameUrl;
        public readonly string NewUrl;

        public IndexViewData(Person currentPerson, MapInitJson mapInitJson, Models.NeptunePage neptunePage)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP, neptunePage)
        {
            PageTitle = "BMPs";
            GridSpec = new TreatmentBMPGridSpec(currentPerson) {ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true};
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(j => j.TreatmentBMPGridJsonData());

            MapInitJson = mapInitJson;
            TreatmentBMPTypeLegendViewData = new TreatmentBMPTypeLegendViewData();
            FindTreatmentBMPByNameUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindByName(null));
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.New());
        }
    }
}
