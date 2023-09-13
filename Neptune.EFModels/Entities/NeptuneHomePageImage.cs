namespace Neptune.EFModels.Entities
{
    public partial class NeptuneHomePageImage
    {
        public DateTime GetCreateDate()
        {
            return FileResource.CreateDate;
        }

        public string GetDeleteUrl()
        {
            return "";//todo: SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(_linkGenerator, x => x.DeleteNeptuneHomePageImage(NeptuneHomePageImageID));
        }

        public string GetCaptionOnFullView()
        {
            return $"{GetCaptionOnGallery()}";
        }

        public string GetCaptionOnGallery()
        {
            return $"{Caption}\r\n{FileResource.GetFileResourceDataLengthString()}";
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
            return "";//todo SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(_linkGenerator, x => x.Edit(NeptuneHomePageImageID));
        }

        public List<string> GetAdditionalCssClasses() => new();
    }
}