using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPAssessmentPhoto : IAuditableEntity, IFileResourcePhoto
    {
        public string GetAuditDescriptionString()
        {
            return
                $"Treatment BMP Assessment Photo {FileResource?.GetOriginalCompleteFileName() ?? "File Resource Not Found"}";
        }

        public DateTime GetCreateDate()
        {
            return FileResource.CreateDate;
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

        public List<string> GetAdditionalCssClasses()
        {
            return new List<string>();
        }
    }
}
