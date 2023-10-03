/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Views.TreatmentBMP;
using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.ModalDialog;
using Neptune.Web.Security;

namespace Neptune.Web.Views.Jurisdiction
{
    public class DetailViewData : NeptuneViewData
    {
        public StormwaterJurisdiction StormwaterJurisdiction { get; }

        public TreatmentBMPGridSpec TreatmentBMPGridSpec { get; }
        public string TreatmentBMPGridName { get; }
        public string TreatmentBMPGridDataUrl { get; }
        public IHtmlContent EditStormwaterJurisdictionLink { get; }
        public bool UserHasJurisdictionEditPermissions { get; }

        public List<Person> UsersAssignedToJurisdiction { get; }
        public UrlTemplate<int> UserDetailUrlTemplate { get; }
        public UrlTemplate<int> OrganizationDetailUrlTemplate { get; }


        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, StormwaterJurisdiction stormwaterJurisdiction, List<Person> usersAssignedToJurisdiction) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration) 
        {
            StormwaterJurisdiction = stormwaterJurisdiction;
            PageTitle = stormwaterJurisdiction.GetOrganizationDisplayName();
            EntityName = $"{FieldDefinitionType.Jurisdiction.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            


            TreatmentBMPGridSpec = new TreatmentBMPGridSpec(currentPerson, false, false, LinkGenerator)
            {
                ObjectNameSingular = "Treatment BMP",
                ObjectNamePlural = $"Treatment BMPs for {stormwaterJurisdiction.GetOrganizationDisplayName()}",
                SaveFiltersInCookie = true
            };
            TreatmentBMPGridName = "jurisdictionTreatmentBMPGrid";
            TreatmentBMPGridDataUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(LinkGenerator, x => x.JurisdictionTreatmentBMPGridJsonData(stormwaterJurisdiction));

            UsersAssignedToJurisdiction = usersAssignedToJurisdiction;
            UserHasJurisdictionEditPermissions = new NeptuneAdminFeature().HasPermissionByPerson(CurrentPerson);

            EditStormwaterJurisdictionLink = UserHasJurisdictionEditPermissions
                ? ModalDialogFormHelper.MakeEditIconLink(SitkaRoute<JurisdictionController>.BuildUrlFromExpression(LinkGenerator, x => x.Edit(stormwaterJurisdiction)),
                    $"Edit Jurisdiction - {StormwaterJurisdiction.GetOrganizationDisplayName()}",
                    true)
                : new HtmlString(string.Empty);
            UserDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<UserController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            OrganizationDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
        }
    }
}
