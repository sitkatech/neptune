using System;

namespace Hippocamp.Models.DataTransferObjects.Person
{
    public class PersonCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public int? RoleID { get; set; }
        public string LoginName { get; set; }
        public Guid UserGuid { get; set; }
    }
}