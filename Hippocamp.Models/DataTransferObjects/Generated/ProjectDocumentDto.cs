//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectDocument]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class ProjectDocumentDto
    {
        public int ProjectDocumentID { get; set; }
        public FileResourceDto FileResource { get; set; }
        public ProjectDto Project { get; set; }
        public string DisplayName { get; set; }
        public DateTime UploadDate { get; set; }
        public string DocumentDescription { get; set; }
    }

    public partial class ProjectDocumentSimpleDto
    {
        public int ProjectDocumentID { get; set; }
        public int FileResourceID { get; set; }
        public int ProjectID { get; set; }
        public string DisplayName { get; set; }
        public DateTime UploadDate { get; set; }
        public string DocumentDescription { get; set; }
    }

}