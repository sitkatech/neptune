/*-----------------------------------------------------------------------
<copyright file="EditLocationViewData.cs" company="Tahoe Regional Planning Agency">
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

namespace Neptune.Web.Views.Shared.Location
{
    public class EditLocationViewData : NeptuneViewData
    {
        public MapInitJson MapInitJson { get; }
        public string MapFormID { get; }
        public Models.TreatmentBMP TreatmentBMP { get; }

        public EditLocationViewData(Person currentPerson,
            Models.TreatmentBMP treatmentBMP,
            MapInitJson mapInitJson,
            string mapFormID) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindABMP());
            EntityUrl = treatmentBMPIndexUrl;
            if (treatmentBMP != null)
            {
                SubEntityName = treatmentBMP.TreatmentBMPName;
                SubEntityUrl = treatmentBMP.GetDetailUrl();
                TreatmentBMP = treatmentBMP;
            }

            PageTitle = "Edit Location";

            MapInitJson = mapInitJson;
            MapFormID = mapFormID;
            TreatmentBMPInformationContainer = $"{mapInitJson.MapDivID}LocationInformationContainer";
        }

        public string TreatmentBMPInformationContainer { get; }
    }
}
