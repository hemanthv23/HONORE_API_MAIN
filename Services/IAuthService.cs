using HONORE_API_MAIN.Models.DTOs;

namespace HONORE_API_MAIN.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterIndividualAsync(IndividualRegistrationRequest request);
        Task<AuthResponse> RegisterBusinessAsync(BusinessRegistrationRequest request);
        Task<IEnumerable<UserInfo>> GetAllUsersAsync();
    }
}
