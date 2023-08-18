using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class NeptuneHomePageImage : IFileResourcePhoto, IAuditableEntity
    {
        public DateTime GetCreateDate()
        {
            return FileResource.CreateDate;
        }

        public string GetDeleteUrl()
        {
            return "";//todo: SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(x => x.DeleteNeptuneHomePageImage(NeptuneHomePageImageID));
        }

        public bool IsKeyPhoto()
        {
            return false;
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

        public string GetPhotoUrlScaledForPrint()
        {
            return FileResource.GetFileResourceUrlScaledForPrint();
        }

        public string PhotoUrlScaledThumbnail(int maxHeight)
        {
            return FileResource.FileResourceUrlScaledThumbnail(maxHeight);
        }

        public string GetEditUrl()
        {
            return "";//todo SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(x => x.Edit(NeptuneHomePageImageID));
        }

        public List<string> GetAdditionalCssClasses() => new();

        public bool IsPersonTheCreator(Person person)
        {
            return FileResource.CreatePerson != null && person != null && person.PersonID == FileResource.CreatePersonID;
        }

        public string GetAuditDescriptionString()
        {
            return $"Image: {Caption}";
        }

        public int? GetEntityImageIDAsNullable()
        {
            return NeptuneHomePageImageID;
        }
    }
}