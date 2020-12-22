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

using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

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


        public FindABMPViewData(Person currentPerson, MapInitJson mapInitJson, Models.NeptunePage neptunePage,
            List<Models.TreatmentBMP> treatmentBMPs, List<TreatmentBMPTypeSimple> treatmentBMPTypeSimples, List<StormwaterJurisdictionSimple> jurisdictions)
            : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = "Find a BMP";
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Index());
            MapInitJson = mapInitJson;
            FindTreatmentBMPByNameUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindByName(null));
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.New());
            AllBMPsUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Index());
            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            ViewDataForAngular = new ViewDataForAngular(mapInitJson, treatmentBMPs, FindTreatmentBMPByNameUrl, treatmentBMPTypeSimples, jurisdictions);
            HasEditPermissions = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
        }
    }

    public class ViewDataForAngular
    {
        public MapInitJson MapInitJson { get; }
        public List<TreatmentBMPSimple> TreatmentBMPs { get; }
        public string FindTreatmentBMPByNameUrl { get; }
        public List<TreatmentBMPTypeSimple> TreatmentBMPTypes { get; }
        public List<StormwaterJurisdictionSimple> Jurisdictions { get; set; }


        public ViewDataForAngular(MapInitJson mapInitJson, List<Models.TreatmentBMP> treatmentBMPs,
            string findTreatmentBMPByNameUrl, List<TreatmentBMPTypeSimple> treatmentBMPTypeSimples, List<StormwaterJurisdictionSimple> jurisdictions)
        {
            MapInitJson = mapInitJson;
            TreatmentBMPs = treatmentBMPs.Select(x=>new TreatmentBMPSimple(x)).ToList();
            FindTreatmentBMPByNameUrl = findTreatmentBMPByNameUrl;
            TreatmentBMPTypes = treatmentBMPTypeSimples;
            Jurisdictions = jurisdictions;
        }

    }
}
