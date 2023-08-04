//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SupportRequestLog]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class SupportRequestLogDto
    {
        public int SupportRequestLogID { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestPersonName { get; set; }
        public string RequestPersonEmail { get; set; }
        public PersonDto RequestPerson { get; set; }
        public SupportRequestTypeDto SupportRequestType { get; set; }
        public string RequestDescription { get; set; }
        public string RequestPersonOrganization { get; set; }
        public string RequestPersonPhone { get; set; }
    }

    public partial class SupportRequestLogSimpleDto
    {
        public int SupportRequestLogID { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestPersonName { get; set; }
        public string RequestPersonEmail { get; set; }
        public System.Int32? RequestPersonID { get; set; }
        public System.Int32 SupportRequestTypeID { get; set; }
        public string RequestDescription { get; set; }
        public string RequestPersonOrganization { get; set; }
        public string RequestPersonPhone { get; set; }
    }

}