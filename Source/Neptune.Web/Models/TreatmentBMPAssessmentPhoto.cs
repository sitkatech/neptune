using System;
using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPAssessmentPhoto : IAuditableEntity, IFileResourcePhoto
    {
        public string GetAuditDescriptionString()
        {
            return
                $"Treatment BMP Assessment Photo {FileResource.GetOriginalCompleteFileName() ?? "<File Name Not Found>"}";
        }

        public DateTime GetCreateDate()
        {
            return FileResource.CreateDate;
        }

        public string GetDeleteUrl()
        {
            return SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(c =>
                c.DeletePhoto(TreatmentBMPAssessmentPhotoID));
        }

        public string GetCaptionOnFullView()
        {
            return Caption;
        }

        public string GetCaptionOnGallery()
        {
            return Caption;
        }

        public string GetPhotoUrl()
        {
            return FileResource.GetFileResourceUrl();
        }

        public string PhotoUrlScaledThumbnail(int maxHeight)
        {
            return FileResource.FileResourceUrlScaledThumbnail(maxHeight);
        }

        public string GetEditUrl()
        {
            return "#";
        }

        public List<string> AdditionalCssClasses
        {
            get { return new List<string>(); }
        }

        public object OrderBy { get; set; }
    }
}
