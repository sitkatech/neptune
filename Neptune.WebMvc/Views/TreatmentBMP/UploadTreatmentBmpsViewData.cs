using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class UploadTreatmentBMPsViewData : NeptuneViewData
    {
        public string TreatmentBMPsUploadUrl { get; }
        public IEnumerable<SelectListItem> TreatmentBMPTypes { get; }
        public List<string> ErrorList { get; }



        public UploadTreatmentBMPsViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            IEnumerable<SelectListItem> treatmentBMPTypes, List<string> errorList, EFModels.Entities.NeptunePage neptunePage,
            string treatmentBMPsUploadUrl) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "BMP Bulk Upload";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            TreatmentBMPsUploadUrl = treatmentBMPsUploadUrl;
            TreatmentBMPTypes = treatmentBMPTypes;
            ErrorList = errorList;
        }
    }
}