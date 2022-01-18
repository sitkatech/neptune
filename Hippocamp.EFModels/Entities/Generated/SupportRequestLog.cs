using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("SupportRequestLog")]
    public partial class SupportRequestLog
    {
        [Key]
        public int SupportRequestLogID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RequestDate { get; set; }
        [Required]
        [StringLength(200)]
        public string RequestPersonName { get; set; }
        [Required]
        [StringLength(256)]
        public string RequestPersonEmail { get; set; }
        public int? RequestPersonID { get; set; }
        public int SupportRequestTypeID { get; set; }
        [Required]
        [StringLength(2000)]
        public string RequestDescription { get; set; }
        [StringLength(500)]
        public string RequestPersonOrganization { get; set; }
        [StringLength(50)]
        public string RequestPersonPhone { get; set; }

        [ForeignKey(nameof(RequestPersonID))]
        [InverseProperty(nameof(Person.SupportRequestLogs))]
        public virtual Person RequestPerson { get; set; }
        [ForeignKey(nameof(SupportRequestTypeID))]
        [InverseProperty("SupportRequestLogs")]
        public virtual SupportRequestType SupportRequestType { get; set; }
    }
}
