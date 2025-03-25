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
    public class GetAllProfessionalAvailabilityHandler : IRequestHandler<GetAllProfessionalAvailabilityQuery, List<ProfessionalAvailabilityDto>>
    {
        private readonly IProfessionalService _professionalService;

        public GetAllProfessionalAvailabilityHandler(IProfessionalService professionalService)
        {
            _professionalService = professionalService;
        }

        public async Task<List<ProfessionalAvailabilityDto>> Handle(GetAllProfessionalAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var availabilityList = await _professionalService.GetAllProfessionalAvailabilityAsync(request.ProfessionalId);
            return availabilityList;
        }
    }
}
