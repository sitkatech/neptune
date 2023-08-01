using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class UploadWqmpsViewData : NeptuneViewData
    {
        public string WqmpsUploadUrl { get; }
        public string WqmpIndexUrl { get; }
        public List<string> ErrorList { get; }
        
        public UploadWqmpsViewData(Person currentPerson, List<string> errorList, Models.NeptunePage neptunePage, string wqmpsUploadUrl) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = "WQMP Bulk Upload";
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            WqmpsUploadUrl = wqmpsUploadUrl;
            WqmpIndexUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
                c.Index());
            ErrorList = errorList;
        }
    }
}