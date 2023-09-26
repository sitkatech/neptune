//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FileResource]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class FileResourceDto
    {
        public int FileResourceID { get; set; }
        public FileResourceMimeTypeDto FileResourceMimeType { get; set; }
        public string OriginalBaseFilename { get; set; }
        public string OriginalFileExtension { get; set; }
        public Guid FileResourceGUID { get; set; }
        public byte[] FileResourceData { get; set; }
        public PersonDto CreatePerson { get; set; }
        public DateTime CreateDate { get; set; }
        public bool InBlobStorage { get; set; }
    }

    public partial class FileResourceSimpleDto
    {
        public int FileResourceID { get; set; }
        public int FileResourceMimeTypeID { get; set; }
        public string OriginalBaseFilename { get; set; }
        public string OriginalFileExtension { get; set; }
        public Guid FileResourceGUID { get; set; }
        public byte[] FileResourceData { get; set; }
        public int CreatePersonID { get; set; }
        public DateTime CreateDate { get; set; }
        public bool InBlobStorage { get; set; }
    }

}