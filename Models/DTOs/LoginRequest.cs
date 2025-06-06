// Models/DTOs/LoginRequest.cs
using System.ComponentModel.DataAnnotations;

namespace HONORE_API_MAIN.Models.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string EmailOrMobile { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}