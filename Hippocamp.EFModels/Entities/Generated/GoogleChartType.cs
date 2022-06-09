using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("GoogleChartType")]
    [Index("GoogleChartTypeDisplayName", Name = "AK_GoogleChartType_GoogleChartTypeDisplayName", IsUnique = true)]
    [Index("GoogleChartTypeName", Name = "AK_GoogleChartType_GoogleChartTypeName", IsUnique = true)]
    public partial class GoogleChartType
    {
        [Key]
        public int GoogleChartTypeID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string GoogleChartTypeName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string GoogleChartTypeDisplayName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SeriesDataDisplayType { get; set; }
    }
}
