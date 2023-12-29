//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SupportRequestLog]

namespace Neptune.Models.DataTransferObjects
{
    public partial class SupportRequestLogSimpleDto
    {
        public int SupportRequestLogID { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestPersonName { get; set; }
        public string RequestPersonEmail { get; set; }
        public int? RequestPersonID { get; set; }
        public int SupportRequestTypeID { get; set; }
        public string RequestDescription { get; set; }
        public string RequestPersonOrganization { get; set; }
        public string RequestPersonPhone { get; set; }
    }
}