using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class UploadTreatmentBMPsViewData : NeptuneViewData
    {
        public string TreatmentBMPsUploadUrl { get; }
        public IEnumerable<SelectListItem> TreatmentBMPTypes { get; }
        public List<string> ErrorList { get; }



        public UploadTreatmentBMPsViewData(Person currentPerson, IEnumerable<SelectListItem> treatmentBMPTypes, List<string> errorList, EFModels.Entities.NeptunePage neptunePage, string treatmentBMPsUploadUrl, LinkGenerator linkGenerator, HttpContext httpContext) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            PageTitle = "BMP Bulk Upload";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            TreatmentBMPsUploadUrl = treatmentBMPsUploadUrl;
            TreatmentBMPTypes = treatmentBMPTypes;
            ErrorList = errorList;
        }
    }
}