using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.TreatmentBMPImage
{
    public class ManageTreatmentBMPImagesViewData : NeptuneViewData
    {
        public EFModels.Entities.TreatmentBMP TreatmentBMP { get; }
        public ManagePhotosWithPreviewViewData ManagePhotosWithPreviewViewData { get; }

        public ManageTreatmentBMPImagesViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP, ManagePhotosWithPreviewViewData managePhotosWithPreviewViewData)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBMP = treatmentBMP;
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            SubEntityName = treatmentBMP.TreatmentBMPName;
            SubEntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMP));
            PageTitle = "Manage Photos";
            ManagePhotosWithPreviewViewData = managePhotosWithPreviewViewData;
        }
    }
}
