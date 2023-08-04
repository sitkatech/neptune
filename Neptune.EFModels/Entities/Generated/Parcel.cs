using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("Parcel")]
public partial class Parcel
{
    [Key]
    public int ParcelID { get; set; }

    [StringLength(22)]
    [Unicode(false)]
    public string? ParcelNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? OwnerName { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ParcelStreetNumber { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? ParcelAddress { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string? ParcelZipCode { get; set; }

    [StringLength(4)]
    [Unicode(false)]
    public string? LandUse { get; set; }

    public int? SquareFeetHome { get; set; }

    public int? SquareFeetLot { get; set; }

    public double ParcelAreaInAcres { get; set; }

    [InverseProperty("Parcel")]
    public virtual ParcelGeometry? ParcelGeometry { get; set; }

    [InverseProperty("Parcel")]
    public virtual ICollection<WaterQualityManagementPlanParcel> WaterQualityManagementPlanParcels { get; set; } = new List<WaterQualityManagementPlanParcel>();
}
