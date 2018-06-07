using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Wordprocessing;
using LtInfo.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentPhotosViewModel : FormViewModel
    {
        public IList<TreatmentBmpAssessmentPhotoSimple> TreatmentBmpAssessmentPhotoSimples { get; set; }

        [DisplayName("Photo to upload")]
        [SitkaFileExtensions("jpg|jpeg|gif|png")]
        public HttpPostedFileBase Photo { get; set; }

        [DisplayName("Caption")]
        [MaxLength(TreatmentBMPAssessmentPhoto.FieldLengths.Caption)]
        public string Caption { get; set; }

        /// <summary>
        /// Needed by model binder
        /// </summary>
        public AssessmentPhotosViewModel()
        {
            TreatmentBmpAssessmentPhotoSimples = new List<TreatmentBmpAssessmentPhotoSimple>();
        }

        public AssessmentPhotosViewModel(Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            TreatmentBmpAssessmentPhotoSimples = treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Select(x =>
                new TreatmentBmpAssessmentPhotoSimple
                {
                    TreatmentBmpAssessmentPhotoID = x.TreatmentBMPAssessmentPhotoID,
                    Caption = x.Caption,
                    FlagForDeletion = false
                }).ToList();
        }

        public void UpdateModels(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            // Merge existing photos
            var treatmentBMPAssessmentPhotosToUpdate = TreatmentBmpAssessmentPhotoSimples.Select(x =>
                    x.FlagForDeletion ?? false // Exclude from list to update if flagged for deletion
                        ? null
                        : new TreatmentBMPAssessmentPhoto(ModelObjectHelpers.NotYetAssignedID,
                            ModelObjectHelpers.NotYetAssignedID)
                        {
                            TreatmentBMPAssessmentPhotoID = x.TreatmentBmpAssessmentPhotoID,
                            Caption = x.Caption
                        })
                .Where(x => x != null)
                .ToList();
            treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.Merge(treatmentBMPAssessmentPhotosToUpdate,
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessmentPhotos.Local,
                (x, y) => x.TreatmentBMPAssessmentPhotoID == y.TreatmentBMPAssessmentPhotoID,
                (x, y) => { x.Caption = y.Caption; });

            // Create new photo if it exists
            if (Photo != null)
            {
                var fileResource = FileResource.CreateNewFromHttpPostedFile(Photo, currentPerson);
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessmentPhotos.Add( 
                    new TreatmentBMPAssessmentPhoto(fileResource, treatmentBMPAssessment) {Caption = Caption});
            }
        }
    }
}
