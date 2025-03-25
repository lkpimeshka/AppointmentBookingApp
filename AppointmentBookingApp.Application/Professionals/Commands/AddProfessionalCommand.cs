using AppointmentBookingApp.Application.Professionals.DTOs;
using AppointmentBookingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBookingApp.Application.Professionals.Commands
{
    public class AddProfessionalCommand : IRequest<ProfessionalResponseDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreateProfessionalHandler : IRequestHandler<AddProfessionalCommand, ProfessionalResponseDto>
    {
        private readonly IProfessionalService _professionalService;

        public CreateProfessionalHandler(IProfessionalService professionalService)
        {
            _professionalService = professionalService;
        }

        public async Task<ProfessionalResponseDto> Handle(AddProfessionalCommand request, CancellationToken cancellationToken)
        {
            var professional = new Professional
            {
                Id = request.Id,
                UserId = request.UserId,
                Name = request.Name,
                Email = request.Email,
                Specialization = request.Specialization,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _professionalService.CreateProfessional(professional);

            return new ProfessionalResponseDto
            {
                IsSuccess = result,
                Message = result ? "Professional saved successfully" : "Failed to save professional"
            };
        }
    }
}
