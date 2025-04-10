﻿namespace Neptune.Models.DataTransferObjects;

public class PersonDto
{
    public int PersonID { get; set; }
    public Guid PersonGuid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int RoleID { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? LastActivityDate { get; set; }
    public bool IsActive { get; set; }
    public int OrganizationID { get; set; }
    public bool ReceiveSupportEmails { get; set; }
    public string LoginName { get; set; }
    public bool ReceiveRSBRevisionRequestEmails { get; set; }
    public Guid WebServiceAccessToken { get; set; }
    public bool IsOCTAGrantReviewer { get; set; }
    public bool HasAssignedStormwaterJurisdiction { get; set; }
}