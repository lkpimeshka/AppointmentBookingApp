using AppointmentBookingApp.Application.Appointments.DTOs;
using AppointmentBookingApp.Application.Professionals.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Appointments.Queries
{
    public class GetAllAppointmentsQuery : IRequest<List<AppointmentListDto>>
    {
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public GetAllAppointmentsQuery(string userId, string userRole)
        {
            UserId = userId;
            UserRole = userRole;
        }
    }
}
