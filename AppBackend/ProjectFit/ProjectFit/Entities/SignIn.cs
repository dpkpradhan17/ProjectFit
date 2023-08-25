using System.ComponentModel.DataAnnotations;

namespace ProjectFit.Entities
{
    public class SignIn
    {
        [Required]
        [EmailAddress(ErrorMessage="Enter a valid email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
