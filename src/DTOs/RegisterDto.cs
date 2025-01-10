using System.ComponentModel.DataAnnotations;

namespace Cat3_API.Src.DTOs
{
    public class RegisterDto
    {
        [EmailAddress]
        public required string Email { get; set; } = string.Empty;

        [MinLength(6)]
        [RegularExpression(".*\\d.*", ErrorMessage = "Password debe contener al menos un número.")]
        public required string Password { get; set; } = string.Empty;
    }
}
