namespace Neptune.Models.DataTransferObjects
{
    public partial class PersonSimpleDto
    {
        public string FullName => $"{FirstName} {LastName}";
    }
}