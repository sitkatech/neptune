/*-----------------------------------------------------------------------
<copyright file="EditLocationViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditLocationViewData : Neptune.Web.Views.Shared.Location.EditLocationViewData
    {
        public EditLocationViewData(Person currentPerson,
            EFModels.Entities.TreatmentBMP treatmentBMP, MapInitJson mapInitJson, string mapFormID) : base(currentPerson, treatmentBMP, mapInitJson, mapFormID)
        {
            
        }
    }
}