using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.WebMvc.Views.TreatmentBMPImage
{
    public class ManageTreatmentBMPImagesViewData : NeptuneViewData
    {
        public ManagePhotosWithPreviewViewData ManagePhotosWithPreviewViewData { get; }

        public ManageTreatmentBMPImagesViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP, ManagePhotosWithPreviewViewData managePhotosWithPreviewViewData)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            SubEntityName = treatmentBMP.TreatmentBMPName;
            SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));
            PageTitle = "Manage Photos";
            ManagePhotosWithPreviewViewData = managePhotosWithPreviewViewData;
        }
    }
}
