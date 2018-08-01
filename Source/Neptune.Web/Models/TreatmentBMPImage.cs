using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPImage : IFileResourcePhoto, IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"Site {TreatmentBMPID} {FileResource.GetOriginalCompleteFileName() ?? "File Resouce Not Found"}";
        }

        public int? GetEntityImageIDAsNullable()
        {
            return TreatmentBMPID;
        }

        public DateTime GetCreateDate()
        {
            return FileResource.CreateDate;
        }

        public string GetDeleteUrl()
        {
            return SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Edit(TreatmentBMPID));
        }

        public bool IsKeyPhoto()
        {
            return false;
        }

        public string GetCaptionOnFullView()
        {
            return $"{Caption}";
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
            return SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Edit(TreatmentBMPID));
        }

        public List<string> AdditionalCssClasses { get; set; } = new List<string>();
        private object _orderBy;

        public object OrderBy
        {
            get { return _orderBy ?? GetCaptionOnFullView(); }
            set { _orderBy = value; }
        }
    }
}