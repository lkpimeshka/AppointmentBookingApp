using AppointmentBookingApp.Application.Professionals.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Professionals.Queries
{
    public class GetAvailableTimeslotByIdQuery : IRequest<List<AvailabilityDto>>
    {
        public int Id { get; set; }

        public GetAvailableTimeslotByIdQuery(int id)
        {
            Id = id;
        }
    }
}
