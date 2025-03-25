using AppointmentBookingApp.Application.Professionals;
using AppointmentBookingApp.Application.Professionals.DTOs;
using AppointmentBookingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Infrastructure.Services
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly ApplicationDbContext _context;

        public ProfessionalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Professional>> GetAllProfessionalsAsync()
        {
            return await _context.Professionals.ToListAsync();
        }

        public async Task<bool> CreateProfessional(Professional professional)
        {
            await _context.Professionals.AddAsync(professional);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Professional>> GetBySpecializationAsync(string specialization)
        {
            return await _context.Professionals
                .Where(p => p.Specialization == specialization)
                .ToListAsync();
        }

        public async Task<Professional> GetByIdAsync(int id)
        {
            return await _context.Professionals.FindAsync(id);
        }

        public async Task<Professional> GetByUserIdAsync(string userId)
        {
            return await _context.Professionals
                        .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<List<ProfessionalAvailabilityDto>> GetAllProfessionalAvailabilityAsync(int professionalId)
        {
            var query = _context.ProfessionalAvailabilities
                .Where(pa => pa.ProfessionalId == professionalId)
                .Select(pa => new ProfessionalAvailabilityDto
                {
                    Id = pa.Id,
                    ProfessionalId = pa.ProfessionalId,
                    AvailableDate = pa.AvailableDate,
                    StartTime = pa.StartTime,
                    EndTime = pa.EndTime,
                    IsBooked = pa.IsBooked
                });

            return await query.ToListAsync();
        }


        public async Task<ProfessionalAvailability> GetAvailableTimeslotByIdAsync(int id)
        {
            return await _context.ProfessionalAvailabilities.FindAsync(id);
        }

        public async Task<List<ProfessionalAvailability>> GetAvailableTimeslotByProfessionalIdAsync(int id)
        {
            return await _context.ProfessionalAvailabilities
                .Where(p => p.ProfessionalId == id)
                .ToListAsync();
        }

        public async Task<bool> CreateTimeSlot(ProfessionalAvailability timeSlot)
        {
            await _context.ProfessionalAvailabilities.AddAsync(timeSlot);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTimeSlot(ProfessionalAvailability timeSlot)
        {
            _context.ProfessionalAvailabilities.Update(timeSlot);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
