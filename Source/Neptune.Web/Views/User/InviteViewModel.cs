using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Views.User
{
    public class InviteViewModel : Shared.UserJurisdictions.EditUserJurisdictionsViewModel
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Organization")]
        public Guid? OrganizationGuid { get; set; }
    }
}
