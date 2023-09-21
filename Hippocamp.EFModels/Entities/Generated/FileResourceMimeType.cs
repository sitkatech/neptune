using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("FileResourceMimeType")]
    [Index("FileResourceMimeTypeDisplayName", Name = "AK_FileResourceMimeType_FileResourceMimeTypeDisplayName", IsUnique = true)]
    [Index("FileResourceMimeTypeName", Name = "AK_FileResourceMimeType_FileResourceMimeTypeName", IsUnique = true)]
    public partial class FileResourceMimeType
    {
        public FileResourceMimeType()
        {
            FileResources = new HashSet<FileResource>();
        }

        [Key]
        public int FileResourceMimeTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string FileResourceMimeTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string FileResourceMimeTypeDisplayName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string FileResourceMimeTypeContentTypeName { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string FileResourceMimeTypeIconSmallFilename { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string FileResourceMimeTypeIconNormalFilename { get; set; }

        [InverseProperty("FileResourceMimeType")]
        public virtual ICollection<FileResource> FileResources { get; set; }
    }
}
