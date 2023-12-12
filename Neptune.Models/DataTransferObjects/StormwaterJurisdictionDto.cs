namespace Neptune.Models.DataTransferObjects
{
    public class StormwaterJurisdictionDto
    {
        public int StormwaterJurisdictionID { get; set; }
        public int OrganizationID { get; set; }
        public int StateProvinceID { get; set; }
        public int StormwaterJurisdictionPublicBMPVisibilityTypeID { get; set; }
        public int StormwaterJurisdictionPublicWQMPVisibilityTypeID { get; set; }
        public OrganizationSimpleDto Organization { get; set; }
    }
}