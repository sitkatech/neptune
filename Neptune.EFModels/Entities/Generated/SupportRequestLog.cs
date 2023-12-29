using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("SupportRequestLog")]
public partial class SupportRequestLog
{
    [Key]
    public int SupportRequestLogID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RequestDate { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? RequestPersonName { get; set; }

    [StringLength(256)]
    [Unicode(false)]
    public string? RequestPersonEmail { get; set; }

    public int? RequestPersonID { get; set; }

    public int SupportRequestTypeID { get; set; }

    [StringLength(2000)]
    [Unicode(false)]
    public string? RequestDescription { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? RequestPersonOrganization { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? RequestPersonPhone { get; set; }

    [ForeignKey("RequestPersonID")]
    [InverseProperty("SupportRequestLogs")]
    public virtual Person? RequestPerson { get; set; }
}
