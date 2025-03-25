using AppointmentBookingApp.Application.Appointments.DTOs;
using AppointmentBookingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Appointments
{
    public interface IAppointmentService
    {
        Task<bool> CreateAppointment(Appointment appointment);
        Task<bool> UpdateAppointment(Appointment appointment);
        Task<List<Professional>> GetBySpecializationAsync(string specialization);
        Task<List<AppointmentListDto>> GetAllAppointmentsAsync(string userId, string userRole);
        Task<Appointment> GetAppointmentById(int id);
    }
}
