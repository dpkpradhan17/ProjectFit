using System.ComponentModel.DataAnnotations;

namespace ProjectFit.Entities
{
    public class SignUp
    {
        [Required(ErrorMessage ="Please enter First Name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        public string Lastname { get; set; }

        [Required(ErrorMessage =("Please enter the valid country code"))]
        public int CountryCode { get; set; }

        [Required(ErrorMessage ="Please enter Mobile Number")]
        [Phone(ErrorMessage ="Please enter a valid mobile number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="Please enter email")]
        [EmailAddress(ErrorMessage ="Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please give a strong password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least 1 lowercase letter, 1 uppercase letter, 1 digit, and 1 special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Please re-enter the password")]
        [Compare("Password",ErrorMessage ="Password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
