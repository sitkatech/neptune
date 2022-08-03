using System;

namespace Hippocamp.Models.DataTransferObjects
{
    public partial class ProjectSimpleDto
    {
        public OrganizationSimpleDto Organization { get; set; }
        public StormwaterJurisdictionSimpleDto StormwaterJurisdiction { get; set; }
        public ProjectStatusSimpleDto ProjectStatus { get; set; }
        public PersonSimpleDto PrimaryContactPerson { get; set; }
        public PersonSimpleDto CreatePerson { get; set; }
        public bool HasModeledResults { get; set; }
    }
}