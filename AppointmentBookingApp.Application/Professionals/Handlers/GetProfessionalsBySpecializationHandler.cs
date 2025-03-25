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
    public class GetProfessionalsBySpecializationHandler : IRequestHandler<GetProfessionalsBySpecializationQuery, List<ProfessionalDto>>
    {
        private readonly IProfessionalService _professionalService;

        public GetProfessionalsBySpecializationHandler(IProfessionalService professionalService)
        { 
            _professionalService = professionalService;
        }

        public async Task<List<ProfessionalDto>> Handle(GetProfessionalsBySpecializationQuery request, CancellationToken cancellationToken)
        {
            var professionals = await _professionalService.GetBySpecializationAsync(request.Specialization);

            return professionals.Select(p => new ProfessionalDto
            {
                Id = p.Id,
                Name = p.Name,
                Specialization = p.Specialization,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber
            }).ToList();
        }
    }
}
