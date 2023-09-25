using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("NeptunePage")]
    public partial class NeptunePage
    {
        public NeptunePage()
        {
            NeptunePageImages = new HashSet<NeptunePageImage>();
        }

        [Key]
        public int NeptunePageID { get; set; }
        public int NeptunePageTypeID { get; set; }
        [Unicode(false)]
        public string NeptunePageContent { get; set; }

        [ForeignKey("NeptunePageTypeID")]
        [InverseProperty("NeptunePages")]
        public virtual NeptunePageType NeptunePageType { get; set; }
        [InverseProperty("NeptunePage")]
        public virtual ICollection<NeptunePageImage> NeptunePageImages { get; set; }
    }
}
