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
    public class GetAvailableTimeslotByIdHandler : IRequestHandler<GetAvailableTimeslotByIdQuery, List<AvailabilityDto>>
    {
        private readonly IProfessionalService _professionalService;

        public GetAvailableTimeslotByIdHandler(IProfessionalService professionalService)
        { 
            _professionalService = professionalService;
        }

        public async Task<List<AvailabilityDto>> Handle(GetAvailableTimeslotByIdQuery request, CancellationToken cancellationToken)
        {
            var tomorrow = DateTime.UtcNow.Date.AddDays(1);

            var availableSlots = await _professionalService.GetAvailableTimeslotByProfessionalIdAsync(request.Id);

            var filteredSlots = availableSlots
                .Where(p => p.AvailableDate >= tomorrow) 
                .Select(p => new AvailabilityDto
                {
                    Id = p.Id,
                    ProfessionalId = p.ProfessionalId,
                    AvailableDate = p.AvailableDate,
                    StartTime = p.StartTime,
                    EndTime = p.EndTime,
                    IsBooked = p.IsBooked,
                })
                .ToList();

            return filteredSlots;
        }

    }
}
