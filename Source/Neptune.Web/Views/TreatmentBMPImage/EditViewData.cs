using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPImage
{
    public class EditViewData : NeptuneViewData
    {
        public readonly Dictionary<int, Models.TreatmentBMPImage> TreatmentBMPImagesByID;

        public EditViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP) : base(currentPerson)
        {
            TreatmentBMPImagesByID = treatmentBMP.TreatmentBMPImages.ToDictionary(x => x.TreatmentBMPImageID, x => x);
        }
    }
}
