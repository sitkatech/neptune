﻿using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class NewWqmpVerifyViewData : NeptuneViewData
    {
        public EFModels.Entities.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public List<WaterQualityManagementPlanVerifyType> WaterQualityManagementPlanVerifyTypes { get; }
        public List<WaterQualityManagementPlanVisitStatus> WaterQualityManagementPlanVisitStatuses { get; }
        public IEnumerable<SelectListItem> WaterQualityManagementPlanVerifyStatuses { get; }
        public string WaterQualityManagementPlanUrl { get; }

        public NewWqmpVerifyViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, 
            EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan, 
            List<WaterQualityManagementPlanVerifyType>  waterQualityManagementPlanVerifyTypes, 
            List<WaterQualityManagementPlanVisitStatus> waterQualityManagementPlanVisitStatuses,
            List<WaterQualityManagementPlanVerifyStatus>  waterQualityManagementPlanVerifyStatuses) 
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            WaterQualityManagementPlan = waterQualityManagementPlan;
            PageTitle = "New Water Quality Management Plan O&M Verification";

            SubEntityName = WaterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));

            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());

            WaterQualityManagementPlanVerifyTypes = waterQualityManagementPlanVerifyTypes;
            WaterQualityManagementPlanVisitStatuses = waterQualityManagementPlanVisitStatuses;
            WaterQualityManagementPlanVerifyStatuses = waterQualityManagementPlanVerifyStatuses.OrderBy(x => x.WaterQualityManagementPlanVerifyStatusName)
                .ToSelectListWithEmptyFirstRow(x => x.WaterQualityManagementPlanVerifyStatusID.ToString(), x => x.WaterQualityManagementPlanVerifyStatusName, "Status Not Set");

            WaterQualityManagementPlanUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                    x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
        }
    }
}