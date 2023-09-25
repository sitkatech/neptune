using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("NeptunePageType")]
    [Index("NeptunePageTypeDisplayName", Name = "AK_NeptunePageType_NeptunePageTypeDisplayName", IsUnique = true)]
    [Index("NeptunePageTypeName", Name = "AK_NeptunePageType_NeptunePageTypeName", IsUnique = true)]
    public partial class NeptunePageType
    {
        public NeptunePageType()
        {
            NeptunePages = new HashSet<NeptunePage>();
        }

        [Key]
        public int NeptunePageTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string NeptunePageTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string NeptunePageTypeDisplayName { get; set; }

        [InverseProperty("NeptunePageType")]
        public virtual ICollection<NeptunePage> NeptunePages { get; set; }
    }
}
