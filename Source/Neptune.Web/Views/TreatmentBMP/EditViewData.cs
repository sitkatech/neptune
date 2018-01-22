/*-----------------------------------------------------------------------
<copyright file="EditViewData.cs" company="Tahoe Regional Planning Agency">
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
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditViewData : NeptuneViewData
    {

        public IEnumerable<SelectListItem> StormwaterJurisdictionSelectListItems { get; }
        public IEnumerable<SelectListItem> TreatmentBMPTypeSelectListItems { get; }
        public MapInitJson MapInitJson { get; }
        public string MapFormID { get; }
        public string TreatmentBMPInformationContainer { get; }
        public Models.TreatmentBMP TreatmentBMP { get; }
        public string TreatmentBMPIndexUrl { get; }

        public EditViewData(Person currentPerson,
            Models.TreatmentBMP treatmentBMP,
            IEnumerable<Models.StormwaterJurisdiction> stormwaterJurisdictions,
            IEnumerable<Models.TreatmentBMPType> treatmentBMPTypes,
            MapInitJson mapInitJson,
            string mapFormID) : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP)
        {
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var treatmentBMPIndexUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Index());
            EntityUrl = treatmentBMPIndexUrl;
            if (treatmentBMP != null)
            {
                SubEntityName = treatmentBMP.TreatmentBMPName;
                SubEntityUrl = treatmentBMP.GetDetailUrl();
                TreatmentBMP = treatmentBMP;
            }
            PageTitle = $"{(treatmentBMP != null ? "Edit" : "New")} {Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabel()}";

            StormwaterJurisdictionSelectListItems = stormwaterJurisdictions.OrderBy(x => x.OrganizationDisplayName).ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.OrganizationDisplayName);
            TreatmentBMPTypeSelectListItems = treatmentBMPTypes.OrderBy(x => x.TreatmentBMPTypeName).ToSelectListWithEmptyFirstRow(x => x.TreatmentBMPTypeID.ToString(CultureInfo.InvariantCulture), y => y.TreatmentBMPTypeName);
            MapInitJson = mapInitJson;
            MapFormID = mapFormID;
            TreatmentBMPInformationContainer = $"{mapInitJson.MapDivID}LocationInformationContainer";
            TreatmentBMPIndexUrl = treatmentBMPIndexUrl;  
        }
    }
}
