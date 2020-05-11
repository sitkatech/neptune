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
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class EditModelingAttributesViewData : NeptuneViewData
    {
        public EditModelingAttributesViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP, IEnumerable<RoutingConfiguration> routingConfigurations, IEnumerable<TimeOfConcentration> timeOfConcentrations, IEnumerable<UnderlyingHydrologicSoilGroup> underlyingHydrologicSoilGroups, List<OperationMonth> monthsOfOperation) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindABMP());
            SubEntityName = treatmentBMP.TreatmentBMPName;
            SubEntityUrl = treatmentBMP.GetDetailUrl();
            PageTitle = $"Edit {Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabel()} Modeling Attributes";
            TreatmentBMP = treatmentBMP;
            MonthsOfOperation = monthsOfOperation.ToSelectList(x => x.OperationMonthID.ToString(CultureInfo.InvariantCulture), x => x.OperationMonthDisplayName);
            UnderlyingHydrologicSoilGroups = underlyingHydrologicSoilGroups.ToSelectListWithEmptyFirstRow(x => x.UnderlyingHydrologicSoilGroupID.ToString(), x => x.UnderlyingHydrologicSoilGroupDisplayName);
            TimeOfConcentrations = timeOfConcentrations.ToSelectListWithEmptyFirstRow(x => x.TimeOfConcentrationID.ToString(), x =>
                $"{x.TimeOfConcentrationDisplayName} minutes");
            RoutingConfigurations = routingConfigurations.ToSelectListWithEmptyFirstRow(x => x.RoutingConfigurationID.ToString(), x => x.RoutingConfigurationDisplayName);
        }

        public Models.TreatmentBMP TreatmentBMP { get; }
        public IEnumerable<SelectListItem> RoutingConfigurations { get; }
        public IEnumerable<SelectListItem> TimeOfConcentrations { get; }
        public IEnumerable<SelectListItem> UnderlyingHydrologicSoilGroups { get; }
        public IEnumerable<SelectListItem> MonthsOfOperation { get; }
    }
}
