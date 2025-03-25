using AppointmentBookingApp.Application.Appointments;
using AppointmentBookingApp.Application.Appointments.DTOs;
using AppointmentBookingApp.Application.Professionals;
using AppointmentBookingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Infrastructure.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProfessionalService _professionalService;

        public AppointmentService(ApplicationDbContext context, IProfessionalService professionalService)
        {
            _context = context;
            _professionalService = professionalService;
        }

        public async Task<bool> CreateAppointment(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<List<Professional>> GetBySpecializationAsync(string specialization)
        {
            return await _context.Professionals
                .Where(p => p.Specialization == specialization)
                .ToListAsync();
        }

        public async Task<List<AppointmentListDto>> GetAllAppointmentsAsync(string userId, string userRole)
        {
            var query = _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Professional)
                .Include(a => a.ProfessionalAvailability)
                .AsQueryable();

            // Apply filtering based on the role
            if (userRole == "Professional")
            {
                var professional = await _professionalService.GetByUserIdAsync(userId);
                query = query.Where(a => a.ProfessionalId == professional.Id);
            }
            else if (userRole == "User")
            {
                query = query.Where(a => a.UserId == userId);
            }
            // Admins get all appointments, so no filter applied

            return await query.Select(a => new AppointmentListDto
            {
                Id = a.Id,
                UserId = a.UserId,
                UserName = a.User.FullName,
                ProfessionalId = a.ProfessionalId,
                ProfessionalName = a.Professional.Name,
                AppointmentDate = a.AppointmentDate,
                ProfessionalAvailabilityId = a.ProfessionalAvailabilityId,
                StartTime = a.ProfessionalAvailability.StartTime,
                EndTime = a.ProfessionalAvailability.EndTime,
                Status = a.Status
            }).ToListAsync();
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            return await _context.Appointments
                        .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
