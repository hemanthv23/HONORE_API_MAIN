using HONORE_API_MAIN.Models.DTOs;

// Models / DTOs / AuthResponse.cs
namespace HONORE_API_MAIN.Models.DTOs
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public UserInfo? User { get; set; }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public CustomerType CustomerType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EstablishmentName { get; set; }
        public string? ContactPersonFirstName { get; set; }
        public string? ContactPersonLastName { get; set; }
    }
}
