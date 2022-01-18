using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("GoogleChartType")]
    [Index(nameof(GoogleChartTypeDisplayName), Name = "AK_GoogleChartType_GoogleChartTypeDisplayName", IsUnique = true)]
    [Index(nameof(GoogleChartTypeName), Name = "AK_GoogleChartType_GoogleChartTypeName", IsUnique = true)]
    public partial class GoogleChartType
    {
        [Key]
        public int GoogleChartTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string GoogleChartTypeName { get; set; }
        [Required]
        [StringLength(50)]
        public string GoogleChartTypeDisplayName { get; set; }
        [StringLength(50)]
        public string SeriesDataDisplayType { get; set; }
    }
}
