/*-----------------------------------------------------------------------
<copyright file="AccountController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using LtInfo.Common.Views;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TrashGeneratingUnit;
using Index = Neptune.Web.Views.TrashGeneratingUnit.Index;
using IndexViewData = Neptune.Web.Views.TrashGeneratingUnit.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class TrashGeneratingUnitController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<TrashGeneratingUnit> TrashGeneratingUnitGridJsonData()
        {
            // ReSharper disable once InconsistentNaming
            var treatmentBMPs = GetTrashGeneratingUnitsAndGridSpec(out var gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TrashGeneratingUnit>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<TrashGeneratingUnit> GetTrashGeneratingUnitsAndGridSpec(out TrashGeneratingUnitGridSpec gridSpec)
        {
            gridSpec = new TrashGeneratingUnitGridSpec();

            return HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits.Include(x => x.TreatmentBMP)
                .Include(x => x.OnlandVisualTrashAssessmentArea).Include(x => x.LandUseBlock)
                .Include(x => x.StormwaterJurisdiction.Organization).ToList();
        }
    }
}
namespace Neptune.Web.Views.TrashGeneratingUnit
{

    public class TrashGeneratingUnitGridSpec : GridSpec<Models.TrashGeneratingUnit>
    {
        public TrashGeneratingUnitGridSpec()
        {

            Add("Trash Generating Unit ID", x => x.TrashGeneratingUnitID.ToString(CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add("Land Use Type", x =>
            {
                if (x.LandUseBlock == null)
                {
                    return "No data provided";
                }

                return x.LandUseBlock?.PriorityLandUseType?.PriorityLandUseTypeDisplayName ?? "Not Priority Land Use";
            }, 140, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Governing OVTA Area", x => x.OnlandVisualTrashAssessmentArea?.GetDisplayNameAsDetailUrlNoPermissionCheck() ?? new HtmlString(""), 300, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Governing Treatment BMP", x => x.TreatmentBMP?.GetDisplayNameAsUrl() ?? new HtmlString(""), 195, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Stormwater Jurisdiction", x => x.StormwaterJurisdiction?.GetDisplayNameAsDetailUrl() ?? new HtmlString(""), 170, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Area", x => ((x.TrashGeneratingUnitGeometry.Area ?? 0) * DbSpatialHelper.SqlGeometryAreaToAcres).ToString("F2", CultureInfo.InvariantCulture), 100, DhtmlxGridColumnFilterType.Numeric);
        }

    }

    public abstract class Index : TypedWebViewPage<IndexViewData>
    {
    }
    public class IndexViewData : NeptuneViewData
    {
        public TrashGeneratingUnitGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public IndexViewData(Person currentPerson) : base (currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Trash Generating Unit";
            PageTitle = "Index";
            GridSpec = new TrashGeneratingUnitGridSpec() { ObjectNameSingular = "Absolute Unit", ObjectNamePlural = "Absolute Units", SaveFiltersInCookie = true };
            GridName = "absoluteUnitsGrid";
            GridDataUrl = SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(j => j.TrashGeneratingUnitGridJsonData());
        }
    }
}
