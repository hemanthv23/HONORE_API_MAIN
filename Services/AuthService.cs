using HONORE_API_MAIN.Data;
using HONORE_API_MAIN.Models;
using HONORE_API_MAIN.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace HONORE_API_MAIN.Services
{
    public class AuthService : IAuthService
    {
        private readonly HonoreDBContext _context;

        public AuthService(HonoreDBContext context)
        {
            _context = context;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u =>
                        (u.Email == request.EmailOrMobile || u.MobileNumber == request.EmailOrMobile)
                        && u.IsActive);

                if (user == null || !VerifyPassword(request.Password, user.Password))
                {
                    return new AuthResponse
                    {
                        Success = false,
                        Message = "Invalid credentials"
                    };
                }

                return new AuthResponse
                {
                    Success = true,
                    Message = "Login successful",
                    User = MapToUserInfo(user)
                };
            }
            catch
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "An error occurred during login"
                };
            }
        }

        public async Task<AuthResponse> RegisterIndividualAsync(IndividualRegistrationRequest request)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                {
                    return new AuthResponse { Success = false, Message = "Email already exists" };
                }

                if (await _context.Users.AnyAsync(u => u.MobileNumber == request.MobileNumber))
                {
                    return new AuthResponse { Success = false, Message = "Mobile number already exists" };
                }

                var user = new User
                {
                    CustomerType = CustomerType.Individual,
                    Email = request.Email,
                    MobileNumber = request.MobileNumber,
                    Password = HashPassword(request.Password),
                    DoorNo = request.DoorNo,
                    Address = request.Address,
                    PinCode = request.PinCode,
                    CommunityName = request.CommunityName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    ApartmentName = request.ApartmentName,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new AuthResponse
                {
                    Success = true,
                    Message = "Individual registration successful",
                    User = MapToUserInfo(user)
                };
            }
            catch
            {
                return new AuthResponse { Success = false, Message = "An error occurred during registration" };
            }
        }

        public async Task<AuthResponse> RegisterBusinessAsync(BusinessRegistrationRequest request)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                {
                    return new AuthResponse { Success = false, Message = "Email already exists" };
                }

                if (await _context.Users.AnyAsync(u => u.MobileNumber == request.MobileNumber))
                {
                    return new AuthResponse { Success = false, Message = "Mobile number already exists" };
                }

                var user = new User
                {
                    CustomerType = CustomerType.Business,
                    Email = request.Email,
                    MobileNumber = request.MobileNumber,
                    Password = HashPassword(request.Password),
                    DoorNo = request.DoorNo,
                    Address = request.Address,
                    PinCode = request.PinCode,
                    ContactPersonFirstName = request.ContactPersonFirstName,
                    ContactPersonLastName = request.ContactPersonLastName,
                    EstablishmentName = request.EstablishmentName,
                    EstablishmentGSTNo = request.EstablishmentGSTNo,
                    EstablishmentPhoneNo = request.EstablishmentPhoneNo,
                    BuildingName = request.BuildingName,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new AuthResponse
                {
                    Success = true,
                    Message = "Business registration successful",
                    User = MapToUserInfo(user)
                };
            }
            catch
            {
                return new AuthResponse { Success = false, Message = "An error occurred during registration" };
            }
        }

        public async Task<IEnumerable<UserInfo>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .Where(u => u.IsActive)
                .ToListAsync();

            return users.Select(MapToUserInfo).ToList();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }

        private static UserInfo MapToUserInfo(User user)
        {
            return new UserInfo
            {
                Id = user.Id,
                Email = user.Email,
                MobileNumber = user.MobileNumber,
                CustomerType = user.CustomerType,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EstablishmentName = user.EstablishmentName,
                ContactPersonFirstName = user.ContactPersonFirstName,
                ContactPersonLastName = user.ContactPersonLastName
            };
        }
    }
}
