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
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class FindABMPViewData : NeptuneViewData
    {
        public const int MaxNumberOfBmpsInList = 40;

        public TreatmentBMPGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public MapInitJson MapInitJson { get; }
        public TreatmentBMPTypeLegendViewData TreatmentBMPTypeLegendViewData { get; }
        public string FindTreatmentBMPByNameUrl { get; }
        public string NewUrl { get; }
        public bool HasManagePermissions { get; }
        public ViewDataForAngular ViewDataForAngular { get; set; }
        public string AllBMPsUrl { get; }


        public FindABMPViewData(Person currentPerson, MapInitJson mapInitJson, Models.NeptunePage neptunePage,
            List<Models.TreatmentBMP> treatmentBMPs, List<TreatmentBMPTypeSimple> treatmentBMPTypeSimples)
            : base(currentPerson, neptunePage)
        {
            PageTitle = "Find a BMP";
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new TreatmentBMPGridSpec(currentPerson, showDelete, showEdit)
            {
                ObjectNameSingular = "Treatment BMP",
                ObjectNamePlural = "Treatment BMPs",
                SaveFiltersInCookie = true
            };
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(j => j.TreatmentBMPGridJsonData());

            MapInitJson = mapInitJson;
            TreatmentBMPTypeLegendViewData = new TreatmentBMPTypeLegendViewData();
            FindTreatmentBMPByNameUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindByName(null));
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.New());
            AllBMPsUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Index());
            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            ViewDataForAngular = new ViewDataForAngular(mapInitJson, treatmentBMPs, FindTreatmentBMPByNameUrl, treatmentBMPTypeSimples);
        }
    }

    public class ViewDataForAngular
    {
        public MapInitJson MapInitJson { get; }
        public List<TreatmentBMPSimple> TreatmentBMPs { get; }
        public string FindTreatmentBMPByNameUrl { get; }
        public List<TreatmentBMPTypeSimple> TreatmentBMPTypes { get; }

        public ViewDataForAngular(MapInitJson mapInitJson, List<Models.TreatmentBMP> treatmentBMPs,
            string findTreatmentBMPByNameUrl, List<TreatmentBMPTypeSimple> treatmentBMPTypeSimples)
        {
            MapInitJson = mapInitJson;
            TreatmentBMPs = treatmentBMPs.Select(x=>new TreatmentBMPSimple(x)).ToList();
            FindTreatmentBMPByNameUrl = findTreatmentBMPByNameUrl;
            TreatmentBMPTypes = treatmentBMPTypeSimples;
        }
    }
}
