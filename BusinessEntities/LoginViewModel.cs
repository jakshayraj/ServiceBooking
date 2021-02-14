using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email Id")]
        [Required(ErrorMessage = "Email Id required")]
        [DisplayName("Email Id")]
        public string emailid { get; set; }
        [Required(ErrorMessage = "Password required")]
        [DisplayName("Password")]
        public string password { get; set; }
    }
}
