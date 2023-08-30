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

using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class FindABMPViewData : NeptuneViewData
    {
        public const int MaxNumberOfBmpsInList = 40;

        public MapInitJson MapInitJson { get; }
        public string FindTreatmentBMPByNameUrl { get; }
        public string NewUrl { get; }
        public bool HasManagePermissions { get; }
        public ViewDataForAngular ViewDataForAngular { get; set; }
        public string AllBMPsUrl { get; }
        public bool HasEditPermissions { get; set; }


        public FindABMPViewData(Person currentPerson, SearchMapInitJson mapInitJson, EFModels.Entities.NeptunePage neptunePage,
            List<TreatmentBMPDisplayDto> treatmentBMPDisplayDtos, List<TreatmentBMPTypeDisplayDto> treatmentBMPTypeDisplayDtos, List<StormwaterJurisdictionDisplayDto> jurisdictions, LinkGenerator linkGenerator, HttpContext httpContext)
            : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            PageTitle = "Find a BMP";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Index());
            MapInitJson = mapInitJson;
            FindTreatmentBMPByNameUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.FindByName(null));
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.New());
            AllBMPsUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.Index());
            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            ViewDataForAngular = new ViewDataForAngular(mapInitJson, treatmentBMPDisplayDtos, FindTreatmentBMPByNameUrl, treatmentBMPTypeDisplayDtos, jurisdictions);
            HasEditPermissions = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
        }
    }

    public class ViewDataForAngular
    {
        public SearchMapInitJson MapInitJson { get; }
        public List<TreatmentBMPDisplayDto> TreatmentBMPs { get; }
        public string FindTreatmentBMPByNameUrl { get; }
        public List<TreatmentBMPTypeDisplayDto> TreatmentBMPTypes { get; }
        public List<StormwaterJurisdictionDisplayDto> Jurisdictions { get; set; }


        public ViewDataForAngular(SearchMapInitJson mapInitJson, List<TreatmentBMPDisplayDto> treatmentBMPs,
            string findTreatmentBMPByNameUrl, List<TreatmentBMPTypeDisplayDto> treatmentBMPTypes, List<StormwaterJurisdictionDisplayDto> jurisdictions)
        {
            MapInitJson = mapInitJson;
            TreatmentBMPs = treatmentBMPs;
            FindTreatmentBMPByNameUrl = findTreatmentBMPByNameUrl;
            TreatmentBMPTypes = treatmentBMPTypes;
            Jurisdictions = jurisdictions;
        }

    }
}
