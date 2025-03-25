using AppointmentBookingApp.Application.Auth.DTOs;

namespace AppointmentBookingApp.Application.Auth
{
    public interface IAuthService
    {
        Task<JwtResponse> Login(LoginDto loginDto);
        Task<RegisterResponseDto> Register(RegisterDto registerDto);
        Task<List<UserDto>> GetUsersAsync();
    }
}
