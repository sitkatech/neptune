namespace Neptune.Models.DataTransferObjects;

public class OrganizationUpsertDto
{
    public Guid? OrganizationGuid { get; set; }
    public string OrganizationName { get; set; }
    public string OrganizationShortName { get; set; }
    public int? PrimaryContactPersonID { get; set; }
    public bool IsActive { get; set; }
    public string OrganizationUrl { get; set; }
    public int? LogoFileResourceID { get; set; }
    public int OrganizationTypeID { get; set; }
}