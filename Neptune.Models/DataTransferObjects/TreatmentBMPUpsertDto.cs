using System;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace Neptune.Models.DataTransferObjects
{
    public class TreatmentBMPUpsertDto
    {
        public int TreatmentBMPID { get; set; }
        [Required]
        [Display(Name="Name")]
        public string? TreatmentBMPName { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int? TreatmentBMPTypeID { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public int? TreatmentBMPModelingTypeID { get; set; }
        public string StormwaterJurisdictionName { get; set; }
        public string WatershedName { get; set; }
        [Required]
        [Display(Name = "Longitude")]
        public double? Longitude { get; set; }
        [Required]
        [Display(Name = "Latitude")]
        public double? Latitude { get; set; }
        public string Notes { get; set; }
        public List<CustomAttributeUpsertDto> ModelingAttributes { get; set; }
        public bool? AreAllModelingAttributesComplete { get; set; }
        public bool? IsFullyParameterized { get; set; }

    }
}