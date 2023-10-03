using System;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects.Person
{
    public class PersonCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string LoginName { get; set; }
        [Required]
        public Guid UserGuid { get; set; }
    }
}