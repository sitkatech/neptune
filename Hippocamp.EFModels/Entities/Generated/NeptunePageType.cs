using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("NeptunePageType")]
    [Index(nameof(NeptunePageTypeDisplayName), Name = "AK_NeptunePageType_NeptunePageTypeDisplayName", IsUnique = true)]
    [Index(nameof(NeptunePageTypeName), Name = "AK_NeptunePageType_NeptunePageTypeName", IsUnique = true)]
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
        public string NeptunePageTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string NeptunePageTypeDisplayName { get; set; }

        [InverseProperty(nameof(NeptunePage.NeptunePageType))]
        public virtual ICollection<NeptunePage> NeptunePages { get; set; }
    }
}
