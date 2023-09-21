using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("NeptunePageImage")]
    [Index("NeptunePageImageID", "FileResourceID", Name = "AK_NeptunePageImage_NeptunePageImageID_FileResourceID", IsUnique = true)]
    public partial class NeptunePageImage
    {
        [Key]
        public int NeptunePageImageID { get; set; }
        public int NeptunePageID { get; set; }
        public int FileResourceID { get; set; }

        [ForeignKey("FileResourceID")]
        [InverseProperty("NeptunePageImages")]
        public virtual FileResource FileResource { get; set; }
        [ForeignKey("NeptunePageID")]
        [InverseProperty("NeptunePageImages")]
        public virtual NeptunePage NeptunePage { get; set; }
    }
}
