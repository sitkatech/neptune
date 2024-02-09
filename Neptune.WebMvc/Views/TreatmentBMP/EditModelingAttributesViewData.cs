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

using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class EditModelingAttributesViewData : NeptuneViewData
    {
        public EditModelingAttributesViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP,
            IEnumerable<RoutingConfiguration> routingConfigurations,
            IEnumerable<TimeOfConcentration> timeOfConcentrations,
            IEnumerable<UnderlyingHydrologicSoilGroup> underlyingHydrologicSoilGroups,
            List<MonthsOfOperation> monthsOfOperation, List<DryWeatherFlowOverride> dryWeatherFlowOverride) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            SubEntityName = treatmentBMP.TreatmentBMPName;
            SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));
            PageTitle = $"Edit {FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabel()} Modeling Attributes";
            TreatmentBMP = treatmentBMP;
            DryWeatherFlowOverride = dryWeatherFlowOverride.ToSelectList(x => x.DryWeatherFlowOverrideID.ToString(),
                x => x.DryWeatherFlowOverrideDisplayName);
            MonthsOfOperation = monthsOfOperation.ToSelectList(x => x.MonthsOfOperationID.ToString(CultureInfo.InvariantCulture), x => x.MonthsOfOperationDisplayName);
            UnderlyingHydrologicSoilGroups = underlyingHydrologicSoilGroups.ToSelectListWithEmptyFirstRow(x => x.UnderlyingHydrologicSoilGroupID.ToString(), x => x.UnderlyingHydrologicSoilGroupDisplayName);
            TimeOfConcentrations = timeOfConcentrations.ToSelectListWithEmptyFirstRow(x => x.TimeOfConcentrationID.ToString(), x =>
                $"{x.TimeOfConcentrationDisplayName} minutes");
            RoutingConfigurations = routingConfigurations.ToSelectListWithEmptyFirstRow(x => x.RoutingConfigurationID.ToString(), x => x.RoutingConfigurationDisplayName);
        }

        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }
        public IEnumerable<SelectListItem> RoutingConfigurations { get; }
        public IEnumerable<SelectListItem> TimeOfConcentrations { get; }
        public IEnumerable<SelectListItem> UnderlyingHydrologicSoilGroups { get; }
        public IEnumerable<SelectListItem> MonthsOfOperation { get; }
        public IEnumerable<SelectListItem> DryWeatherFlowOverride { get; }
    }
}
