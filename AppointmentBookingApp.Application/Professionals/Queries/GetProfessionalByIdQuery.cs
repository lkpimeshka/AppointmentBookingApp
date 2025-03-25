using AppointmentBookingApp.Application.Professionals.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Professionals.Queries
{
    public class GetProfessionalByIdQuery : IRequest<ProfessionalDto>
    {
        public int Id { get; set; }

        public GetProfessionalByIdQuery(int id)
        {
            Id = id;
        }
    }
}
