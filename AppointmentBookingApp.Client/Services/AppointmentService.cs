using AppointmentBookingApp.Application.Appointments.Commands;
using AppointmentBookingApp.Application.Appointments.DTOs;
using AppointmentBookingApp.Application.Appointments.Queries;
using AppointmentBookingApp.Application.Professionals.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Client.Services
{
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;

        public AppointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AppointmentResponseDto> CreateAppointment(AddEditAppointmentCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync("api/appointments/create-appointment", command);
            return await response.Content.ReadFromJsonAsync<AppointmentResponseDto>();
        }

        public async Task<AppointmentResponseDto> UpdateAppointment(AddEditAppointmentCommand command)
        {
            var response = await _httpClient.PutAsJsonAsync("api/appointments/update-appointment", command);
            return await response.Content.ReadFromJsonAsync<AppointmentResponseDto>();
        }

        public async Task<List<AppointmentListDto>> GetAllAppointments(string userId, string userRole)
        {
            return await _httpClient.GetFromJsonAsync<List<AppointmentListDto>>($"api/appointments/get-all-appointments?userId={userId}&userRole={userRole}");
        }

        public async Task<AppointmentDto> GetAppointmentById(int id)
        {
            return await _httpClient.GetFromJsonAsync<AppointmentDto>($"api/appointments/get-appointment-by-id/{id}");
        }

    }
}
