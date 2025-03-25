using AppointmentBookingApp.Application.Professionals.Commands;
using AppointmentBookingApp.Application.Professionals.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AppointmentBookingApp.Client.Services
{
    public class ProfessionalService
    {
        private readonly HttpClient _httpClient;

        public ProfessionalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProfessionalDto>> GetProfessionalsBySpecialization(string specialization)
        {
            return await _httpClient.GetFromJsonAsync<List<ProfessionalDto>>($"api/professional/get-by-specialization/{specialization}");
        }

        public async Task<ProfessionalDto> GetByUserId(string userid)
        {
            return await _httpClient.GetFromJsonAsync<ProfessionalDto>($"api/professional/get-by-userid/{userid}");
        }

        // Available Timelots from Tomorrow
        public async Task<List<AvailabilityDto>> GetAvailableTimeSlotsByProfessionalId(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<AvailabilityDto>>($"api/professional/get-available-timeslots-by-professional-id/{id}");
        }

        // Available Timelots All the time
        public async Task<List<ProfessionalAvailabilityDto>> GetAvailableTimeSlotsByProfessionalIdAllTime(int professionalId)
        {
            return await _httpClient.GetFromJsonAsync<List<ProfessionalAvailabilityDto>>($"api/professional/get-all-professional-availability-by-professional-id/{professionalId}");
        }

        public async Task<AvailabilityResponseDto> CreateTimeSlot(AddEditAvailabilityCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync("api/professional/create-timeslot", command);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AvailabilityResponseDto>();
            }
            else
            {
                return new AvailabilityResponseDto { IsSuccess = false, Message = "Failed to create time slot" };
            }
        }

        public async Task<AvailabilityResponseDto> UpdateTimeSlot(AddEditAvailabilityCommand command)
        {
            var response = await _httpClient.PutAsJsonAsync("api/professional/update-timeslot", command);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AvailabilityResponseDto>();
            }
            else
            {
                return new AvailabilityResponseDto { IsSuccess = false, Message = "Failed to update time slot" };
            }
        }
    }

}
