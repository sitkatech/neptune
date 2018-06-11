using System;
using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPAssessmentPhoto : IAuditableEntity, IFileResourcePhoto
    {
        public string AuditDescriptionString =>
            $"Treatment BMP Assessment Photo {FileResource?.OriginalCompleteFileName ?? "<File Name Not Found>"}";
        public DateTime CreateDate => FileResource.CreateDate;
        public string DeleteUrl =>
            SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(c => c.DeletePhoto(TreatmentBMPAssessmentPhotoID));
        public string CaptionOnFullView => Caption;
        public string CaptionOnGallery => Caption;
        public string PhotoUrl => FileResource.FileResourceUrl;
        public string PhotoUrlScaledThumbnail(int maxHeight) => FileResource.FileResourceUrlScaledThumbnail(maxHeight);
        public string EditUrl => "#";
        public List<string> AdditionalCssClasses => new List<string>();
        public object OrderBy { get; set; }
    }
}
