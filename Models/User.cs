// Models/User.cs
using System.ComponentModel.DataAnnotations;

namespace HONORE_API_MAIN.Models
{
    public enum CustomerType
    {
        Individual,
        Business
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        public CustomerType CustomerType { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? DoorNo { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string PinCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Individual-specific properties
        public string? CommunityName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ApartmentName { get; set; }

        // Business-specific properties
        public string? ContactPersonFirstName { get; set; }
        public string? ContactPersonLastName { get; set; }
        public string? EstablishmentName { get; set; }
        public string? EstablishmentGSTNo { get; set; }
        public string? EstablishmentPhoneNo { get; set; }
        public string? BuildingName { get; set; }
    }
}