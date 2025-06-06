using System.ComponentModel.DataAnnotations;

namespace HONORE_API_MAIN.Models.DTOs
{
    public class BusinessRegistrationRequest
    {
        [Required]
        public string ContactPersonFirstName { get; set; } = string.Empty;

        [Required]
        public string ContactPersonLastName { get; set; } = string.Empty;

        [Required]
        public string EstablishmentName { get; set; } = string.Empty;

        public string? EstablishmentGSTNo { get; set; }

        public string? EstablishmentPhoneNo { get; set; }

        [Required]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? DoorNo { get; set; }

        public string? BuildingName { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string PinCode { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}