using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    [Index("table_schema", "table_name", "pk_column", Name = "UQ__gt_pk_me__E9F04F89F4C92A3B", IsUnique = true)]
    public partial class gt_pk_metadatum
    {
        [Required]
        [StringLength(32)]
        [Unicode(false)]
        public string table_schema { get; set; }
        [Required]
        [StringLength(32)]
        [Unicode(false)]
        public string table_name { get; set; }
        [Required]
        [StringLength(32)]
        [Unicode(false)]
        public string pk_column { get; set; }
        public int? pk_column_idx { get; set; }
        [StringLength(32)]
        [Unicode(false)]
        public string pk_policy { get; set; }
        [StringLength(64)]
        [Unicode(false)]
        public string pk_sequence { get; set; }
    }
}
