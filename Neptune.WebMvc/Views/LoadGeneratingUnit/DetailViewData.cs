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

using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.FieldVisit;
using Neptune.WebMvc.Views.HRUCharacteristic;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.Shared.HRUCharacteristics;
using Neptune.WebMvc.Views.Shared.ModeledPerformance;
using Neptune.WebMvc.Views.TreatmentBMP;
using System.Reflection.Metadata;

namespace Neptune.WebMvc.Views.LoadGeneratingUnit
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.LoadGeneratingUnit LoadGeneratingUnit { get; }
        public MapInitJson MapInitJson { get; }

        public HRUCharacteristicsViewData? HRUCharacteristicsViewData { get; }
        public string MapServiceUrl { get; }

        //public ModeledPerformanceViewData ModeledPerformanceViewData { get; }
        public string RegionalSubbasinDetailUrl { get; }
        public UrlTemplate<int> DetailUrlTemplate { get; }
        public string WaterQualityManagementPlanDetailUrl { get; }
        public UrlTemplate<int> WaterQualityManagementPlanDetailUrlTemplate { get; }
        public EFModels.Entities.TreatmentBMP? TreatmentBMP { get; }
        public EFModels.Entities.RegionalSubbasin? RegionalSubbasin { get; }
        public EFModels.Entities.WaterQualityManagementPlan? WaterQualityManagementPlan { get; }
        public GridSpec<vHRUCharacteristic> HRUCharacteristicsGridSpec { get; }
        public string HRUCharacteristicsGridName { get; }
        public string HRUCharacteristicsGridDataUrl { get; }
        public string TreatmentBMPDetailUrl { get; set; }

        public string DetailUrl { get; set; }

        public DetailViewData(HttpContext httpContext,
            LinkGenerator linkGenerator,
            WebConfiguration webConfiguration,
            Person currentPerson,
            EFModels.Entities.LoadGeneratingUnit loadGeneratingUnit,
            EFModels.Entities.RegionalSubbasin? regionalSubbasin,
            EFModels.Entities.TreatmentBMP? treatmentBMP,
            EFModels.Entities.WaterQualityManagementPlan? wqmp,
            EFModels.Entities.HRULog? hruLog,
            MapInitJson mapInitJson,
            HRUCharacteristicsViewData hruCharacteristicsViewData, string mapServiceUrl)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            LoadGeneratingUnit = loadGeneratingUnit;
            RegionalSubbasin = regionalSubbasin;
            TreatmentBMP = treatmentBMP;
            WaterQualityManagementPlan = wqmp;

            DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<LoadGeneratingUnitController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            DetailUrl = DetailUrlTemplate.ParameterReplace(loadGeneratingUnit.LoadGeneratingUnitID);
            RegionalSubbasinUrlTemplate = new UrlTemplate<int>(SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            RegionalSubbasinDetailUrl = regionalSubbasin == null
                ? string.Empty
                : RegionalSubbasinUrlTemplate.ParameterReplace(regionalSubbasin.RegionalSubbasinID);
            TreatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            TreatmentBMPDetailUrl = treatmentBMP == null ? string.Empty : TreatmentBMPDetailUrlTemplate.ParameterReplace(treatmentBMP.TreatmentBMPID);
            WaterQualityManagementPlanDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
            WaterQualityManagementPlanDetailUrl = wqmp == null ? string.Empty : WaterQualityManagementPlanDetailUrlTemplate.ParameterReplace(wqmp.WaterQualityManagementPlanID);

            PageTitle = loadGeneratingUnit.LoadGeneratingUnitID.ToString();
            EntityName = "Load Generating Units";
            EntityUrl = SitkaRoute<LoadGeneratingUnitController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            MapInitJson = mapInitJson;
            MapServiceUrl = mapServiceUrl;
            HRUCharacteristicsViewData = hruCharacteristicsViewData;

            HRUCharacteristicsGridSpec = new HRUCharacteristicGridSpec(LinkGenerator);
            HRUCharacteristicsGridName = "HRUCharacteristics";
            HRUCharacteristicsGridDataUrl = SitkaRoute<LoadGeneratingUnitController>.BuildUrlFromExpression(LinkGenerator, x => x.HRUCharacteristicGridJsonData(loadGeneratingUnit));

            HRURequest = hruLog?.HRURequest;
            HRUResponse = hruLog?.HRUResponse;
            HRURequestDate = hruLog?.RequestDate;
        }

        public DateTime? HRURequestDate { get; set; }

        public string? HRUResponse { get; set; }

        public string? HRURequest { get; set; }

        public UrlTemplate<int> RegionalSubbasinUrlTemplate { get; set; }

        public UrlTemplate<int> TreatmentBMPDetailUrlTemplate { get; set; }
    }
}
