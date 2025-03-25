using AppointmentBookingApp.Application.Professionals.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Professionals.Queries
{
    public class GetProfessionalsBySpecializationQuery : IRequest<List<ProfessionalDto>>
    {
        public string Specialization { get; set; }

        public GetProfessionalsBySpecializationQuery(string specialization)
        {
            Specialization = specialization;
        }
    }
}
