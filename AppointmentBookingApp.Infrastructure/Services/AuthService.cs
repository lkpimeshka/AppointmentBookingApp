using AppointmentBookingApp.Application.Auth;
using AppointmentBookingApp.Application.Auth.DTOs;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;



namespace AppointmentBookingApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JwtResponse> Login(LoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<JwtResponse>();
            }

            return null;
        }

        public async Task<RegisterResponseDto> Register(RegisterDto registerDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerDto);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<RegisterResponseDto>();
                }
                else 
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new RegisterResponseDto
                    {
                        IsSuccess = false,
                        Message = errorMessage?? "An unknown error occurred"
                    };
                }
            }
            catch (Exception ex)
            {
                return new RegisterResponseDto
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("api/auth/users");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserDto>>();
            }

            return new List<UserDto>();
        }


    }
}
