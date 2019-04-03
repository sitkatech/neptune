using Neptune.Web.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class UploadTreatmentBMPsViewData : NeptuneViewData
    {
        public string TreatmentBMPsUploadUrl { get; }


        public UploadTreatmentBMPsViewData(Person currentPerson, string treatmentBMPsUploadUrl) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = "BMP Bulk Upload";
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            TreatmentBMPsUploadUrl = treatmentBMPsUploadUrl;
        }
    }
}