using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPImage : IFileResourcePhoto, IAuditableEntity
    {
        public string AuditDescriptionString => $"Site {TreatmentBMPID} {FileResource?.OriginalCompleteFileName ?? "File Resouce Not Found"}";
        public int? EntityImageIDAsNullable => TreatmentBMPID;
        public DateTime CreateDate => FileResource.CreateDate;
        public string DeleteUrl => SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Edit(TreatmentBMPID));
        public bool IsKeyPhoto => false;
        public string CaptionOnFullView => $"{Caption}";
        public string CaptionOnGallery => Caption;
        public string GetCaptionOnGallery() => $"{Caption}";
        public string PhotoUrl => FileResource.FileResourceUrl;
        public string PhotoUrlScaledThumbnail(int maxHeight)
        {  
            return FileResource.FileResourceUrlScaledThumbnail(maxHeight);
        } 
        public string EditUrl => SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.Edit(TreatmentBMPID));
        public List<string> AdditionalCssClasses { get; set; } = new List<string>();
        private object _orderBy;
        public object OrderBy
        {
            get => _orderBy ?? CaptionOnFullView;
            set => _orderBy = value;
        }
    }
}