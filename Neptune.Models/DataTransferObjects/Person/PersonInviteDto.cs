using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects.Person
{
    public class PersonInviteDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int? RoleID { get; set; }
    }
}