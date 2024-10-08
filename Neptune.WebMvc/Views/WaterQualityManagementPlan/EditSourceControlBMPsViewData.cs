﻿using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class EditSourceControlBMPsViewData : NeptuneViewData
    {
        public string WaterQualityManagementPlanDetailUrl { get; }

        public EditSourceControlBMPsViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            SubEntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
            PageTitle = "Edit Source Control BMPs";

            WaterQualityManagementPlanDetailUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(waterQualityManagementPlan.WaterQualityManagementPlanID));
        }
    }
}
