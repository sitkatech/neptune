﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects
{
    public class ProjectUpsertDto
    {
        [Display(Name = "Project Name")]
        [Required]
        [StringLength(200, ErrorMessage = "Project Name cannot exceed 200 characters.")]
        public string ProjectName { get; set; }
        [Display(Name = "Project Owner")]
        [Required]
        public int? OrganizationID { get; set; }
        [Display(Name = "Jurisdiction")]
        [Required]
        public int? StormwaterJurisdictionID { get; set; }
        [Required]
        public int? PrimaryContactPersonID { get; set; }
        [StringLength(500, ErrorMessage = "Project Description cannot exceed 500 characters.")]
        public string ProjectDescription { get; set; }
        [StringLength(500, ErrorMessage = "Additional Contact Information field cannot exceed 500 characters.")]
        public string AdditionalContactInformation { get; set; }
        public bool? DoesNotIncludeTreatmentBMPs { get; set; }
        public bool? CalculateOCTAM2Tier2Scores { get; set; }
        public bool? ShareOCTAM2Tier2Scores { get; set; }
    }
}