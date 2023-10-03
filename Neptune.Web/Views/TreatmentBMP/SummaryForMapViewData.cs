/*-----------------------------------------------------------------------
<copyright file="SummaryForMapViewData.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class SummaryForMapViewData : NeptuneViewData
    {

        public EFModels.Entities.TreatmentBMP TreatmentBMP{ get; }
        public EFModels.Entities.TreatmentBMPImage? KeyPhoto { get; }
        public string FieldVisitUrl { get; }
        public bool UserHasFieldVisitPermissions { get; }
        public string TreatmentBMPDetailUrl { get; }
        public string StormwaterJurisdictionDetailUrl { get; }

        public SummaryForMapViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.TreatmentBMP treatmentBMP, EFModels.Entities.TreatmentBMPImage? keyPhoto) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            TreatmentBMP = treatmentBMP;
            KeyPhoto = keyPhoto;
            TreatmentBMPDetailUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));
            StormwaterJurisdictionDetailUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP.StormwaterJurisdictionID));
            FieldVisitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(LinkGenerator, x => x.New(TreatmentBMP));
            UserHasFieldVisitPermissions = new FieldVisitCreateFeature().HasPermission(currentPerson, TreatmentBMP).HasPermission;
        }
    }
}
