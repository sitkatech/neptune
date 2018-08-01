using System;
using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class NeptuneHomePageImage : IFileResourcePhoto, IAuditableEntity
    {
        public DateTime GetCreateDate()
        {
            return FileResource.CreateDate;
        }

        public string GetDeleteUrl()
        {
            return SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(x =>
                x.DeleteNeptuneHomePageImage(NeptuneHomePageImageID));
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
            return SitkaRoute<NeptuneHomePageImageController>.BuildUrlFromExpression(x => x.Edit(NeptuneHomePageImageID));
        }

        public List<string> AdditionalCssClasses { get; set; } = new List<string>();

        private object _orderBy;
        public object OrderBy
        {
            get { return _orderBy ?? SortOrder; }
            set { _orderBy = value; }
        }

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