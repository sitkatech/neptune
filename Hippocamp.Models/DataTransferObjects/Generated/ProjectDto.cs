//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Project]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class ProjectDto
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public OrganizationDto Organization { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public ProjectStatusDto ProjectStatus { get; set; }
        public PersonDto PrimaryContactPerson { get; set; }
        public PersonDto CreatePerson { get; set; }
        public DateTime DateCreated { get; set; }
        public string ProjectDescription { get; set; }
        public string AdditionalContactInformation { get; set; }
        public bool DoesNotIncludeTreatmentBMPs { get; set; }
        public bool CalculateOCTAM2Tier2Scores { get; set; }
        public bool ShareOCTAM2Tier2Scores { get; set; }
        public DateTime? OCTAM2Tier2ScoresLastSharedDate { get; set; }
    }

    public partial class ProjectSimpleDto
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int OrganizationID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int ProjectStatusID { get; set; }
        public int PrimaryContactPersonID { get; set; }
        public int CreatePersonID { get; set; }
        public DateTime DateCreated { get; set; }
        public string ProjectDescription { get; set; }
        public string AdditionalContactInformation { get; set; }
        public bool DoesNotIncludeTreatmentBMPs { get; set; }
        public bool CalculateOCTAM2Tier2Scores { get; set; }
        public bool ShareOCTAM2Tier2Scores { get; set; }
        public DateTime? OCTAM2Tier2ScoresLastSharedDate { get; set; }
    }

}