using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.TreatmentBMPImage
{
    public class ManageTreatmentBMPImagesViewData : NeptuneViewData
    {
        public Models.TreatmentBMP TreatmentBMP { get; }
        public ManagePhotosWithPreviewViewData ManagePhotosWithPreviewViewData { get; }

        public ManageTreatmentBMPImagesViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP, ManagePhotosWithPreviewViewData managePhotosWithPreviewViewData)
            : base(currentPerson)
        {
            TreatmentBMP = treatmentBMP;
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.FindABMP());
            SubEntityName = treatmentBMP.TreatmentBMPName;
            SubEntityUrl = treatmentBMP.GetDetailUrl();
            PageTitle = "Manage Photos";
            ManagePhotosWithPreviewViewData = managePhotosWithPreviewViewData;
        }
    }
}
