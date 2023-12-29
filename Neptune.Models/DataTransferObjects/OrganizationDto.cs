namespace Neptune.Models.DataTransferObjects;

public class OrganizationDto
{
    public int OrganizationID { get; set; }
    public Guid? OrganizationGuid { get; set; }
    public string OrganizationName { get; set; }
    public string OrganizationShortName { get; set; }
    public PersonDto PrimaryContactPerson { get; set; }
    public bool IsActive { get; set; }
    public string OrganizationUrl { get; set; }
    public FileResourceDto LogoFileResource { get; set; }
    public OrganizationTypeSimpleDto OrganizationType { get; set; }
}