using AppointmentBookingApp.Application.Professionals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentBookingApp.Application.Appointments.DTOs;
using MediatR;
using AppointmentBookingApp.Application.Appointments.Queries;

namespace AppointmentBookingApp.Application.Appointments.Handlers
{
    public class GetAllAppointmentsHandlerIRequestHandler : IRequestHandler<GetAllAppointmentsQuery, List<AppointmentListDto>>
    {
        private readonly IAppointmentService _appointmentService;

        public GetAllAppointmentsHandlerIRequestHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<List<AppointmentListDto>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointmentList = await _appointmentService.GetAllAppointmentsAsync(request.UserId, request.UserRole);
            return appointmentList;
        }


    }
}
