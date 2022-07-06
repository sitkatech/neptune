using System.Collections.Generic;
using System.Web.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class UploadWqmpsViewData : NeptuneViewData
    {
        public string WqmpsUploadUrl { get; }
        public List<string> ErrorList { get; }



        public UploadWqmpsViewData(Person currentPerson, List<string> errorList, Models.NeptunePage neptunePage, string wqmpsUploadUrl) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = "WQMP Bulk Upload";
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            WqmpsUploadUrl = wqmpsUploadUrl;
            ErrorList = errorList;
        }
    }
}