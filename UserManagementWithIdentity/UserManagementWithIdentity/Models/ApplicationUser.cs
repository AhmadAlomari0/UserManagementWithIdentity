using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserManagementWithIdentity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage ="Maximum Letters are 25 letter."), MaxLength(25)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Maximum Letters are 25 letter."), MaxLength(25)]
        public string LastName { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}
