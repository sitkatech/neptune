using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("NeptuneArea")]
    [Index("NeptuneAreaDisplayName", Name = "AK_NeptuneArea_NeptuneAreaDisplayName", IsUnique = true)]
    [Index("NeptuneAreaName", Name = "AK_NeptuneArea_NeptuneAreaName", IsUnique = true)]
    public partial class NeptuneArea
    {
        [Key]
        public int NeptuneAreaID { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string NeptuneAreaName { get; set; }
        [Required]
        [StringLength(40)]
        [Unicode(false)]
        public string NeptuneAreaDisplayName { get; set; }
        public int SortOrder { get; set; }
        public bool ShowOnPrimaryNavigation { get; set; }
    }
}
