using AppointmentBookingApp.Application.Professionals.DTOs;
using AppointmentBookingApp.Application.Professionals.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Professionals.Handlers
{
    public class GetProfessionalByIdHandler : IRequestHandler<GetProfessionalByIdQuery, ProfessionalDto>
    {
        private readonly IProfessionalService _professionalService;

        public GetProfessionalByIdHandler(IProfessionalService professionalService)
        { 
            _professionalService = professionalService;
        }

        public async Task<ProfessionalDto> Handle(GetProfessionalByIdQuery request, CancellationToken cancellationToken)
        {
            var professional = await _professionalService.GetByIdAsync(request.Id);

            if (professional == null)
                return null; 

            return new ProfessionalDto
            {
                Id = professional.Id,
                Name = professional.Name,
                Specialization = professional.Specialization,
                Email = professional.Email,
                PhoneNumber = professional.PhoneNumber
            };
        }

    }
}
