using AppointmentBookingApp.Application.Professionals.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Professionals.Queries
{
    public class GetProfessionalByUserIdQuery : IRequest<ProfessionalDto>
    {
        public string UserId { get; set; }

        public GetProfessionalByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
