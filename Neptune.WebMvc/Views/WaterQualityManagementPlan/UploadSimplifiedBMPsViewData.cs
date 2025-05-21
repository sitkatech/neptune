﻿using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class UploadSimplifiedBMPsViewData : NeptuneViewData
    {
        public string WqmpsUploadUrl { get; }
        public string WqmpIndexUrl { get; }
        public List<string> ErrorList { get; }
        public string UploadTemplateUrl { get; }
        
        public UploadSimplifiedBMPsViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, List<string> errorList, EFModels.Entities.NeptunePage neptunePage, string wqmpsUploadUrl) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "Simplified BMPs Bulk Upload";
            EntityName = $"Data Hub";
            EntityUrl = SitkaRoute<DataHubController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            WqmpsUploadUrl = wqmpsUploadUrl;
            WqmpIndexUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            ErrorList = errorList;
            UploadTemplateUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.SimplifiedBMPBulkUploadTemplate());
        }
    }
}