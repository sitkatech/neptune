namespace Hippocamp.Models.DataTransferObjects
{
    public partial class UserSimpleDto
    {
        public string FullName => $"{FirstName} {LastName}";
    }
}