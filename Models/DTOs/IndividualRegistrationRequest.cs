// Models/DTOs/IndividualRegistrationRequest.cs
using System.ComponentModel.DataAnnotations;

namespace HONORE_API_MAIN.Models.DTOs
{
    public class IndividualRegistrationRequest
    {
        public string? CommunityName { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? DoorNo { get; set; }

        public string? ApartmentName { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string PinCode { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}