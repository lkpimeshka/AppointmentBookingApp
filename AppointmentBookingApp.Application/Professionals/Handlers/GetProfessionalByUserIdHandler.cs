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
    public class GetProfessionalByUserIdHandler : IRequestHandler<GetProfessionalByUserIdQuery, ProfessionalDto>
    {
        private readonly IProfessionalService _professionalService;

        public GetProfessionalByUserIdHandler(IProfessionalService professionalService)
        { 
            _professionalService = professionalService;
        }

        public async Task<ProfessionalDto> Handle(GetProfessionalByUserIdQuery request, CancellationToken cancellationToken)
        {
            var professional = await _professionalService.GetByUserIdAsync(request.UserId);

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
