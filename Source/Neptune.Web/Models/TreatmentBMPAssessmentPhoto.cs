using System;
using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPAssessmentPhoto : IAuditableEntity, IFileResourcePhoto
    {
        public string GetAuditDescriptionString() =>
            $"Treatment BMP Assessment Photo {FileResource.GetOriginalCompleteFileName() ?? "<File Name Not Found>"}";

        public DateTime GetCreateDate() => FileResource.CreateDate;

        public string GetDeleteUrl() =>
            SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(c =>
                c.DeletePhoto(TreatmentBMPAssessmentPhotoID));

        public string GetCaptionOnFullView() => Caption;
        public string GetCaptionOnGallery() => Caption;
        public string GetPhotoUrl() => FileResource.GetFileResourceUrl();
        public string PhotoUrlScaledThumbnail(int maxHeight) => FileResource.FileResourceUrlScaledThumbnail(maxHeight);
        public string GetEditUrl() => "#";
        public List<string> AdditionalCssClasses => new List<string>();
        public object OrderBy { get; set; }
    }
}
