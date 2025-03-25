using AppointmentBookingApp.Application.Professionals.DTOs;
using AppointmentBookingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Professionals
{
    public interface IProfessionalService
    {
        Task<List<Professional>> GetAllProfessionalsAsync();
        Task<bool> CreateProfessional(Professional professional);
        Task<List<Professional>> GetBySpecializationAsync(string specialization);
        Task<Professional> GetByIdAsync(int id);
        Task<Professional> GetByUserIdAsync(string userId);
        Task<ProfessionalAvailability> GetAvailableTimeslotByIdAsync(int id);
        Task<List<ProfessionalAvailability>> GetAvailableTimeslotByProfessionalIdAsync(int id);
        Task<bool> CreateTimeSlot(ProfessionalAvailability timeSlot);
        Task<bool> UpdateTimeSlot(ProfessionalAvailability timeSlot);
        Task<List<ProfessionalAvailabilityDto>> GetAllProfessionalAvailabilityAsync(int professionalId);
    }
}
