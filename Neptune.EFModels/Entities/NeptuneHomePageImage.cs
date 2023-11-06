using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class NeptuneHomePageImage
    {
        public DateTime GetCreateDate()
        {
            return FileResource.CreateDate;
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

        public List<string> GetAdditionalCssClasses() => new();

        public async Task DeleteFull(NeptuneDbContext dbContext)
        {
            await dbContext.NeptuneHomePageImages.Where(x => x.NeptuneHomePageImageID == NeptuneHomePageImageID)
                .ExecuteDeleteAsync();
        }
    }
}