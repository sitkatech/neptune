﻿namespace Neptune.Models.DataTransferObjects
{
    public class ProjectDto
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
        public string OCTAWatersheds { get; set; }
        public double? PollutantVolume { get; set; }
        public double? PollutantMetals { get; set; }
        public double? PollutantBacteria { get; set; }
        public double? PollutantNutrients { get; set; }
        public double? PollutantTSS { get; set; }
        public double? TPI { get; set; }
        public double? SEA { get; set; }
        public double? DryWeatherWQLRI { get; set; }
        public double? WetWeatherWQLRI { get; set; }
        public double? AreaTreatedAcres { get; set; }
        public double? ImperviousAreaTreatedAcres { get; set; }
        public int? UpdatePersonID { get; set; }
        public DateTime? DateUpdated { get; set; }
        public OrganizationSimpleDto Organization { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public ProjectStatusSimpleDto ProjectStatus { get; set; }
        public PersonSimpleDto PrimaryContactPerson { get; set; }
        public PersonSimpleDto CreatePerson { get; set; }
        public bool HasModeledResults { get; set; }
    }
}