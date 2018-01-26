using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPImage
{
    public class NewViewModel : FormViewModel
    {
        [DisplayName("Image"), Required]
        public HttpPostedFileBase Image { get; set; }

        [DisplayName("Caption"), Required, StringLength(Models.TreatmentBMPImage.FieldLengths.Caption)]
        public string Caption { get; set; }

        public void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson)
        {
            var fileResource = FileResource.CreateNewFromHttpPostedFileAndSave(Image, currentPerson);
            var treatmentBMPImage = new Models.TreatmentBMPImage(fileResource, treatmentBMP, Caption, DateTime.Today);
            treatmentBMP.TreatmentBMPImages.Add(treatmentBMPImage);
        }
    }
}
