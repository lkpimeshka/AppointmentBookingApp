using AppointmentBookingApp.Application.Professionals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingApp.Application.Appointments.DTOs;
using MediatR;
using AppointmentBookingApp.Application.Appointments.Queries;
using AppointmentBookingApp.Domain.Entities;

namespace AppointmentBookingApp.Application.Appointments.Handlers
{
    public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
    {
        private readonly IAppointmentService _appointmentService;

        public GetAppointmentByIdHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetAppointmentById(request.Id);
            if (appointment == null)
                return null;

            return new AppointmentDto
            {
                Id = appointment.Id,
                UserId = appointment.UserId,
                ProfessionalId = appointment.ProfessionalId,
                AppointmentDate = appointment.AppointmentDate,
                ProfessionalAvailabilityId = appointment.ProfessionalAvailabilityId,
                Status = appointment.Status
            };
        }


    }
}
