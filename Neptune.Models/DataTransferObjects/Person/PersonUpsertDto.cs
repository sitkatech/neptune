using System;

namespace Neptune.Models.DataTransferObjects.Person
{
    public class PersonUpsertDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public int? RoleID { get; set; }
    }
}