using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    [Index(nameof(table_schema), nameof(table_name), nameof(pk_column), Name = "UQ__gt_pk_me__E9F04F89F4C92A3B", IsUnique = true)]
    public partial class gt_pk_metadatum
    {
        [Required]
        [StringLength(32)]
        public string table_schema { get; set; }
        [Required]
        [StringLength(32)]
        public string table_name { get; set; }
        [Required]
        [StringLength(32)]
        public string pk_column { get; set; }
        public int? pk_column_idx { get; set; }
        [StringLength(32)]
        public string pk_policy { get; set; }
        [StringLength(64)]
        public string pk_sequence { get; set; }
    }
}
