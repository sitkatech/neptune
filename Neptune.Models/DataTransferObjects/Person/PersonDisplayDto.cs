namespace Neptune.Models.DataTransferObjects;

public class PersonDisplayDto
{
    public int PersonID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int OrganizationID { get; set; }
    public string OrganizationName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}