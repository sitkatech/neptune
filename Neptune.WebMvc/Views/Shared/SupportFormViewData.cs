/*-----------------------------------------------------------------------
<copyright file="SupportFormViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.Shared
{
    public class SupportFormViewData : NeptuneViewData
    {
        public readonly bool IsUserAnonymous;
        public readonly IEnumerable<SelectListItem> SupportRequestTypes;
        public string CancelUrl { get; }
        public string GoogleRecaptchaSiteKey { get; }

        public SupportFormViewData(HttpContext httpContext, LinkGenerator linkGenerator,
            WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.NeptunePage neptunePage,
            string googleRecaptchaSiteKey, IEnumerable<SelectListItem> supportRequestTypes,
            bool isUserAnonymous, string cancelUrl) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = "Stormwater Tools";
            PageTitle = "Request Support";
            CancelUrl = cancelUrl;
            GoogleRecaptchaSiteKey = googleRecaptchaSiteKey;
            IsUserAnonymous = isUserAnonymous;
            SupportRequestTypes = supportRequestTypes;
        }
    }
}
