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
    public class AddEditAvailabilityCommand : IRequest<AvailabilityResponseDto>
    {
        public int Id { get; set; }
        public int ProfessionalId { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; }
    }

    public class CreateAvailabilityHandler : IRequestHandler<AddEditAvailabilityCommand, AvailabilityResponseDto>
    {
        private readonly IProfessionalService _professionalService;

        public CreateAvailabilityHandler(IProfessionalService professionalService)
        {
            _professionalService = professionalService;
        }

        public async Task<AvailabilityResponseDto> Handle(AddEditAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var response = new AvailabilityResponseDto();

            try
            {
                if (request.Id == 0) // Creating new time slot
                {
                    var newTimeSlot = new ProfessionalAvailability
                    {
                        ProfessionalId = request.ProfessionalId,
                        AvailableDate = request.AvailableDate,
                        StartTime = request.StartTime,
                        EndTime = request.EndTime,
                        IsBooked = request.IsBooked
                    };

                    bool isCreated = await _professionalService.CreateTimeSlot(newTimeSlot);

                    response.IsSuccess = isCreated;
                    response.Message = isCreated ? "Time slot created successfully." : "Failed to create time slot.";
                }
                else // Updating existing time slot
                {
                    var existingTimeSlot = new ProfessionalAvailability
                    {
                        Id = request.Id,
                        ProfessionalId = request.ProfessionalId,
                        AvailableDate = request.AvailableDate,
                        StartTime = request.StartTime,
                        EndTime = request.EndTime,
                        IsBooked = request.IsBooked
                    };

                    bool isUpdated = await _professionalService.UpdateTimeSlot(existingTimeSlot);

                    response.IsSuccess = isUpdated;
                    response.Message = isUpdated ? "Time slot updated successfully." : "Failed to update time slot.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}