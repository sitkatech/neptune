using System.Collections.Generic;
using System.Web.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class UploadTreatmentBMPsViewData : NeptuneViewData
    {
        public string TreatmentBMPsUploadUrl { get; }
        public IEnumerable<SelectListItem> TreatmentBMPTypes { get; }
        public List<string> ErrorList { get; }



        public UploadTreatmentBMPsViewData(Person currentPerson, IEnumerable<SelectListItem> treatmentBMPTypes, List<string> errorList, Models.NeptunePage neptunePage, string treatmentBMPsUploadUrl) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = "BMP Bulk Upload";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            TreatmentBMPsUploadUrl = treatmentBMPsUploadUrl;
            TreatmentBMPTypes = treatmentBMPTypes;
            ErrorList = errorList;
        }
    }
}