using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
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
        public string NeptunePageContent { get; set; }

        [ForeignKey(nameof(NeptunePageTypeID))]
        [InverseProperty("NeptunePages")]
        public virtual NeptunePageType NeptunePageType { get; set; }
        [InverseProperty(nameof(NeptunePageImage.NeptunePage))]
        public virtual ICollection<NeptunePageImage> NeptunePageImages { get; set; }
    }
}
