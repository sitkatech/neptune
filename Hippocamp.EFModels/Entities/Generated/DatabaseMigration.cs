using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("DatabaseMigration")]
    public partial class DatabaseMigration
    {
        [Key]
        public int DatabaseMigrationNumber { get; set; }
    }
}
