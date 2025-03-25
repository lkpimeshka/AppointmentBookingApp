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
    public class GetAppointmentByIdQuery : IRequest<AppointmentDto>
    {
        public int Id { get; set; }
        public GetAppointmentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
