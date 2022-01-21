namespace Hippocamp.Models.DataTransferObjects
{
    public partial class ProjectCreateDto
    {
        public string ProjectName { get; set; }
        public int OrganizationID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int PrimaryContactPersonID { get; set; }
        public string ProjectDescription { get; set; }
        public string AdditionalContactInformation { get; set; }
    }
}