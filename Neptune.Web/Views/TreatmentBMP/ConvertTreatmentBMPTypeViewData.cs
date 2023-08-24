/*-----------------------------------------------------------------------
<copyright file="ConvertTreatmentBMPTypeViewData.cs" company="Tahoe Regional Planning Agency">
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

using System.Globalization;
using LtInfo.Common.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class ConvertTreatmentBMPTypeViewData : NeptuneUserControlViewData
    {
        public IEnumerable<SelectListItem> TreatmentBMPTypeSelectListItems { get; }
        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }

        public ConvertTreatmentBMPTypeViewData(EFModels.Entities.TreatmentBMP treatmentBMP, IEnumerable<EFModels.Entities.TreatmentBMPType> treatmentBMPTypes)
        {
            TreatmentBMP = treatmentBMP;
            TreatmentBMPTypeSelectListItems = treatmentBMPTypes.OrderBy(x => x.TreatmentBMPTypeName)
                .ToSelectListWithEmptyFirstRow(x => x.TreatmentBMPTypeID.ToString(CultureInfo.InvariantCulture),
                    y => y.TreatmentBMPTypeName);
        }
    }
}
