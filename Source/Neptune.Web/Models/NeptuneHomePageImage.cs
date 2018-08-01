using System;
using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class NeptuneHomePageImage : IFileResourcePhoto, IAuditableEntity
    {
        public DateTime CreateDate => FileResource.CreateDate;

        public string DeleteUrl
        {
            get { return SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(x => x.DeleteNeptuneHomePageImage(NeptuneHomePageImageID)); }
        }

        public bool IsKeyPhoto => false;

        public string CaptionOnFullView => $"{CaptionOnGallery}";

        public string CaptionOnGallery => $"{Caption}\r\n{FileResource.GetFileResourceDataLengthString()}";

        public string PhotoUrl => FileResource.GetFileResourceUrl();

        public string PhotoUrlScaledForPrint => FileResource.GetFileResourceUrlScaledForPrint();

        public string PhotoUrlScaledThumbnail(int maxHeight)
        {
            return FileResource.FileResourceUrlScaledThumbnail(maxHeight);
        }

        public string EditUrl
        {
            get { return SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(x => x.Edit(NeptuneHomePageImageID)); }
        }

        public List<string> AdditionalCssClasses { get; set; } = new List<string>();

        private object _orderBy;
        public object OrderBy
        {
            get => _orderBy ?? SortOrder;
            set => _orderBy = value;
        }

        public bool IsPersonTheCreator(Person person)
        {
            return FileResource.CreatePerson != null && person != null && person.PersonID == FileResource.CreatePersonID;
        }

        public string GetAuditDescriptionString() => $"Image: {Caption}";

        public int? EntityImageIDAsNullable => NeptuneHomePageImageID;
    }
}