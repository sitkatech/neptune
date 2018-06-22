/*-----------------------------------------------------------------------
<copyright file="JurisdictionController.cs" company="Tahoe Regional Planning Agency">
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
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Security.Shared;
using Neptune.Web.Views.Jurisdiction;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMP;
using Detail = Neptune.Web.Views.Jurisdiction.Detail;
using DetailViewData = Neptune.Web.Views.Jurisdiction.DetailViewData;
using Index = Neptune.Web.Views.Jurisdiction.Index;
using IndexViewData = Neptune.Web.Views.Jurisdiction.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class JurisdictionController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.Jurisdiction);
            var viewData = new IndexViewData(CurrentPerson, neptunePage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<StormwaterJurisdiction> IndexGridJsonData()
        {
            IndexGridSpec gridSpec;
            var jurisdictions = GetJurisdictionsAndGridSpec(out gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<StormwaterJurisdiction>(jurisdictions, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<StormwaterJurisdiction> GetJurisdictionsAndGridSpec(out IndexGridSpec gridSpec)
        {
            gridSpec = new IndexGridSpec();
            return HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.ToList();
        }

        [NeptuneViewFeature]
        public ViewResult Detail(StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey)
        {
            var stormwaterJurisdiction = stormwaterJurisdictionPrimaryKey.EntityObject;
           
            var viewData = new DetailViewData(CurrentPerson, stormwaterJurisdiction);
            return RazorView<Detail, DetailViewData>(viewData);        
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<ModeledCatchment> JurisdictionModeledCatchmentGridJsonData(StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey)
        {
            JurisdictionModeledCatchmentsGridSpec gridSpec;
            var modeledCatchments = GetModeledCatchmentsAndGridSpec(out gridSpec, stormwaterJurisdictionPrimaryKey.EntityObject);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ModeledCatchment>(modeledCatchments, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<ModeledCatchment> GetModeledCatchmentsAndGridSpec(out JurisdictionModeledCatchmentsGridSpec gridSpec, StormwaterJurisdiction stormwaterJurisdiction)
        {
            gridSpec = new JurisdictionModeledCatchmentsGridSpec(CurrentPerson);
            return stormwaterJurisdiction.ModeledCatchments.ToList();
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMP> JurisdictionTreatmentBMPGridJsonData(StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey)
        {
            var stormwaterJurisdiction = stormwaterJurisdictionPrimaryKey.EntityObject;
            TreatmentBMPGridSpec gridSpec;
            var treatmentBMPs = GetJurisdictionTreatmentBMPsAndGridSpec(out gridSpec, CurrentPerson, stormwaterJurisdiction);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMP>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<TreatmentBMP> GetJurisdictionTreatmentBMPsAndGridSpec(out TreatmentBMPGridSpec gridSpec, Person currentPerson, StormwaterJurisdiction stormwaterJurisdiction)
        {
            gridSpec = new TreatmentBMPGridSpec(currentPerson, false, false);
            return
                HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList()
                    .Where(x => x.CanView(CurrentPerson))
                    .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID)
                    .ToList();
        }

    }
}
